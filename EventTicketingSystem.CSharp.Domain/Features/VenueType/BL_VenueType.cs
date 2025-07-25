using EventTicketingSystem.CSharp.Domain.Models.Features.VenueType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Features.VenueType
{
    public class BL_VenueType
    {
        private readonly DA_VenueType _dataAccess;

        public BL_VenueType(DA_VenueType dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Result<VenueTypeResponseModel>> getVenueTypeList()
        {
            return await _dataAccess.GetVenueTypeList();
        }

        public async Task<Result<CreateVenueTypeResponseModel>> CreateVenueType(VenueTypeRequestModel request)
        {

            return await _dataAccess.CreateVenueType(request);
        }

        public async Task<Result<UpdateVenueTypeResponseModel>> UpdateVenueType(VenueTypeRequestModel request)
        {
            return await _dataAccess.UpdateVenueType(request);
        }

        public async Task<Result<VenueTypeResponseModel>> DeleteVenueType(VenueTypeRequestModel request)
        {
            return await _dataAccess.DeleteVenueType(request.VenueTypeCode);
        }

    }
}
