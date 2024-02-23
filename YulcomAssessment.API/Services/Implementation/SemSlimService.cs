namespace YulcomAssesment.API.Services.Implementation
{
    public class SemSlimService
    {
        public readonly SemaphoreSlim semaphore;
        public readonly SemaphoreSlim trxSemaphore;
        public SemSlimService()
        {
            semaphore = new SemaphoreSlim(1, 1);
            trxSemaphore = new SemaphoreSlim(1, 1);
        }
    }
}
