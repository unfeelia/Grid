using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GridAndCloud.Web.Models;
using GridAndCloud.BusinessLogic;
using GridAndCloud.CoreModels;

namespace GridAndCloud.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IElementRepository _repository;
        private readonly ILogic _logic;
        private readonly IClassterisater _classterisater;

        public HomeController(ILogger<HomeController> logger,
            IElementRepository repository,
            IClassterisater classterisater,
            ILogic logic)
        {
            _logger = logger;
            _repository = repository;
            _classterisater = classterisater;
            _logic = logic;
        }

        public IActionResult Index()
        {
            return View(_logic.GetFilters().Select(x => x.GetModel()).ToList());
        }

        [HttpPost]
        public IActionResult ClustersByFilters(FilterModel[] Filters)
        {
            var realFilter = Filters.Select(x => x.GetFilter(GetModel().Attributes.Values.ToArray()));

            var items = _repository.Get(realFilter);

            var clusters = _classterisater.ClasteriseElements(items, 4);

            return View("Clusters", clusters);
        }

        public IActionResult ClustersByIds(int[] ids)
        {
            var items = _repository.Get(ids);

            var clusters = _classterisater.ClasteriseElements(items, 4);

            return View("Clusters", clusters);
        }

        private Model GetModel()
        {
            return _logic.GetElementModel();
        }
        private IEnumerable<FilterModel> GetFilters()
        {
            return _logic.GetFilters().Select(x => x.GetModel());
        }

    }
}