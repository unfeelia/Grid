using GridAndCloud.CoreModels;
using GridAndCloud.CoreModels.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace GridAndCloud.BusinessLogic
{
    public interface IClassterisater
    {
        IEnumerable<Cluster> ClasteriseElements(IEnumerable<Element> elements, int clustersNumber);
    }
}
