using System.Net;
using Haru.Api.Commons.Constants;
using Haru.Api.Commons.Interfaces;
using Newtonsoft.Json.Linq;

namespace Haru.Api.Services;

public class GoogleCaptchaService : ICaptchaService
{
    public async Task<bool> SiteVerify(string captcha)
    {
        var endpoint = $"{SettingConstant.GoogleCaptchaUrl}/siteverify?secret={SettingConstant.GoogleCaptchaSecret}&response={captcha}";
        var request = (HttpWebRequest) WebRequest.Create(endpoint);
        using var response = request.GetResponse();
        using var stream = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException());
        var jResponse = JObject.Parse(await stream.ReadToEndAsync());
            
        return jResponse.Value<bool>("success");
    }
}