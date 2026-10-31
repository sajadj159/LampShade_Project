using MediatR;

namespace LampShade.BuildingBlocks.Application;

public interface ICommand<out TResponse> : IRequest<TResponse>;