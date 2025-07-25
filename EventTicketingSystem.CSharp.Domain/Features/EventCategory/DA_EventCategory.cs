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
                EventCategoryid = x.Eventcategoryid,
                EventCategorycode = x.Eventcategorycode,
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

    public async Task<Result<CreateEventCategoryResponseModel>> CreateCategory(EventCategoryRequestModel request)
    {
        if (request.CategoryName.IsNullOrEmpty())
        {
            return Result<CreateEventCategoryResponseModel>.ValidationError("Fill up Category Name!");
        }
        else if (isCategoryNameExist(request.CategoryName))
        {
            return Result<CreateEventCategoryResponseModel>.ValidationError("CategoryName already exist!");
        }
        else
        {
            try
            {
                var newCategory = new TblEventcategory
                {
                    Eventcategoryid = Ulid.NewUlid().ToString(),
                    Eventcategorycode =  await _commonService.GenerateSequenceCode(EnumTableUniqueName.Tbl_EventCategory),
                    Categoryname = request.CategoryName,
                    Createdat = DateTime.Now,
                    Createdby = CreatedByUserId,
                    Deleteflag = false
                };
                var entry = await _db.TblEventcategories.AddAsync(newCategory);
                await _db.SaveAndDetachAsync();

                var createdEventCategory = entry.Entity;
                var model = new CreateEventCategoryResponseModel
                {
                    EventCategoryid = createdEventCategory.Eventcategoryid,
                    EventCategorycode= createdEventCategory.Eventcategorycode,
                    Categoryname = createdEventCategory.Categoryname,
                    Createdat = createdEventCategory.Createdat,
                    Createdby = createdEventCategory.Createdby
                };

                return Result<CreateEventCategoryResponseModel>.Success(model, "New Category Created");
            }
            catch (Exception ex)
            {
                _logger.LogExceptionError(ex);
                return Result<CreateEventCategoryResponseModel>.SystemError(ex.Message);
            }
        }
    }

    #endregion

    #region Update Category

    public async Task<Result<UpdateEventCategoryResponseModel>> UpdateCategory(EventCategoryRequestModel request)
    {
        
        if (request.AdminCode.IsNullOrEmpty())
        {
            return Result<UpdateEventCategoryResponseModel>.ValidationError("Admin not found");
        }
        else if (request.CategoryName.IsNullOrEmpty())
        {
            return Result<UpdateEventCategoryResponseModel>.ValidationError("Category name not found");
        }
        else
        {
            try
            {
                var existingCategory = await _db.TblEventcategories
                                        .FirstOrDefaultAsync(
                                            x => x.Eventcategorycode == request.EventCategoryCode &&
                                            x.Deleteflag == false
                                        );
                if (existingCategory is null)
                {
                    return Result<UpdateEventCategoryResponseModel>.NotFoundError("Category name not found");
                }

                existingCategory.Categoryname = request.CategoryName;
                existingCategory.Modifiedby = CreatedByUserId;
                existingCategory.Modifiedat = DateTime.Now;
                var entry = _db.TblEventcategories.Update(existingCategory);
                await _db.SaveAndDetachAsync();

                var updateEventCategory = entry.Entity;
                var model = new UpdateEventCategoryResponseModel
                {
                    EventCategoryid = updateEventCategory.Eventcategoryid,
                    EventCategorycode = updateEventCategory.Eventcategorycode,
                    Categoryname = updateEventCategory.Categoryname,
                    Modifiedat = updateEventCategory.Modifiedat,
                    Modifiedby = updateEventCategory.Modifiedby
                };

                return Result<UpdateEventCategoryResponseModel>.Success(model, "Category Name updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogExceptionError(ex);
                return Result<UpdateEventCategoryResponseModel>.SystemError(ex.Message);
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
                return Result<EventCategoryResponseModel>.NotFoundError($"No Event Category Found with Code: {categoryCode}");
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


    #endregion
}