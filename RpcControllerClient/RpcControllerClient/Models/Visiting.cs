using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RpcControllerClient.Models
{
    public class Visiting
    {
        [Key]
        [Display(Name = "ID посещения")]
        public int VisitingId { get; set; }


        [Required]
        [Display(Name = "Дата посещения")]
        public DateTime VisitingDateTime { get; set; }


        [Required]
        [Display(Name = "Направление")]
        public bool Direction {  get; set; }

        public int DevicesId { get; set; }
        [ForeignKey(nameof(DevicesId))]
        public Devices Devices { get; set; }

        public List<Pupils>? Pupils { get; set; } 
    }
}
