using System;
using System.Collections.Generic;
using System.Text;

namespace GridAndCloud.CoreModels
{
    public class Distance
    {
        public Distance(double dist)
        {
            if(dist < 0.0 || dist > 1.0)
            {
                throw new ArgumentException(nameof(dist));
            }
            
            _value = dist;
        }

        public Distance()
        {
            _value = 1.0;
        }

        private double _value;

        public double Value
        {
            get => _value;
        }
    }
}
