using Haru.Api.Commons.Mappings;
using Haru.Api.Domains.Entities;

namespace Haru.Api.Features.V1.Intranet.Services.GetServices;

public class GetServicesResponse : IMapFrom<Service>
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}