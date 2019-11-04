using papuff.domain.Interfaces.Services.Core;
using papuff.services.Services.Base;
using System;
using System.Threading.Tasks;

namespace papuff.services.Services.Core {
    public class ServiceGuide : ServiceBase, IServiceGuide {
        public ServiceGuide(IServiceProvider provider) : base(provider) {
        }

        public async Task FirsStep() {

        }
    }
}
