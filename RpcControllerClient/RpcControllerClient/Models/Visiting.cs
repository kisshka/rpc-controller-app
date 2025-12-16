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
        public bool Direction { get; set; }

        [Required]
        public int DeviceId { get; set; }
        [ForeignKey(nameof(DeviceId))]
        public Devices? Device { get; set; }

        [Required]
        public int PupilId { get; set; }
        [ForeignKey(nameof(PupilId))]
        public Pupils? Pupil { get; set; }
    }
}
