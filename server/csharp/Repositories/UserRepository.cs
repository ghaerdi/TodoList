namespace todolist.Repositories;

public class UserRepository : GenericRepository<UserEntity>
{
    public UserRepository(TodoListDataContext context) : base(context)
    { }

    public bool UsernameExist(string username) =>
        Get(item => item.Username == username).Any();

    public bool EmailExist(string email) =>
        Get(item => item.Email == email).Any();

    public UserEntity? GetByUsername(string username)
    {
        var users = Get(item => item.Username == username);
        return users.Any() ? users.First() : null;
    }

    public async Task<bool> UsernameExistAsync(string username) =>
        (await GetAsync(item => item.Username == username)).Any();

    public async Task<bool> EmailExistAsync(string email) =>
        (await GetAsync(item => item.Email == email)).Any();

    public async Task<UserEntity?> GetByUsernameAsync(string username)
    {
        var users = await GetAsync(item => item.Username == username);
        return users.Any() ? users.First() : null;
    }
}
