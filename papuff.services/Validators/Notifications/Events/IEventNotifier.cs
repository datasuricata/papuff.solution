using FluentValidation;
using System;
using System.Collections.Generic;

namespace papuff.services.Validators.Notifications.Events {
    public interface IEventNotifier : IDisposable {

        void Add<N>(string message);
        void When<N>(bool hasError, string message);
        void Validate<T>(T model, AbstractValidator<T> validator);
        void AddException<N>(string message, Exception exception = null);
        bool IsValid { get; }
        IEnumerable<Notification> GetNotifications();
    }
}
