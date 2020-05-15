using GridAndCloud.CoreModels;
using GridAndCloud.CoreModels.Filters;
using System.Collections.Generic;

namespace GridAndCloud.BusinessLogic
{
    public interface IElementRepository
    {
        IEnumerable<Element> Get(IEnumerable<int> ids);
        IEnumerable<Element> Get(IEnumerable<int> ids, IEnumerable<Filter> filters);
        IEnumerable<Element> Get(IEnumerable<Filter> filters);
    }
}
