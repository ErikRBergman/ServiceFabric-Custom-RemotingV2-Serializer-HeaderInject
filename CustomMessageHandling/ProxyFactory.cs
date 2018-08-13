using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomMessageHandling
{
    using Microsoft.ServiceFabric.Actors.Client;
    using Microsoft.ServiceFabric.Actors.Remoting.V2.FabricTransport.Client;

    public static class ProxyFactory
    {
        public static ActorProxyFactory ActorProxyFactory { get; } = new ActorProxyFactory(c => new CustomTransportServiceRemotingClientFactory(new FabricTransportActorRemotingClientFactory(c)));
    }
}
