using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Haru.Api.Commons.Constants;
using Haru.Api.Commons.Enums;
using Haru.Api.Domains.Entities;
using Microsoft.AspNetCore.Identity;

namespace Haru.Api.Persistences.Seeders;

public static class IdentitySeeder
{
    public static async void Seed(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

        if (await userManager.FindByNameAsync("administrator@haru.com") is not null) 
            return;
        
        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = "administrator@haru.com",
            Email = "administrator@haru.com",
            EmailConfirmed = false,
            PhoneNumber = "946678198",
            Status = Enum.GetName(typeof(StatusEnum), StatusEnum.Active),
            TwoFactorEnabled = false
        };

        await userManager.CreateAsync(user, "Admin2023$$");

        if (await roleManager.FindByNameAsync(Enum.GetName(typeof(RoleEnum), RoleEnum.Administrator)!) is not null)
            return;
        
        await roleManager.CreateAsync(new Role
        {
            Id = Guid.Parse(RoleConstant.AdministratorId),
            Name = Enum.GetName(typeof(RoleEnum), RoleEnum.Administrator),
            Status = Enum.GetName(typeof(StatusEnum), StatusEnum.Active),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        });
        
        await roleManager.CreateAsync(new Role
        {
            Id = Guid.Parse(RoleConstant.ManagementId),
            Name = Enum.GetName(typeof(RoleEnum), RoleEnum.Management),
            Status = Enum.GetName(typeof(StatusEnum), StatusEnum.Active),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        });
        
        await roleManager.CreateAsync(new Role
        {
            Id = Guid.Parse(RoleConstant.OperatorId),
            Name = Enum.GetName(typeof(RoleEnum), RoleEnum.Operator),
            Status = Enum.GetName(typeof(StatusEnum), StatusEnum.Active),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        });
        
        await roleManager.CreateAsync(new Role
        {
            Id = Guid.Parse(RoleConstant.KeeperId),
            Name = Enum.GetName(typeof(RoleEnum), RoleEnum.Keeper),
            Status = Enum.GetName(typeof(StatusEnum), StatusEnum.Active),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        });
        
        await roleManager.CreateAsync(new Role
        {
            Id = Guid.Parse(RoleConstant.OwnerId),
            Name = Enum.GetName(typeof(RoleEnum), RoleEnum.Owner),
            Status = Enum.GetName(typeof(StatusEnum), StatusEnum.Active),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        });

        await roleManager.CreateAsync(new Role
        {
            Id = Guid.Parse(RoleConstant.GuestId),
            Name = Enum.GetName(typeof(RoleEnum), RoleEnum.Guest),
            Status = Enum.GetName(typeof(StatusEnum), StatusEnum.Active),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        });

        await userManager.AddToRoleAsync(user, Enum.GetName(typeof(RoleEnum), RoleEnum.Administrator)!);
        
        await userManager.AddClaimsAsync(user, new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Name, "Jose Luis"),
            new(JwtRegisteredClaimNames.FamilyName, "Oshiro Gushiken"),
            new(JwtRegisteredClaimNames.GivenName, "JO"),
            new(JwtRegisteredClaimNames.Birthdate, "1986-08-31"),
            new(JwtRegisteredClaimNames.GivenName, "JO"),
            new(JwtRegisteredClaimNames.UniqueName, user.PhoneNumber!),
            new(JwtRegisteredClaimNames.Gender, "male"),
            new(JwtRegisteredClaimNames.Actort, GeneralConstant.DefaultAvatar),
            new("role", Enum.GetName(typeof(RoleEnum), RoleEnum.Administrator)!)
        });
    }
}