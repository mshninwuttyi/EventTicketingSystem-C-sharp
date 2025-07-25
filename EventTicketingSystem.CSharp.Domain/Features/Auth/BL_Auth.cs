using Microsoft.Extensions.Options;

namespace EventTicketingSystem.CSharp.Domain.Features.Auth;

public class BL_Auth
{
    private readonly JwtSettings _jwtSettings;
    private readonly JwtService  _jwtService;
    private readonly DA_Auth _daAuth;
    private readonly ILogger<BL_Auth> _logger;

    public BL_Auth(IOptions<JwtSettings> jwtSettingsOptions, JwtService jwtService , DA_Auth daAuth, ILogger<BL_Auth> logger)
    {
        _jwtSettings = jwtSettingsOptions.Value;
        _jwtService = jwtService;
        _daAuth = daAuth;
        _logger = logger;
    }

    public async Task<Result<LoginResponseModel>> Login(LoginRequestModel requestModel)
    {
        var user = await _daAuth.GetUserByUsername(requestModel.Username);

        if (user == null)
        {
            _logger.LogInformation($"User not found: {requestModel.Username}");
            return Result<LoginResponseModel>.ValidationError("Invalid username or password.");
        }
        
        string hashed = BCrypt.Net.BCrypt.HashPassword("MyTestPassword123!");

        bool isValid = BCrypt.Net.BCrypt.Verify(requestModel.Password, user.Password);

        if (!isValid)
        {
            _logger.LogInformation($"Invalid password for user: {requestModel.Username}");
            return Result<LoginResponseModel>.ValidationError("Invalid username or password.");
        }

        var token = _jwtService.GenerateToken(user.Admincode, user.Username);
        _logger.LogInformation($"Generated Token: {token}");
        
        return  Result<LoginResponseModel>.Success(new LoginResponseModel
        {
            Token = token,
            TokenExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpiryMinutes)
        }, "Login succeeded.");
    }
}