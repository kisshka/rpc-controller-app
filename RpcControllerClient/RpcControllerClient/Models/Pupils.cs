using System.ComponentModel.DataAnnotations;

namespace RpcControllerClient.Models
{
    public class Pupils
    {
        [Key]
        [Display(Name = "ID ученика")]
        public int PupilsId { get; set; }


        [Required]
        [Display(Name = "Фамилия")]
        public string SurnamePupil { get; set; }


        [Required]
        [Display(Name = "Имя")]
        public string NamePupil { get; set; }


        [Required]
        [Display(Name = "Отчество")]
        public string PatronymicPupil { get; set; }


        [Required]
        [Display(Name = "Номер класса")]
        public string ClassNumber { get; set; }


        [Required]
        [Display(Name = "Срок действия карты")]
        public DateTime CardValidityPeriod { get; set; }


        [Required]
        [Display(Name = "Номер карты")]
        public int CardNumber { get; set; }

        public List<Visiting>? Visitings { get; set; }
    }
}
