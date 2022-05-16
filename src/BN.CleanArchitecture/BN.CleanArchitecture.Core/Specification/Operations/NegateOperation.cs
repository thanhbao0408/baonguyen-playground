using System.Linq.Expressions;

namespace BN.CleanArchitecture.Core.Specification;

public class Negated<T> : SpecificationBase<T>
{
    private readonly ISpecification<T> _inner;

    public Negated(ISpecification<T> inner)
    {
        _inner = inner;
    }

    // NegatedSpecification
    public override Expression<Func<T, bool>> Criteria
    {
        get
        {
            ParameterExpression? objParam = Expression.Parameter(typeof(T), "obj");

            Expression<Func<T, bool>>? newExpr = Expression.Lambda<Func<T, bool>>(
                Expression.Not(
                    Expression.Invoke(_inner.Criteria, objParam)
                ),
                objParam
            );

            return newExpr;
        }
    }
}