using unit_32._7_mvc.Models.Db;

namespace unit_32._7_mvc.Repositories
{
    public interface IRequestRepository
    {
        Task AddRequestToDbAsync(Request req);
        Task<Request []> GetAllRequestsAsync();
    }
}