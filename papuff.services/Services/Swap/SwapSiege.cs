﻿using papuff.domain.Core.Enums;
using papuff.domain.Core.Sieges;
using papuff.domain.Core.Users;
using papuff.domain.Interfaces.Services.Swap;
using System.Collections.Generic;
using System.Linq;

namespace papuff.services.Services.Swap {
    public sealed class SwapSiege : ISwapSiege {

        private readonly List<Siege> Sieges = new List<Siege>();
        private static readonly object _lock = new object();

        public Siege GetById(string id) {
            return Sieges.FirstOrDefault(x => x.Id == id);
        }


        public IEnumerable<Siege> ListSieges() {
            return Sieges;
        }

        public IEnumerable<Siege> ListAvaiables() {
            return Sieges.Where(s => new[] { SiegeStatus.Available, SiegeStatus.Opened }.Contains(s.Status));
        }

        public IEnumerable<Siege> CheckIn(IEnumerable<Siege> sieges, User logged) {

            var accessible = sieges.Where(s => !s.Users.Any(u => u.Id == logged.Id) && s.Visibility == VisibilityType.Public);

            lock (_lock) {
                foreach (var s in accessible) {
                    s.Users.Add(logged);
                }
            }

            return accessible;
        }


        public void AddSiege(Siege siege) {
            lock (_lock) {
                Sieges.Add(siege);
            }
        }

        public void AddAds(string id, Advertising advertising) {

            var siege = GetById(id);

            lock (_lock) {
                siege.Advertising = advertising;
                siege.Push(true);
            }
        }

        public void AddTickets(string id, List<Ticket> tickets) {

            var siege = GetById(id);

            lock (_lock) {
                siege.Tickets.AddRange(tickets);
            }
        }

        public void AssignTicket(string id, string ticketId, string logged) {

            var siege = GetById(id);

            var ticket = siege.Tickets.FirstOrDefault(x => x.Id == ticketId);

            lock (_lock) {
                ticket.AssignTicket(logged);
            }
        }

        public void Close(string id) {

            var siege = GetById(id);

            lock (_lock) {
                siege.End(true);
            }
        }

        public void Sync(string id, Siege entity) {

            var siege = GetById(id);

            lock (_lock) {
                entity.Sync(siege);
            }
        }

        public void CheckOut(string id, string logged) {

            var siege = GetById(id);

            lock (_lock) {
                siege.Users.RemoveAll(u => u.Id == logged);
            }
        }

        public bool IsOwner(string id, string OwnerId) {
            return Sieges.Exists(s => s.Id == id && s.OwnerId == OwnerId);
        }
    }
}