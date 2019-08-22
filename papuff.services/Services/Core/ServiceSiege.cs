using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using papuff.domain.Arguments.Sieges;
using papuff.domain.Core.Enums;
using papuff.domain.Core.Sieges;
using papuff.domain.Core.Users;
using papuff.domain.Helpers;
using papuff.domain.Interfaces.Services.Core;
using papuff.domain.Interfaces.Services.Swap;
using papuff.services.Hubs;
using papuff.services.Services.Base;
using papuff.services.Validators.Core.Sieges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace papuff.services.Services.Core {
    public class ServiceSiege : ServiceApp<Siege>, IServiceSiege {

        #region - attributes -

        private readonly ISwapSiege _swap;
        private readonly IHubContext<NotifyHub> _hub;

        #endregion

        #region - ctor -

        public ServiceSiege(IServiceProvider provider, ISwapSiege swap, IHubContext<NotifyHub> hub) : base(provider) {
            _swap = swap;
            _hub = hub;
        }

        #endregion

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

        public async Task Close(string id, string logged) {

            var siege = await repository.GetByIdAsync(id);
            Notifier.When<ServiceSiege>(siege.OwnerId != logged, 
                "Somente o proprietário pode fechar o grupo");

            if (Notifier.IsValid) {
                siege.Ended = DateTime.UtcNow;
                _swap.Close(id);
                repository.Update(siege);
            }
        }

        public IEnumerable<Siege> ReceiveEntry(LocationRequest request, User logged) {

            var sieges = _swap.ListAvaiables()
                .Where(s => new Coordinate(s.Latitude, s.Longitude)
                .DistanceTo(new Coordinate(request.Latitude, request.Longitude)) <= s.Range);

            if (sieges.Any()) {
                Notifier.When<ServiceSiege>(new[] { CurrentStage.Recused, CurrentStage.Blocked }.Contains(logged.General.Stage),
                 "Existe pendencias em, entre em contato com o suporte.");

                if (Notifier.IsValid)
                    return _swap.CheckIn(sieges, logged);
            }

            return null;
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