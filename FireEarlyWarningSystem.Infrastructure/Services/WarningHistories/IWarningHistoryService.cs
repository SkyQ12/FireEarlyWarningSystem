using FireEarlyWarningSystem.Infrastructure.Domain.Models.DataType;
using FireEarlyWarningSystem.Infrastructure.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireEarlyWarningSystem.Infrastructure.Domain.Resources.WarningHistory;

namespace FireEarlyWarningSystem.Infrastructure.Services.WarningHistories
{
    public interface IWarningHistoryService
    {
        public Task<List<WarningHistoryViewModel>> GetAllWarningHistory();
        public Task<List<WarningHistoryViewModel>> GetWarningHistoryByCameraId(string cameraId);
        public Task <bool> DeleteAllWarningHistory();
        public Task<bool> DeleteWarningHistoryByCameraId(string cameraId);
    }
}
