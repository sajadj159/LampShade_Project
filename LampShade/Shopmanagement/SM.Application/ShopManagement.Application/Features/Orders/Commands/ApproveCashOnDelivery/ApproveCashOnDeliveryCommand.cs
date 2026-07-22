using MediatR;
using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Application.Features.Orders.Commands.ApproveCashOnDelivery;

public class ApproveCashOnDeliveryCommand : IRequest<string>
{
    public long OrderId { get; set; }
}

public class ApproveCashOnDeliveryCommandHandler : IRequestHandler<ApproveCashOnDeliveryCommand, string>
{
    private readonly IOrderApplication _application;

    public ApproveCashOnDeliveryCommandHandler(IOrderApplication application) => _application = application;

    public Task<string> Handle(ApproveCashOnDeliveryCommand request, CancellationToken cancellationToken)
        => Task.FromResult(_application.ApproveCashOnDelivery(request.OrderId));
}
