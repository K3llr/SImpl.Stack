namespace SImpl.Http.Storage.Module
{
    public class HttpStorageModuleConfig
    {
        public bool TransactionPerRequestsEnabled { get; set; } = false;
        
        public HttpStorageModuleConfig AddTransactionPerRequests(bool enable = true)
        {
            TransactionPerRequestsEnabled = enable;
            return this;
        }
    }
}