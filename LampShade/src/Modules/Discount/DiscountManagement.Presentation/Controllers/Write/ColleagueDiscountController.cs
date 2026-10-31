using LampShade.BuildingBlocks.Application;
using System.Threading;
using System.Threading.Tasks;
using DiscountManagement.Application.Contracts.Commands.ColleagueDiscounts.DefineColleagueDiscount;
using DiscountManagement.Application.Contracts.Commands.ColleagueDiscounts.EditColleagueDiscount;
using DiscountManagement.Application.Contracts.Commands.ColleagueDiscounts.RemoveColleagueDiscount;
using DiscountManagement.Application.Contracts.Commands.ColleagueDiscounts.RestoreColleagueDiscount;
using DiscountManagement.Application.ReadModel.Queries.ColleagueDiscounts.GetColleagueDiscountById;
using DiscountManagement.Application.ReadModel.Queries.ColleagueDiscounts.SearchColleagueDiscounts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiscountManagement.Presentation.Controllers.Write;

[Authorize(Roles = Roles.Administrator)]
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
