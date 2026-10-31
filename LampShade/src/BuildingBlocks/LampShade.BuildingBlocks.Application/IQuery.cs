using MediatR;

namespace LampShade.BuildingBlocks.Application;

public interface IQuery<out TResponse> : IRequest<TResponse>;
