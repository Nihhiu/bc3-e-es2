using ComparacaoPrecos.Data;
using ComparacaoPrecos.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComparacaoPrecos.Repository;

public class LogsRepository : ILogsRepository
{
    private readonly ApplicationDbContext _context;

    public LogsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Logs>> GetUserLogs(string userId)
    {
        return await _context.Logs
            .Where(log => log.Id == userId)
            .OrderByDescending(log => log.DataHora)
            .ToListAsync();
    }

    public async Task AddLog(string userId, string mensagem)
    {
        var log = new Logs
        {
            Id = userId,
            DataHora = DateTime.UtcNow,
            Message = mensagem
        };

        _context.Logs.Add(log);
        await _context.SaveChangesAsync();
    }
}