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
        private readonly AppDbContext _db ;

        public DA_EventCategory(ILogger<DA_EventCategory> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        private int getCategoryCount()
        {
            return _db.TblCategories.Count();
        }

        #region Get category list
        public async Task<Result<EventCategoryResponseModel>> GetList()
        {
            var responseModel = new Result<EventCategoryResponseModel>();
            var model = new EventCategoryResponseModel();
            try
            {
                var data = await _db.TblCategories.ToListAsync();
                model.EventCategories = data.Select(x => new EventCategoryModel
                {
                    Categoryid = x.Categoryid,
                    Categorycode = x.Categorycode,
                    Categoryname = x.Categoryname,
                    Createdby = x.Createdby,
                    Createdat = x.Createdat,
                    Modifiedby = x.Modifiedby,
                    Modifiedat = x.Modifiedat,
                    
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
                    if (isCategoryNameExist(request.CategoryName)){
                        return Result<EventCategoryResponseModel>.ValidationError("Category name already exist", model);
                    }
                    else
                    {
                        int count = getCategoryCount();
                        count++;
                        _db.TblCategories.Add(new TblCategory()
                        {
                            Categoryid = Ulid.NewUlid().ToString(),
                            Categorycode = "E00" + count.ToString(),
                            Categoryname = request.CategoryName,
                            Createdat = DateTime.Now,
                            Createdby = request.AdminName,
                            Deleteflag = false
                        });
                        _db.SaveChanges();
                        return Result<EventCategoryResponseModel>.Success(model);
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

        #region UpdateCategory
        public async Task<Result<EventCategoryResponseModel>> UpdateCategory(EventCategoryRequestModel request)
        {
            var model = new EventCategoryResponseModel();
            if (request.AdminName.IsNullOrEmpty())
            {
                return Result<EventCategoryResponseModel>.ValidationError("Category name not found", model);
            }
            else if (request.CategoryName.IsNullOrEmpty())
            {
                return Result<EventCategoryResponseModel>.ValidationError("Admin name not found", model);
            }
            else
            {
                try
                {

                    if (isCategoryCodeExist(request.CategoryCode))
                    {
                       var existingCategory = _db.TblCategories.FirstOrDefault(x => x.Categorycode == request.CategoryCode);
                    
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
        //public async Task<Result<EventCategoryResponseModel>> DeleteCategory(string categoryCode)
        //{


        //}
        #endregion

        #region Private function 
        private bool isCategoryNameExist(string? categoryName)
        {

            return _db.TblCategories
        .AsEnumerable()
        .Any(x => string.Equals(x.Categoryname, categoryName, StringComparison.OrdinalIgnoreCase));
        }

        private bool isCategoryCodeExist(string? categoryCode)
        {
            if (_db.TblCategories.FirstOrDefault(x => x.Categorycode == categoryCode) is not null)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        #endregion

    }
}
