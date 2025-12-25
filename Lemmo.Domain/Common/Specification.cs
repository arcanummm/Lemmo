using System.Linq.Expressions;

namespace Lemmo.Domain.Common
{
    public abstract class Specification<TEntity>(Expression<Func<TEntity, bool>>? criteria = null) where TEntity : BaseEntity
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; init; } = criteria;

        public bool IsNotTracking { get; protected set; }
        public bool IsSingleQuery { get; protected set; }
        public bool IsSplitQuery { get; protected set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) =>
            IncludeExpressions.Add(includeExpression);

        private readonly List<Expression<Func<TEntity, object>>> _orderByExpressions = [];
        private readonly List<Expression<Func<TEntity, object>>> _orderByDescExpressions = [];

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression) =>
            _orderByExpressions.Add(orderByExpression);

        protected void AddOrderByDesc(Expression<Func<TEntity, object>> orderByDescExpression) =>
            _orderByDescExpressions.Add(orderByDescExpression);

        public IEnumerable<Expression<Func<TEntity, object>>> GetOrderBy() => _orderByExpressions;
        public IEnumerable<Expression<Func<TEntity, object>>> GetOrderByDesc() => _orderByDescExpressions;

        public int? Take { get; private set; }
        public int? Skip { get; private set; }

        protected void SetPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }
    }

}
