using System.Linq.Expressions;

namespace BN.CleanArchitecture.Core.Specification;

public class OrOperation<T> : SpecificationBase<T>
{
    private readonly ISpecification<T> _left;
    private readonly ISpecification<T> _right;

    public OrOperation(
        ISpecification<T> left,
        ISpecification<T> right)
    {
        _left = left;
        _right = right;
    }

    // OrSpecification
    public override Expression<Func<T, bool>> Criteria
    {
        get
        {
            ParameterExpression objParam = Expression.Parameter(typeof(T), "obj");

            Expression<Func<T, bool>> newExpr = Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(
                    Expression.Invoke(_left.Criteria, objParam),
                    Expression.Invoke(_right.Criteria, objParam)
                ),
                objParam
            );

            return newExpr;
        }
    }
}