using GridAndCloud.CoreModels.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridAndCloud.CoreModels
{
    public class Cluster
    {
        public Model Model { get; }
        public Element MassCenter { get; }
        public IEnumerable<Element> Elements { get; }
        public IDictionary<int, (ValueType upperBound, ValueType lowerBound)> Attributes 
        {
            get;
        }

        public Cluster(Model model, Element massCenter, IEnumerable<Element> clusterElements)
        {
            Model = model;
            MassCenter = massCenter;
            Elements = clusterElements;

            var dictionary = new Dictionary<int, (ValueType upperBound, ValueType lowerBound)>();

            foreach(var attributeId in Model.Attributes.Select(x => x.Key))
            {
                dictionary.Add(attributeId, Elements.GetElementsBounds(attributeId));
            }
        }

        public IEnumerable<ValueType> GetAttributeValues(int attributeId)
        {
            return Elements.Select(x => x.GetAttribute(attributeId));
        }
    }
}
