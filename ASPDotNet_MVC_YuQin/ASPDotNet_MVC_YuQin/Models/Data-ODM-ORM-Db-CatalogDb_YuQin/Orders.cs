using System;
using System.ComponentModel.DataAnnotations;
namespace CatalogDb_YuQin.DB.Models
{
    public class Orders : Object
    {
        [Key]
        public Int32 OrderID { get; set; }       
        public String BuyerID { get; set; } // 一一对应Identity_YuQin数据库的Identity_YuQin表的UserName
        public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.Now;
    }
}