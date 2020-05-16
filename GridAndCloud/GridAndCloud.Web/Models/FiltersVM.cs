using GridAndCloud.CoreModels.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GridAndCloud.Web.Models
{
    public class FiltersVM
    {
        public List<FilterModel> Filters { set; get; }

        public FiltersVM()
        {
            Filters = new List<FilterModel>();
        }
    }
}
