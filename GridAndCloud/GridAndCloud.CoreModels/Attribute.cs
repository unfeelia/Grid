using System;
using System.Collections.Generic;
using System.Linq;

using GridAndCloud.CoreModels.Enums;

namespace GridAndCloud.CoreModels.Attributes
{
    [Serializable]
    public abstract class Attribute
    {
        private protected int _id;
        public IEnumerable<ValueType> Options { get; private set; }
        
        public string Description { get; set; }
        public string Name { get; set; }

        public int Id { get => _id; }
        public Type AttributeType { get; private set; }
        public virtual double GetMaxDistance { get => Math.Abs(Comparator(UpperBound, LowerBound)); }

        public Func<ValueType, ValueType, double> Comparator { get; private set; }
        public ValueType UpperBound { get; private set; }
        public ValueType LowerBound { get; private set; }

        public Attribute(int id,
            Type attributeType,
            IEnumerable<ValueType> options,
            Func<ValueType, ValueType, double> Comparator,
            ValueType upperBound,
            ValueType lowerBound)
        {
            _id = id;
            AttributeType = attributeType;
            this.Comparator = Comparator;
            UpperBound = upperBound;
            LowerBound = lowerBound;
            Options = options;

            if (Comparator(upperBound, lowerBound) < 0.0)
            {
                throw new ArgumentException(nameof(upperBound) + " " + nameof(lowerBound));
            }
        }

        public virtual Distance Distance(ValueType a, ValueType b)
        {
            return new Distance(Math.Abs(Comparator(a, b) / GetMaxDistance));
        }
        
        public virtual Distance Magnitude(ValueType obj)
        {
            return new Distance(Comparator(obj, LowerBound) / GetMaxDistance);
        }

        public virtual bool IsValid(ValueType obj)
        {
            return Options.Contains(obj)
                && Comparator(obj, LowerBound) >= 0.0
                && Comparator(obj, UpperBound) <= 0.0;
        }
    }

    [Serializable]
    public class Attribute<T> : Attribute
        where T : struct
    {
        public Attribute(int id,
            IEnumerable<T> options,
            Func<T, T, double> comparator,
            T upperBound,
            T lowerBound) : base(id,
                typeof(T),
                options.Select(x => (ValueType)x),
                (x, y) =>
                {
                    if (x is T && y is T)
                    {
                        return comparator((T)x, (T)y);
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                },
                upperBound,
                lowerBound)
        {

        }
    }

    [Serializable]
    public sealed class NumberAttribute : Attribute<int>
    {
        public NumberAttribute(int id, 
            IEnumerable<int> options, 
            Func<int, int, double> comparator, 
            int upperBound, int lowerBound) : base(id, options, comparator, upperBound, lowerBound)
        {
        }
    }
}
