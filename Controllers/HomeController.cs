using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using reg_log.Models;

namespace reg_log.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index() => View();

    public IActionResult Register() => View();

    public IActionResult Login() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(UserLogin model)
    {
        if (ModelState.IsValid)
        {
            User user = null;
            using (UserContext db = new UserContext())
            {
                user = db.Users.FirstOrDefault(u => u.Email == model.Name && u.Password == model.Password);
            }
            if (user != null)
                return RedirectToAction("Index", "Home");
            else
                ModelState.AddModelError("", "user with such login or password not found");
        }
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(UserRegister model)
    {
        if (ModelState.IsValid)
        {
            User user = null;
            using (UserContext db = new UserContext())
            {
                user = db.Users.FirstOrDefault(u => u.Email == model.Name);
            }
            if (user == null) //create new user
            {
                using (UserContext db = new UserContext())
                {
                    db.Users.Add(new User { Email = model.Name, Password = model.Password });
                    db.SaveChanges();

                    user = db.Users.Where(u => u.Email == model.Name && u.Password == model.Password).FirstOrDefault();
                }
            }
            if (user != null) // user create success
                return RedirectToAction("Index", "Home");
            else
                ModelState.AddModelError("", "User with this login alredy exist");
        }
        return View(model);
    }
}
