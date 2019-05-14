namespace Synker.Infrastructure
{
    using Synker.Application.Interfaces;
    using Synker.Application.Notifications;
    using System.Threading.Tasks;

    public class NotificationService : INotificationService
    {
        public Task SendAsync(Message message)
        {
            return Task.CompletedTask;
        }
    }
}
