using GridAndCloud.CoreModels;
using GridAndCloud.CoreModels.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace GridAndCloud.BusinessLogic.Implementation
{
    public class Classterisater : IClassterisater
    {
        public Classterisater()
        {

        }

        public IEnumerable<Cluster> ClasteriseElements(IEnumerable<Element> elements, int clustersNumber)
        {
            List<Element> massCenters = new List<Element>();

            Dictionary<Element, List<Element>> clusters = new Dictionary<Element, List<Element>>();

            var el = elements.ToArray();

            for (int i = 0; i < el.Length && i < clustersNumber; ++i)
            {
                var massCenter = el[i].DeepClone();
                massCenters.Add(massCenter);
                clusters.Add(massCenter, new List<Element>());
            }

            int iterations = 0;
            int changedMassCentersCount;

            do
            {
                foreach (var element in elements)
                {
                    var closestMassCenter = massCenters[0];
                    var distance = Extensions.GetDistance(element, closestMassCenter);

                    foreach (var massCenter in massCenters)
                    {
                        var dist = Extensions.GetDistance(element, massCenter);

                        if (dist < distance)
                        {
                            closestMassCenter = massCenter;
                            distance = dist;
                        }
                    }

                    clusters[closestMassCenter].Add(element);
                }

                changedMassCentersCount = 0;

                foreach (var cluster in clusters)
                {
                    var newMassCenter = Extensions.GetMassCenter(cluster.Value);

                    if (newMassCenter.IsDifferent(cluster.Key))
                    {
                        changedMassCentersCount++;

                        foreach (var attribute in newMassCenter.Attributes)
                        {
                            cluster.Key.SetAttribute(attribute.Item1, attribute.Item3);
                        }
                    }
                }

                if (changedMassCentersCount > 0)
                {

                }

                iterations++;
            }
            while (changedMassCentersCount != 0 || iterations < 50);

            var cl = new List<Cluster>();

            foreach (var c in clusters)
            {
                cl.Add(new Cluster(c.Key.Model, c.Key, c.Value));
            }

            return cl;
        }
    }
}
