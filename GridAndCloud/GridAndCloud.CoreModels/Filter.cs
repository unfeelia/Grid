using System;
using System.Collections.Generic;
using System.Linq;

namespace GridAndCloud.CoreModels.Filters
{
    public class Filter
    {
        private int _attributeId;

        public int Id => _attributeId;

        public Filter(int attributeId)
        {
            _attributeId = attributeId;
        }

        public virtual bool IsValid(Element element)
        {
            var val = element.Attributes.FirstOrDefault(x => x.Item1 == Id);

            if(val.Item3 == null)
            {
                return false;
            }

            return val.Item2.IsValid(val.Item3);
        }
    }

    public sealed class CompareWithFilter : Filter
    {
        private ValueType _bound;
        private bool _inclusive;
        private bool _greaterThan;

        public CompareWithFilter(int attributeId, ValueType bound, bool greaterThan = true, bool inclusive = false) : base(attributeId)
        {
            _bound = bound;
            _inclusive = inclusive;
            _greaterThan = greaterThan;
        }

        public sealed override bool IsValid(Element element)
        {
            if(base.IsValid(element))
            {
                var val = element.Attributes.FirstOrDefault(x => x.Item1 == Id);

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

        public InBetweenFilter(int attributeId, 
            ValueType from, 
            ValueType to, 
            bool includeLowerBound = true, 
            bool includeUpperBound = true) : base(attributeId)
        {
            _includeLowerBound = includeLowerBound;
            _includeUpperBound = includeUpperBound;

            _from = new CompareWithFilter(attributeId, true, _includeLowerBound);
            _to = new CompareWithFilter(attributeId, false, _includeUpperBound);
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
        private IEnumerable<ValueType> _options;

        public OneOfFilter(int attributeId, IEnumerable<ValueType> options) : base(attributeId)
        {
            _options = options;
        }

        public sealed override bool IsValid(Element element)
        {
            if (base.IsValid(element))
            {
                var val = element.Attributes.FirstOrDefault(x => x.Item1 == Id);

                return _options.Contains(val.Item3);
            }
            else
            {
                return false;
            }
        }
    }
}
