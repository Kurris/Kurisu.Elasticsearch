using System.Collections.Generic;
using System.Dynamic;

namespace Kurisu.Elasticsearch.Abstractions
{
    /// <summary>
    /// Elasticsearch DSL body item基类 
    /// </summary>
    public abstract class BaseElasticsearchItem : DynamicObject
    {
        protected BaseElasticsearchItem(Dictionary<string, object> properties)
        {
            Properties = properties;
        }

        protected Dictionary<string, object> Properties { get; }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return Properties.Keys;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (Properties.ContainsKey(binder.Name))
            {
                result = Properties[binder.Name];
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (Properties.ContainsKey(binder.Name))
            {
                Properties[binder.Name] = value;
                return true;
            }

            return false;
        }
    }
}