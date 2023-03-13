using unit_32._7_mvc.Models.Db;

namespace unit_32._7_mvc.Repositories
{
    public interface IBlogRepository
    {
        Task AddUser(User user);
        Task<User[]> GetUsers();
    }
}