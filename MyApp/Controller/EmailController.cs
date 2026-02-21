using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class EmailController(IEmailService service, UserManager<User> userManager) : ControllerBase
    {
          private readonly IEmailService _emailService = service;
        private readonly UserManager<User> _userManager = userManager;

        [Authorize(Policy = "SendEmailPolicy")]
        [HttpPost]
        public async Task<IActionResult> SendTestEmail()
        {
            await _emailService.SendAsync("sobirovazulhija091@gamil.com",
            "WELLCOME TO  INTERNET SHOP",
            "Test email from Myapp API");
            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("add-permission")]
        public async Task<IActionResult> AddPermissionToUser(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                await _userManager.AddClaimAsync(user,
                    new Claim("Permission", "SendEmail"));
            return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
