using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class BillProducts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Opciono, ukoliko želite automatsko generisanje ključa
        public int Id { get; set; }
        public int BillId { get; set; }     
        public string ProductName { get; set; }      
        public float Price { get; set; }
    }
}
