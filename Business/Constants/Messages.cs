using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        //Tag
        internal static string TagListed = "Etiketler listelendi";
        internal static string TagAdded = "Etiket eklendi";
        internal static string TagUpdated = "Etiket güncellendi";
        internal static string TagDeleted = "Etiket silindi";
        internal static string GetByTag = "Etiket görüntülendi";

        //Blog
        internal static string BlogListed = "Bloglar listelendi";
        internal static string BlogAdded = "Blog eklendi";
        internal static string BlogUpdated = "Blog güncellendi";
        internal static string BlogDeleted = "Blog silindi";
        internal static string BlogDetails = "Blogların detayları görüntülendi";
        internal static string BlogListedByTagId = "Etiket Id'sine göre bloglar listelendi";
        internal static string BlogListedByTagName = "Etiket ismine göre bloglar listelend";

        //BlogImage
        internal static string OverflowBlogImageMessage = "Blogun 5 ten fazla resmi olamaz";
        internal static string BlogImageAdded = "Blog resmi başarı ile eklendi";
        internal static string BlogImageNotFound = "Resim bulunamadı";
        internal static string BlogImageDeleted = "Blog resmi silindi";
        internal static string BlogImagesListed = "Blogların resimleri listelendi";
        internal static string BlogImageListed = "Blogun resmi görüntülendi";
        internal static string BlogImageUpdated = "Blogun resmi güncellendi";
        internal static string BlogImageLimitExceeded = "Fotoğraf yükleme limitini aştınız (=5)";

        //Author
        internal static string AuthorList = "Yazarlar listelendi";
        internal static string AuthorAdd = "Yazar eklendi";
        internal static string AuthorUpdate = "Yazar güncellendi";
        internal static string AuthorDelete = "Yazar silindi";

        //User
        internal static string UserRegistered = "Kullanıcı başarıyla oluşturuldu.";
        internal static string UsersListed = "Kullanıcılar listelendi";
        internal static string UserNotFound = "Kullanıcı Bulunamadı.";
        internal static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        internal static string PasswordError = "Şifre Hatalı";
        internal static string SuccessfulLogin = "Sisteme giriş başarılı";

        //Auth
        internal static string AuthorizationDenied = "Yetkiniz yok.";
        internal static string AccessTokenCreated = "Giriş Başarılı";
    }
}