using AutoMapper;
using Haru.Api.Commons.Bases;
using Haru.Api.Commons.Constants;
using Haru.Api.Persistences.Contexts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Haru.Api.Features.V1.Intranet.Services.GetServices;

public class GetServicesQuery : IRequest<ResponseBase<List<GetServicesResponse>>>, IAllowAnonymous
{

}

public class GetCategoriesQueryHandler : IRequestHandler<GetServicesQuery, ResponseBase<List<GetServicesResponse>>>
{
    private readonly DefaultContext _context;
    private readonly IMapper _mapper;
    
    public GetCategoriesQueryHandler(DefaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ResponseBase<List<GetServicesResponse>>> Handle(GetServicesQuery request, CancellationToken cancellationToken)
    {
        var services = await _context.Services.ToListAsync(cancellationToken);

        return Response.Ok(ResponseConstant.MessageSuccess, _mapper.Map<List<GetServicesResponse>>(services));
    }
}