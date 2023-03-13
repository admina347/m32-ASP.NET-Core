using Microsoft.EntityFrameworkCore;
using unit_32._7_mvc.Models.Db;

namespace unit_32._7_mvc.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        // ссылка на контекст
        private readonly BlogContext _context;

        public RequestRepository(BlogContext context)
        {
            _context = context;
        }

        //Add request to Db table
        public async Task AddRequestToDbAsync(Request req)
        {
            await _context.Requests.AddAsync(req);
            await _context.SaveChangesAsync();
        }
        //Get all requests
        public async Task<Request[]> GetAllRequestsAsync()
        {
            // Получим всех активных пользователей
            return await _context.Requests.OrderByDescending(r => r.Date).ToArrayAsync();
        }
    }
}