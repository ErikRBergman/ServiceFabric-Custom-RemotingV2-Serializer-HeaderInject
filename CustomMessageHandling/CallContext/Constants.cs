namespace CustomMessageHandling.CallContext
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides common constants
    /// </summary>
    public static partial class Constants
    {
        /// <summary>
        /// Provides names for common call context properties
        /// </summary>
        public static class ExecutionTree
        {
            public const string CorrelationIdPropertyName = "FG.ServiceFabric.Services.RemotingV2.CorrelationId_af072ed9-aa50-496d-8dfa-8bf55a228401";

            public const string ServiceUriPropertyName = "FG.ServiceFabric.Services.RemotingV2.ServiceUri_af072ed9-aa50-496d-8dfa-8bf55a228401";

            public const string UserIdPropertyName = "FG.ServiceFabric.Services.RemotingV2.UserId_af072ed9-aa50-496d-8dfa-8bf55a228401";

            /// <summary>
            /// Gets a list of all propety names
            /// </summary>
            public static IEnumerable<string> All { get; } = new[] { CorrelationIdPropertyName, UserIdPropertyName, ServiceUriPropertyName };
        }
    }
}