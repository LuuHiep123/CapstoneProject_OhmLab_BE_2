using BusinessLayer.RequestModel.Security;
using BusinessLayer.ResponseModel.BaseResponse;
using BusinessLayer.ResponseModel.RegistrationSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IsecurityService
    {
        Task<BaseResponse> CheckIn(CheckInSecurityRequestModel model);
    }
}
