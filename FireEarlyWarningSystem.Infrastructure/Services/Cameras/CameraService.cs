using AutoMapper;
using FireEarlyWarningSystem.Infrastructure.Domain.Resources.Camera;
using FireEarlyWarningSystem.Infrastructure.Repositories.Cameras;
using FireEarlyWarningSystem.Infrastructure.Domain.Exceptions;
using FireEarlyWarningSystem.Infrastructure.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireEarlyWarningSystem.Infrastructure.Domain.Models;
using FireEarlyWarningSystem.Infrastructure.Repositories.WarningHistories;

namespace FireEarlyWarningSystem.Infrastructure.Services.Cameras
{
    public class CameraService : ICameraService
    {
        public IWarningHistoryRepository _WarningHistoryRepository { get; set; }
        public ICameraRepository _CameraRepository { get; set; }
        public IMapper _mapper { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }

        public CameraService(IWarningHistoryRepository warningHistoryRepository, ICameraRepository cameraRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _WarningHistoryRepository = warningHistoryRepository;
            _CameraRepository = cameraRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CameraViewModel>> GetCamera()
        {
            var source = await _CameraRepository.GetAllCameraAsync() ?? throw new ResourceNotfoundException();
            var viewmodel = _mapper.Map<List<FireEarlyWarningSystem.Infrastructure.Domain.Models.Camera>, List<CameraViewModel>>(source);
            return viewmodel;
        }

        public async Task<CameraViewModel> GetCameraById(string id)
        {
            var source = await _CameraRepository.GetCameraByIdAsync(id);
            var viewmodel = _mapper.Map<FireEarlyWarningSystem.Infrastructure.Domain.Models.Camera, CameraViewModel>(source);
            return viewmodel;
        }

        public async Task<bool> CreateNewCamera(AddCameraViewModel addCamera)
        {
            var IsExist = await _CameraRepository.IsExistCamera(addCamera.Id);
            if (IsExist)
            {
                throw new EntityDuplicationException("This camera is existing!");
            }
            
            var mapping = _mapper.Map<AddCameraViewModel, Camera>(addCamera);
            var newdevice = _CameraRepository.CreateCameraAsync(mapping);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteCamera(string cameraId)
{
    var camera = await _CameraRepository.GetCameraByIdAsync(cameraId);

    if (camera == null)
    {
        return false;
    }

    return await _CameraRepository.DeleteCameraAsync(camera);
}

        public async Task<bool> UpdateCameraStatus(UpdateCameraViewModel updateCamera, string cameraId)
        {
            var isExist = await _CameraRepository.IsExistCamera(cameraId);
            if (!isExist)
            {
                throw new ResourceNotfoundException("Not found camera!");
            }
            if (isExist)
            {
                var camera = await _CameraRepository.GetCameraByIdAsync(cameraId);
                if (!string.IsNullOrEmpty(updateCamera.UserId))
                {
                    camera.UserId = updateCamera.UserId;
                }              
                if (!string.IsNullOrEmpty(updateCamera.CameraName))
                {
                    camera.CameraName = updateCamera.CameraName;
                }
                if (!string.IsNullOrEmpty(updateCamera.Battery.ToString()))
                {
                    camera.Battery = updateCamera.Battery;
                }
                if (!string.IsNullOrEmpty(updateCamera.RealtimeCameraLink))
                {
                    camera.RealtimeCameraLink = updateCamera.RealtimeCameraLink;
                }
                if (!string.IsNullOrEmpty(updateCamera.AIDetection.ToString()))
                {
                    camera.AIDetection = updateCamera.AIDetection;
                }
                if (!string.IsNullOrEmpty(updateCamera.FlameSensor.ToString()))
                {
                    camera.FlameSensor = updateCamera.FlameSensor;             
                }
                if (!string.IsNullOrEmpty(updateCamera.SmokeValue.ToString()))
                {
                    camera.SmokeValue = updateCamera.SmokeValue;
                }
                if (!string.IsNullOrEmpty(updateCamera.CameraState.ToString()))
                {
                    camera.CameraState = updateCamera.CameraState;
                }
                if (!string.IsNullOrEmpty(updateCamera.TimeStamp.ToString()))
                {
                    camera.TimeStamp = updateCamera.TimeStamp;
                }
                await _CameraRepository.UpdateCameraAsync(camera);
                return await _unitOfWork.CompleteAsync();
            }
            else
            {
                return false;
            }    
            
        }

        public async Task<bool> IsExistCamera(string cameraId)
        {
            return await _CameraRepository.IsExistCamera(cameraId);
        }

    }
}
