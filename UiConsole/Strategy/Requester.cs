namespace UiConsole.Strategy
{
    public class Requester<T>
    {
        IRequestStrategy<T> RequestingActor { get; set; }

        public Requester(IRequestStrategy<T> requestingActor)
        {
            RequestingActor = requestingActor;
        }

        public async Task<T?> GetRequestResult(string uri, string? content)
        {
           return await RequestingActor.GetResponce(uri, content);
        }
    }
}
