using System.Linq.Expressions;
using BN.CleanArchitecture.Core.Domain.Cqrs;

namespace BN.CleanArchitecture.Core.Specification;

public abstract class GridSpecificationBase<T> : IGridSpecification<T>
{
    public virtual List<Expression<Func<T, bool>>> Criterias { get; } = new();
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public List<string> IncludeStrings { get; } = new();
    public Expression<Func<T, object>> OrderBy { get; private set; }
    public Expression<Func<T, object>> OrderByDescending { get; private set; }
    public Expression<Func<T, object>> GroupBy { get; private set; }

    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPagingEnabled { get; set; }

    protected void ApplyIncludeList(IEnumerable<Expression<Func<T, object>>> includes)
    {
        foreach (var include in includes)
        {
            AddInclude(include);
        }
    }

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected void ApplyIncludeList(IEnumerable<string> includes)
    {
        foreach (string? include in includes)
        {
            AddInclude(include);
        }
    }

    protected void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    protected IGridSpecification<T> ApplyFilterList(IEnumerable<FilterModel> filters)
    {
        foreach (var (fieldName, comparision, fieldValue) in filters)
        {
            ApplyFilter(PredicateBuilder.Build<T>(fieldName, comparision, fieldValue));
        }

        return this;
    }

    protected IGridSpecification<T> ApplyFilter(Expression<Func<T, bool>> expr)
    {
        Criterias.Add(expr);

        return this;
    }

    protected void ApplyPaging(int skip, int take, bool isPagingEnabled)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = isPagingEnabled;
    }

    protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
    }

    protected void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
    {
        GroupBy = groupByExpression;
    }

    protected void ApplySortingList(IEnumerable<string> sorts)
    {
        foreach (string? sort in sorts)
        {
            ApplySorting(sort);
        }
    }

    protected void ApplySorting(string sort)
    {
        this.ApplySorting(sort, nameof(ApplyOrderBy), nameof(ApplyOrderByDescending));
    }
}