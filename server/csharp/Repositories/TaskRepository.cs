namespace todolist.Repositories;

public class TaskRepository : GenericRepository<TaskEntity>
{
    public TaskRepository(TodoListDataContext context) : base(context) { }

    public async Task CreateAsync(TaskEntity entity, UserEntity owner)
    {
        entity.Owner = owner;
        entity.OwnerId = owner.Id;

        _table.Add(entity);
        await SaveAsync();
    }

    public async Task<IEnumerable<TaskEntity>> GetByOwnerAsync(UserEntity owner) =>
        await GetAsync(item => item.OwnerId == owner.Id);

    public async Task<bool> ExistAsync(int id, UserEntity owner) =>
        (await GetAsync(item => item.Id == id && item.OwnerId == owner.Id)).Count() > 0;

    public void Create(TaskEntity entity, UserEntity owner)
    {
        entity.Owner = owner;
        entity.OwnerId = owner.Id;

        _table.Add(entity);
        Save();
    }

    public IEnumerable<TaskEntity> GetByOwner(UserEntity owner) =>
        Get(item => item.OwnerId == owner.Id);

    public bool Exist(int id, UserEntity owner) =>
        Get(item => item.Id == id && item.OwnerId == owner.Id).Count() > 0;

    public void Update(TaskEntity original, TaskEntity edited)
    {
        original.Title = edited.Title ?? original.Title;
        original.Description = edited.Description ?? original.Description;

        Update(original);
    }

    public void SetDone(TaskEntity original)
    {
        original.Done = !original.Done;

        Update(original);
    }
}
