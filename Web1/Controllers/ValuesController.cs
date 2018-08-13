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
        public async Task<object> GetAsync()
        {
            // Get the proxy factory
            var factory = ProxyFactory.ActorProxyFactory;

            // Create test correlation id and user id
            var correlationId = DateTime.Now.ToString("O");
            var userId = "testUser " + DateTime.Now.ToString("O");

            // Set the the values to the current call context 
            CallContext.Current.CorrelationId(correlationId).UserId(userId);

            // Use a random actor id
            var actorId = ActorId.CreateRandom();

            // Create actor service proxy
            var actorService = factory.CreateActorServiceProxy<IActorService1>(
                new Uri("fabric:/ServiceFabric_Custom_RemotingV2_Serializer_HeaderInject/Actor1ActorService"),
                actorId);
            
            // Call the actor service to get call context data
            var serviceResult = await actorService.GetTransferredCallContextDataAsync(CancellationToken.None);

            // Create actor proxy
            var actor = factory.CreateActorProxy<IActor1>(actorId);

            // Call the actor to get call context data
            var actorResult = await actor.GetTransferredCallContextDataAsync(CancellationToken.None);

            return new { Original = new { CorrelationId = correlationId, UserId = userId }, serviceResult, actorResult };
        }
    }
}