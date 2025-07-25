namespace EventTicketingSystem.CSharp.Domain.Features.VerificationCode;

public class BL_VerificationCode
{
    private readonly DA_VerificationCode _daService;

    public BL_VerificationCode(DA_VerificationCode dataAccess)
    {
        _daService = dataAccess;
    }

    public async Task<Result<VCResponseModel>> GetList()
    {
        return await _daService.GetList();
    }

    public async Task<Result<VCResponseModel>> GetVCById(VCRequestModel request)
    {
        return await _daService.GetVCodeById(request.Verificationid);
    }

    public async Task<Result<VCResponseModel>> CreateNewVC(VCRequestModel request)
    {
        return await _daService.CreateVCode(request);
    }

    public async Task<Result<bool>> VerifyCode(VCRequestModel request)
    {
        return await _daService.VerifyCodeByEmail(request.Email, request.Verificationcode);
    }
}