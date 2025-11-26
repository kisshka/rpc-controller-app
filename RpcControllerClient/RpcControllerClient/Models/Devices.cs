using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RpcControllerClient.Models
{
    public class Devices
    {
        [Key]
        [Display(Name = "ID устройства")]
        public int DevicesId { get; set; }

        [Required]
        [NotNull]
        [Display(Name = "Название устройства")]
        public string DeviceName { get; set; }


        [Required]
        [NotNull]
        [Display(Name = "Статус устройства(активен/не активен)")]
        public bool DeviceStatus { get; set; }
    }
}
