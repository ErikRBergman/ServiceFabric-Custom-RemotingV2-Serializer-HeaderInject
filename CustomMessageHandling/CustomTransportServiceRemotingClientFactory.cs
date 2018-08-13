namespace CustomMessageHandling
{
    using System;
    using System.Fabric;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.ServiceFabric.Services.Client;
    using Microsoft.ServiceFabric.Services.Communication.Client;
    using Microsoft.ServiceFabric.Services.Remoting.V2;
    using Microsoft.ServiceFabric.Services.Remoting.V2.Client;

    public class CustomTransportServiceRemotingClientFactory : IServiceRemotingClientFactory
    {
        private readonly IServiceRemotingClientFactory _wrapped;

        public CustomTransportServiceRemotingClientFactory(IServiceRemotingClientFactory wrapped)
        {
            this._wrapped = wrapped ?? throw new ArgumentNullException(nameof(wrapped));
            this._wrapped.ClientConnected += this.WrappedClientConnected;
            this._wrapped.ClientDisconnected += this.WrappedClientDisconnected;
        }

        public event EventHandler<CommunicationClientEventArgs<IServiceRemotingClient>> ClientConnected;

        public event EventHandler<CommunicationClientEventArgs<IServiceRemotingClient>> ClientDisconnected;

        public async Task<IServiceRemotingClient> GetClientAsync(
            Uri serviceUri,
            ServicePartitionKey partitionKey,
            TargetReplicaSelector targetReplicaSelector,
            string listenerName,
            OperationRetrySettings retrySettings,
            CancellationToken cancellationToken)
        {
            var client = await this._wrapped.GetClientAsync(serviceUri, partitionKey, targetReplicaSelector, listenerName, retrySettings, cancellationToken);
            return new CustomServiceRemotingClient(client);
        }

        public async Task<IServiceRemotingClient> GetClientAsync(
            ResolvedServicePartition previousRsp,
            TargetReplicaSelector targetReplicaSelector,
            string listenerName,
            OperationRetrySettings retrySettings,
            CancellationToken cancellationToken)
        {
            var client = await this._wrapped.GetClientAsync(previousRsp, targetReplicaSelector, listenerName, retrySettings, cancellationToken);
            return new CustomServiceRemotingClient(client);
        }

        public IServiceRemotingMessageBodyFactory GetRemotingMessageBodyFactory()
        {
            return this._wrapped.GetRemotingMessageBodyFactory();
        }

        public Task<OperationRetryControl> ReportOperationExceptionAsync(
            IServiceRemotingClient client,
            ExceptionInformation exceptionInformation,
            OperationRetrySettings retrySettings,
            CancellationToken cancellationToken)
        {
            var customClient = client as CustomServiceRemotingClient;
            if (customClient != null)
            {
                client = customClient.Wrapped;
            }

            return this._wrapped.ReportOperationExceptionAsync(client, exceptionInformation, retrySettings, cancellationToken);
        }

        private void WrappedClientConnected(object sender, CommunicationClientEventArgs<IServiceRemotingClient> e)
        {
            this.ClientConnected?.Invoke(sender, e);
        }

        private void WrappedClientDisconnected(object sender, CommunicationClientEventArgs<IServiceRemotingClient> e)
        {
            this.ClientDisconnected?.Invoke(sender, e);
        }
    }
}