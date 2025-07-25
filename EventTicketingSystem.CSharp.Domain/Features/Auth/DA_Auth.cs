namespace EventTicketingSystem.CSharp.Domain.Features.Auth;

public class DA_Auth
{
    private readonly ILogger<DA_Auth>  _logger;
    private readonly AppDbContext _db;

    public DA_Auth(ILogger<DA_Auth> logger, AppDbContext db)
    {
        _logger = logger;
        _db = db;
    }
    public async Task<TblAdmin?> GetUserByUsername(string username)
    {
        return await _db.TblAdmins.FirstOrDefaultAsync(x => x.Username == username);
    }
}