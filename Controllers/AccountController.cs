using Microsoft.AspNetCore.Mvc;
using RailwayTicketSystem.Models;

public class AccountController : Controller
{
    private readonly RailwayDbContext _context;

    public AccountController(RailwayDbContext context)
    {
        _context = context;
    }

    // GET: /Account/Signup
    public IActionResult Signup()
    {
        return View();
    }

    // POST: /Account/Signup
    [HttpPost]
    public IActionResult Signup(User user)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            TempData["Success"] = "Signup successful. Please login.";
            return RedirectToAction("Login");
        }
        return View(user);
    }

    // GET: /Account/Login
    public IActionResult Login()
    {
        return View();
    }

    // POST: /Account/Login
    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        if (user != null)
        {
            HttpContext.Session.SetInt32("UserId", user.Id);
            return RedirectToAction("Create", "Booking");
        }

        ViewBag.Error = "Invalid credentials.";
        return View();
    }

    // GET: /Account/Logout
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
