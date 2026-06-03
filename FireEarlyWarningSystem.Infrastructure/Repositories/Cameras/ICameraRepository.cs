using FireEarlyWarningSystem.Infrastructure.Domain.Models;
using FireEarlyWarningSystem.Infrastructure.Domain.Models.DataType;
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
        public Task<bool> DeleteCameraAsync(Camera deleteCamera);
        public Task UpdateCameraAsync(Camera device);
        public Task<bool> IsExistCamera(string id);
        public Task<bool> AssignCamera(string cameraId, string userId);
        public Task SaveCameraMetricToDatabase(string cameraId, AIDetectType aIDetection, FlameSensorType flameSensor, double smokeValue, StateType cameraState, double battery, DateTime warningTime);
        public Task SaveCameraLinkToDatabase(string cameraId, string realtimeCameraLink);
        public Task<List<Camera>> GetCameraIdByUserId(string userId);
    }
}
