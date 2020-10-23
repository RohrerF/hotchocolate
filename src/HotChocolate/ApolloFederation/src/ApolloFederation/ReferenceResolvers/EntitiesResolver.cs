using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HotChocolate.Language;
using HotChocolate.Resolvers;
using HotChocolate.Types;

namespace HotChocolate.ApolloFederation
{
    public static class EntitiesResolver
    {
        public static async Task<List<object>> _Entities(ISchema schema, IReadOnlyList<Representation> representations, IResolverContext c)
        {
            var ret = new List<object>();
            foreach (var representation in representations)
            {
                var representationType = schema.Types.SingleOrDefault(type => type.Name == representation.Typename && type.ContextData.ContainsKey(WellKnownContextData.ExtendMarker));
                if (representationType != null)
                {
                    // GetFactory from contextdata
                    // var obj = invoke with representation
                    // ret.add(obj)
                    var runtimeType = representationType.ToRuntimeType();
                    var obj = Activator.CreateInstance(runtimeType);
                    foreach (ObjectFieldNode objectFieldNode in representation.Data.Fields)
                    {
                        var nameValue = objectFieldNode.Name.Value;
                        var propName = nameValue.First().ToString().ToUpper() + nameValue.Substring(1);
                        PropertyInfo? prop = runtimeType.GetProperty(propName);
                        prop?.SetValue (obj, objectFieldNode.Value.Value, null);
                    }
                    ret.Add(obj);
                }
                else
                {
                    if (schema.TryGetType(representation.Typename, out ObjectType type) &&
                        type.ContextData.TryGetValue(WellKnownContextData.EntityResolver, out object? o) &&
                        o is FieldResolverDelegate resolver)
                    {
                        c.SetLocalValue("data", representation.Data);
                        ret.Add(await resolver.Invoke(c));
                    }
                }
            }

            return ret;
        }
    }
}
