using Haru.Api.Commons.Constants;
using Haru.Api.Commons.Dtos;
using Haru.Api.Commons.Interfaces;
using Haru.Api.Domains.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Haru.Api.Features.v1.Account.Reset;

public class ResetNotification : INotification
    {
        public string UserId { get; set; }
    }
    
    public class ResetNotificationHandler : INotificationHandler<ResetNotification>
    {
        private readonly ICacheService _cacheService;
        private readonly INotificationService _notificationService;
        private readonly UserManager<User> _userManager;

        public ResetNotificationHandler(ICacheService cacheService, INotificationService notificationService, UserManager<User> userManager)
        {
            _cacheService = cacheService;
            _notificationService = notificationService;
            _userManager = userManager;
        }

        public async Task Handle(ResetNotification notification, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(notification.UserId);

            if (user is null)
                throw new Exception($"User {notification.UserId} not exist");
            
            var claims = await _userManager.GetClaimsAsync(user);
            
            var firstname = claims.Where(x => x.Type.Equals(ClaimTypeConstant.FirstName)).Select(x => x.Value).FirstOrDefault();
            
            var html = _cacheService.Template(TemplateConstant.TemplatePassword);
            html = html.Replace("{0}", SettingConstant.WebUrl);
            
            await _notificationService.SendEmail(new EmailDto
            {
                Sender = new SenderDto { Id = TemplateConstant.SenderId },
                To = new List<ToDto> { new() { Name = firstname, Email = user.Email } },
                Subject = TemplateConstant.SubjectReset,
                HtmlContent = html,
                TextContent = TemplateConstant.SubjectReset,
            });
        }
    }