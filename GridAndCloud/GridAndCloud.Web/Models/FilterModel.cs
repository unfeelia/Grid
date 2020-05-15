using GridAndCloud.CoreModels.Filters;
using System;
using System.Collections.Generic;

namespace GridAndCloud.Web.Models
{
    public enum FilterType { Filter, CompareWithFilter, InBetweenFilter, OneOfFilter }

    public class FilterModel
    {
        public int AttributeId { get; set; }

        public virtual Filter GetFilter()
        {
            return new Filter(AttributeId);
        }
    }

    public sealed class CompareWithFilterModel : FilterModel
    {
        public ValueType Bound { get; set; }
        public bool Inclusive { get; set; }
        public bool GreaterThan { get; set; }

        public override Filter GetFilter()
        {
            return new CompareWithFilter(AttributeId, Bound, GreaterThan, Inclusive);
        }
    }

    public sealed class InBetweenFilterModel : FilterModel
    {
        public ValueType LowerBound { get; set; }
        public ValueType UpperBound { get; set; }
        public bool IncludeLowerBound { get; set; }
        public bool IncludeUpperBound { get; set; }

        public override Filter GetFilter()
        {
            return new InBetweenFilter(AttributeId, LowerBound, UpperBound, IncludeLowerBound, IncludeUpperBound);
        }
    }

    public sealed class OneOfFilterModel : FilterModel
    {
        public IEnumerable<ValueType> Options { get; set; }

        public override Filter GetFilter()
        {
            return new OneOfFilter(AttributeId, Options);
        }
    }
}
