namespace CustomMessageHandling.CallContext
{
    public static class CallContextSpecificPropertiesExtensions
    {
        public static string CorrelationId(this CallContext callContext)
        {
            return callContext.GetItem(Constants.ExecutionTree.CorrelationIdPropertyName) as string;
        }

        public static CallContext CorrelationId(this CallContext callContext, string correlationId)
        {
            return callContext.SetItem(Constants.ExecutionTree.CorrelationIdPropertyName, correlationId);
        }

        public static string ServiceUri(this CallContext callContext)
        {
            return callContext.GetItem(Constants.ExecutionTree.ServiceUriPropertyName) as string;
        }

        public static CallContext ServiceUri(this CallContext callContext, string serviceUri)
        {
            return callContext.SetItem(Constants.ExecutionTree.ServiceUriPropertyName, serviceUri);
        }

        public static string UserId(this CallContext callContext)
        {
            return callContext.GetItem(Constants.ExecutionTree.UserIdPropertyName) as string;
        }

        public static CallContext UserId(this CallContext callContext, string userId)
        {
            return callContext.SetItem(Constants.ExecutionTree.UserIdPropertyName, userId);
        }
    }
}