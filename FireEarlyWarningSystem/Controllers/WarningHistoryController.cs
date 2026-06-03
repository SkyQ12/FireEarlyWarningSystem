using Microsoft.AspNetCore.Mvc;
using FireEarlyWarningSystem.Infrastructure.Services.WarningHistories;

using Microsoft.AspNetCore.Mvc;
using FireEarlyWarningSystem.Infrastructure.Domain.Resources.User;
using FireEarlyWarningSystem.Infrastructure.Domain.Resources.WarningHistory;

namespace FireEarlyWarningSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WarningHistoryController : ControllerBase
    {
        public IWarningHistoryService _warningHistoryService { get; set; }

        public WarningHistoryController(IWarningHistoryService warningHistoryService)
        {
            _warningHistoryService = warningHistoryService;
        }
        [HttpGet]
        [Route("GetAllWarningHistories")]
        public async Task<List<WarningHistoryViewModel>> GetAllWarningHistories()
        {
            return await _warningHistoryService.GetAllWarningHistory();
        }

        [HttpGet]
        [Route("GetWarningHistoryByCameraId")]
        public async Task<List<WarningHistoryViewModel>> GetWarningHistoryByCameraId([FromQuery] string cameraId)
        {
            return await _warningHistoryService.GetWarningHistoryByCameraId(cameraId);
        }
        [HttpDelete]
        [Route("DeleteAllWarningHistory")]
        public async Task<IActionResult> DeleteAllWarningHistory()
        {
            var result = await _warningHistoryService.DeleteAllWarningHistory();
            if (result)
            {
                return new OkObjectResult("Delete all warning history successfully.");
            }
            else
            {
                return new OkObjectResult("Delete all warning history failed.");
            }
        }

        [HttpDelete]
        [Route("DeleteWarningHistoryByCameraId")]
        public async Task<IActionResult> DeleteWarningHistoryByCameraId([FromQuery] string cameraId)
        {
            var result = await _warningHistoryService.DeleteWarningHistoryByCameraId(cameraId);
            if (result)
            {
                return new OkObjectResult("Delete warning history by camera id successfully.");
            }
            else
            {
                return new OkObjectResult("Delete warning history by camera id failed.");
            }
        }
    }
}
