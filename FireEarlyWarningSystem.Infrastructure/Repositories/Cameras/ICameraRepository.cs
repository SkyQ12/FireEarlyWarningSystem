using FireEarlyWarningSystem.Infrastructure.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Repositories.Cameras
{
    public interface ICameraRepository
    {
        public Task<List<Camera>> GetAllCameraAsync();
        public Task<Camera> GetCameraByIdAsync(string id);
        public Task<Camera> CreateCameraAsync(Camera device);
        public bool DeleteCameraAsync(Camera device);
        public Task UpdateCameraAsync(Camera device);
        public Task<bool> IsExistCamera(string id);
        public Task<bool> AssignCamera(string cameraId, string userId);
    }
}
