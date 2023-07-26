using Haru.Api.Commons.Constants;
using Haru.Api.Commons.Dtos;
using Haru.Api.Commons.Interfaces;
using MediatR;

namespace Haru.Api.Features.v1.Account.Recovery;

public class RecoveryNotification : INotification
{
    public string Name { get; init; }

    public string Email { get; init; }

    public string Callback { get; init; }
}
    
public class RecoveryNotificationHandler : INotificationHandler<RecoveryNotification>
{
    private readonly ICacheService _cacheService;
    private readonly INotificationService _notificationService;

    public RecoveryNotificationHandler(ICacheService cacheService, INotificationService notificationService)
    {
        _cacheService = cacheService;
        _notificationService = notificationService;
    }
        
    public async Task Handle(RecoveryNotification notification, CancellationToken cancellationToken)
    {
        var html = _cacheService.Template(TemplateConstant.TemplateReset);
        html = html.Replace("{0}", notification.Callback);
            
        await _notificationService.SendEmail(new EmailDto
        {
            Sender = new SenderDto { Id = TemplateConstant.SenderId },
            To = new List<ToDto> { new() { Name = notification.Name, Email = notification.Email } },
            Subject = TemplateConstant.SubjectRecovery,
            HtmlContent = html,
            TextContent = TemplateConstant.SubjectRecovery
        });
    }
}