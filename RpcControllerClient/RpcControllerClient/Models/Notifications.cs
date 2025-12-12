using AntDesign;

namespace RpcControllerClient.Models
{
    public class Notifications
    {
        private readonly INotificationService _notice;

        public Notifications(INotificationService notice)
        {
            _notice = notice ?? throw new ArgumentNullException(nameof(notice));
        }
        private async Task Notices(string title, NotificationType type, string message)
        {
            await _notice.Open(new NotificationConfig()
            {
                Message = title,
                Description = message,
                NotificationType = type,
            });
        }

        public async Task OnError(string message)
        {
            await Notices("Ошибка", NotificationType.Error, message);
        }
        public async Task OnSucccess(string message)
        {
            await Notices("Успех", NotificationType.Success, message);
        }
        public async Task OnInfo(string message)
        {
            await Notices("К сведению", NotificationType.Info, message);
        }
        public async Task OnWarning(string message)
        {
            await Notices("Предупреждение", NotificationType.Warning, message);
        }
        public async Task OnEvent(string message)
        {
            await Notices("Событие", NotificationType.Success, message);
        }
    }
}
