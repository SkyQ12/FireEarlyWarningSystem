using FireEarlyWarningSystem.Infrastructure.Domain.Context;
using FireEarlyWarningSystem.Infrastructure.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireEarlyWarningSystem.Infrastructure.Repositories;
using FireEarlyWarningSystem.Infrastructure.Domain.Exceptions;
using FireEarlyWarningSystem.Infrastructure.Domain.Context.Configurations;
using FireEarlyWarningSystem.Infrastructure.Domain.Models.DataType;

namespace FireEarlyWarningSystem.Infrastructure.Repositories.Cameras
{
    public class CameraRepository : BaseRepository, ICameraRepository
    {
        public CameraRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Domain.Models.Camera>> GetAllCameraAsync()
        {
            return await _context.Cameras.ToListAsync();
        }

        public async Task<Camera> GetCameraByIdAsync(string id)
        {
            return await _context.Cameras.Where(c => c.Id == id).FirstOrDefaultAsync() ?? throw new ResourceNotfoundException("Not found camera");
        }

        public async Task<Camera> CreateCameraAsync(Camera newCamera)
        {
            if (string.IsNullOrWhiteSpace(newCamera.Id))
            {
                throw new ResourceNotfoundException("Impossible create this id!");
            }
            else
            {
                newCamera.RegistationDate = DateTime.Now;
                var entity = await _context.Cameras.AddAsync(newCamera);
                return entity.Entity;
            }
        }

        public async Task<bool> DeleteCameraAsync(Camera deleteCamera)
        {
            if (deleteCamera == null)
            {
                return false;
            }

            try
            {
                await _context.WarningHistories
                    .Where(x => x.CameraId == deleteCamera.Id)
                    .ExecuteDeleteAsync();

                await _context.Cameras
                    .Where(x => x.Id == deleteCamera.Id)
                    .ExecuteDeleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task UpdateCameraAsync(Camera updateCamera)
        {
            _context.Update(updateCamera);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsExistCamera(string id)
        {
            return await _context.Cameras.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> AssignCamera(string cameraId, string userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var camera = await _context.Cameras
                    .FirstOrDefaultAsync(x => x.Id == cameraId);

                if (camera == null)
                {
                    return false;
                }

                // Xóa toàn bộ warning history của camera
                var histories = await _context.WarningHistories
                    .Where(x => x.CameraId == cameraId)
                    .ToListAsync();

                if (histories.Any())
                {
                    _context.WarningHistories.RemoveRange(histories);
                }

                // Gán user mới
                camera.UserId = userId;

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task SaveCameraMetricToDatabase(string cameraId, AIDetectType aiDetection, FlameSensorType flameSensor, double smokeValue, StateType cameraState, double battery, DateTime timeStamp)
        {
            if (string.IsNullOrWhiteSpace(cameraId))
            {
                throw new ArgumentException("CameraId is required.");
            }

            var camera = await _context.Cameras
                .FirstOrDefaultAsync(x => x.Id == cameraId);

            if (camera == null)
            {
                throw new Exception($"Camera '{cameraId}' does not exist.");
            }

            camera.AIDetection = aiDetection;
            camera.FlameSensor = flameSensor;
            camera.SmokeValue = smokeValue;
            camera.CameraState = cameraState;
            camera.Battery = battery;
            camera.TimeStamp = timeStamp;

            await _context.SaveChangesAsync();
        }
        public async Task SaveCameraLinkToDatabase(string cameraId, string realtimeCameraLink)
        {
            if (string.IsNullOrWhiteSpace(cameraId))
            {
                throw new ArgumentException("CameraId is required.");
            }

            if (string.IsNullOrWhiteSpace(realtimeCameraLink))
            {
                throw new ArgumentException("RealtimeCameraLink is required.");
            }

            var camera = await _context.Cameras
                .FirstOrDefaultAsync(x => x.Id == cameraId);

            if (camera == null)
            {
                throw new Exception($"Camera '{cameraId}' does not exist.");
            }

            camera.RealtimeCameraLink = realtimeCameraLink;

            await _context.SaveChangesAsync();
        }
        
        public async Task<List<Camera>> GetCameraIdByUserId(string userId)
        {
            return await _context.Cameras.Where(c => c.UserId == userId).ToListAsync();
        }
        
    }
}
