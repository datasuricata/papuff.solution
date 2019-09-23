using papuff.domain.Core.Base;
using papuff.domain.Core.Enums;
using papuff.domain.Core.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Timers;

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

        public VisibilityType Visibility { get; private set; }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ImageUri { get; private set; }

        public double Range { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public int OperationIn { get; private set; }
        public int OperationTime { get; private set; }

        public int Ads { get; private set; }

        public DateTime Available { get; private set; }
        public DateTime? Start { get; private set; }
        public DateTime? Ended { get; private set; }

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
                    return (int)result?.TotalMilliseconds;

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

        [NotMapped]
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();

        #endregion

        #region - ctor -

        protected Siege() { }

        public Siege(VisibilityType visibility, string title, string description, string imageUri, double latitude, double longitude, double range, int seconds, int operationIn, int operationTime, string ownerId) {
            Visibility = visibility;
            Title = title;
            Description = description;
            ImageUri = imageUri;
            Latitude = latitude;
            Longitude = longitude;
            Range = range;
            OperationIn = operationIn;
            OperationTime = OperationTime;
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

        public void Open(bool invoke) {
            lock (_lock) {
                Start = DateTime.UtcNow;

                if (invoke)
                    OnOpen?.Invoke(this);
            }
        }

        public void Push(bool invoke) {

            _ads = new Timer(new TimeSpan(0, 0, 20).TotalMilliseconds) { AutoReset = false };
            _ads.Elapsed += Ads_Elapsed;
            _ads.Start();

            lock (_lock) {
                Ads++;

                if (invoke)
                    OnAds?.Invoke(this);
            }
        }

        public void End(bool invoke) {
            lock (_lock) {
                Ended = DateTime.UtcNow;

                if (invoke)
                    OnEnd?.Invoke(this);
            }
        }

        public void Sync(Siege siege) {

            Visibility = siege.Visibility;
            Title = siege.Title;
            Description = siege.Description;
            ImageUri = siege.ImageUri;
            Range = siege.Range;
            Ads = siege.Ads;
            Available = siege.Available;
            Start = siege.Start;
            Ended = siege.Ended;
        }

        public void SetOperation(int timeStart, int operationTime) {
            OperationIn = timeStart;
            OperationTime = OperationTime;
        }

        #endregion

        #region - events -

        private void Avaiable_Elapsed(object sender, ElapsedEventArgs e) {
            OnAvaiable?.Invoke(this);
        }

        private void Audit_Elapsed(object sender, ElapsedEventArgs e) {
            if (Users.Any())
                Open(true);
            else
                End(true);
        }

        private void Ads_Elapsed(object sender, ElapsedEventArgs e) {
            Advertising = null;

            _ads.Close();
            _ads.Dispose();
        }

        #endregion
    }
}