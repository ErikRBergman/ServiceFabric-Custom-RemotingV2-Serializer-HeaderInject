namespace CustomMessageHandling
{
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
            ExtractCorrelationHeaders(requestMessage.GetHeader());
            base.HandleOneWayMessage(requestMessage);
        }

        public override Task<IServiceRemotingResponseMessage> HandleRequestResponseAsync(
            IServiceRemotingRequestContext requestContext,
            IServiceRemotingRequestMessage requestMessage)
        {
            ExtractCorrelationHeaders(requestMessage.GetHeader());
            return base.HandleRequestResponseAsync(requestContext, requestMessage);
        }

        private static void DecodeHeaderAndSetCallContext(string headerName, byte[] headerValue)
        {
            var headerString = Encoding.UTF8.GetString(headerValue);
            CallContext.CallContext.Current.SetItem(headerName, headerString);
        }

        private static void ExtractCorrelationHeaders(IServiceRemotingRequestMessageHeader header)
        {
            foreach (var headerName in Constants.ExecutionTree.All)
            {
                if (header.TryGetHeaderValue(headerName, out var headerValue))
                {
                    DecodeHeaderAndSetCallContext(headerName, headerValue);
                }
            }
        }
    }
}