using papuff.domain.Core.Base;
using papuff.domain.Core.Enums;
using papuff.domain.Core.Users;
using System;
using System.Timers;

namespace papuff.domain.Core.Sieges {
    public class Ticket : EntityBase {

        #region - threads -

        private Timer _audit;

        private readonly object _lock = new object();

        #endregion

        #region - attributes -

        public User User { get; set; }
        public string UserId { get; set; }

        public Siege Siege { get; set; }
        public string SiegeId { get; set; }

        public string Hash { get; set; }
        public int DateDue { get; set; }

        public DateTime? AssignDate { get; set; }

        public TicketType Type { get; set; }

        public TicketStage Stage {
            get {
                if (IsDeleted)
                    return TicketStage.Closed;

                if (AssignDate.HasValue)
                    return TicketStage.Opened;

                return TicketStage.Opened;
            }
        }

        #endregion

        #region - ctor -

        protected Ticket() {
        }

        public Ticket(string siegeId, TicketType type, string hash, int dateDue) {
            SiegeId = siegeId;
            Hash = hash ?? CustomHash(16);
            DateDue = dateDue;
            Type = type;

            var expires = DateTime.Now.AddDays(dateDue).Millisecond;

            lock (_lock) {
                _audit = new Timer(expires) { AutoReset = false };
                _audit.Elapsed += Audit_Elapsed;
                _audit.Start();
            }
        }

        #endregion

        #region - methods -

        public void ProcessTicket() {
            IsDeleted = true;
        }

        public void AssignTicket(string userId) {
            UserId = userId;
            AssignDate = DateTime.UtcNow;
        }

        #endregion

        #region - events -

        private void Audit_Elapsed(object sender, ElapsedEventArgs e) {
            _audit.Close();
            _audit.Dispose();

            ProcessTicket();
        }

        #endregion
    }
}