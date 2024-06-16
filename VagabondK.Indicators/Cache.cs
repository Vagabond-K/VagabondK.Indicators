using System;
using System.Collections.Generic;

namespace VagabondK.Indicators
{
    class Cache<TKey, TValue>
    {
        public Cache()
        {
            references = new Dictionary<TKey, ValueReference>();
        }

        private readonly Dictionary<TKey, ValueReference> references;

        class ValueReference
        {
            public int referenceCount = 0;
            public TValue value;
        }

        public TValue GetValue(TKey key, Func<TValue> defaultValue)
        {
            TValue result;
            lock (references)
            {
                if (references.TryGetValue(key, out var reference))
                {
                    reference.referenceCount++;
                    result = reference.value;
                }
                else
                {
                    result = defaultValue();
                    references[key] = new ValueReference
                    {
                        referenceCount = 1,
                        value = result
                    };
                }
            }
            return result;
        }

        public void Release(TKey key)
        {
            lock (references)
                if (references.TryGetValue(key, out var reference))
                {
                    reference.referenceCount--;
                    if (reference.referenceCount <= 0)
                        references.Remove(key);
                }
        }
    }
}
