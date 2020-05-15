using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace GridAndCloud.CoreModels.Extensions
{
    public static class Extensions
    {
        public static double GetDistance(Element a, Element b)
        {
            if (!Element.IsTheSameField(a, b))
            {
                var distance = a.Attributes.
                    Join(b.Attributes,
                    x => x.Item1,
                    y => y.Item1,
                    (x, y) => new { aValue = x.Item3, bValue = y.Item3, comparable = x.Item2.GetMaxDistance, attribute = x.Item2 }).
                    Select(x => x.attribute.Distance(x.aValue, x.bValue).Value / x.attribute.GetMaxDistance).
                    Sum(x => Math.Pow(x, 2));
                distance = Math.Sqrt(distance);
                return distance;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        public static Element GetMassCenter(IEnumerable<Element> elements)
        {
            Model model = elements.First().Model;

            Element massCenter = new Element(elements.First().Model);

            foreach(var attribute in model.Attributes)
            {
                var sortedValues = elements.Select(x => x.Attributes
                                      .Where(attr => attr.Item1 == attribute.Key)
                                      .First().Item3)
                        .OrderBy(z => attribute.Value.Magnitude(z).Value)
                        .ToArray();

                massCenter.SetAttribute(attribute.Key, sortedValues[(int)(sortedValues.Length / 2)]);
            }

            return massCenter;
        }

        public static bool IsDifferent(this Element comparable, Element comparer)
        {
            if(comparable == null || comparer == null || !Element.IsTheSameField(comparable, comparer))
            {
                throw new ArgumentException();
            }

            foreach(var attribute in comparable.Attributes)
            {
                var comparableAttributeValue = comparer.Attributes
                                                  .Where(x => x.Item1 == attribute.Item1)
                                                  .Select(x => x.Item2);
                if(!comparableAttributeValue.Equals(attribute.Item3))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
