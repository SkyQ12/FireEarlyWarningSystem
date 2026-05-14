using AutoMapper;
using FireEarlyWarningSystem.Infrastructure.Domain.Models;
using FireEarlyWarningSystem.Infrastructure.Domain.Resources.Camera;
using FireEarlyWarningSystem.Infrastructure.Domain.Resources.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Domain.Mapping
{
    public class ModelToViewModelProfile : Profile
    {
        public ModelToViewModelProfile()
            {
            // CreateMap<Source, Destination>();
            CreateMap<User, UserViewModel>();
            CreateMap<Camera, CameraViewModel>();
            CreateMap<AddCameraViewModel, Camera>();
            CreateMap<UpdateCameraViewModel, Camera>();
        }
    }
}
