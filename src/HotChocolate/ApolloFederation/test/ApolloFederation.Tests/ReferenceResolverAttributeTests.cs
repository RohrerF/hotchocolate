using System;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Language;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Snapshooter.Xunit;
using Xunit;

namespace HotChocolate.ApolloFederation
{
    public class ReferenceResolverAttributeTests
    {
        [Fact]
        public async void InClassReferenceResolverPureCodeFirst()
        {
            // arrange
            Snapshot.FullName();

            ISchema schema = SchemaBuilder.New()
                .AddApolloFederation()
                .AddQueryType<Query>()
                .Create();

            // act
            ObjectType inClassResolver = schema.GetType<ObjectType>("InClassReferenceResolver");

            // assert
            var inClassResolverContextObject =
                inClassResolver.ContextData[WellKnownContextData.EntityResolver];
            Assert.NotNull(inClassResolverContextObject);
            var inClassResolverDelegate =
                Assert.IsType<FieldResolverDelegate>(inClassResolverContextObject);
            var context = new MockResolverContext(schema);

            context.SetLocalValue("data", new ObjectValueNode());
            var result = await inClassResolverDelegate.Invoke(context);
            Assert.Equal("1",
                Assert.IsType<InClassReferenceResolver>(result).Id);
        }

        [Fact]
        public async void ExternalClassReferenceResolverPureCodeFirst()
        {
            // arrange
            Snapshot.FullName();

            ISchema schema = SchemaBuilder.New()
                .AddApolloFederation()
                .AddQueryType<Query>()
                .Create();

            // act
            ObjectType externalClassReesolver =
                schema.GetType<ObjectType>("ClassWithExternalReferenceResolver");

            // assert
            var inClassResolverContextObject =
                externalClassReesolver.ContextData[WellKnownContextData.EntityResolver];
            Assert.NotNull(inClassResolverContextObject);
            var inClassResolverDelegate =
                Assert.IsType<FieldResolverDelegate>(inClassResolverContextObject);
            var context = new MockResolverContext(schema);

            context.SetLocalValue("data", new ObjectValueNode());
            var result = await inClassResolverDelegate.Invoke(context);
            Assert.Equal("2",
                Assert.IsType<ClassWithExternalReferenceResolver>(result).Id);
        }

        public class Query
        {
            public InClassReferenceResolver InClassReferenceResolver { get; set; } =
                new InClassReferenceResolver();
            public ClassWithExternalReferenceResolver ClassWithExternalReferenceResolver { get; set; }
                = new ClassWithExternalReferenceResolver();
        }


        [ReferenceResolver(EntityResolver = nameof(GetAsync))]
        public class InClassReferenceResolver
        {
            [Key]
            public string Id { get; set; }

            public async Task<InClassReferenceResolver> GetAsync([LocalState] ObjectValueNode data)
            {
                return new InClassReferenceResolver(){Id = "1"};
            }
        }

        [ReferenceResolver(EntityResolverType = typeof(ExternalReferenceResolver))]
        public class ClassWithExternalReferenceResolver
        {
            [Key]
            public string Id { get; set; }
        }

        public static class ExternalReferenceResolver
        {
            public static async Task<ClassWithExternalReferenceResolver> GetExternalReferenceResolverAsync(
                [LocalState] ObjectValueNode data)
            {
                return new ClassWithExternalReferenceResolver(){Id = "2"};
            }
        }

    }
}
