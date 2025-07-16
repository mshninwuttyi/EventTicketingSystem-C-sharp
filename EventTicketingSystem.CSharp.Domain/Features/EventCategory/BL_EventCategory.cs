
using EventTicketingSystem.CSharp.Domain.Models.Features.EventCategory;

namespace EventTicketingSystem.CSharp.Domain.Features.EventCategory
{
    public class BL_EventCategory
    {
        private readonly DA_EventCategory _dataAccess;
        public BL_EventCategory(DA_EventCategory dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Result<EventCategoryResponseModel>> GetList()
        {
            return await _dataAccess.GetList();
        }

        public async Task<Result<EventCategoryResponseModel>> CreateCategory(EventCategoryRequestModel request)
        {
          
            return await _dataAccess.CreateCategory(request);
        }

        public async Task<Result<EventCategoryResponseModel>> UpdateCategory(EventCategoryRequestModel request)
        {
            return await _dataAccess.UpdateCategory(request);
        }

        public async Task<Result<EventCategoryResponseModel>> DeleteCategory(EventCategoryRequestModel request)
        {
            return await _dataAccess.DeleteCategory(request.CategoryCode);
        }
    }
}
