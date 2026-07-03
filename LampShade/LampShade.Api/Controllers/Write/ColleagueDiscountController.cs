using LampShade.Api.Features.ColleagueDiscounts.Commands.DefineColleagueDiscount;
using LampShade.Api.Features.ColleagueDiscounts.Commands.EditColleagueDiscount;
using LampShade.Api.Features.ColleagueDiscounts.Commands.RemoveColleagueDiscount;
using LampShade.Api.Features.ColleagueDiscounts.Commands.RestoreColleagueDiscount;
using LampShade.Api.Features.ColleagueDiscounts.Queries.GetColleagueDiscountById;
using LampShade.Api.Features.ColleagueDiscounts.Queries.SearchColleagueDiscounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Write;

[ApiController]
[Route("api/write/[controller]")]
public class ColleagueDiscountController : ControllerBase
{
    private readonly IMediator _mediator;
    public ColleagueDiscountController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Define(DefineColleagueDiscountCommand command) => Ok(await _mediator.Send(command));

    [HttpPut]
    public async Task<IActionResult> Edit(EditColleagueDiscountCommand command) => Ok(await _mediator.Send(command));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(long id) => Ok(await _mediator.Send(new RemoveColleagueDiscountCommand { Id = id }));

    [HttpPost("{id}/restore")]
    public async Task<IActionResult> Restore(long id) => Ok(await _mediator.Send(new RestoreColleagueDiscountCommand { Id = id }));

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SearchColleagueDiscountsQuery query) => Ok(await _mediator.Send(query));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(long id) => Ok(await _mediator.Send(new GetColleagueDiscountByIdQuery { Id = id }));
}
