using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Haru.Api.Commons.Bases;
using Haru.Api.Commons.Constants;
using Haru.Api.Commons.Interfaces;
using Haru.Api.Domains.Entities;
using Haru.Api.Features.v1.Authentication.Otp;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Haru.Api.Features.v1.Authentication.SignIn;

public class SignInCommand : IRequest<ResponseBase<SignInResponse>>
{
    public SignInCommand(string userName, string password, bool rememberMe, string captcha, string returnUrl)
    {
        UserName = userName;
        Password = password;
        RememberMe = rememberMe;
        Captcha = captcha;
        ReturnUrl = returnUrl;
    }

    public string UserName { get; }

    public string Password { get;}

    public bool RememberMe { get; }

    public string Captcha { get; }

    public string ReturnUrl { get; }
}

public class SignInHandler : IRequestHandler<SignInCommand, ResponseBase<SignInResponse>>
{
    private readonly IMediator _mediator;
    private readonly ICaptchaService _captchaService;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IDateTimeService _dateTimeService;

    public SignInHandler(IMediator mediator, ICaptchaService captchaService, UserManager<User> userManager, SignInManager<User> signInManager, IDateTimeService dateTimeService)
    {
        _mediator = mediator;
        _captchaService = captchaService;
        _userManager = userManager;
        _signInManager = signInManager;
        _dateTimeService = dateTimeService;
    }
    
    public async Task<ResponseBase<SignInResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        //var isValid = await _captchaService.SiteVerify(request.Captcha);

        //if (!isValid)
            //return Response.Fail(ResponseConstant.MessageFail, new SignInResponse());

        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user is null)
            return Response.Fail(ResponseConstant.MessageFail, new SignInResponse());
            
        var result = await _signInManager.PasswordSignInAsync(user.UserName!, request.Password, request.RememberMe, true);
            
        if (result.IsNotAllowed)
            return Response.Fail(ResponseConstant.MessageConfirm, new SignInResponse());

        if (result.IsLockedOut)
        {
            await _mediator.Publish(new SignInNotification { UserName = user.UserName }, cancellationToken);
            return Response.Fail(ResponseConstant.MessageLockedAccount, new SignInResponse());
        }

        if (result.RequiresTwoFactor)
        {
            await _mediator.Publish(new OtpNotification { UserId = user.Id.ToString(), ReturnUrl = request.ReturnUrl }, cancellationToken);
            return Response.Ok(ResponseConstant.MessageSuccess, new SignInResponse { IsOtp = result.RequiresTwoFactor });
        }

        var roles = await _userManager.GetRolesAsync(user);

        var claims = await _userManager.GetClaimsAsync(user);

        var expireIn = _dateTimeService.Now.AddMinutes(SettingConstant.JwtExpireIn);
        
        var tokenClaims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new (JwtRegisteredClaimNames.Jti, new Guid().ToString()),
            new (JwtRegisteredClaimNames.Email, user.Email),
            new (ClaimTypes.MobilePhone, user.PhoneNumber),
            new ("email_confirmed", user.EmailConfirmed.ToString()),
            new(ClaimTypes.Expiration, expireIn.ToString(CultureInfo.InvariantCulture))
        }.Union(claims).Union(roles.Select(x => new Claim(ClaimTypes.Role, x)));
        
        if(!tokenClaims.Any())
            return Response.Fail(ResponseConstant.MessageFail, new SignInResponse());

        return result.Succeeded
            ? Response.Ok(ResponseConstant.MessageSuccess, new SignInResponse
            {
                AccessToken = GenerateAccessToken(tokenClaims, expireIn),
                RefreshToken = GenerateRefreshToken(),
                ExpireIn = expireIn
            })
            : Response.Fail(ResponseConstant.MessageFail, new SignInResponse());
    }
    
    private static string GenerateAccessToken(IEnumerable<Claim> claims, DateTime expireIn)
    {
        var securityToken = new JwtSecurityToken
        (
            issuer: SettingConstant.JwtIssuer,
            audience: SettingConstant.JwtAudience,
            claims: claims,
            expires: expireIn,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SettingConstant.JwtSecret)),
                SecurityAlgorithms.HmacSha256Signature)
        );
        
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
    
    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}