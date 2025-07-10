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
                return Result<EventCategoryResponseModel>.ValidationError("Admin name not found", model);
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
                        _db.TblCategories.Add(new TblCategory()
                        {
                            Categorycode = Guid.NewGuid().ToString(),
                            Categoryname = request.CategoryName,
                            Createdat = DateTime.Now,
                            Createdby = request.AdminName,
                            
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

        //#region UpdateCategory
        //public async Task<Result<EventCategoryResponseModel>> UpdateCategory(EventCategoryRequestModel request)
        //{
        //    var model = new EventCategoryResponseModel();
        //    if (request.Categoryname.IsNullOrEmpty())
        //    {
        //        return Result<EventCategoryResponseModel>.ValidationError("Category name not found", model);
        //    }
        //    else if (request.AdminName.IsNullOrEmpty())
        //    {
        //        return Result<EventCategoryResponseModel>.ValidationError("Admin name not found", model);
        //    }
        //    else
        //    {
        //        try
        //        {
                    
        //            if (isCategoryCodeExist(request.Categorycode))
        //            {
        //                _db.TblCategories.Update(new TblCategory()
        //                {

        //                });
        //            }
        //            else
        //            {
        //                _db.TblCategories.Add(new TblCategory()
        //                {
        //                    Categorycode = Guid.NewGuid().ToString(),
        //                    Categoryname = request.Categoryname,
        //                    Createdat = DateTime.Now,
        //                    Createdby = request.AdminName
        //                });
        //                _db.SaveChanges();
        //                return Result<EventCategoryResponseModel>.Success(model);
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogExceptionError(ex);
        //            return Result<EventCategoryResponseModel>.SystemError(ex.Message);
        //        }
        //    }
        //}
        //#endregion
        private bool isCategoryNameExist(string? categoryName)
        {
            if (_db.TblCategories.FirstOrDefault(x => x.Categoryname == categoryName) is not null)
            {
                return true;
            }

            else
            {
                return false;
            }
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

    }
}
