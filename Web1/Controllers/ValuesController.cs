using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web1.Controllers
{
    using System.Threading;

    using Actor1.Interfaces;

    using CustomMessageHandling;
    using CustomMessageHandling.CallContext;

    using Microsoft.ServiceFabric.Actors;
    using Microsoft.ServiceFabric.Actors.Client;
    using Microsoft.ServiceFabric.Actors.Remoting.V2.FabricTransport.Client;

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> GetAsync()
        {
            // var factory = new ActorProxyFactory(c => new CustomTransportServiceRemotingClientFactory(new FabricTransportActorRemotingClientFactory(c)));
            var factory = ProxyFactory.ActorProxyFactory;
            
            var correlationId = DateTime.Now.ToString("O");

            CallContext.Current.CorrelationId(correlationId);

            var actorId = ActorId.CreateRandom();

            var actorService = factory.CreateActorServiceProxy<IActorService1>(new Uri("fabric:/ServiceFabric_Custom_RemotingV2_Serializer_HeaderInject/Actor1ActorService"), actorId);
            var actor = factory.CreateActorProxy<IActor1>(actorId);

            var serviceCorrelationId = await actorService.GetCorrelationIdAsync(CancellationToken.None);
            var actorCorrelationId = await actor.GetCorrelationIdAsync(CancellationToken.None);

            return new[] { correlationId, serviceCorrelationId, actorCorrelationId };
        }
    }
}
