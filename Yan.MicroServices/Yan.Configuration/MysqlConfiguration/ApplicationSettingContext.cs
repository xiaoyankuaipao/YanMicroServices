using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Yan.Configuration.MysqlConfiguration
{
    /// <summary>
    /// 
    /// </summary>
    class ApplicationSettingContext: DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public ApplicationSettingContext(DbContextOptions options) : base(options)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<ApplicationSetting> Settings { get; set; }
    }


    [Table("ApplicationSettings")]
    public class ApplicationSetting
    {
        [Key]
        public int Id { get; set; }

        private string key;

        public string Key
        {
            get => key;
            set => key = value.ToLowerInvariant();
        }

        [Required]
        [MaxLength(512)]
        public string Value { get; set; }

        public ApplicationSetting() { }

        public ApplicationSetting(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }

}
