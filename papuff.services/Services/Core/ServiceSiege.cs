using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using papuff.domain.Arguments.Sieges;
using papuff.domain.Core.Sieges;
using papuff.domain.Interfaces.Services.Core;
using papuff.domain.Interfaces.Services.Swap;
using papuff.services.Hubs;
using papuff.services.Services.Base;
using papuff.services.Validators.Core.Sieges;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace papuff.services.Services.Core {
    public class ServiceSiege : ServiceApp<Siege>, IServiceSiege {

        private readonly ISwapSiege _swap;
        private readonly IHubContext<NotifyHub> _hub;

        public ServiceSiege(IServiceProvider provider, ISwapSiege swap, IHubContext<NotifyHub> hub) : base(provider) {
            _swap = swap;
            _hub = hub;
        }

        public Siege GetById(string id) => _swap.GetById(id);

        public IEnumerable<Siege> ListSieges() => _swap.ListSieges();

        public async Task Register(SiegeRequest request) {
            new SiegeValidator().Validate(request);
            var siege = new Siege(request.Visibility, request.Title, request.Description,
                request.ImageUri, request.Latitude, request.Longitude, 
                request.Range, request.Seconds, request.OwnerId);

            _swap.Add(siege);

            #region - events -

            siege.OnAvaiable += Siege_OnStart;
            siege.OnOpen += Siege_OnOpen;
            siege.OnAds += Siege_OnAds;
            siege.OnEnd += Siege_OnEnd;

            #endregion

            siege.Init();

            await repository.RegisterAsync(siege);
        }

        public async Task Close(string id) {
            _swap.Close(id);
            var siege = await repository.GetByIdAsync(id);
            siege.Ended = DateTime.UtcNow;
            repository.Update(siege);
        }

        #region - sockets -

        private async void Siege_OnStart(Siege siege) {
            await _hub.Clients.All.SendAsync(nameof(Siege_OnStart),
                JsonConvert.SerializeObject(siege));
        }

        private async void Siege_OnOpen(Siege siege) {
            await _hub.Clients.All.SendAsync(nameof(Siege_OnOpen),
                JsonConvert.SerializeObject(siege));
        }

        private async void Siege_OnAds(Siege siege) {
            await _hub.Clients.Group(siege.Id).SendAsync(nameof(Siege_OnAds), 
                JsonConvert.SerializeObject(siege));
        }

        private async void Siege_OnEnd(Siege siege) {
            await _hub.Clients.Group(siege.Id).SendAsync(nameof(Siege_OnEnd),
                JsonConvert.SerializeObject(siege));
        }

        #endregion
    }
}