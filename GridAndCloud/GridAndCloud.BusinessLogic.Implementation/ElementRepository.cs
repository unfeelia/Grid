using GridAndCloud.CoreModels;
using GridAndCloud.CoreModels.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace GridAndCloud.BusinessLogic.Implementation
{
    public class ElementRepository : IElementRepository
    {
        public IEnumerable<Element> Get(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Element> Get(IEnumerable<int> ids, IEnumerable<Filter> filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Element> Get(IEnumerable<Filter> filters)
        {
            throw new NotImplementedException();
        }
    }
}
