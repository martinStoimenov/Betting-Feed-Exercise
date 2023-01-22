namespace UltraPlayBettingSystemExercise.Data.Repositoies
{
    using System.Linq;
    using UltraPlayBettingSystemExercise.Data.Common;

    public interface IDeletableEntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        IQueryable<TEntity> AllWithDeleted();

        IQueryable<TEntity> AllAsNoTrackingWithDeleted();

        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);
    }
}
