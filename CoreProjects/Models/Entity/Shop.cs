using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Entity
{
    [Table("Shop")]
    public class Shop
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string ShopName { get; set; }
        public double ShopPrice { get; set; }
        public bool? States { get; set; }
        public int? ShopType { get; set; }
    }
}
