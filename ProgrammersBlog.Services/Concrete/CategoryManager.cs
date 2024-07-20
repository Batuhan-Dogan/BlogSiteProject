using AutoMapper;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Data.Concrete;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos.Classes;
using ProgrammersBlog.Entities.Dtos.Records;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Services.Utilities;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Concrete
{
    public class CategoryManager : ManagerBase, ICategoryService
    {

        # region Constractors
        public CategoryManager(IUnitOfWork unitOfWork,IMapper mapper):base(mapper,unitOfWork)
        {

        }
        #endregion

        #region Methods
        public async Task<IResult> AddAsync(CategoryAddDto categoryDto, string createdByName)
        {
            Category category = Mapper.Map<Category>(categoryDto);
            category.CreatedByName = createdByName;
            category.ModifiedByName = createdByName;

            var categoryAddedDto = await UnitOfWork.CategoryRepository.AddAsync(category);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStatus.Success ,Message.CategoryMessages.Added(category.Name,true));
        }
        public async Task<IResult> DeleteAsync(int categoryId, string createdByName)
        {
            Category category = await UnitOfWork.CategoryRepository.GetAsync(c => c.Id.Equals(categoryId));
            if (category == null)
            {
                return new Result(ResultStatus.Error, Message.CategoryMessages.NotFound(isPlural: false));
            }
            if (category.IsDeleted.Equals(true))
            {
                return new Result(ResultStatus.Warning,Message.CategoryMessages.AlreadyDeleted(category.Name));
            }
            category.IsDeleted = true;
            category.IsActive = false;
            category.CreatedByName = createdByName;
            category.CreatedTime = DateTime.Now.ToUniversalTime();
            try
            {
                await UnitOfWork.CategoryRepository.UpdateAsync(category);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Message.CategoryMessages.Deleted(category.Name,true));
            }
            catch
            {
                return new Result(ResultStatus.Error, Message.CategoryMessages.Deleted(category.Name, false));
            }
        }
        public async Task<IDataResult<CategoryDtoForGetOne>> GetAsync(int categoryId)
        {
            var category = await UnitOfWork.CategoryRepository.GetAsync(c=>c.Id.Equals(categoryId));
            if(category is not null)
            {
                return new DataResult<CategoryDtoForGetOne>(ResultStatus.Success, new CategoryDtoForGetOne()
                {
                    Category = category,
                    ResultStatus=ResultStatus.Success,
                }) ;
            }
            return new DataResult<CategoryDtoForGetOne>(ResultStatus.Error, Message.CategoryMessages.NotFound(isPlural: false), new CategoryDtoForGetOne()
            {
                Category = null,
                ResultStatus=ResultStatus.Error,
                Message = Message.CategoryMessages.NotFound(isPlural: false)
            }) ;
        }

        public async Task<IDataResult<CategoryListDto>> GetAllAsync()
        {
            var categories= await UnitOfWork.CategoryRepository.GetAllAsync(null);
            if (categories.Count>-1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto() { Categories=categories,ResultStatus=ResultStatus.Success});
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, Message.CategoryMessages.NotFound(isPlural: true), new CategoryListDto() 
                {
                    Categories=null,
                    ResultStatus=ResultStatus.Error,Message = Message.CategoryMessages.NotFound(isPlural: true) 
                }
            );
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByIsDeletedAsync(bool isDeleted)
        {
            var categories =await UnitOfWork.CategoryRepository.GetAllAsync(c=>c.IsDeleted.Equals(isDeleted));
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto() { Categories=categories,ResultStatus=ResultStatus.Success});
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, Message.CategoryMessages.NotFound(isPlural: true), null);
        }

        public async Task<IResult> HardDeleteAsync(int CategoryId)
        {
            Category category = await UnitOfWork.CategoryRepository.GetAsync(c => c.Id.Equals(CategoryId));
            if (category != null)
            {
                await UnitOfWork.CategoryRepository.DeleteAsync(category);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success,Message.CategoryMessages.HardDeleted(category.Name,true) );
            }
            return new Result(ResultStatus.Error,  Message.CategoryMessages.NotFound(isPlural:false));
        }
        public async Task<IDataResult<CategoryDtoForGetOne>> UpdateAsync(CategoryUpdateDto categoryDto, string createdByName)
        {
            Category category = Mapper.Map<Category>(categoryDto);
            category.ModifiedByName = createdByName;
            category.CreatedByName= createdByName;
            try
            {
                var categoryUpdatedDto= await UnitOfWork.CategoryRepository.UpdateAsync(category);
                await UnitOfWork.SaveAsync();
                return new DataResult<CategoryDtoForGetOne>(ResultStatus.Success, new CategoryDtoForGetOne() { Category = categoryUpdatedDto, ResultStatus = ResultStatus.Success, Message = Message.CategoryMessages.Updated(category.Name, true) });
            }
            catch
            {
                return new DataResult<CategoryDtoForGetOne>(ResultStatus.Error, Message.CategoryMessages.Updated(category.Name, false), null);
            }
        }


        public async Task<int> GetActiveDataCountAsync()
        {
            return await UnitOfWork.CategoryRepository.CountAsync(c => c.IsActive.Equals(true));
        }

        public async Task<int> GetDeletedDataCountAsync()
        {
            return await UnitOfWork.CategoryRepository.CountAsync(c=>c.IsDeleted.Equals(true));
        }

        public async Task<int> CountAsync()
        {
            return await UnitOfWork.CategoryRepository.CountAsync();
        }

        #endregion
    }
}
