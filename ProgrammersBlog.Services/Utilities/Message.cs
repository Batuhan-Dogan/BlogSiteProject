using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Utilities
{
    public static class Message
    {
        public static string InCorrectDataEntry()
        {
            return "Hatalı veri girişi yaptınız.";
        }
        public static class CategoryMessages
        {
            public static string NotFound(bool isPlural)
            {
                return isPlural ? "Hiçbir kategori bulunamadı." : "Böyle bir kategori bulunamadı";
            }
            public static string Added(string categoryName, bool successfully)
            {
                return successfully? $"{categoryName} kategorisi başarıyla eklendi": $"{categoryName} kategorisi oluşturulurken beklenmedik bir hata oluştu lütfen daha sonra tekrar deneyin.";
            }
            public static string AlreadyDeleted(string categoryName)
            {
                return $"{categoryName} kategorisi zaten silinmiş";
            }
            public static string Deleted(string categoryName, bool successfully)
            {
                return successfully? $"{categoryName} kategorisi başarıyla silindi": $"{categoryName} kategorisi silinirken beklenmedik bir hata oluştu lütfen daha sonra tekrar deneyin.";
            }
            public static string HardDeleted(string categoryName, bool successfully)
            {
                return successfully? $"{categoryName} kategorisi başarıyla veritabanından kaldırıldı.": $"{categoryName} kategorisi veritabanından kaldırılırken beklenmedik bir hata oluştu. Lütfen daha sonra tekrar deneyin";
            }
            public static string Updated(string categoryName,bool successfully)
            {
                return successfully? $"{categoryName} kategorisi başarıyla düzenlendi.": $"{categoryName} kategorisi denlenirken beklenmedik bir hata oluştu. Lütfen daha sonra tekrar deneyin.";
            }
        }
        public static class ArticleMessages
        {
            public static string NotFound(bool isPlural)
            {
                return isPlural ? "Hiçbir makale bulunamadı." : "Böyle bir makale bulunamadı";
            }
            public static string NotFoundByCategory()
            {
                return "Bu kategoriye ait hiçbir makale bulunamadı.";
            }
            public static string Added(string articleName)
            {
                return $"{articleName} makalesi başarıyla eklendi";
            }
            public static string Deleted(string articleName)
            {
                return $"{articleName} makalesi başarıyla silindi";
            }
            public static string HardDeleted(string articleName)
            {
                return $"{articleName} makalesi başarıyla veritabanından kaldırıldı.";
            }
            public static string Updated(string articleName)
            {
                return $"{articleName} makalesi başarıyla düzenlendi.";
            }
            public static string IncreasedViewCount()
            {
                return "Makale görünüm sayısı başarıyla arttırıldı";
            }
        }
        public static class UserMessages
        {
            public static string NotFound(bool isPlural)
            {
                return isPlural ? "Hiçbir kullanıcı bulunamadı." : "Böyle bir kullanıcı bulunamadı";
            }
            public static string PasswordChanged(bool successfully)
            {
                return successfully? "Şifreniz başarıyla değiştirildi.":"Şifre değiştirilirken beklenmedik bir hata oluştu lütfen daha sonra tekrar deneyin.";
            }
            public static string Added(string userName, bool successfully)
            {
                return successfully? $"{userName} isimli yeni kullanıcı başarıyla oluşturuldu":$"{userName} isimli yeni kullanıcı oluşturulurken malesef beklenmedik bir hata oluştu daha sonra tekrar deneyiniz."; 
            }
            public static string Deleted(string userName, bool successfully)
            {
                return successfully ? $"{userName} isimli kullanıcı başarıyla silindi" : $"{userName} isimli kullanıcı silinirken beklenmedik bir hata oluştu lütfen daha sonra tekrar deneyin.";
            }
            public static string CanNotBeDeletedLogInUser()
            {
                return "Giriş yapan kullanıcı kendi kaydını silemez";
            }
            public static string Updated(string userName, bool successfully)
            {
                return successfully? $"{userName} isimli kullanıcı başarıyla düzenlendi." : $"{userName} isimli kullanıcı düzenlenirken beklenmedik bir hata oluştu lütfen daha sonra tekrar deneyiniz.";
            }

        }
        public static class RoleMessages
        {
            public static string AddFailed() 
            {
                return "Yeni roller eklenirken beklenmedik bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }
            public static string UpdateFailed()
            {
                return "Roller güncellenirken beklenmedik bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }
        }
        public static class CommentMessages
        {
            public static string NotFound(bool isPlural)
            {
                return isPlural ? "Hiçbir yorum bulunamadı." : "Böyle bir yorum bulunamadı";
            }
            public static string Added()
            {
                return " Yeni yorum başarıyla eklendi";
            }
            public static string CouldNotAdd()
            {
                return $" Yorum eklenirken sorun oluştu";
            }
            public static string Deleted()
            {
                return "Seçili yorum başarıyla silindi";
            }
            public static string AlreadyDeleted()
            {
                return "Seçili yorum zaten silinmiş";
            }
            public static string Updated()
            {
                return "Yorum başarıyla güncellendi";
            }

        }
    }
}
