using MediatR;
using ShopManagement.Application.Contract.Order;

using ShopManagement.Application.Contracts.Commands.Orders.ApproveCashOnDelivery;

namespace ShopManagement.Application.Features.Orders.Commands.ApproveCashOnDelivery;



public class ApproveCashOnDeliveryCommandHandler : IRequestHandler<ApproveCashOnDeliveryCommand, string>
{
    private readonly IOrderApplication _application;

    public ApproveCashOnDeliveryCommandHandler(IOrderApplication application) => _application = application;

    public Task<string> Handle(ApproveCashOnDeliveryCommand request, CancellationToken cancellationToken)
        => Task.FromResult(_application.ApproveCashOnDelivery(request.OrderId));
}
