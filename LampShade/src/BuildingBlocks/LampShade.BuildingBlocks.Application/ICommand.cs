using MediatR;

namespace _0_Framework.Application;

public interface ICommand<out TResponse> : IRequest<TResponse>;