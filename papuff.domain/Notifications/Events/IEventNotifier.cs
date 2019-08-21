using System;
using System.Collections.Generic;

namespace papuff.domain.Notifications.Events {
    public interface IEventNotifier : IDisposable {

        void Add<N>(string message);
        void When<N>(bool hasError, string message);
        void AddException<N>(string message, Exception exception = null);
        bool IsValid { get; }
        IEnumerable<Notification> GetNotifications();
    }
}
