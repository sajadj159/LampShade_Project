using LampShade.BuildingBlocks.Application;
using System.Threading;
using System.Threading.Tasks;
using DiscountManagement.Application.Contracts.Commands.CustomerDiscounts.DefineCustomerDiscount;
using DiscountManagement.Application.Contracts.Commands.CustomerDiscounts.EditCustomerDiscount;
using DiscountManagement.Application.ReadModel.Queries.CustomerDiscounts.GetCustomerDiscountById;
using DiscountManagement.Application.ReadModel.Queries.CustomerDiscounts.SearchCustomerDiscounts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiscountManagement.Presentation.Controllers.Write;

[Authorize(Roles = Roles.Administrator)]
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
