using System;
using System.Collections.Generic;
using System.Text;

namespace HotChocolate.Types.Filters.Conventions
{
    public interface IFilterConventionDescriptor : IFluent
    {
        IFilterConventionDescriptor ArgumentName(NameString argumentName);

        IFilterConventionDescriptor ArrayFilterPropertyName(
            NameString arrayFilterPropertyName);

        IFilterConventionDescriptor GetFilterTypeName(
            GetFilterTypeName getFilterTypeName);

        IFilterConventionOperationDescriptor Filter(FilterOperationKind kind);
    }
}
