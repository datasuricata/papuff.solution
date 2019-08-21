using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;

namespace papuff.domain.Notifications.Events {

    public class EventNotifier : IEventNotifier {

        #region [ parameters ]

        private Notifier Notifier;
        private bool _disposed;

        #endregion

        #region [ ctor ]

        public EventNotifier() => Notifier = new Notifier();

        #endregion

        #region [ methods ]

        // use for fast validations
        public void When<N>(bool hasError, string message) {
            if (hasError)
                Notifier.Notifications
                    .Add(new Notification {
                        Key = typeof(N).Name,
                        Value = message,
                        StatusCode = 400
                    });
        }

        public void Add<N>(string message) => Notifier.Notifications.Add(new Notification { Key = typeof(N).Name, Value = message, StatusCode = 400 });

        public void AddException<N>(string message, Exception exception = null) {
            var stack = new StackTrace(exception);
            var frames = stack.GetFrames();

            string[] trace = new string[frames.Count()];
            string[] lines;

            int i = 0;

            string ex = exception.InnerException?.InnerException == null ? exception.Message : exception.InnerException.InnerException.Message;

            if (ex.Contains(Environment.NewLine))
                lines = ex.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            else
                lines = new string[] { ex };

            foreach (var x in frames)
                trace[i++] = $"{i}. ↓ {x.GetMethod().Name}";

            Notifier.Notifications.Add(new Notification {
                Key = typeof(N).Name,
                Value = message,
                Exception = lines,
                StatusCode = 500,
                Call = trace
            });
        }

        public bool IsValid => !Notifier.HasAny;

        public IEnumerable<Notification> GetNotifications() => Notifier.Notifications.AsEnumerable();

        #endregion

        #region [ dispose ]

        protected virtual void Dispose(bool disposing) {
            if (!this._disposed)
                if (disposing)
                    Notifier.Notifications.Clear();

            this._disposed = true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
