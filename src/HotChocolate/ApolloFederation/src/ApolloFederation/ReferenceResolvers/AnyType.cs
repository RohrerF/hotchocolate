using System.Linq;
using HotChocolate.ApolloFederation.Properties;
using HotChocolate.Language;
using HotChocolate.Types;

namespace HotChocolate.ApolloFederation
{
    public sealed class AnyType: ScalarType<Representation, ObjectValueNode>
    {

        public AnyType()
            : this(WellKnownTypeNames.Any)
        {
        }

        protected override bool IsInstanceOfType(ObjectValueNode valueSyntax)
        {
            return valueSyntax.Fields.Any(field => field.Name.Value == "__typename");
        }

        public AnyType(NameString name, BindingBehavior bind = BindingBehavior.Explicit)
            : base(name, bind)
        {
            Description = FederationResources.FieldsetType_Description; // TODO
        }

        public override IValueNode ParseResult(object? resultValue)
        {
            throw new System.NotImplementedException();
        }

        protected override Representation ParseLiteral(ObjectValueNode valueSyntax)
        {
            if (valueSyntax.Fields.FirstOrDefault(field => field.Name.Value == "__typename") is {} typename && typename.Value is StringValueNode s)
            {
                return new Representation()
                {
                    Typename = s.Value,
                    Data = valueSyntax
                };
            }
            throw new System.NotImplementedException();
        }

        public override bool TrySerialize(object? runtimeValue, out object? resultValue)
        {
            throw new System.NotImplementedException();
        }

        protected override ObjectValueNode ParseValue(Representation runtimeValue)
        {
            throw new System.NotImplementedException();
            // return ObjectValueNode(runtimeValue);
        }
        public override bool TryDeserialize(object? resultValue, out object? runtimeValue)
        {
            throw new System.NotImplementedException();
        }
    }
}
