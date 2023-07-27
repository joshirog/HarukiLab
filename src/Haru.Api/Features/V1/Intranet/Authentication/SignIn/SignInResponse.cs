namespace Haru.Api.Features.V1.Intranet.Authentication.SignIn;

public class SignInResponse
{
    public bool IsOtp { get; set; }

    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    public DateTime ExpireIn { get; set; }
}