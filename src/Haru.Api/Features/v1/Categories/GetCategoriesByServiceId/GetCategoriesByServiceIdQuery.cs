using AutoMapper;
using Haru.Api.Commons.Bases;
using Haru.Api.Commons.Constants;
using Haru.Api.Persistences.Contexts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Haru.Api.Features.v1.Categories.GetCategoriesByServiceId;

public class GetCategoriesByServiceIdQuery : IRequest<ResponseBase<List<GetCategoriesByServiceIdResponse>>>, IAllowAnonymous
{
    public Guid ServiceId { get; set; }
}

public class GetCategoriesByServiceIdQueryHandler : IRequestHandler<GetCategoriesByServiceIdQuery, ResponseBase<List<GetCategoriesByServiceIdResponse>>>
{
    private readonly DefaultContext _context;
    private readonly IMapper _mapper;
    
    public GetCategoriesByServiceIdQueryHandler(DefaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ResponseBase<List<GetCategoriesByServiceIdResponse>>> Handle(GetCategoriesByServiceIdQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.Categories.Where(x => x.ServiceId.Equals(request.ServiceId))
            .ToListAsync(cancellationToken: cancellationToken);

        return Response.Ok(ResponseConstant.MessageSuccess, _mapper.Map<List<GetCategoriesByServiceIdResponse>>(categories));
    }
}