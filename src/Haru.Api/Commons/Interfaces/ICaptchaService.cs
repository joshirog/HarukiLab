namespace Haru.Api.Commons.Interfaces;

public interface ICaptchaService
{
    Task<bool> SiteVerify(string captcha);
}