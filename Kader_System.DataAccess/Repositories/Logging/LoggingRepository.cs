namespace Kader_System.DataAccess.Repositories.Logging;

public class LoggingRepository : ILoggingRepository
{
    private readonly IHttpContextAccessor _accessor;
    private readonly KaderDbContext _context;
    private readonly ILogger _logger;
    public LoggingRepository(KaderDbContext context, ILoggerFactory loggerFactory, IHttpContextAccessor accessor)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger("db_logs");
        _accessor = accessor;
    }

    public async Task<bool> LogExceptionInDb(Exception exception, string objJson = null!, LogType logType = LogType.Bug)
    {
        try
        {
            await _context.Logs.AddAsync(new ComLog()
            {
                Message = exception?.Message ?? string.Empty,
                ExceptionPath = exception?.Source ?? string.Empty,
                ExceptionInnerPath = exception?.InnerException?.Source ?? string.Empty,
                InnerException = exception?.InnerException?.Message ?? string.Empty,
                StackTrace = exception?.StackTrace ?? string.Empty,
                ObjJson = objJson,
                Added_by = _accessor?.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.NameId)?.Value ?? string.Empty
            });
            return await _context.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _logger.LogError(JsonConvert.SerializeObject(e));
            throw;
        }
    }
}
