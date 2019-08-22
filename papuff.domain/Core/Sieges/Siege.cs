using papuff.domain.Core.Ads;
using papuff.domain.Core.Base;
using papuff.domain.Core.Enums;
using papuff.domain.Core.Users;
using System;
using System.Linq;
using System.Timers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace papuff.domain.Core.Sieges {
    public class Siege : EntityBase {

        #region - handlers -

        public delegate void SiegeHandler(Siege siege);

        public event SiegeHandler OnOpen;
        public event SiegeHandler OnAvaiable;
        public event SiegeHandler OnEnd;
        public event SiegeHandler OnAds;

        #endregion

        #region - threads -

        private Timer _audit;
        private Timer _available;
        private Timer _ads;

        private readonly object _lock = new object();

        #endregion

        #region - mapped attributes -

        public VisibilityType Visibility { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUri { get; set; }

        public double Range { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public DateTime Available { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? Ended { get; set; }

        public string OwnerId { get; set; }
        public User Owner { get; set; }

        #endregion

        #region - not mapped attributes -

        [NotMapped]
        public SiegeStatus Status {
            get {
                if (Ended.HasValue)
                    return SiegeStatus.Closed;

                if (Start.HasValue)
                    return SiegeStatus.Opened;

                var now = DateTime.UtcNow;

                return now > Available ? SiegeStatus.Available : SiegeStatus.Invisible;
            }
        }

        [NotMapped]
        public int TimeOpened {
            get {
                if (new[] { SiegeStatus.Closed, SiegeStatus.Opened }
                .Contains(Status))
                    return 0;

                var result = Start - DateTime.UtcNow;
                
                if (result?.TotalMilliseconds != null) 
                    return (int) result?.TotalMilliseconds;
                
                return 0;
            }
        }

        [NotMapped]
        public int TimeToAvailable {
            get {
                if (new[] { SiegeStatus.Closed, SiegeStatus.Opened, SiegeStatus.Invisible }
                .Contains(Status))
                    return 0;

                var result = Available - DateTime.UtcNow;
                return (int)result.TotalMilliseconds;
            }
        }

        [NotMapped]
        public Advertising Advertising { get; set; }

        [NotMapped]
        public List<User> Users { get; set; } = new List<User>();

        #endregion

        #region - ctor -

        protected Siege() { }

        public Siege(VisibilityType visibility, string title, string description, string imageUri, double latitude, double longitude, double range, int seconds, string ownerId) {
            Visibility = visibility;
            Title = title;
            Description = description;
            ImageUri = imageUri;
            Latitude = latitude;
            Longitude = longitude;
            Range = range;
            Available = DateTime.UtcNow.AddSeconds(seconds);
            OwnerId = ownerId;
        }

        #endregion

        #region - methods -

        public void Init() {
            lock (_lock) {

                var avaiable = Available - DateTime.UtcNow;
                var audit = Available - DateTime.UtcNow.AddDays(1);

                _available = new Timer(avaiable.TotalMilliseconds) { AutoReset = false };
                _audit = new Timer(audit.TotalMilliseconds) { AutoReset = false };

                _available.Elapsed += Avaiable_Elapsed;
                _audit.Elapsed += Audit_Elapsed;
                
                _available.Start();
                _audit.Start();
            }
        }

        public void Open() {
            lock (_lock) {
                Start = DateTime.UtcNow;
                OnOpen?.Invoke(this);
            }
        }

        public void End() {
            lock (_lock) {
                Ended = DateTime.UtcNow;
                OnEnd?.Invoke(this);
            }
        }

        #endregion

        #region - events -

        private void Avaiable_Elapsed(object sender, ElapsedEventArgs e) {
            OnAvaiable?.Invoke(this);
        }

        private void Audit_Elapsed(object sender, ElapsedEventArgs e) {
            if (Users.Any())
                Open();
            else
                End();
        }

        #endregion
    }
}