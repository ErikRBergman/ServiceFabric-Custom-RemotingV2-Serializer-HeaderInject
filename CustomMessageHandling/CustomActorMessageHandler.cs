namespace CustomMessageHandling
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    using CustomMessageHandling.CallContext;

    using Microsoft.ServiceFabric.Actors.Remoting.V2.Runtime;
    using Microsoft.ServiceFabric.Actors.Runtime;
    using Microsoft.ServiceFabric.Services.Remoting.V2;
    using Microsoft.ServiceFabric.Services.Remoting.V2.Runtime;

    public class CustomActorMessageHandler : ActorServiceRemotingDispatcher, IServiceRemotingMessageHandler
    {
        public CustomActorMessageHandler(ActorService actorService, IServiceRemotingMessageBodyFactory serviceRemotingRequestMessageBodyFactory)
            : base(actorService, serviceRemotingRequestMessageBodyFactory)
        {
        }

        public override void HandleOneWayMessage(IServiceRemotingRequestMessage requestMessage)
        {
            var header = requestMessage.GetHeader();

            var headerName = "CorrelationId";

            if (header.TryGetHeaderValue(headerName, out byte[] headerValue))
            {
                var correlationIdString = Encoding.Unicode.GetString(headerValue);
                CallContext.CallContext.Current.CorrelationId(correlationIdString);
            }
            base.HandleOneWayMessage(requestMessage);
        }

        public override Task<IServiceRemotingResponseMessage> HandleRequestResponseAsync(IServiceRemotingRequestContext requestContext, IServiceRemotingRequestMessage requestMessage)
        {
            var header = requestMessage.GetHeader();

            if (header.TryGetHeaderValue("CorrelationId", out byte[] correlationId))
            {
                var correlationIdString = Encoding.Unicode.GetString(correlationId);
                CallContext.CallContext.Current.CorrelationId(correlationIdString);
            }
            return base.HandleRequestResponseAsync(requestContext, requestMessage);
        }
    }
}