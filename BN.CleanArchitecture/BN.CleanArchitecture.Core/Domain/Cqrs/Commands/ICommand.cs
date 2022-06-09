using MediatR;

namespace BN.CleanArchitecture.Core.Domain.Cqrs;

public interface ICommand<T> : IRequest<ResultModel<T>> where T: notnull
{
    
}