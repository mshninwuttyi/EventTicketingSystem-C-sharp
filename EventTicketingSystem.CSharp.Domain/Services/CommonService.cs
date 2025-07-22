namespace EventTicketingSystem.CSharp.Domain.Common;

public class CommonService
{
    private readonly AppDbContext _db;
    private readonly DapperService _dapper;
 
    public CommonService(AppDbContext db, DapperService dapper)
    {
        _db = db;
        _dapper = dapper;
    }

    #region Create Sequence for Event

    public async Task CreateSequence(string uniqueName, string eventCode)
    {
        var eventSequence = new TblSequence
        {
            Uniquename = uniqueName,
            Sequenceno = "0000000",
            Sequencedate = DateTime.Now,
            Sequencetype = EnumSequenceType.Event.ToString(),
            Eventcode = eventCode,
            Deleteflag = false
        };

        var transactionSequence = new TblSequence
        {
            Uniquename = uniqueName,
            Sequenceno = "0000000",
            Sequencedate = DateTime.Now,
            Sequencetype = EnumSequenceType.Transaction.ToString(),
            Eventcode = eventCode,
            Deleteflag = false
        };

        await _db.TblSequences.AddRangeAsync(eventSequence, transactionSequence);
        await _db.SaveAndDetachAsync();
    }

    #endregion

    #region Generate Table Sequence Code

    public async Task<string> GenerateSequenceCode(EnumTableUniqueName uniqueName)
    {
        var sequence = await _db.TblSequences
                        .AsNoTracking()
                        .Where(
                            x => x.Uniquename == uniqueName.GetEnumDescription() &&
                            x.Sequencetype == EnumSequenceType.Table.ToString() &&
                            x.Deleteflag == false)
                        .FirstOrDefaultAsync();

        if (sequence is null)
        {
            throw new Exception("Sequence not found for the given event code.");
        }

        var sequnceCode = $"{sequence.Uniquename}";

        var param = new { id = sequence.Sequenceid };

        string? seqNo = await _dapper.QueryStoredProcedureFirstOrDefault<string>(Queries.sp_sequencecode, param);

        string sequenceNo = seqNo ?? throw new Exception("Sequence not found.");

        sequnceCode += sequenceNo;

        return sequnceCode;
    }

    #endregion
    
    #region Generate Event Sequence Code

    public async Task<string> GenerateEventSequenceCode(string uniqueName, string eventCode)
    {
        var sequence = await _db.TblSequences
                        .AsNoTracking()
                        .Where(
                            x => x.Uniquename == uniqueName &&
                            x.Sequencetype == EnumSequenceType.Event.ToString() &&
                            x.Eventcode == eventCode && 
                            x.Deleteflag == false)
                        .FirstOrDefaultAsync();

        if (sequence is null)
        {
            throw new Exception("Sequence not found for the given event code.");
        }

        var sequenceCode = $"{sequence.Uniquename}";

        var param = new { id = sequence.Sequenceid };

        string? seqNo = await _dapper.QueryStoredProcedureFirstOrDefault<string>(Queries.sp_sequencecode, param);

        string sequenceNo = seqNo ?? throw new Exception("Sequence not found.");

        sequenceCode += sequenceNo;

        return sequenceCode;
    }

    #endregion

    #region Generate Transaction Sequence Code

    public async Task<string> GenerateTransactionSequenceCode(string uniqueName, string eventCode)
    {
        var sequence = await _db.TblSequences
                        .AsNoTracking()
                        .Where(
                            x => x.Uniquename == uniqueName &&
                            x.Sequencetype == EnumSequenceType.Transaction.ToString() &&
                            x.Eventcode == eventCode &&
                            x.Deleteflag == false)
                        .FirstOrDefaultAsync();

        if (sequence is null)
        {
            throw new Exception("Sequence not found for the given event code.");
        }

        var date = $"{sequence.Sequencedate.Year}{sequence.Sequencedate.Month}{sequence.Sequencedate.Day}";
        var sequenceCode = $"{sequence.Uniquename}{date}";

        var param = new { id = sequence.Sequenceid };

        string? seqNo = await _dapper.QueryStoredProcedureFirstOrDefault<string>(Queries.sp_sequencecode, param);

        string sequenceNo = seqNo ?? throw new Exception("Sequence not found.");

        sequenceCode += sequenceNo;

        return sequenceCode;
    }

    #endregion
}