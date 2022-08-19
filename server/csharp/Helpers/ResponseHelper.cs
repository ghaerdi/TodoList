namespace todolist.Helpers;

class ResponseHelper<T>
{
    public string Message { get; set; }
    public bool IsError { get; set; }
    public T? Body { get; set; }

    public ResponseHelper(string message, T body, bool isError = false)
    {
        Message = message;
        IsError = isError;
        Body = body;
    }
}

class ResponseHelper
{
    public string Message { get; set; }
    public bool IsError { get; set; }

    public ResponseHelper(string message, bool isError = false)
    {
        Message = message;
        IsError = isError;
    }
}
