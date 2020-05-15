using GridAndCloud.CoreModels.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace GridAndCloud.CoreModels
{
    public class Model
    {
        private IEnumerable<Attribute> _attributes;
        public IDictionary<int, Attribute> Attributes => _attributes.ToDictionary(x => x.Id);

        public Model(IEnumerable<Attribute> attributes)
        {
            _attributes = attributes;
        }
    }
}
