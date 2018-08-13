namespace Actor1.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.ServiceFabric.Actors;

    public interface IActorService1 : IActorService
    {
        Task<string[]> GetCorrelationIdAsync(CancellationToken cancellationToken);
    }
}