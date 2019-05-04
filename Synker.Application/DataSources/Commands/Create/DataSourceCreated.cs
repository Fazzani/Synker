namespace Synker.Application.DataSources.Commands.Create
{
    using MediatR;
    using Synker.Application.Interfaces;
    using Synker.Application.Notifications;
    using System.Threading;
    using System.Threading.Tasks;

    public class DataSourceCreated : INotification
    {
        public long DataSourceId { get; set; }

        public class DataSourceCreatedHandler : INotificationHandler<DataSourceCreated>
        {
            private readonly INotificationService _notification;

            public DataSourceCreatedHandler(INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle(DataSourceCreated notification, CancellationToken cancellationToken)
            {
                await _notification.SendAsync(new Message { Origin = nameof(DataSourceCreated) });
            }
        }
    }
}
