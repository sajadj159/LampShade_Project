using MediatR;
using ShopManagement.Application.Contract.Order;

namespace LampShade.Api.Features.Orders.Commands.ApproveCashOnDelivery;

public class ApproveCashOnDeliveryCommand : IRequest<string>
{
    public long OrderId { get; set; }
}

public class ApproveCashOnDeliveryCommandHandler : IRequestHandler<ApproveCashOnDeliveryCommand, string>
{
    private readonly IOrderApplication _application;

    public ApproveCashOnDeliveryCommandHandler(IOrderApplication application) => _application = application;

    public async Task<string> Handle(ApproveCashOnDeliveryCommand request, CancellationToken cancellationToken)
        => await Task.FromResult(_application.ApproveCashOnDelivery(request.OrderId));
}
