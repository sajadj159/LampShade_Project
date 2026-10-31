using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Application.Contracts.Commands.Orders.ApproveCashOnDelivery;

namespace ShopManagement.Application.Features.Orders.Commands.ApproveCashOnDelivery;

public class ApproveCashOnDeliveryCommandHandler(IOrderApplication application) : IRequestHandler<ApproveCashOnDeliveryCommand, string>
{
    public Task<string> Handle(ApproveCashOnDeliveryCommand request, CancellationToken cancellationToken) => application.ApproveCashOnDeliveryAsync(request.OrderId, cancellationToken);
}
