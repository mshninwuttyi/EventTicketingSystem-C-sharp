using Microsoft.Extensions.Options;

namespace EventTicketingSystem.CSharp.Domain.Features.Auth;

public class BL_Auth
{
    private readonly JwtSettings _jwtSettings;
    private readonly JwtService  _jwtService;
    private readonly DA_Auth _daAuth;
    private readonly ILogger<BL_Auth> _logger;
    private readonly UserContextService  _userContextService;

    public BL_Auth(IOptions<JwtSettings> jwtSettingsOptions, JwtService jwtService , 
                        DA_Auth daAuth, UserContextService  userContextService, ILogger<BL_Auth> logger)
    {
        _jwtSettings = jwtSettingsOptions.Value;
        _jwtService = jwtService;
        _daAuth = daAuth;
        _userContextService = userContextService;
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
        
        string hashedPassword = requestModel.Password.HashPassword(requestModel.Username);

        bool isValid = BCrypt.Net.BCrypt.Verify(user.Password, hashedPassword);

        if (!isValid)
        {
            _logger.LogInformation($"Invalid password for user: {requestModel.Username}");
            return Result<LoginResponseModel>.ValidationError("Invalid username or password.");
        }
        
        var token = _jwtService.GenerateToken(user.Admincode, user.Username);
        
        var refreshToken = GenerateUlid();
        var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(1); 
        
        var adminCode = _userContextService.AdminCode();
        if (adminCode == null)
            throw new InvalidOperationException("AdminCode cannot be null during login.");

        var sessionId = _userContextService.SessionId();
        if (sessionId == null)
            throw new InvalidOperationException("SessionId cannot be null during login.");
        
        // Create a new login session in tbl_login
        var newLogin = new TblLogin
        {
            Loginid = GenerateUlid(),
            Admincode = adminCode,
            Username = user.Username,
            Sessionid = sessionId,
            Refreshtoken = refreshToken,
            Refreshtokenexpiresat = refreshTokenExpiresAt,
            Loginstatus = "Login",
            Createdby = "SYSTEM",
            Createdat = DateTime.UtcNow,
            Deleteflag = false
        };
        
        await _daAuth.CreateLogin(newLogin);

        _logger.LogInformation($"Generated Token: {token}");
        
        return  Result<LoginResponseModel>.Success(new LoginResponseModel
        {
            Token = token,
            TokenExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpiryMinutes),
            RefreshToken = refreshToken,
            RefreshTokenExpiresAt = refreshTokenExpiresAt
        }, "Login succeeded.");
    }
    
    public async Task<Result<RefreshTokenResponseModel>> RefreshToken(RefreshTokenRequestModel request)
    {
        var login = await _daAuth.GetUserByRefreshToken(request.RefreshToken);

        if (login == null || login.Refreshtokenexpiresat < DateTime.UtcNow)
        {
            return Result<RefreshTokenResponseModel>.ValidationError("Invalid or expired refresh token.");
        }

        var newJwt = _jwtService.GenerateToken(login.Admincode, login.Username);

        // Check if refresh token is about to expire
        if ((login.Refreshtokenexpiresat - DateTime.UtcNow).TotalDays < 1)
        {
            _logger.LogInformation($"Rotating refresh token for loginid: {login.Loginid}");
            login.Refreshtoken = GenerateUlid();
            login.Refreshtokenexpiresat = DateTime.UtcNow.AddDays(1);
            await _daAuth.UpdateLogin(login);
        }

        return Result<RefreshTokenResponseModel>.Success(new RefreshTokenResponseModel
        {
            Token = newJwt,
            TokenExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpiryMinutes),
            RefreshToken = login.Refreshtoken,
            RefreshTokenExpiresAt = login.Refreshtokenexpiresat
        }, "Token refreshed.");
    }
    
    public async Task<Result<string>> Logout(LogoutRequestModel request)
    {
        var login = await _daAuth.GetUserByRefreshToken(request.RefreshToken);

        if (login == null)
        {
            return Result<string>.ValidationError("Invalid refresh token.");
        }

        // Update login status as "logout"
        login.Loginstatus = "Logout";
        login.Refreshtokenexpiresat = DateTime.UtcNow; 
        login.Modifiedby = "SYSTEM";
        login.Modifiedat = DateTime.UtcNow;

        await _daAuth.UpdateLogin(login);

        _logger.LogInformation($"User Logged out: {login.Loginid}");

        return Result<string>.Success("Logout succeeded.");
    }


}