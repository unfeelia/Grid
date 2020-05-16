using GridAndCloud.CoreModels;
using GridAndCloud.CoreModels.Filters;
using System.Collections.Generic;

namespace GridAndCloud.BusinessLogic
{
    public interface ILogic
    {
        Model GetElementModel();
        IEnumerable<Filter> GetFilters();
    }
}
