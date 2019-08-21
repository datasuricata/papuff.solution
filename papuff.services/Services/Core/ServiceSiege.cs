using papuff.domain.Arguments.Sieges;
using papuff.domain.Core.Sieges;
using papuff.domain.Interfaces.Services.Core;
using papuff.domain.Interfaces.Services.Swap;
using papuff.services.Services.Base;
using papuff.services.Validators.Core.Sieges;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using papuff.domain.Core.Users;

namespace papuff.services.Services.Core {
    public class ServiceSiege : ServiceApp<Siege>, IServiceSiege {

        private readonly ISwapSiege _swap;

        public ServiceSiege(IServiceProvider provider, ISwapSiege swap) : base(provider) {
            _swap = swap;
        }

        public Siege GetById(string id) {
            return _swap.GetById(id);
        }

        public List<Siege> ListSieges() {
            return _swap.ListSieges();
        }

        public async Task Register(SiegeRequest request) {
            var siege = new Siege(request.Visibility, request.Title, request.Description,
                request.ImageUri, request.Range, request.Start, request.Available, request.OwnerId);

            ValidEntity<SiegeValidator>(siege);
            _swap.Add(siege);
            //RegisterEvents(siege); //todo events invoke
            //swap.Start(); //todo start Siege
            await repository.RegisterAsync(siege);
        }

        public async Task Close(string id) {
            _swap.Close(id);
            //swap.End(); // todo end Siege
            var siege = await repository.GetByIdAsync(id);
            siege.Ended = DateTime.UtcNow;
            repository.Update(siege);
        }

        public async Task ReceiveUser(string id, User logged) {

            // todo valid user
            _swap.ReceiveUser(id, logged);
            // todo push notification new entry
        }

        public async Task RemoveUser(string id, User logged) {
            
            // todo valid user
            _swap.RemoveUser(id, logged);
        }
    }
}