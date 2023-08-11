using Haru.Api.Commons.Dtos;

namespace Haru.Api.Commons.Interfaces;

public interface INotificationService
{
    Task<string> SendEmail(EmailDto entity);
}