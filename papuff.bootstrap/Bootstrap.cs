using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using papuff.datainfra.ORM;
using papuff.datainfra.Persistence;
using papuff.datainfra.Transaction;
using papuff.domain.Interfaces.Repositories;
using papuff.domain.Interfaces.Services.Base;
using papuff.domain.Interfaces.Services.Core;
using papuff.domain.Interfaces.Services.Swap;
using papuff.domain.Notifications.Events;
using papuff.services.Services.Base;
using papuff.services.Services.Core;
using papuff.services.Services.Swap;

namespace papuff.bootstrap {
    public static class Bootstrap {
        public static void Configure(IServiceCollection services, string conn) {
            #region - context -

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(conn));

            #endregion

            #region - kernel -

            services.AddScoped(typeof(IServiceBase), typeof(ServiceBase));
            services.AddScoped(typeof(IServiceApp<>), typeof(ServiceApp<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IEventNotifier), typeof(EventNotifier));

            #endregion

            #region - core -

            services.AddScoped(typeof(IServiceUser), typeof(ServiceUser));

            #endregion

            #region - swap -

            services.AddScoped(typeof(ISwapSiege), typeof(SwapSiege));

            #endregion
        }
    }
}