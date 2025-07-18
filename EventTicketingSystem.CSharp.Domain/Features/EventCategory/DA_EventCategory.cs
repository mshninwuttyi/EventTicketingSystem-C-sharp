using EventTicketingSystem.CSharp.Database.AppDbContext;
using EventTicketingSystem.CSharp.Domain.Models.Features.EventCategory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Features.EventCategory
{
    public class DA_EventCategory
    {
        private readonly ILogger<DA_EventCategory> _logger;
        private readonly AppDbContext _db;

        public DA_EventCategory(ILogger<DA_EventCategory> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }


        #region Get category list
        public async Task<Result<EventCategoryResponseModel>> GetList()
        {
            var responseModel = new Result<EventCategoryResponseModel>();
            var model = new EventCategoryResponseModel();
            try
            {
                var data = await _db.TblEventcategories
                                    .AsNoTracking()
                                    .Where(x=>x.Deleteflag == false)
                                    .ToListAsync();

                model.EventCategories = data.Select(x => new EventCategoryModel 
                {
                    Categoryid = x.Eventcategoryid,
                    Categorycode = x.Eventcategorycode,
                    Categoryname = x.Categoryname,
                    Createdby = x.Createdby,
                    Createdat = x.Createdat,
                    Modifiedby = x.Modifiedby,
                    Modifiedat = x.Modifiedat,
                    Deleteflag = false

                }).ToList();

                return Result<EventCategoryResponseModel>.Success(model);
            }
            catch (Exception ex)
            {
                _logger.LogExceptionError(ex);
                return Result<EventCategoryResponseModel>.SystemError(ex.Message);
            }

        }
        #endregion
        #region create category

        public async Task<Result<EventCategoryResponseModel>> CreateCategory(EventCategoryRequestModel request)
        {
            var responseModel = new Result<EventCategoryResponseModel>();
            var model = new EventCategoryResponseModel();

            if (request.CategoryName.IsNullOrEmpty())
            {
                responseModel = Result<EventCategoryResponseModel>.ValidationError("Fill up Category Name!");
                goto ResultReturn;
            } else if (isCategoryNameExist(request.CategoryName))
            {
                responseModel = Result<EventCategoryResponseModel>.ValidationError("CategoryName already exist!");
                goto ResultReturn;
            }
            else
            {
                try
                {
                    var newCategory = new TblEventcategory
                    {
                        Eventcategoryid = Ulid.NewUlid().ToString(),
                        Eventcategorycode = GenerateCategoryCode(),
                        Categoryname = request.CategoryName,
                        Createdat = DateTime.Now,
                        Createdby = "",
                        Deleteflag = false
                    };
                    await _db.TblEventcategories.AddAsync(newCategory);
                    await _db.SaveChangesAsync();

                    model.eventCategory = EventCategoryModel.FromTblCategory(newCategory);
                   
                    responseModel = Result<EventCategoryResponseModel>.Success(model, "New Category Created");

                }
                catch (Exception ex)
                {
                    _logger.LogExceptionError(ex);
                    return Result<EventCategoryResponseModel>.SystemError(ex.Message);
                }
            }
            ResultReturn:
            return responseModel;
        }

        #endregion

        #region UpdateCategory
        public async Task<Result<EventCategoryResponseModel>> UpdateCategory(EventCategoryRequestModel request)
        {
            var model = new EventCategoryResponseModel();
            if (request.AdminName.IsNullOrEmpty())
            {
                return Result<EventCategoryResponseModel>.ValidationError("Admin name not found", model);
            }
            else if (request.CategoryName.IsNullOrEmpty())
            {
                return Result<EventCategoryResponseModel>.ValidationError("Category name not found", model);
            }
            else
            {
                try
                {

                    if (isCategoryCodeExist(request.EventCategoryCode))
                    {
                       var existingCategory = _db.TblEventcategories.FirstOrDefault(x => x.Eventcategorycode == request.EventCategoryCode);
                    
                        existingCategory.Categoryname = request.CategoryName;
                        existingCategory.Modifiedby = request.AdminName;
                        existingCategory.Modifiedat = DateTime.Now;

                        _db.Update(existingCategory);
                        _db.SaveChanges();
                        return Result<EventCategoryResponseModel>.Success(model);
                    }
                    else
                    {
                        return Result<EventCategoryResponseModel>.ValidationError("Category Code Not Found", model);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogExceptionError(ex);
                    return Result<EventCategoryResponseModel>.SystemError(ex.Message);
                }
            }
        }
        #endregion

        #region Delete Category
        public async Task<Result<EventCategoryResponseModel>> DeleteCategory(string? categoryCode)
        {
            var responseModel = new Result<EventCategoryResponseModel>();
            var model = new EventCategoryResponseModel();
            if (categoryCode.IsNullOrEmpty())
            {
                responseModel = Result<EventCategoryResponseModel>.UserInputError("Owner Code can't be Null or Empty!");
                goto ResultReturn;
            }
            try
            {
                var data = await _db.TblEventcategories
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Eventcategorycode == categoryCode && x.Deleteflag == false);

                if (data != null)
                {
                    data.Deleteflag = true;
                    _db.Entry(data).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    responseModel = Result<EventCategoryResponseModel>.Success(model, "Category Deleted Successfully!");
                }
                else
                {
                    responseModel = Result<EventCategoryResponseModel>.NotFoundError($"No Owner Found with Code: {categoryCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogExceptionError(ex);
                responseModel = Result<EventCategoryResponseModel>.SystemError(ex.Message);
            }
        ResultReturn:
            return responseModel;
        }
        #endregion

        #region Private function 
        private bool isCategoryNameExist(string? categoryName)
        {

            return _db.TblEventcategories
        .AsEnumerable()
        .Any(x => string.Equals(x.Categoryname, categoryName, StringComparison.OrdinalIgnoreCase));
        }

        private bool isCategoryCodeExist(string? categoryCode)
        {
            if (_db.TblEventcategories.FirstOrDefault(x => x.Eventcategorycode == categoryCode) is not null)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        private string GenerateCategoryCode()
        {
            var categoryCount = _db.TblEventcategories.Count();
            return "EC" + categoryCount.ToString("D6");
        }

        #endregion

    }
}
