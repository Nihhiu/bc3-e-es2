namespace ComparacaoPrecos.Repository.Interfaces;

public interface ILogsRepository
{
    public Task<List<Logs>> GetUserLogs(string userId);
    public Task AddLog(string userId, string mensagem);
}