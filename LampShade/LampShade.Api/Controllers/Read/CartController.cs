using LampShade.Api.Features.Cart.ComputeCart;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Read;

[ApiController]
[Route("api/read/[controller]")]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;
    public CartController(IMediator mediator) => _mediator = mediator;

    [HttpPost("compute")]
    public async Task<IActionResult> ComputeCart([FromBody] ComputeCartQuery query) => Ok(await _mediator.Send(query));
}
