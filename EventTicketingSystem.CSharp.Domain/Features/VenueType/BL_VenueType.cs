﻿using EventTicketingSystem.CSharp.Domain.Models.Features.VenueType;
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
        public BL_VenueType( DA_VenueType dataAccess )
        {
            _dataAccess = dataAccess; 
        }

        public async Task<Result<VenueTypeListResponseModel>> List()
        {
            return await _dataAccess.List();
        }

        public async Task<Result<VenueTypeEditResponseModel>> Edit(string venueTypeCode)
        {
            return await _dataAccess.Edit(venueTypeCode);
        }

        public async Task<Result<VenueTypeCreateResponseModel>> Create(VenueTypeCreateRequestModel requestModel)
        {
            return await _dataAccess.Create(requestModel);
        }

        public async Task<Result<VenueTypeUpdateResponseModel>> Update(VenueTypeUpdateRequestModel requestModel)
        {
            return await _dataAccess.Update(requestModel);
        }

        public async Task<Result<VenueTypeDeleteResponseModel>> Delete(string venueTypeCode)
        {
            return await _dataAccess.Delete(venueTypeCode);
        }
    }
}
