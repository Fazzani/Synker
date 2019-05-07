namespace Synker.Application.Playlists.Commands.Create
{
    using MediatR;
    using Synker.Application.Interfaces;
    using Synker.Application.Notifications;
    using System.Threading;
    using System.Threading.Tasks;
    public class PlaylistCreated : INotification
    {
        public long PlaylistId { get; set; }

        public class PlaylistCreatedHandler : INotificationHandler<PlaylistCreated>
        {
            private readonly INotificationService _notification;

            public PlaylistCreatedHandler(INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle(PlaylistCreated notification, CancellationToken cancellationToken)
            {
                await _notification.SendAsync(new Message { Origin = nameof(PlaylistCreated) });
            }
        }
    }
}
