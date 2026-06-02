using FireEarlyWarningSystem.Infrastructure.Domain.Models.DataType;
using FireEarlyWarningSystem.Infrastructure.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Repositories.WarningHistories
{
    public interface IWarningHistoryRepository
    {
        public Task SaveWarningHistoryToDatabase(string cameraId, AIDetectType aIDetection, FlameSensorType flameSensor, double smokeValue, StateType cameraState, DateTime warningTime);
        public Task<List<WarningHistory>> GetAllWanringHistoryAsync();
        public Task<List<WarningHistory>> GetWarningHistoryByCameraIdAsync(string cameraId);
        public Task<bool> DeleteAllWarningHistoryAsync();
        public Task<bool> DeleteWarningHistoryByCameraIdAsync(string cameraId);

    }
}
