namespace Synker.Infrastructure
{
    using Synker.Application.Interfaces;
    using Synker.Application.Notifications;
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class NotificationService : INotificationService
    {
        public Task SendAsync(Message message)
        {
            Debug.WriteLine(message);
            return Task.CompletedTask;
        }
    }
}
