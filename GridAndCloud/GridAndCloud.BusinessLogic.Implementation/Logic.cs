using GridAndCloud.CoreModels;
using GridAndCloud.CoreModels.Filters;
using System;
using System.Collections.Generic;
using Attribute = GridAndCloud.CoreModels.Attributes.Attribute;

namespace GridAndCloud.BusinessLogic.Implementation
{
    public class Logic : ILogic
    {
        public Model GetElementModel()
        {
            return new Model(GetAttributes());
        }

        public IEnumerable<Attribute> GetAttributes()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Filter> GetFilters()
        {
            return new Filter[] 
            { 
                new Filter(new Attribute(1, new int[]{ 1, 2, 4, 5, 9, 11 }, (x, y) => x - y, 10, 0, (x) => x.ToString())),
                new OneOfFilter(new Attribute(2, new int[]{ 1, 2, 4, 5, 9, 11 }, (x, y) => x - y, 10, 0, (x) => x.ToString()), new int[] { 1, 2, 123 }),
                new CompareWithFilter(new Attribute(3, new int[]{ 1, 2, 4, 5, 9, 11 }, (x, y) => x - y, 10, 0, (x) => x.ToString()), 3),
                new InBetweenFilter(new Attribute(4, new int[]{ 1, 2, 4, 5, 9, 11 }, (x, y) => x - y, 10, 0, (x) => x.ToString()), 3, 6)
            };
        }
    }
}
