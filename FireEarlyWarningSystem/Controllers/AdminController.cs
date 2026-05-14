using FireEarlyWarningSystem.Infrastructure.Domain.Resources.Camera;
using FireEarlyWarningSystem.Infrastructure.Domain.Resources.Admin;
using FireEarlyWarningSystem.Infrastructure.Services.Admin;
using FireEarlyWarningSystem.Infrastructure.Services.Cameras;
using FireEarlyWarningSystem.Infrastructure.Services.Users;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FireEarlyWarningSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public ICameraService _cameraService { get; set; }
        public IUserService _userService { get; set; }
        public IAdminService _adminService { get; set; }

        public AdminController(ICameraService cameraService, IUserService userService, IAdminService adminService)
        {
            _cameraService = cameraService;
            _userService = userService;
            _adminService = adminService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] AdminLoginViewModel adlogin)
        {
            var token = await _adminService.Login(adlogin);
            return new OkObjectResult(token);
        }

        [HttpPost]
        [Route("CreateNewCamera")]
        public async Task<IActionResult> CreateNewCamera([FromBody] AddCameraViewModel addCamera)
        {
            try
            {
                var result = await _cameraService.CreateNewCamera(addCamera);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("DeleteCamera")]
        public async Task<IActionResult> DeleteCamera([FromQuery] string CameraId)
        {
            try
            {
                var result = await _cameraService.DeleteCamera(CameraId);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("DeleteUserById")]
        public async Task<IActionResult> DeleteUserById([FromQuery] string userId)
        {
            var delete = await _userService.DeleteUserById(userId);
            return new OkObjectResult("User deleted successfully.");
        }
    }
}
