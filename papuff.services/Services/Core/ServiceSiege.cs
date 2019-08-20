using papuff.domain.Arguments.Sieges;
using papuff.domain.Core.Sieges;
using papuff.domain.Interfaces.Services.Core;
using papuff.domain.Interfaces.Services.Swap;
using papuff.services.Services.Base;
using papuff.services.Validators.Core.Sieges;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace papuff.services.Services.Core {
    public class ServiceSiege : ServiceApp<Siege>, IServiceSiege {

        private readonly ISwapSiege swap;

        public ServiceSiege(IServiceProvider provider, ISwapSiege swap) : base(provider) {
            this.swap = swap;
        }

        public Siege GetById(string id) {
            return swap.GetById(id);
        }

        public List<Siege> ListSieges() {
            return swap.ListSieges();
        }

        public async Task Register(SiegeRequest request) {
            var siege = new Siege(request.Visibility, request.Title, request.Description,
                request.ImageUri, request.Range, request.Start, request.Available, request.OwnerId);

            ValidEntity<SiegeValidator>(siege);
            swap.Add(siege);
            //RegisterEvents(siege); //todo events invoke
            //swap.Start(); //todo start Siege
            await repository.RegisterAsync(siege);
        }

        public async Task Close(string id) {
            swap.Close(id);
            //swap.End(); // todo end Siege
            var siege = await repository.GetByIdAsync(id);
            siege.Ended = DateTime.UtcNow;
            repository.Update(siege);
        }
    }
}