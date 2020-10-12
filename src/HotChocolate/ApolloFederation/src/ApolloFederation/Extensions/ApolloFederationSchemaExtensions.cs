using System;
using System.Collections.Generic;
using HotChocolate.ApolloFederation;
using HotChocolate.Language;

namespace HotChocolate
{
    public static class ApolloFederationSchemaExtensions
    {
        public static Func<ObjectValueNode, object> GetReferenceResolver(
            this ISchema schema,
            string resolvedTypeName)
        {
            var referenceResolvers = schema
                .ContextData[ApolloFederation.WellKnownContextData.EntityResolvers];
            if (referenceResolvers is
                Dictionary<string, Func<ObjectValueNode, object>> rrs)
            {
                return rrs[resolvedTypeName];
            }

            throw ThrowHelper.EntityResolver_NoResolverFound();
        }
    }
}
