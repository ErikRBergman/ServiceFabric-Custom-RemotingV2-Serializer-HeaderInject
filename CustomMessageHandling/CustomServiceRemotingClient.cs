namespace CustomMessageHandling
{
    using System;
    using System.Fabric;
    using System.Text;
    using System.Threading.Tasks;

    using CustomMessageHandling.CallContext;

    using Microsoft.ServiceFabric.Services.Remoting.V2;
    using Microsoft.ServiceFabric.Services.Remoting.V2.Client;

    public class CustomServiceRemotingClient : IServiceRemotingClient
    {
        public ResolvedServicePartition ResolvedServicePartition { get => this.Wrapped.ResolvedServicePartition; set => this.Wrapped.ResolvedServicePartition = value; }
        public string ListenerName { get => this.Wrapped.ListenerName; set => this.Wrapped.ListenerName = value; }
        public ResolvedServiceEndpoint Endpoint { get => this.Wrapped.Endpoint; set => this.Wrapped.Endpoint = value; }

        public IServiceRemotingClient Wrapped { get; }

        public CustomServiceRemotingClient(IServiceRemotingClient wrapped)
        {
            this.Wrapped = wrapped ?? throw new ArgumentNullException(nameof(wrapped));
        }

        public Task<IServiceRemotingResponseMessage> RequestResponseAsync(IServiceRemotingRequestMessage requestRequestMessage)
        {
            var correlationId = Encoding.Unicode.GetBytes(CallContext.CallContext.Current.CorrelationId());
            requestRequestMessage.GetHeader().AddHeader("CorrelationId", correlationId);
            return this.Wrapped.RequestResponseAsync(requestRequestMessage);
        }

        public void SendOneWay(IServiceRemotingRequestMessage requestMessage)
        {
            var correlationId = Encoding.Unicode.GetBytes(CallContext.CallContext.Current.CorrelationId());
            requestMessage.GetHeader().AddHeader("CorrelationId", correlationId);
            this.Wrapped.SendOneWay(requestMessage);
        }
    }
}