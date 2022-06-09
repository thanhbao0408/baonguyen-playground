using MediatR;

namespace BN.CleanArchitecture.Core.Domain.Cqrs;

public interface IQuery<T> : IRequest<ResultModel<T>>
    where T : notnull
{
}