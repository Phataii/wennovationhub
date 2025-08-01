﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using wennovation_hub.Data;
using wennovation_hub.Models;

namespace wennovation_hub.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserHelper _userHelper;
    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserHelper userHelper)
    {
        _logger = logger;
        _context = context;
        _userHelper = userHelper;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Route("contact")]
    public IActionResult Contact()
    {
        return View();
    }

    [Route("services")]
    public IActionResult Services()
    {
        return View();
    }

    [Route("impact")]
    public IActionResult Impact()
    {
        return View();
    }

    [Route("gallery")]
    public IActionResult Gallery()
    {
        return View();
    }

    [Route("our-works")]
    public IActionResult OurWork()
    {
        return View();
    }

    [Route("meet-our-team")]
    public IActionResult Team()
    {
        return View();
    }

    [Route("bookings")]
    public async Task <IActionResult> Booking()
    {
        var spaces = await _context.BookingRates.ToListAsync();
        return View(spaces);
    }

    [Route("become-a-wennovator")]
    public IActionResult Wennovator()
    {
        return View();
    }

    [Route("consult-with-us")]
    public IActionResult Consult()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [Route("success")]
    public IActionResult Success()
    {
        return View();
    }

     [Route("admins")]
    public async Task <IActionResult> Admins()
    {
        var loggedInUser = await _userHelper.GetLoggedInUser(Request);
        if (loggedInUser == null)
        {
            return Redirect("/login");
        }
        ViewData["admin"] = loggedInUser.Name;
        var admins = await _context.Admins.ToListAsync();
        return View(admins);
    }

    [Route("summary/{id}")]
    public async Task <IActionResult> Summary(string id)
    {
        var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.BookingId == id);

        var bookingVM = new BookingSummaryViewModel
        {
            Booking = booking,
        };

        var SpaceType = await _context.BookingRates.FirstOrDefaultAsync(x => x.Location == booking.Location && x.SpaceName == booking.SpaceType);
        ViewData["rate"] = SpaceType.Price;
        return View(bookingVM);
    }

    [Route("login")]
    public IActionResult Login()
    {
        return View();
    }

    [Route("hub")]
    public async Task <IActionResult> Register()
    {
        var loggedInUser = await _userHelper.GetLoggedInUser(Request);
        if (loggedInUser == null)
        {
            return Redirect("/login");
        }
        return View();
    }

    [Route("dashboard")]
    public async Task<IActionResult> Dashboard()
    {
        var loggedInUser = await _userHelper.GetLoggedInUser(Request);
        if (loggedInUser == null)
        {
            return Redirect("/login");
        }
        ViewData["admin"] = loggedInUser.Name;
        var bookings = await _context.Bookings.ToListAsync();
        var wennovators = await _context.Wennovators.ToListAsync();
        var consult = await _context.Consults.ToListAsync();

        var adminVM = new AdminDash
        {
            Bookings = bookings,
            Wennovators = wennovators,
            Consults = consult,
        };

        ViewData["bookingsCount"] = bookings.Count();
        ViewData["wennovatorsCount"] = wennovators.Count();
        ViewData["consultCount"] = consult.Count();

        return View(adminVM);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    ///////////// BUSINESS LOGIC ////////////////////////
    /// 
    /// 
    // public async Task<IActionResult> Bookings()
    // {

    // }

    [HttpPost]
    [Route("home/wennovator")]
    public async Task<IActionResult> Wennovator(WennovatorApplicationVM model)
    {
        try
        {
            // Map ViewModel to Domain Model (using AutoMapper or manually)
            var application = new Wennovator
            {
                ApplicationId = GenerateWennovatorId(),
                BusinessName = model.BusinessName,
                Industry = model.Industry,
                RegistrationNumber = model.RegistrationNumber,
                YearsOperation = model.YearsOperation,
                TeamSize = model.TeamSize,
                Founders = model.Founders,
                BusinessDescription = model.BusinessDescription,
                ValueProposition = model.ValueProposition,
                Headquarters = model.Headquarters,
                RevenueModel = model.RevenueModel,
                AnnualRevenue = model.AnnualRevenue,
                Profitability = model.Profitability.ToString(),
                ProfitMargin = model.ProfitMargin,
                FundingHistory = model.FundingHistory,
                FundingNeeds = model.FundingNeeds,
                Documents = model.Documents != null ? string.Join(",", model.Documents) : null,
                InvestmentChallenges = model.InvestmentChallenges,
                Trainings = model.Trainings != null ? string.Join(",", model.Trainings) : null,
                AdditionalInfo = model.AdditionalInfo,
                SubmissionDate = DateTime.UtcNow,
                // Status = "pending",
            };

            // TODO: Save to database
            // await _repository.SaveApplicationAsync(application);
            _context.Wennovators.Add(application);
            await _context.SaveChangesAsync();

            // TODO: Send email notification
            // await _emailService.SendApplicationConfirmation(application);

            return Redirect("/success");
        }
        catch (Exception ex)
        {
            // Log error
            // _logger.LogError(ex, "Error submitting Wennovator application");

            return StatusCode(500, new { success = false, message = "An error occurred while processing your application." });
        }
    }
    [HttpPost]
    [Route("Home/booking")]
    public async Task<IActionResult> Booking(Bookings model)
    {
        try
        {
            var booking = new Bookings
            {
                BookingId = GenerateBookingId(),
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Company = model.Company,
                Location = model.Location,
                SpaceType = model.SpaceType,
                Date = model.Date,
                Duration = model.Duration,
                Purpose = model.Purpose,
                NoOfPeople = model.NoOfPeople,
                CreatedAt = DateTime.Now,
                // Status = "pending",
            };
            var SpaceType = await _context.BookingRates.FirstOrDefaultAsync(x => x.Location == booking.Location && x.SpaceName == booking.SpaceType);
            if (SpaceType.Per == "person")
            {
                var amount = SpaceType.Price * booking.NoOfPeople;
                booking.Amount = amount;
            }
            else if (SpaceType.Per == "hour")
            {
                var amount = SpaceType.Price * booking.Duration;
                booking.Amount = amount;
            }
            else if (SpaceType.Per == "day")
            {
                var amount = SpaceType.Price * booking.Duration;
                booking.Amount = amount;
            }
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
           return Redirect($"/summary/{booking.BookingId}");
        }
        catch (Exception ex)
        {
            // Log error
            // _logger.LogError(ex, "Error submitting Wennovator application");

            return StatusCode(500, new { success = false, message = "An error occurred while processing your application." });
        }
    }
    [HttpPost()]
    [Route("home/login")]
    public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password)
    {

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            TempData["error"] = "Something went wrong";
            return Redirect("/login");
        }
        // Step 1: Find the user by email
        var user = await _context.Admins.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            TempData["error"] = "Invalid email or password.";
            return Redirect("/login");
        }

        // Step 2: Validate password (assuming it's hashed)
        // var passwordHasher = new Microsoft.AspNetCore.Identity.PasswordHasher<User>();
        bool isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);

        if (!isValid)
        {
            TempData["error"] = "Invalid email or password.";
            return Redirect("/login");
        }

        // Step 3: Generate JWT token
        var jwtHelper = new JwtHelper();
        var token = jwtHelper.GenerateJwtToken(user);

        // Step 4: Get roles
        // var userRoles = await _context.UserRoles
        //     .Where(ur => ur.UserId == user.Id)
        //     .Select(ur => ur.RoleName.RoleName) // Adjust this based on your navigation model
        //     .ToListAsync();

        // Step 5: Set cookies
        Response.Cookies.Append("jwt", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Expires = DateTime.UtcNow.AddHours(1)
        });

        return Redirect("/dashboard");
    }

    [HttpPost()]
    [Route("home/signup")]
    public async Task<IActionResult> Signup([FromForm] string name, [FromForm] string email, [FromForm] string password)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            TempData["error"] = "All fields are required.";
            return Redirect("/hub");
        }

        // if (password != confirmPassword)
        // {
        //     TempData["error"] = "Passwords do not match.";
        //     return Redirect("/hub");
        // }

        var existingUser = await _context.Admins.FirstOrDefaultAsync(u => u.Email == email);
        if (existingUser != null)
        {
            TempData["error"] = "Email already exists.";
            return Redirect("/hub");
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        var newUser = new Admins
        {
            Name = name,
            Email = email,
            Password = hashedPassword
        };

        _context.Admins.Add(newUser);
        await _context.SaveChangesAsync();

        TempData["success"] = "Signup successful. Please log in.";
        return Redirect("/dashboard");
    }


     [HttpPost]
    [Route("home/consult")]
    public async Task<IActionResult> Consult(Consult consult)
    {
        try
        {
            var consultData = new Consult
            {
                ConsultId = GenerateConsultId(),
                Name = consult.Name,
                Email = consult.Email,
                Phone = consult.Phone,
                Organization = consult.Organization,
                Service = consult.Service,
                Message = consult.Message,
                //  Status = "pending",
            };

            _context.Consults.Add(consultData);
            await _context.SaveChangesAsync();
            return Redirect("/success");
        }
        catch (Exception ex)
        {
            // Log error
            // _logger.LogError(ex, "Error submitting Wennovator application");

            return StatusCode(500, new { success = false, message = "An error occurred while processing your application." });
        }
    }
    [HttpGet]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        // Sign out the user
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        // Clear the authentication cookie manually (optional)
        Response.Cookies.Delete("jwt");
        // Response.Cookies.Delete("UserRole");

        // Redirect to login or home page
        return Redirect("/");
    }

    private string GenerateBookingId()
    {
        // Example: BK-7GHTKX-2407061032 (random + YYMMDDHHmm)
        var randomPart = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper(); // 6 random alphanumerics
        var timestamp = DateTime.UtcNow.ToString("yyMMddHHmm"); // e.g. 2407061032 for 6th July 2024, 10:32 AM UTC
        return $"BK-{randomPart}-{timestamp}";
    }

    private string GenerateWennovatorId()
    {
        // Example: BK-7GHTKX-2407061032 (random + YYMMDDHHmm)
        var randomPart = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper(); // 6 random alphanumerics
        var timestamp = DateTime.UtcNow.ToString("yyMMddHHmm"); // e.g. 2407061032 for 6th July 2024, 10:32 AM UTC
        return $"WT-{randomPart}-{timestamp}";
    }

    private string GenerateConsultId()
    {
        // Example: BK-7GHTKX-2407061032 (random + YYMMDDHHmm)
        var randomPart = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper(); // 6 random alphanumerics
        var timestamp = DateTime.UtcNow.ToString("yyMMddHHmm"); // e.g. 2407061032 for 6th July 2024, 10:32 AM UTC
        return $"CT-{randomPart}-{timestamp}";
    }

}
