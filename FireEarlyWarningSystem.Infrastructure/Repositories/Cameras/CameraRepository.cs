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

        public bool DeleteCameraAsync(Camera deleteCamera)
        {
            _context.Cameras.RemoveRange(deleteCamera);
            return true;
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
            var camera = await _context.Cameras.FirstOrDefaultAsync(x => x.Id == cameraId);

            camera.UserId = userId;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
