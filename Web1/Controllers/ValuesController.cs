namespace Web1.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Actor1.Interfaces;

    using CustomMessageHandling;
    using CustomMessageHandling.CallContext;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.ServiceFabric.Actors;

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string[]>> GetAsync()
        {
            var factory = ProxyFactory.ActorProxyFactory;

            var correlationId = DateTime.Now.ToString("O");
            var userId = "testUser " + DateTime.Now.ToString("O");

            CallContext.Current.CorrelationId(correlationId).UserId(userId);

            var actorId = ActorId.CreateRandom();

            var actorService = factory.CreateActorServiceProxy<IActorService1>(
                new Uri("fabric:/ServiceFabric_Custom_RemotingV2_Serializer_HeaderInject/Actor1ActorService"),
                actorId);
            var actor = factory.CreateActorProxy<IActor1>(actorId);

            var serviceResult = await actorService.GetCorrelationIdAsync(CancellationToken.None);
            var actorResult = await actor.GetCorrelationIdAsync(CancellationToken.None);

            return new[] { new[] { correlationId, userId }, serviceResult, actorResult };
        }
    }
}