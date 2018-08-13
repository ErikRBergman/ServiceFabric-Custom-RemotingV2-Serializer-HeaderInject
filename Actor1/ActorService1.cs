namespace Actor1
{
    using System;
    using System.Collections.Generic;
    using System.Fabric;
    using System.Threading;
    using System.Threading.Tasks;

    using CustomMessageHandling;
    using CustomMessageHandling.CallContext;

    using global::Actor1.Interfaces;

    using Microsoft.ServiceFabric.Actors;
    using Microsoft.ServiceFabric.Actors.Remoting.V2.FabricTransport.Runtime;
    using Microsoft.ServiceFabric.Actors.Runtime;
    using Microsoft.ServiceFabric.Services.Communication.Runtime;
    using Microsoft.ServiceFabric.Services.Remoting.V2;

    public class ActorService1 : ActorService, IActorService1
    {
        public ActorService1(
            StatefulServiceContext context,
            ActorTypeInformation actorTypeInfo,
            Func<ActorService, ActorId, ActorBase> actorFactory = null,
            Func<ActorBase, IActorStateProvider, IActorStateManager> stateManagerFactory = null,
            IActorStateProvider stateProvider = null,
            ActorServiceSettings settings = null)
            : base(context, actorTypeInfo, actorFactory, stateManagerFactory, stateProvider, settings)
        {
        }

        public async Task<string[]> GetCorrelationIdAsync(CancellationToken cancellationToken)
        {
            return new[] { CallContext.Current.CorrelationId(), CallContext.Current.UserId() };
        }

        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new[]
                       {
                           new ServiceReplicaListener(
                               context =>
                                   {
                                       var serializationProvider = new ServiceRemotingDataContractSerializationProvider();
                                       var messageDispatcher = new CustomActorMessageHandler(this, serializationProvider.CreateMessageBodyFactory());
                                       return new FabricTransportActorServiceRemotingListener(context, messageDispatcher, serializationProvider: serializationProvider);
                                   },
                               "V2Listener")
                       };
        }
    }
}