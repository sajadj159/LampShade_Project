using LampShade.Api.Features.ProductPictures.Commands.CreateProductPicture;
using LampShade.Api.Features.ProductPictures.Commands.EditProductPicture;
using LampShade.Api.Features.ProductPictures.Commands.RemoveProductPicture;
using LampShade.Api.Features.ProductPictures.Commands.RestoreProductPicture;
using LampShade.Api.Features.ProductPictures.Queries.GetProductPictureById;
using LampShade.Api.Features.ProductPictures.Queries.SearchProductPictures;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Write;

[ApiController]
[Route("api/write/[controller]")]
public class ProductPictureController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductPictureController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateProductPictureCommand command) => Ok(await _mediator.Send(command));

    [HttpPut]
    public async Task<IActionResult> Edit([FromForm] EditProductPictureCommand command) => Ok(await _mediator.Send(command));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(long id) => Ok(await _mediator.Send(new RemoveProductPictureCommand { Id = id }));

    [HttpPost("{id}/restore")]
    public async Task<IActionResult> Restore(long id) => Ok(await _mediator.Send(new RestoreProductPictureCommand { Id = id }));

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SearchProductPicturesQuery query) => Ok(await _mediator.Send(query));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(long id) => Ok(await _mediator.Send(new GetProductPictureByIdQuery { Id = id }));
}
