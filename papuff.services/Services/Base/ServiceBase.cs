using papuff.datainfra.Transaction;
using papuff.domain.Interfaces.Services.Base;
using papuff.domain.Notifications.Events;
using System;
using System.Threading.Tasks;

namespace papuff.services.Services.Base {
    public class ServiceBase : IServiceBase {

        #region [ attributes ]

        private readonly IUnitOfWork _uow;
        
        protected readonly IEventNotifier Notifier;
        //protected readonly ILogEvent Log;
        
        #endregion

        #region [ ctor ]

        public ServiceBase(IServiceProvider provider) {
            _uow = (IUnitOfWork)provider.GetService(typeof(IUnitOfWork));
            Notifier = (IEventNotifier)provider.GetService(typeof(IEventNotifier));
        }

        #endregion

        #region [ persistence ]

        public async Task Commit() {
            if (!Notifier.IsValid)
                return;

            await _uow.Commit();
        }

        public async Task CommitForce() => await _uow.Commit();

        #endregion
    }
}
