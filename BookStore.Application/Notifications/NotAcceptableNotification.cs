using BookStore.Shared.Notifications;

namespace BookStore.Application.Notifications
{
    class NotAcceptableNotification : NotificationBase
    {
        public NotAcceptableNotification(string message = "NotAcceptableNotification") : base(message) { }
    }
}
