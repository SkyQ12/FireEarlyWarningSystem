using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireEarlyWarningSystem.Infrastructure.Domain.Resources.Camera;

namespace FireEarlyWarningSystem.Infrastructure.Services.Cameras
{
    public interface ICameraService
    {
        public Task<List<CameraViewModel>> GetCamera();
        public Task<CameraViewModel> GetCameraById(string id);
        public Task<bool> CreateNewCamera(AddCameraViewModel addCamera);
        public Task<bool> DeleteCamera(string cameraId);
        public Task<bool> UpdateCameraStatus(UpdateCameraViewModel updateCamera, string cameraId);
        public Task<bool> IsExistCamera(string cameraId);
    }
}
