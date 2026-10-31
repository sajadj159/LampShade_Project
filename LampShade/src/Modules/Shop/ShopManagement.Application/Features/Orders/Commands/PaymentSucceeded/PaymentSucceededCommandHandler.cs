using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Application.Contracts.Commands.Orders.PaymentSucceeded;

namespace ShopManagement.Application.Features.Orders.Commands.PaymentSucceeded;

public class PaymentSucceededCommandHandler(IOrderApplication application) : IRequestHandler<PaymentSucceededCommand, string>
{
    public Task<string> Handle(PaymentSucceededCommand request, CancellationToken cancellationToken) => application.PaymentSucceededAsync(request.OrderId, request.RefId, cancellationToken);
}
