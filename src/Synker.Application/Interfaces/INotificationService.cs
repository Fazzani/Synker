namespace Synker.Application.Interfaces
{
    using Synker.Application.Notifications;
    using System.Threading.Tasks;
    public interface INotificationService
    {
        Task SendAsync(Message message);
    }
}
