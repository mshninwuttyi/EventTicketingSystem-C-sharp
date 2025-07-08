namespace EventTicketingSystem.CSharp.Shared;

public partial class Result<T>
{
    public bool IsSuccess { get; set; }
    public bool IsError { get { return !IsSuccess; } }
    public T Data { get; set; } = default!;
    public string Message { get; set; } = string.Empty!;

    private EnumRespType Type { get; set; }
    public bool IsValidationError { get { return Type == EnumRespType.ValidationError; } }
    public bool IsDataError { get { return Type == EnumRespType.Error; } }
    public bool IsDuplicateRecord { get { return Type == EnumRespType.DuplicateRecord; } }
    public bool IsNotFound { get { return Type == EnumRespType.NotFound; } }
    public bool IsUserError { get { return Type == EnumRespType.UserInputError; } }

    public static Result<T> Success(T data, string message = "Success!") 
    { 
        return new Result<T> { IsSuccess = true, Type = EnumRespType.Success, Data = data, Message = message }; 
    }

    public static Result<T> ValidationError(string message, T? data = default) 
    { 
        return new Result<T> { IsSuccess = false, Data = data, Message = message, Type = EnumRespType.ValidationError }; 
    }

    public static Result<T> Error(string message = "Something went wrong!", T? data = default) 
    { 
        return new Result<T> { IsSuccess = false, Data = data, Message = message, Type = EnumRespType.Error }; 
    }

    public static Result<T> DuplicateRecordError(string message = "There are Duplicated Records!", T? data = default) 
    { 
        return new Result<T> { IsSuccess = false, Data = data, Message = message, Type = EnumRespType.DuplicateRecord }; 
    }

    public static Result<T> NotFoundError(string message = "No Data Found!", T? data = default) 
    { 
        return new Result<T> { IsSuccess = false, Data = data, Message = message, Type = EnumRespType.NotFound }; 
    }

    public static Result<T> UserInputError(string message = "Wrong User Input!", T? data = default) 
    { 
        return new Result<T> { IsSuccess = false, Data = data, Message = message, Type = EnumRespType.UserInputError }; 
    }
}