using System;
using System.Runtime.CompilerServices;

namespace FOSCBot.Common.Pipeline
{
    public class Watcher
    {
        private readonly ConditionalWeakTable<object, string> _table;

        public Watcher()
        {
            _table = new ConditionalWeakTable<object, string>();
        }

        public string Add(object key)
        {
            var identifier = Guid.NewGuid().ToString();
            
            _table.AddOrUpdate(key, identifier);

            return identifier;
        }
        
        public string Get(object key)
        {
            if (_table.TryGetValue(key, out var identifier))
            {
                return identifier;
            }

            return string.Empty;
        }
    }
}