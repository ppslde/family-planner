namespace FamilyPlaner.Application.Common.Models;

//public class Result
//{
//    internal Result(bool succeeded, IEnumerable<string> errors)
//    {
//        Succeeded = succeeded;
//        Errors = errors.ToArray();
//    }

//    public bool Succeeded { get; set; }

//    public string[] Errors { get; set; }

//    public static Result Success()
//    {
//        return new Result(true, Array.Empty<string>());
//    }

//    public static Result Failure(IEnumerable<string> errors)
//    {
//        return new Result(false, errors);
//    }
//}

public class Result<T>
{
    protected Result(bool succeeded, IEnumerable<string> errors, T? data = default)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
        Data = data;
    }

    public bool Succeeded { get; }
    public T? Data { get; }
    public string[] Errors { get; }

    public static Result<T> Success(T data)
    {
        return new Result<T>(true, Array.Empty<string>(), data);
    }

    public static Result<T> Failure(IEnumerable<string> errors)
    {
        return new Result<T>(false, errors);
    }
}
