namespace FlipZon.Service.Interfaces
{
    public interface IResponseResult<T>
    {
        T Result { get; set; }
        bool Status { get; set; }
        string Message { get; set; }
    }
}

