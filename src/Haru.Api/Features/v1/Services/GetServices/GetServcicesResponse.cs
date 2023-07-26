using Haru.Api.Commons.Mappings;
using Haru.Api.Domains.Entities;

namespace Haru.Api.Features.v1.Services.GetServices;

public class GetServicesResponse : IMapFrom<Service>
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}