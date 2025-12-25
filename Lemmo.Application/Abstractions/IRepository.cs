using Lemmo.Domain.Common;

namespace Lemmo.Application.Abstractions
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetQueryable();

        IQueryable<TEntity> GetQueryable<TSpecification>(TSpecification specification)
            where TSpecification : Specification<TEntity>;

        void Update(TEntity entity);
        void Update(List<TEntity> entities);

        void Remove(TEntity entity);
        void Remove(List<TEntity> entities);

        void Insert(TEntity entity);
        Task InsertAsync(TEntity entity);

        void Insert(List<TEntity> entities);
        Task InsertAsync(List<TEntity> entities);

        Task<TEntity?> GetAsync<TSpecification>(TSpecification specification)
            where TSpecification : Specification<TEntity>;
        TEntity Get<TSpecification>(TSpecification specification)
            where TSpecification : Specification<TEntity>;

        Task<List<TEntity>> GetEntitiesAsync<TSpecification>(TSpecification specification)
            where TSpecification : Specification<TEntity>;
        List<TEntity> GetEntities<TSpecification>(TSpecification specification)
            where TSpecification : Specification<TEntity>;

        List<TEntity> Get();

        bool Any(Func<TEntity, bool>? predicate);
    }
}
