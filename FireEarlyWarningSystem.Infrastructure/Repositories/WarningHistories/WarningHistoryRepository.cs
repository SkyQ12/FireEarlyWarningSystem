using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireEarlyWarningSystem.Infrastructure.Domain.Context;
using FireEarlyWarningSystem.Infrastructure.Domain.Models.DataType;
using FireEarlyWarningSystem.Infrastructure.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FireEarlyWarningSystem.Infrastructure.Repositories.WarningHistories
{
    public class WarningHistoryRepository : BaseRepository, IWarningHistoryRepository
    {
        public WarningHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task SaveWarningHistoryToDatabase(string cameraId, AIDetectType aIDetection, FlameSensorType flameSensor, double smokeValue, StateType cameraState, DateTime warningTime)
        {
            var history = new WarningHistory
            {
                CameraId = cameraId,
                AIDetection = aIDetection,
                FlameSensor = flameSensor,
                SmokeValue = smokeValue,
                CameraState = cameraState,
                WarningTime = warningTime
            };

            _context.WarningHistories.Add(history);
            await _context.SaveChangesAsync();
        }

        public async Task<List<WarningHistory>> GetAllWanringHistoryAsync()
        {
            return await _context.WarningHistories.ToListAsync();
        }

        public async Task<List<WarningHistory>> GetWarningHistoryByCameraIdAsync(string cameraId)
        {
            return await _context.WarningHistories.Where(w => w.CameraId == cameraId).ToListAsync();
        }
        public async Task<bool> DeleteAllWarningHistoryAsync()
        {
            var allHistories = await _context.WarningHistories.ToListAsync();
            _context.WarningHistories.RemoveRange(allHistories);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteWarningHistoryByCameraIdAsync(string cameraId)
        {
            var histories = await _context.WarningHistories.Where(w => w.CameraId == cameraId).ToListAsync();
            _context.WarningHistories.RemoveRange(histories);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
