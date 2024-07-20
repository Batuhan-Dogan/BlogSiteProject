using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos.Classes;
using ProgrammersBlog.Entities.Dtos.Records;
using ProgrammersBlog.Entities.RequestParameters;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Abstract
{
    public interface IArticleService:IDataCountService
    {
        /// <summary>
        /// Makale ID'sine göre bir makale bilgilerini geri dönürür.
        /// </summary>
        /// <param name="articleId"> Getirilecek makalenin ID'si </param>
        /// <returns>İlgili makaleyi bir IDataResult ile geri döner</returns>
        Task<IDataResult<ArticleDtoForGetOne>> GetAsync(int articleId);

        /// <summary>
        /// Aktiflik durumuna göre, istenen makaleyi alır ve ilişkili kullanıcı ve kategori bilgilerini döndürür.
        /// </summary>
        /// <param name="articleId">Getirilecek makalenin ID'si.</param>
        /// <param name="isActive">Makalenin aktiflik durumu.</param>
        /// <returns>İlgili makaleye ait kullanıcı ve kategori bilgileriyle birlikte bir IDataResult sonucu.</returns>
        Task<IDataResult<ArticleDtoForGetOne>> GetArticleWithUserAndCategoryAsync(int articleId,bool isActive);

        /// <summary>
        /// Bütün Makale bilgilerini geri döndürür.
        /// </summary>
        /// <returns> Bütün makaleleri bulunduran bir IDataResult sonucu</returns>
        Task<IDataResult<ArticleListDto>> GetAllAsync();

        /// <summary>
        /// Aktiflik durumu ve ArticleRequestParameters filtresine göre istenilen makalenin seçilen propertyleriyle döndürür
        /// </summary>
        /// <param name="requestParameters">Makaleler getirilirken istenilen filtreler</param>
        /// <param name="isActive">Makalenin aktiflik durumu.</param>
        /// <returns>aktiflik durumu ve requestParametersla filtrelenmiş makalelerin IDataResult sonucu</returns>
        Task<IDataResult<ArticleListDto>> GetArticleSummariesByRequestParameters(ArticleRequestParameters requestParameters, bool isActive);

        /// <summary> 
        /// Aktiflik durumu ve ArticleRequestParameters filtresine göre istenilen makalenin seçilen propertyleriyle döndürür
        /// </summary>
        /// <param name="requestParameters">Makaleler getirilirken istenilen filtreler</param>
        /// <param name="isActive">Makalenin aktiflik durumu.</param>
        /// <returns>aktiflik durumu ve requestParametersla filtrelenmiş makalelerin IDataResult sonucu</returns>
        Task<IDataResult<ArticleListDto>> GetArticlesListPropertiesByRequestParameters(ArticleRequestParameters requestParameters,bool isActive);

        /// <summary>
        /// IsDeleted durumuna göre makaleleri getirir.
        /// </summary>
        /// <param name="isDeleted">Makalenin silinmişlik durumu</param>
        /// <returns>Silinmişlik durumuna göre filtrelenmiş IDataResult sonucu</returns>
        Task<IDataResult<ArticleListDto>> GetAllByIsDeletedAsync(bool isDeleted);

        /// <summary>
        /// CategoryId durumuna göre makaleleri getirir.
        /// </summary>
        /// <param name="categoryId">Makalenin Kategori Id değeri</param>
        /// <returns>Kategori Id değerine göre göre filtrelenmiş IDataResult sonucu</returns>
        Task<IDataResult<ArticleListDto>> GetAllByCategoryAsync(int categoryId);

        /// <summary>
        /// Veritabanına yeni makale ekler
        /// </summary>
        /// <param name="articleDto">Yeni makale bilgilerini taşıyan record type</param>
        /// <param name="createdByName">Makale yaratıcısının string değeri</param>
        /// <returns>İşlem sonucunu taşıyan IResult sonucu</returns>
        Task<IResult> AddAsync(ArticleAddDto articleDto, string createdByName);

        /// <summary>
        /// Veritabanındaki makaleyi günceller
        /// </summary>
        /// <param name="articleDto">Güncellenecek makale bilgilerini taşıyan record type</param>
        /// <param name="modifiedByName">Makalede değişim yapanın string değeri</param>
        /// <returns>İşlem sonucunu taşıyan IResult sonucu</returns>
        Task<IResult> UpdateAsync(ArticleUpdateDto articleDto, string modifiedByName);

        /// <summary>
        /// Veritabanındaki makaleyi silinmiş duruma getirir
        /// </summary>
        /// <param name="articleId">Silinecek makalenin ID değeri</param>
        /// <param name="modifiedByName">Makalede değişim yapanın string değeri</param>
        /// <returns>İşlem sonucunu taşıyan IResult sonucu</returns>
        Task<IResult> DeleteAsync(int articleId, string modifiedByName);

        /// <summary>
        /// Makaleyi vertabanından kaldırır
        /// </summary>
        /// <param name="articleId">Silinecek makalenin ID değeri</param>
        /// <returns>İşlem sonucunu taşıyan IResult sonucu</returns>
        Task<IResult> HardDeleteAsync(int articleId);


        Task<int> GetAllSelectedMembersCountsync(ArticleRequestParameters requestParameters);

        /// <summary>
        /// Makalenin görüntülenme sayısını arttırır
        /// </summary>
        /// <param name="articleId">Görüntülenme sayı arttırılacak makale</param>
        /// <returns>İşlem sonucunu taşıyan IResult sonucu</returns>
        Task<IResult> IncreaseViewCountAsync(int articleId);

    }
}
