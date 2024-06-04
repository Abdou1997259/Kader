public interface IBaseRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);

    Task<IEnumerable<TType>> GetSpecificSelectAsync<TType>(
        Expression<Func<T, bool>> filter,
        Expression<Func<T, TType>> select,
        string includeProperties = null!,
        int? skip = null,
        int? take = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!) where TType : class;

    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null!,
     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!, Expression<Func<T, bool>> includeFilter = null!,
     string includeProperties = null!,
     int? skip = null,
     int? take = null);

    Task<bool> ExistAsync(int id);

    Task<bool> ExistAsync(Expression<Func<T, bool>> filter = null!, string includeProperties = null!);
    Task<T> GetFirstOrDefaultAsync(
        Expression<Func<T, bool>> filter = null!,
        string includeProperties = null!
    );

    Task<IEnumerable<TResult>> GetGrouped<TKey, TResult>(
        Expression<Func<T, TKey>> groupingKey,
        Expression<Func<IGrouping<TKey, T>, TResult>> resultSelector,
        string includeProperties = null!,
        int? skip = null,
        int? take = null,
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!);
    Task<IEnumerable<T>> GetWithJoinAsync(Expression<Func<T, bool>> predicate
        , string includeProperties);
    Task<T> AddAsync(T entity);

    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
    T Remove(T entity);

    T Update(T entity);
    Task<int> CountAsync(Expression<Func<T, bool>> filter = null!, string includeProperties = null!);
    Task<bool> AnyAsync(Expression<Func<T, bool>> filter = null!);
    void RemoveRange(IEnumerable<T> entities);
    void UpdateRange(IEnumerable<T> entities);
}
