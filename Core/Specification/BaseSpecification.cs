using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specification;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
{
    protected BaseSpecification(): this(null) {}
    public Expression<Func<T, bool>>? Criteria { get; } = criteria;
    
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }
    
    public bool isDistinct { get; private set;  }

    protected void AddOrderBy(Expression<Func<T, object>> orderBy)
    {
        OrderBy = orderBy;
    }

    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescending)
    {
        OrderByDescending = orderByDescending;
    }

    protected void ApplyDistinct()
    {
        isDistinct = true;
    }
}

public class BaseSpecification<T, TResult> : BaseSpecification<T>, ISpecification<T, TResult>
{
    public BaseSpecification(Expression<Func<T, bool>>? criteria) : base(criteria)
    {
    }
    
    protected BaseSpecification(): this(null) {}
    
    public Expression<Func<T, TResult>>? Select { get; private set; }

    protected void AddSelect(Expression<Func<T, TResult>> select)
    {
        Select = select;
    }
}