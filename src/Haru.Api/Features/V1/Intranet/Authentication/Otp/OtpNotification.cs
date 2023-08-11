using Haru.Api.Commons.Constants;
using Haru.Api.Commons.Dtos;
using Haru.Api.Commons.Interfaces;
using Haru.Api.Domains.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Haru.Api.Features.V1.Intranet.Authentication.Otp;

public class OtpNotification : INotification
{
    public string UserId { get; init; }

    public string ReturnUrl { get; init; }
}

public class OtpNotificationHandler : INotificationHandler<OtpNotification>
{
    private readonly ICacheService _cacheService;
    private readonly INotificationService _notificationService;
    private readonly UserManager<User> _userManager;

    public OtpNotificationHandler(ICacheService cacheService, INotificationService notificationService, UserManager<User> userManager)
    {
        _cacheService = cacheService;
        _notificationService = notificationService;
        _userManager = userManager;
    }
    
    public async Task Handle(OtpNotification notification, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(notification.UserId);

        if (user is null)
            throw new Exception($"User {notification.UserId} not exist");
        
        var claims = await _userManager.GetClaimsAsync(user);
        var firstName = claims.Where(x => x.Type.Equals(ClaimTypeConstant.FirstName)).Select(x => x.Value).FirstOrDefault();

        await _userManager.UpdateSecurityStampAsync(user);
            
        var code = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

        var link = $"{SettingConstant.WebUrl}/account/otp?returnUrl={notification.ReturnUrl}";

        var html = _cacheService.Template(TemplateConstant.TemplateOtp);
        html = html.Replace("{0}", code);
        html = html.Replace("{1}", link);
        
        await _notificationService.SendEmail(new EmailDto
        {
            Sender = new SenderDto { Id = TemplateConstant.SenderId },
            To = new List<ToDto> { new() { Name = firstName, Email = user.Email } },
            Subject = TemplateConstant.SubjectTwoFactor,
            HtmlContent = html,
            TextContent = TemplateConstant.SubjectTwoFactor
        });
    }
}