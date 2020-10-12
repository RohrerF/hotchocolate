// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Reflection;
// using HotChocolate.Configuration;
// using HotChocolate.Language;
// using HotChocolate.Types;
// using HotChocolate.Types.Descriptors;
//
// namespace HotChocolate.ApolloFederation
// {
//     public class ReferenceResolverSchemaInterceptor: SchemaInterceptor
//     {
//         public override void OnBeforeCreate(IDescriptorContext context, ISchemaBuilder schemaBuilder)
//         {
//             var entityResolvers = new Dictionary<string, Func<ObjectValueNode, object>>();
//
//             schemaBuilder.SetContextData(
//                 WellKnownContextData.EntityResolvers,
//                 entityResolvers
//             );
//         }
//
//         public override void OnAfterCreate(IDescriptorContext context, ISchema schema)
//         {
//             foreach (INamedType schemaType in schema.Types)
//             {
//                 var runtimeType = schemaType.ToRuntimeType();
//                 if (runtimeType.IsDefined(typeof(ReferenceResolverAttribute)))
//                 {
//                     Console.WriteLine("break");
//                 }
//                 else
//                 {
//                     var referenceResolverMethod = runtimeType.GetMethods()
//                         .FirstOrDefault(method => method.IsDefined(typeof(ReferenceResolverAttribute)));
//                     if (referenceResolverMethod != null)
//                     {
//
//                     }
//                 }
//             }
//
//             Console.WriteLine("break");
//         }
//     }
// }
