using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Data_Photo_Album
{
    public static class MD5Extensions
    {
        public static string ToMD5(this string str)
        {
            using (var cryptoMD5 = System.Security.Cryptography.MD5.Create())
            {
                //將字串編碼成 UTF8 位元組陣列
                var bytes = Encoding.UTF8.GetBytes(str);

                //取得雜湊值位元組陣列
                var hash = cryptoMD5.ComputeHash(bytes);

                //取得 MD5
                var md5 = BitConverter.ToString(hash)
                    .Replace("-", String.Empty)
                    .ToUpper();

                return md5;
            }
        }
    }
    public class AlbumContext : DbContext
    {
        public AlbumContext(DbContextOptions<AlbumContext> options) : base(options) { }
        public DbSet<Photo> Photos{ get; set; }
        public DbSet<User> Users { get; set; }
    }

    public class Photo : BaseDate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string FileLocation { get; set; }
    }

    public class User
    {
        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AuthToken { get; set; }

        public string GetAuthToken(string email, string password)
        {
            return $"{email}{DateTime.UtcNow}".ToMD5();
        }
    }

    public class BaseDate
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
