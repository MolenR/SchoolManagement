using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Caching.Memory;
using SchoolManagement.MVC.Data;
using SchoolManagement.MVC.DocumentModels;

namespace SchoolManagement.MVC.Controllers
{
    public class CosmosController : Controller
    {
        private readonly IMemoryCache _cache;
        private readonly CosmosClient _cosmosClient;
        private Container documentContainer;

        public CosmosController(IMemoryCache cache, CosmosClient cosmosClient)
        {
            _cache = cache;
            _cosmosClient = cosmosClient;
            documentContainer = cosmosClient.GetContainer("SchoolManagementDb", "Courses");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post(CourseCosmos course)
        {
            course.Id = Guid.NewGuid();
            await documentContainer.CreateItemAsync(course, new PartitionKey(course.Code));

            return NoContent();
        }

        // GET: Cosmos
        public ActionResult Index()
        {
            return View();
        }

        // GET: Cosmos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cosmos/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Cosmos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // GET: Cosmos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cosmos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: Cosmos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: Cosmos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
