// ReSharper disable StyleCop.SA1311

namespace CustomMessageHandling.CallContext
{
    using System.Collections.Immutable;
    using System.Threading;

    public class CallContext
    {
        private readonly AsyncLocal<ImmutableDictionary<string, object>> _executionTreeStorage = new AsyncLocal<ImmutableDictionary<string, object>>();

        /// <summary>
        /// Gets the current call context
        /// </summary>
        public static CallContext Current { get; } = new CallContext();

        /// <summary>
        /// Gets an item by key from the call context or null if the item does not exit
        /// </summary>
        /// <param name="key">Key of the item to get</param>
        /// <returns>The value</returns>
        public object GetItem(string key)
        {
            var dictionary = this._executionTreeStorage.Value;

            if (dictionary == null || !dictionary.TryGetValue(key, out var value))
            {
                return null;
            }

            return value;
        }

        /// <summary>
        /// Removes an item from the call context
        /// </summary>
        /// <param name="key">Key of the item to remove</param>
        /// <returns>The call context to allow fluent syntax</returns>
        public CallContext RemoveItem(string key)
        {
            var dictionary = this._executionTreeStorage.Value;

            if (dictionary != null)
            {
                this._executionTreeStorage.Value = dictionary.Remove(key);
            }

            return this;
        }

        /// <summary>
        /// Sets a value by key to the call context
        /// </summary>
        /// <param name="key">Key of the new item</param>
        /// <param name="value">Value of the new item</param>
        /// <returns>The call context to allow fluent syntax</returns>
        public CallContext SetItem(string key, object value)
        {
            this._executionTreeStorage.Value = (this._executionTreeStorage.Value ?? ImmutableDictionary<string, object>.Empty).SetItem(key, value);

            return this;
        }
    }
}