using AutoMapper;
using FireEarlyWarningSystem.Infrastructure.Domain.Models;
using FireEarlyWarningSystem.Infrastructure.Domain.Resources.User;
using FireEarlyWarningSystem.Infrastructure.Domain.Resources.WarningHistory;
using FireEarlyWarningSystem.Infrastructure.Repositories.UnitOfWork;
using FireEarlyWarningSystem.Infrastructure.Repositories.WarningHistories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Services.WarningHistories
{
    public class WarningHistoryService : IWarningHistoryService
    {
        public IWarningHistoryRepository _warningHistoryRepository { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }
        public IMapper _mapper { get; set; }

        public WarningHistoryService(IWarningHistoryRepository warningHistoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _warningHistoryRepository = warningHistoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<WariningHistoryViewModel>> GetAllWarningHistory()
        {
            var source = await _warningHistoryRepository.GetAllWanringHistoryAsync();
            var result = _mapper.Map<List<WarningHistory>, List<WariningHistoryViewModel>>(source);
            return result;
        }

        public async Task<List<WariningHistoryViewModel>> GetWarningHistoryByCameraId(string cameraId)
        {
            var source = await _warningHistoryRepository.GetAllWanringHistoryAsync();
            var filterSource = source.Where(x => x.CameraId == cameraId).ToList();
            if (filterSource.Count == 0)
            {
                return new List<WariningHistoryViewModel>();
            }
            var result = _mapper.Map<List<WarningHistory>, List<WariningHistoryViewModel>>(filterSource);
            return result;
        }
        public async Task<bool> DeleteAllWarningHistory()
        {
            var source = await _warningHistoryRepository.GetAllWanringHistoryAsync();
            if (source.Count == 0)
            {
                return false;
            }
            _warningHistoryRepository.DeleteAllWarningHistoryAsync();
            return true;
        }

        public async Task<bool> DeleteWarningHistoryByCameraId(string cameraId)
        {
            var source = await _warningHistoryRepository.GetAllWanringHistoryAsync();
            var isexist = source.Where(x => x.CameraId == cameraId).FirstOrDefault();
            if (isexist is not null)
            {
                await _warningHistoryRepository.DeleteWarningHistoryByCameraIdAsync(cameraId);
                return await _unitOfWork.CompleteAsync();
            }
            else
            {
                return false;
            }
        }
    }
}
