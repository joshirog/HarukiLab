using Microsoft.AspNetCore.Authentication;

namespace Haru.Api.Commons.Interfaces;

public interface ICacheService
{
    string Template(string key);

    Task<List<AuthenticationScheme>> ExternalLogin();

    void Remove(string key);
}