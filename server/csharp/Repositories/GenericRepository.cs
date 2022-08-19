global using todolist.Entities;
global using Microsoft.EntityFrameworkCore;
global using todolist.Database;

using System.Linq.Expressions;

namespace todolist.Repositories;

public interface IGenericRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T? GetById(int id);
    IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
    void Create(T entity);
    void Update(T entity);
    void Delete(int id);
    void Save();
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task SaveAsync();
}

public class GenericRepository<T> : IGenericRepository<T> where T : class
{

    protected TodoListDataContext _context;
    protected readonly DbSet<T> _table;

    public GenericRepository(TodoListDataContext context)
    {
        _context = context;
        _table = _context.Set<T>();
    }

    public IEnumerable<T> GetAll() =>
        _table.ToList();

    public T? GetById(int id) =>
        _table.Find(id);

    public IEnumerable<T> Get(Expression<Func<T, bool>> predicate) =>
        _table.Where(predicate).ToList();

    public void Create(T entity)
    {
        _table.Add(entity);
        _context.SaveChanges();
    }

    public void Update(T entity)
    {
        _table.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var data = _table.Find(id);

        if (data == null) return;
        _table.Remove(data);
        _context.SaveChanges();
    }

    public void Save() =>
        _context.SaveChanges();

    public async Task<IEnumerable<T>> GetAllAsync() =>
        await _table.ToListAsync();

    public async Task<T?> GetByIdAsync(int id) =>
        await _table.FindAsync(id);

    public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate) =>
        await _table.Where(predicate).ToListAsync();

    public async Task CreateAsync(T entity)
    {
        _table.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _table.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var data = await _table.FindAsync(id);

        if (data == null) return;
        _table.Remove(data);
        await _context.SaveChangesAsync();
    }

    public async Task SaveAsync() =>
        await _context.SaveChangesAsync();
}
