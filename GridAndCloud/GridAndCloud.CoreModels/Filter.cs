using System;
using System.Collections.Generic;
using System.Linq;
using Attribute = GridAndCloud.CoreModels.Attributes.Attribute;

namespace GridAndCloud.CoreModels.Filters
{
    public class Filter
    {
        public int AttributeId => Attribute.Id;

        public Attribute Attribute { get; }

        public Filter(Attribute attribute)
        {
            Attribute = attribute;
        }

        public virtual bool IsValid(Element element)
        {
            var val = element.Attributes.FirstOrDefault(x => x.Item1 == AttributeId);

            return val.Item2.IsValid(val.Item3);
        }
    }

    public sealed class CompareWithFilter : Filter
    {
        private int _bound;
        private bool _inclusive;
        private bool _greaterThan;

        public int Bound { get => _bound; }
        public bool Inclusive { get => _inclusive; }
        public bool GreaterThan { get => _greaterThan; }

        public CompareWithFilter(Attribute attributeId, int bound, bool greaterThan = true, bool inclusive = false) : base(attributeId)
        {
            _bound = bound;
            _inclusive = inclusive;
            _greaterThan = greaterThan;
        }

        public sealed override bool IsValid(Element element)
        {
            if(base.IsValid(element))
            {
                var val = element.Attributes.FirstOrDefault(x => x.Item1 == AttributeId);

                var elementMagnitude = val.Item2.Magnitude(val.Item3).Value - val.Item2.Magnitude(_bound).Value;

                return elementMagnitude > 0 && _greaterThan || elementMagnitude < 0 && !_greaterThan ? 
                    true : (_inclusive && elementMagnitude == 0);
            }
            else
            {
                return false;
            }
        }
    }

    public sealed class InBetweenFilter : Filter
    {
        private CompareWithFilter _from;
        private CompareWithFilter _to;
        private bool _includeLowerBound;
        private bool _includeUpperBound;

        public int From { get => _from.Bound; }
        public int To { get => _to.Bound; }
        public bool IncludeLowerBound { get => _includeLowerBound; }
        public bool IncludeUpperBound { get => _includeUpperBound; }

        public InBetweenFilter(Attribute attributeId,
            int from,
            int to, 
            bool includeLowerBound = true, 
            bool includeUpperBound = true) : base(attributeId)
        {
            _includeLowerBound = includeLowerBound;
            _includeUpperBound = includeUpperBound;

            _from = new CompareWithFilter(attributeId, from, _includeLowerBound);
            _to = new CompareWithFilter(attributeId, to, _includeUpperBound);
        }

        public sealed override bool IsValid(Element element)
        {
            if (base.IsValid(element))
            {
                return _from.IsValid(element) && _to.IsValid(element);
            }
            else
            {
                return false;
            }
        }
    }

    public sealed class OneOfFilter : Filter
    {
        private IEnumerable<int> _options;

        public IEnumerable<int> Options { get => _options; }

        public OneOfFilter(Attribute attributeId, IEnumerable<int> options) : base(attributeId)
        {
            _options = options;
        }

        public sealed override bool IsValid(Element element)
        {
            if (base.IsValid(element))
            {
                var val = element.Attributes.FirstOrDefault(x => x.Item1 == AttributeId);

                return _options.Contains(val.Item3);
            }
            else
            {
                return false;
            }
        }
    }
}
