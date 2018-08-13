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
        public CustomServiceRemotingClient(IServiceRemotingClient wrapped)
        {
            this.Wrapped = wrapped ?? throw new ArgumentNullException(nameof(wrapped));
        }

        public ResolvedServiceEndpoint Endpoint
        {
            get => this.Wrapped.Endpoint;
            set => this.Wrapped.Endpoint = value;
        }

        public string ListenerName
        {
            get => this.Wrapped.ListenerName;
            set => this.Wrapped.ListenerName = value;
        }

        public ResolvedServicePartition ResolvedServicePartition
        {
            get => this.Wrapped.ResolvedServicePartition;
            set => this.Wrapped.ResolvedServicePartition = value;
        }

        public IServiceRemotingClient Wrapped { get; }

        public Task<IServiceRemotingResponseMessage> RequestResponseAsync(IServiceRemotingRequestMessage requestMessage)
        {
            AddCorrelationHeaders(requestMessage.GetHeader());
            return this.Wrapped.RequestResponseAsync(requestMessage);
        }

        public void SendOneWay(IServiceRemotingRequestMessage requestMessage)
        {
            AddCorrelationHeaders(requestMessage.GetHeader());
            this.Wrapped.SendOneWay(requestMessage);
        }

        private static void AddCorrelationHeaders(IServiceRemotingRequestMessageHeader header)
        {
            var callContext = CallContext.CallContext.Current;

            foreach (var headerName in Constants.ExecutionTree.All)
            {
                AddStringHeader(callContext, headerName, header);
            }
        }

        private static void AddStringHeader(CallContext.CallContext callContext, string headerName, IServiceRemotingRequestMessageHeader header)
        {
            var headerValue = callContext.Get<string>(headerName);
            if (headerValue != null)
            {
                var headerValueBytes = CommonEncoding.DefaultEncoding.GetBytes(headerValue);
                header.AddHeader(headerName, headerValueBytes);
            }
        }
    }
}