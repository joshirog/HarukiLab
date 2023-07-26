using Haru.Api.Commons.Mappings;
using Haru.Api.Domains.Entities;

namespace Haru.Api.Features.v1.Categories.GetCategoriesByServiceId;

public class GetCategoriesByServiceIdResponse : IMapFrom<Category>
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}