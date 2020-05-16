using GridAndCloud.CoreModels.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using Attribute = GridAndCloud.CoreModels.Attributes.Attribute;

namespace GridAndCloud.Web.Models
{
    public enum FilterType { Filter, CompareWithFilter, InBetweenFilter, OneOfFilter }

    public sealed class FilterModel
    {
        public Attribute Attribute { get; set; }

        public int AttributeId { get; set; }

        public FilterType FilterType
        {
            get
            {
                if (Options != null)
                {
                    return FilterType.OneOfFilter;
                }
                else if (LowerBound != null && UpperBound != null)
                {
                    return FilterType.InBetweenFilter;
                }
                else if (Bound != null)
                {
                    return FilterType.CompareWithFilter;
                }
                else
                {
                    return FilterType.Filter;
                }
            }
        }

        public int[] Options { get; set; }

        public int? LowerBound { get; set; }
        public int? UpperBound { get; set; }
        public bool IncludeLowerBound { get; set; }
        public bool IncludeUpperBound { get; set; }

        public int? Bound { get; set; }
        public bool Inclusive { get; set; }
        public bool GreaterThan { get; set; }

        public Filter GetFilter(Attribute[] attributes)
        {
            var attr = attributes.Where(x => x.Id == AttributeId).First();
            switch (FilterType)
            {
                case FilterType.Filter:
                    {
                        return new Filter(attr);
                    }
                case FilterType.CompareWithFilter:
                    {
                        return new CompareWithFilter(attr, Bound.Value, GreaterThan, Inclusive);
                    }
                case FilterType.InBetweenFilter:
                    {
                        return new InBetweenFilter(attr, LowerBound.Value, UpperBound.Value, IncludeLowerBound, IncludeUpperBound);
                    }
                case FilterType.OneOfFilter:
                    {
                        return new OneOfFilter(attr, Options);
                    }
                default:
                    {
                        return new Filter(attr);
                    }
            }
        }
    }

    public static class Extensions
    {
        public static FilterModel GetModel(this Filter filter)
        {
            if (filter is CompareWithFilter compareFilter)
            {
                return new FilterModel()
                {
                    Attribute = filter.Attribute,
                    Bound = (int)compareFilter.Bound,
                    AttributeId = compareFilter.AttributeId,
                    GreaterThan = compareFilter.GreaterThan,
                    Inclusive = compareFilter.Inclusive
                };
            }
            else if (filter is InBetweenFilter inBetweenFilter)
            {
                return new FilterModel()
                {
                    Attribute = filter.Attribute,
                    AttributeId = inBetweenFilter.AttributeId,
                    IncludeLowerBound = inBetweenFilter.IncludeLowerBound,
                    IncludeUpperBound = inBetweenFilter.IncludeUpperBound,
                    LowerBound = (int)inBetweenFilter.From,
                    UpperBound = (int)inBetweenFilter.To
                };
            }
            else if (filter is OneOfFilter oneOfFilter)
            {
                return new FilterModel()
                {
                    Attribute = filter.Attribute,
                    AttributeId = oneOfFilter.AttributeId,
                    Options = oneOfFilter.Options.ToArray()
                };
            }
            else
            {
                return new FilterModel()
                {
                    Attribute = filter.Attribute,
                    AttributeId = filter.AttributeId
                };
            }
        }
    }
}
