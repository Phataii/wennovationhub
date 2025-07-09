using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt; // For JWT handling
using System.Security.Claims;        // For Claims
using Microsoft.IdentityModel.Tokens; // For SymmetricSecurityKey and SigningCredentials
using Microsoft.EntityFrameworkCore;
using System.Text;                   // For Encoding
using wennovation_hub.Models;                    // Assuming your User class is in the "rps.Models" namespace
using wennovation_hub.Data;


public class UserHelper
{
    private readonly ApplicationDbContext _context;
    public UserHelper(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Admins> GetLoggedInUser(HttpRequest request)
    {
       //1. Get JWT token from cookie
        var token = request.Cookies["jwt"];
        if (string.IsNullOrEmpty(token))
        {
             return null; // Redirect to login if token is missing
        }

        // 2. Decode JWT and get user claims (e.g., email)
        var userClaims = JwtHelper.DecodeJwt(token); // Your custom JWT decoding logic
        var userEmail = userClaims["email"];

        // 3. Retrieve user from the database
        var user = await _context.Admins.FirstOrDefaultAsync(u => u.Email == userEmail);
      
        return user;
    }
}
