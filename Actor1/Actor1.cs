namespace Actor1
{
    using System.Threading;
    using System.Threading.Tasks;

    using CustomMessageHandling.CallContext;

    using global::Actor1.Interfaces;

    using Microsoft.ServiceFabric.Actors;
    using Microsoft.ServiceFabric.Actors.Runtime;

    [StatePersistence(StatePersistence.Persisted)]
    internal class Actor1 : Actor, IActor1
    {
        public Actor1(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        public async Task<string[]> GetTransferredCallContextDataAsync(CancellationToken cancellationToken)
        {
            return new[] { CallContext.Current.CorrelationId(), CallContext.Current.UserId() };
        }

        public async Task TestTransferredCallContextDataAsync()
        {
            var cid = CallContext.Current.CorrelationId();
            var uid = CallContext.Current.UserId();
        }
    }
}