using System.Security.Claims;
using api.Dtos.Account;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace api.Controller
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<Account> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<Account> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            IAccountRepository accountRepository,
            UserManager<Account> userManager,
            ITokenService tokenService,
            SignInManager<Account> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] QueryObjectAccount query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var accounts = await _accountRepository.GetAllAccountsAsync(query);
            var accountsDto = accounts.Select(account => account.ToAccountDto()).ToList();
            return Ok(accountsDto);
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            var account = await _accountRepository.GetByUsernameAsync(username);

            if (account == null)
                return NotFound("Tài khoản không tồn tại");

            var accountDto = account.ToAccountDto();
            accountDto.Role = await _accountRepository.GetUserRoleAsync(username);

            return Ok(accountDto);
        }

        [HttpGet("{username}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var account = await _accountRepository.GetByUsernameAsync(username);

            if (account == null)
                return NotFound($"Không tìm thấy tài khoản có username: {username}");

            var accountDto = account.ToAccountDto();
            accountDto.Role = await _accountRepository.GetUserRoleAsync(username);

            return Ok(accountDto);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateAccountDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (createDto.DateOfBirth >= DateTime.Now)
            {
                return BadRequest("Ngày sinh không hợp lệ (không thể nằm ở tương lai)");
            }

            if (await _userManager.FindByEmailAsync(createDto.Email) != null)
                return BadRequest("Email đã được sử dụng");

            var account = createDto.ToAccountFromRegister();

            string roleName = !string.IsNullOrEmpty(createDto.Role) ? createDto.Role : "Employee";

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                return BadRequest($"Role '{roleName}' không tồn tại trong hệ thống");
            }

            try
            {
                await _accountRepository.CreateAsync(account, createDto.PassWord);
                await _accountRepository.UpdateRoleAsync(account.UserName, roleName);

                var (accessToken, refreshToken) = await _tokenService.CreateTokens(account);

                return Ok(new NewAccountDto
                {
                    UserName = account.UserName,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var account = await _userManager.FindByEmailAsync(loginDto.Email);

            if (account == null)
                return Unauthorized("Email hoặc mật khẩu không chính xác");

            var result = await _signInManager.CheckPasswordSignInAsync(account, loginDto.Password, false);

            if (!result.Succeeded)
                return Unauthorized("Email hoặc mật khẩu không chính xác");

            account.LastLogin = DateTime.UtcNow;
            await _userManager.UpdateAsync(account);

            var (accessToken, refreshToken) = await _tokenService.CreateTokens(account);

            return Ok(new
            {
                UserName = account.UserName,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateAccountDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (updateDto.DateOfBirth >= DateTime.Now)
            {
                return BadRequest("Ngày sinh không hợp lệ (không thể nằm ở tương lai)");
            }

            var username = User.FindFirstValue(ClaimTypes.Name);
            var accountToUpdate = updateDto.ToAccountFromUpdate();

            try
            {
                var updatedAccount = await _accountRepository.UpdateAsync(username, accountToUpdate);

                if (updatedAccount == null)
                    return NotFound("Tài khoản không tồn tại");

                return Ok(new
                {
                    account = updatedAccount.ToAccountDto(),
                    token = await _tokenService.CreateTokens(updatedAccount)
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<IActionResult> DeleteAccount()
        {
            var username = User.FindFirstValue(ClaimTypes.Name);

            try
            {
                var deletedAccount = await _accountRepository.DeleteAsync(username);

                if (deletedAccount == null)
                    return NotFound("Tài khoản không tồn tại");

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("roles/{username}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole(string username, [FromBody] string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                return BadRequest("Role không được để trống");

            var success = await _accountRepository.UpdateRoleAsync(username, roleName);

            if (!success)
                return BadRequest($"Không thể cập nhật role '{roleName}' cho người dùng '{username}'");

            var updatedAccount = await _accountRepository.GetByUsernameAsync(username);
            if (updatedAccount == null)
                return NotFound("Tài khoản không tồn tại");

            return Ok(new
            {
                message = $"Đã cập nhật role thành '{roleName}' cho người dùng '{username}'",
                account = (await _accountRepository.GetByUsernameAsync(username)).ToAccountDto(),
                token = await _tokenService.CreateTokens(updatedAccount)
            });
        }
        [HttpPost("refresh")]
        [Authorize]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                var username = User.FindFirstValue(ClaimTypes.Name);
                var account = await _accountRepository.GetByUsernameAsync(username);

                if (account == null)
                    return Unauthorized("Invalid account");

                if (string.IsNullOrEmpty(account.RefreshToken))
                    return Unauthorized("No refresh token found");

                if (account.RefreshTokenExpiryTime <= DateTime.UtcNow)
                    return Unauthorized("Refresh token has expired. Please login again.");

                var newAccessToken = await _tokenService.CreateAccessToken(account);

                return Ok(new
                {
                    AccessToken = newAccessToken
                });
            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized("Invalid token");
            }
        }
    }
}