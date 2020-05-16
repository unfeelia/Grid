using System;
using System.Collections.Generic;
using System.Linq;

using GridAndCloud.CoreModels.Enums;

namespace GridAndCloud.CoreModels.Attributes
{
    [Serializable]
    public class Attribute
    {
        private protected int _id;
        public IEnumerable<int> Options { get; private set; }
        
        public string Description { get; set; }
        public string Name { get; set; }

        public int Id { get => _id; }
        public virtual double GetMaxDistance { get => Math.Abs(Comparator(UpperBound, LowerBound)); }

        public Func<int, int, double> Comparator { get; private set; }
        public int UpperBound { get; private set; }
        public int LowerBound { get; private set; }

        public Func<int, string> ValueDescriptor { get; private set; }

        public Attribute(int id,
            IEnumerable<int> options,
            Func<int, int, double> Comparator,
            int upperBound,
            int lowerBound,
            Func<int, string> ValueDescriptor)
        {
            _id = id;
            this.Comparator = Comparator;
            UpperBound = upperBound;
            LowerBound = lowerBound;
            Options = options;
            this.ValueDescriptor = ValueDescriptor;

            if (Comparator(upperBound, lowerBound) < 0.0)
            {
                throw new ArgumentException(nameof(upperBound) + " " + nameof(lowerBound));
            }
        }

        public virtual Distance Distance(int a, int b)
        {
            return new Distance(Math.Abs(Comparator(a, b) / GetMaxDistance));
        }
        
        public virtual Distance Magnitude(int obj)
        {
            return new Distance(Comparator(obj, LowerBound) / GetMaxDistance);
        }

        public virtual bool IsValid(int obj)
        {
            return Options.Contains(obj)
                && Comparator(obj, LowerBound) >= 0.0
                && Comparator(obj, UpperBound) <= 0.0;
        }
    }
}
