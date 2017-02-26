using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace iStore.Controllers
{
    /**
        Provides the Home page and the Inventory Page.
     */
    public class HomeController : Controller
    {
        //injected configuration
        private readonly StoreConfig _config;
        private readonly StoreContext _context;

        public HomeController(IOptionsSnapshot<StoreConfig> config, StoreContext context) {
            _config = config.Value;
            _context = context;
        }

        // shows using the view data
        private void AddDefaultViewData() {
            ViewData.Add("StoreName", _config.StoreName);
        }



        // main index page
        public IActionResult Index()
        {
            AddDefaultViewData();
            return View();
        }

        // Inventory shows a list of the details of the items in the store
        public IActionResult Inventory()
        {
            AddDefaultViewData();
            return View(_context.StoreItems.ToList()); // pass the list of store items from the DbContext
        }

        // Render Error page
        public IActionResult Error()
        {
            return View();
        }
    }
}
