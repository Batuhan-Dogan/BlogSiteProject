using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos.Classes;
using ProgrammersBlog.Entities.Dtos.Records;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Abstract
{
    public interface ICategoryService: IDataCountService
    {
        /// <summary>
        /// Kategori geri döner
        /// </summary>
        /// <param name="categoryId">Kategori ID değeri</param>
        /// <returns>CategoryDtoForGetOne türünde işlem sonucu. </returns>
        Task<IDataResult<CategoryDtoForGetOne>> GetAsync(int categoryId);

        /// <summary>
        /// Bütün kategorileri geirir
        /// </summary>
        /// <returns>CategoryListDto türünde işlem sonucu</returns>
        Task<IDataResult<CategoryListDto>> GetAllAsync();

        /// <summary>
        /// IDeleted değerine göre bütün Kategorileri getirir
        /// </summary>
        /// <param name="isDeleted">Makalenin silinmişlik durumu</param>
        /// <returns>İşlem sonucunu taşıyan CategoryListDto sonucu</returns>
        Task<IDataResult<CategoryListDto>> GetAllByIsDeletedAsync(bool isDeleted);

        /// <summary>
        /// Veritabanına yeni kategori ekler
        /// </summary>
        /// <param name="categoryDto">Yeni kategorinin değerlerini taşıyan record type</param>
        /// <param name="createdByName">Kategori oluşturucusunun adı</param>
        /// <returns>İşlem sonucunu taşıyan IResult</returns>
        Task<IResult> AddAsync(CategoryAddDto categoryDto,string createdByName);

        /// <summary>
        /// Kategoriyi günceller
        /// </summary>
        /// <param name="categoryDto">Güncel kategori verilerini tutan record type</param>
        /// <param name="modifiedByName">İşlemi yapan kişinin string değeri</param>
        /// <returns>İşlem sonucunu taşıyan CategoryDtoForGetOne sonucu</returns>
        Task<IDataResult<CategoryDtoForGetOne>> UpdateAsync(CategoryUpdateDto categoryDto, string modifiedByName);

        /// <summary>
        /// Veritabaında kategoriyi silinmiş hale getirir
        /// </summary>
        /// <param name="categoryId">Silinmesi gereken Category'nin ID değeri</param>
        /// <param name="modifiedByName"></param>
        /// <returns>İşlemi yapan kişinin string değeri</returns>
        Task<IResult> DeleteAsync(int categoryId, string modifiedByName);

        /// <summary>
        /// Veritabanından kategoriyi kaldırır
        /// </summary>
        /// <param name="CategoryId">Kaldırılacak kategorinin ID değeri</param>
        /// <returns>İşlem sonucunu taşıyan IResult değeri</returns>
        Task<IResult> HardDeleteAsync(int CategoryId);

    }
}
