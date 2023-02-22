using Microsoft.AspNetCore.Mvc;
using Project.AndroidIosApp.Business.Abstract.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _contactService.GetAllAsync();
            return View(response.Data);
        }
    }
}
