namespace BasicIdentityCustomApi.Types
{
    public class ServiceMessage
    {
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
    }

    // T olarak userinfodto alacak ve datasınıda beraber gönderecektir.
    public class ServiceMessage<T>
    {
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
    }
}
