namespace EventTicketingSystem.CSharp.Shared;

public partial class Result<T>
{
    public bool IsSuccess { get; set; }
    public bool IsError => !IsSuccess;
    public string Message { get; set; } = string.Empty!;
    public T Data { get; set; } = default!;

    public static Result<T> Success(T data, string message = "Success!") 
    { 
        return new Result<T> { IsSuccess = true, Data = data, Message = message }; 
    }

    public static Result<T> Success(string message = "Success!")
    {
        return Success(default!, message);
    }

    public static Result<T> ValidationError(string message, T? data = default) 
    { 
        return new Result<T> { IsSuccess = false, Data = data, Message = message }; 
    }

    public static Result<T> SystemError(string message = "Something went wrong!", T? data = default) 
    { 
        return new Result<T> { IsSuccess = false, Data = data, Message = message}; 
    }

    public static Result<T> DuplicateRecordError(string message = "There are Duplicated Records!", T? data = default) 
    { 
        return new Result<T> { IsSuccess = false, Data = data, Message = message}; 
    }

    public static Result<T> NotFoundError(string message = "No Data Found!", T? data = default) 
    { 
        return new Result<T> { IsSuccess = false, Data = data, Message = message}; 
    }

    public static Result<T> UserInputError(string message = "Wrong User Input!", T? data = default) 
    { 
        return new Result<T> { IsSuccess = false, Data = data, Message = message}; 
    }
}