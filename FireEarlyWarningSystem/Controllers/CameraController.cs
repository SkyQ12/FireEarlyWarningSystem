using FireEarlyWarningSystem.Infrastructure.Services.Cameras;
using FireEarlyWarningSystem.Infrastructure.Domain.Resources.Camera;
using Microsoft.AspNetCore.Mvc;

namespace FireEarlyWarningSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CameraController : ControllerBase
    {
        public ICameraService _cameraService { get; set; }

        public CameraController(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        [HttpGet]
        [Route("GetAllCameras")]
        public async Task<List<CameraViewModel>> GetAllGPSDevices()
        {
            return await _cameraService.GetCamera();
        }
        [HttpGet]
        [Route("GetCameraById")]
        public async Task<CameraViewModel> GetCameraById([FromQuery] string Id)
        {
            return await _cameraService.GetCameraById(Id);
        }

        [HttpPatch]
        [Route("UpdateCameraStatus")]
        public async Task<IActionResult> UpdateCameraStatus([FromBody] UpdateCameraViewModel updateCamera, [FromQuery] string CameraId)
        {
            try
            {
                var result = await _cameraService.UpdateCameraStatus(updateCamera, CameraId);
                if (result)
                {
                    return new OkObjectResult("Update successful!");
                }
                else
                {
                    return new OkObjectResult("Not found camera!");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
