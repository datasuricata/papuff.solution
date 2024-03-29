﻿using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using papuff.domain.Arguments.Companies;
using papuff.domain.Arguments.Sieges;
using papuff.domain.Core.Enums;
using papuff.domain.Core.Sieges;
using papuff.domain.Core.Users;
using papuff.domain.Helpers;
using papuff.domain.Interfaces.Repositories;
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
    public class ServiceSiege : ServiceBase, IServiceSiege {

        #region - attributes -

        private readonly ISwapSiege _swap;
        private readonly IRepository<Siege> _repoSiege;
        private readonly IRepository<Ticket> _repoTicket;
        private readonly IHubContext<NotifyHub> _hub;

        #endregion

        #region - ctor -

        public ServiceSiege(IServiceProvider provider,
            ISwapSiege swap,
            IHubContext<NotifyHub> hub,
            IRepository<Ticket> repoTicket,
            IRepository<Siege> repoSiege) : base(provider) {
            _swap = swap;
            _hub = hub;
            _repoSiege = repoSiege;
            _repoTicket = repoTicket;
        }

        #endregion

        #region - siege -

        public Siege GetById(string id) => _swap.GetById(id);

        public IEnumerable<Siege> ListSieges() => _swap.ListSieges();

        public IEnumerable<Siege> ReceiveEntry(LocationRequest request, User logged) {
            var sieges = _swap.ListAvaiables()
                .Where(s => new Coordinate(s.Latitude, s.Longitude)
                .DistanceTo(new Coordinate(request.Latitude, request.Longitude)) <= s.Range);

            if (sieges.Any()) {
                _notify.When<ServiceSiege>(new[] { CurrentStage.Recused, CurrentStage.Blocked }.Contains(logged.General.Stage),
                 "Entre em contato com o suporte para resolver pendencias com seu login.");

                if (_notify.IsValid)
                    return _swap.CheckIn(sieges, logged);
            }

            return null;
        }

        public async Task Create(SiegeRequest request) {
            new SiegeValidator().Validate(request);

            var siege = new Siege(request.Visibility, request.Title, request.Description,
                request.ImageUri, request.Latitude, request.Longitude, request.Range, request.Seconds, 
                request.OperationIn, request.OperationTime, request.OwnerId);

            if (_notify.IsValid) {
                _swap.AddSiege(siege);

                #region - events -

                siege.OnAvaiable += Siege_OnStart;
                siege.OnOpen += Siege_OnOpen;
                siege.OnAds += Siege_OnAds;
                siege.OnEnd += Siege_OnEnd;

                #endregion

                siege.Init();

                await _repoSiege.Register(siege);
            }
        }

        public async Task Close(string id, string logged) {
            var siege = await _repoSiege.ById(false, id);

            _notify.When<ServiceSiege>(siege.OwnerId != logged,
                "Somente o proprietário pode fechar o grupo");

            if (_notify.IsValid) {
                _swap.Close(id);
                _swap.Sync(id, siege);
                _repoSiege.Update(siege);
            }
        }

        public void ReceiveAds(AdsRequest request) {
            _notify.When<ServiceSiege>(_swap.IsOwner(request.SiegeId, request.OwnerId),
                "Somente o proprietário pode criar uma propaganda.");

            var ads = new Advertising(request.Title, request.Description,
                    request.ContentUri, request.RedirectUri);

            new AdsValidator().Validate(ads);

            if (_notify.IsValid)
                _swap.AddAds(request.SiegeId, ads);
        }

        #endregion

        #region - tickets -

        public async Task ReceiveTickets(string id, int range, TicketType type, int dateDue) {

            var tickets = new List<Ticket>();
            for (int i = 1; i > range; i++)
                tickets.Add(new Ticket(id, type, null, dateDue));

            tickets.ForEach(t => _notify.Validate(t, new TicketValidator()));

            if (_notify.IsValid) {
                _swap.AddTickets(id, tickets);
                await _repoTicket.RegisterRange(tickets);
            }
        }

        public async Task BindTicket(string id, string logged, TicketType type) {

            var ticket = await _repoTicket.By(true, t => t.Type == type && t.SiegeId == id && string.IsNullOrEmpty(t.UserId));

            #region - validator -

            _notify.When<ServiceSiege>(ticket == null,
                "Sem tickets disponiveis.");

            _notify.When<ServiceSiege>(string.IsNullOrEmpty(logged),
                "Sem usuário para ser vinculado");

            #endregion

            ticket.AssignTicket(logged);

            if (_notify.IsValid) {
                _swap.AssignTicket(id, ticket.Id, logged);
                _repoTicket.Update(ticket);
            }
        }

        #endregion

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