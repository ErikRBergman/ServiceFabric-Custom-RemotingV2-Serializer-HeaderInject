namespace CustomMessageHandling.CallContext
{
    /// <summary>
    /// Provides extensions for some common call context properties
    /// </summary>
    public static class CallContextSpecificPropertiesExtensions
    {
        /// <summary>
        /// Gets the call context correlation id
        /// </summary>
        /// <param name="callContext">The call context</param>
        /// <returns>The correlation id</returns>
        public static string CorrelationId(this CallContext callContext)
        {
            return callContext.GetItem(Constants.ExecutionTree.CorrelationIdPropertyName) as string;
        }

        /// <summary>
        /// Sets the call context correlation id
        /// </summary>
        /// <param name="callContext">The call context</param>
        /// <param name="correlationId">The correlation id to set</param>
        /// <returns>The call context</returns>
        public static CallContext CorrelationId(this CallContext callContext, string correlationId)
        {
            return callContext.SetItem(Constants.ExecutionTree.CorrelationIdPropertyName, correlationId);
        }

        /// <summary>
        /// Gets the call context service uri
        /// </summary>
        /// <param name="callContext">The call context</param>
        /// <returns>service uri</returns>
        public static string ServiceUri(this CallContext callContext)
        {
            return callContext.GetItem(Constants.ExecutionTree.ServiceUriPropertyName) as string;
        }

        /// <summary>
        /// Sets the call context service uri
        /// </summary>
        /// <param name="callContext">The call context</param>
        /// <param name="serviceUri">The service uri to set</param>
        /// <returns>The call context</returns>
        public static CallContext ServiceUri(this CallContext callContext, string serviceUri)
        {
            return callContext.SetItem(Constants.ExecutionTree.ServiceUriPropertyName, serviceUri);
        }

        /// <summary>
        /// Gets the call context user id
        /// </summary>
        /// <param name="callContext">The call context</param>
        /// <returns>The user id</returns>
        public static string UserId(this CallContext callContext)
        {
            return callContext.GetItem(Constants.ExecutionTree.UserIdPropertyName) as string;
        }

        /// <summary>
        /// Sets the call context user id
        /// </summary>
        /// <param name="callContext">The call context</param>
        /// <param name="userId">The user id to set</param>
        /// <returns>The call context</returns>
        public static CallContext UserId(this CallContext callContext, string userId)
        {
            return callContext.SetItem(Constants.ExecutionTree.UserIdPropertyName, userId);
        }
    }
}