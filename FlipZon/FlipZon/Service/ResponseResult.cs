namespace FlipZon.Service.Interfaces
{
    public class ResponseResult<T> : IResponseResult<T>
    {
        public T Result { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}

