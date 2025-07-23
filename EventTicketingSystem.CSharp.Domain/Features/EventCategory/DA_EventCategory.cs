namespace EventTicketingSystem.CSharp.Domain.Features.EventCategory;

public class DA_EventCategory
{
    private readonly ILogger<DA_EventCategory> _logger;
    private readonly AppDbContext _db;
    private readonly CommonService _commonService;
    private const string CreatedByUserId = "Admin";

    public DA_EventCategory(ILogger<DA_EventCategory> logger, AppDbContext db, CommonService commonService)
    {
        _logger = logger;
        _db = db;
        _commonService = commonService;
    }

    #region Get category list

    public async Task<Result<EventCategoryResponseModel>> GetList()
    {
        var responseModel = new Result<EventCategoryResponseModel>();
        var model = new EventCategoryResponseModel();
        try
        {
            var data = await _db.TblEventcategories
                                .Where(x => x.Deleteflag == false)
                                .ToListAsync();
            if (data is null)
            {
                return Result<EventCategoryResponseModel>.NotFoundError("No Event Categories Found!");
            }

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
            return Result<EventCategoryResponseModel>.ValidationError("Fill up Category Name!");
        }
        else if (isCategoryNameExist(request.CategoryName))
        {
            return Result<EventCategoryResponseModel>.ValidationError("CategoryName already exist!");
        }
        else
        {
            try
            {
                var newCategory = new TblEventcategory
                {
                    Eventcategoryid = Ulid.NewUlid().ToString(),
                    Eventcategorycode = await _commonService.GenerateSequenceCode(EnumTableUniqueName.Tbl_EventCategory),
                    Categoryname = request.CategoryName,
                    Createdat = DateTime.Now,
                    Createdby = CreatedByUserId,
                    Deleteflag = false
                };
                await _db.TblEventcategories.AddAsync(newCategory);
                await _db.SaveAndDetachAsync();

                model.eventCategory = EventCategoryModel.FromTblCategory(newCategory);
                responseModel = Result<EventCategoryResponseModel>.Success(model, "New Category Created");
            }
            catch (Exception ex)
            {
                _logger.LogExceptionError(ex);
                return Result<EventCategoryResponseModel>.SystemError(ex.Message);
            }
        }

        return responseModel;
    }

    #endregion

    #region Update Category

    public async Task<Result<EventCategoryResponseModel>> UpdateCategory(EventCategoryRequestModel request)
    {
        var model = new EventCategoryResponseModel();
        if (request.AdminCode.IsNullOrEmpty())
        {
            return Result<EventCategoryResponseModel>.ValidationError("Admin not found");
        }
        else if (request.CategoryName.IsNullOrEmpty())
        {
            return Result<EventCategoryResponseModel>.ValidationError("Category name not found");
        }
        else
        {
            try
            {
                var existingCategory = await _db.TblEventcategories
                                        .FirstOrDefaultAsync(
                                            x => x.Eventcategoryid == request.EventCategoryID &&
                                            x.Deleteflag == false
                                        );
                if (existingCategory is null)
                {
                    return Result<EventCategoryResponseModel>.NotFoundError("Category name not found");
                }

                existingCategory.Categoryname = request.CategoryName;
                existingCategory.Modifiedby = CreatedByUserId;
                existingCategory.Modifiedat = DateTime.Now;

                _db.Entry(existingCategory).State = EntityState.Modified;
                await _db.SaveAndDetachAsync();
                return Result<EventCategoryResponseModel>.Success(model);
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

    public async Task<Result<EventCategoryResponseModel>> DeleteCategory(string? categoryCode, string userCode)
    {
        if (categoryCode.IsNullOrEmpty())
        {
            return Result<EventCategoryResponseModel>.UserInputError("Category Code can't be Null or Empty!");
        }

        try
        {
            var data = await _db.TblEventcategories
                        .FirstOrDefaultAsync(
                            x => x.Eventcategorycode == categoryCode &&
                            x.Deleteflag == false
                        );
            if (data is null)
            {
                return Result<EventCategoryResponseModel>.NotFoundError($"No Owner Found with Code: {categoryCode}");
            }

            data.Deleteflag = true;
            data.Modifiedby = CreatedByUserId;
            data.Modifiedat = DateTime.Now;
            _db.Entry(data).State = EntityState.Modified;
            await _db.SaveAndDetachAsync();

            return Result<EventCategoryResponseModel>.Success("Category Deleted Successfully!");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<EventCategoryResponseModel>.SystemError(ex.Message);
        }
    }

    #endregion

    #region Private function
    
    private bool isCategoryNameExist(string? categoryName)
    {

        return _db.TblEventcategories
    .AsEnumerable()
    .Any(x => string.Equals(x.Categoryname, categoryName, StringComparison.OrdinalIgnoreCase));
    }

    //private bool isCategoryIDExist(string? categoryID)
    //{
    //    if (_db.TblEventcategories.FirstOrDefault(x => x.Eventcategoryid == categoryID) is not null)
    //    {
    //        return true;
    //    }

    //    else
    //    {
    //        return false;
    //    }
    //}

    //private string GenerateCategoryCode()
    //{
    //    var categoryCount = _db.TblEventcategories.Count();
    //    categoryCount++;
    //    return "CAT" + categoryCount.ToString("D3");
    //}

    #endregion
}