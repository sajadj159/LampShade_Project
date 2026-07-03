using LampShade.Api.Features.CustomerDiscounts.Commands.DefineCustomerDiscount;
using LampShade.Api.Features.CustomerDiscounts.Commands.EditCustomerDiscount;
using LampShade.Api.Features.CustomerDiscounts.Queries.GetCustomerDiscountById;
using LampShade.Api.Features.CustomerDiscounts.Queries.SearchCustomerDiscounts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Write;

[Authorize]
[ApiController]
[Route("api/write/[controller]")]
public class CustomerDiscountController : ControllerBase
{
    private readonly IMediator _mediator;
    public CustomerDiscountController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Define(DefineCustomerDiscountCommand command) => Ok(await _mediator.Send(command));

    [HttpPut]
    public async Task<IActionResult> Edit(EditCustomerDiscountCommand command) => Ok(await _mediator.Send(command));

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SearchCustomerDiscountsQuery query) => Ok(await _mediator.Send(query));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(long id) => Ok(await _mediator.Send(new GetCustomerDiscountByIdQuery { Id = id }));
}
