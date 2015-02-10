using KippAccounting.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KippAcctCodeSearch.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountingQueryHandler _query;

        public HomeController(IAccountingQueryHandler query)
        {
            _query = query;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var indexViewModel = BuildHomeIndexViewModel(null);

            return View(indexViewModel);
        }

        [HttpPost]
        public ActionResult Index(string purchase, int? organizationCode, int? fundId, int? schoolYear)
        {
            var results = _query.SearchBy(purchase, organizationCode, fundId, schoolYear);
            var indexViewModel = BuildHomeIndexViewModel(results);

            return View(indexViewModel);
        }

        private HomeIndexViewModel BuildHomeIndexViewModel(IEnumerable<AccountingCodeViewModel> accountingCodes)
        {
            var funds = _query.GetAllFunds();
            var organizations = _query.GetAllOrganizations();
            var schoolYears = _query.GetAllSchoolYears();

            ModelState.Clear();

            return new HomeIndexViewModel
            {
                Funds = funds.Select(x => new SelectListItem { Text = x.FundName, Value = x.FundId.ToString(), Selected = false }),
                Organizations = organizations.Select(x => new SelectListItem { Text = x.OrganizationName, Value = x.OrganizationCode.ToString(), Selected = false }),
                SchoolYears = schoolYears.Select(x => new SelectListItem { Text = x.Year, Value = x.Fy.ToString(), Selected = false }),
                AccountingCodes = accountingCodes
            };
        }
    }

    public class HomeIndexViewModel
    {
        public IEnumerable<SelectListItem> Funds { get; set; }
        public IEnumerable<SelectListItem> Organizations { get; set; }
        public IEnumerable<SelectListItem> SchoolYears { get; set; }
        public IEnumerable<AccountingCodeViewModel> AccountingCodes { get; set; }
    }
}