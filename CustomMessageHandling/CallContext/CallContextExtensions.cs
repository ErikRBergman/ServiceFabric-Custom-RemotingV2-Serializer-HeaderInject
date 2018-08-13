namespace CustomMessageHandling.CallContext
{
    /// <summary>
    /// Provides extensions to support the old .NET Framework syntax of call context
    /// </summary>
    public static class CallContextExtensions
    {
        public static void FreeNamedDataSlot(this CallContext callContext, string key)
        {
            callContext.RemoveItem(key);
        }

        public static T Get<T>(this CallContext callContext, string key)
        {
            return (T)callContext.GetItem(key);
        }

        public static T GetItem<T>(this CallContext callContext, string key)
        {
            return (T)callContext.GetItem(key);
        }

        public static object LogicalGetData(this CallContext callContext, string key)
        {
            return callContext.GetItem(key);
        }

        public static void LogicalSetData(this CallContext callContext, string key, object value)
        {
            callContext.SetItem(key, value);
        }
    }
}