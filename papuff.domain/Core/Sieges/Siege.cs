using papuff.domain.Core.Ads;
using papuff.domain.Core.Base;
using papuff.domain.Core.Enums;
using papuff.domain.Core.Users;
using System;
using System.Collections.Generic;
using System.Threading;

namespace papuff.domain.Core.Sieges {
    public class Siege : EntityBase {

        public delegate void SiegeHandler(Siege siege);

        public event SiegeHandler OnOpen;
        public event SiegeHandler OnAvaiable;
        public event SiegeHandler OnEnd;
        public event SiegeHandler OnAds;

        private readonly Timer _available;
        private readonly Timer _ads;

        private readonly object _lock = new object();

        public SiegeStatus Status {
            get {
                if (Ended.HasValue)
                    return SiegeStatus.Closed;

                var now = DateTime.UtcNow;

                if (now > Start)
                    return SiegeStatus.Opened;

                if (now > Available)
                    return SiegeStatus.Available;

                return SiegeStatus.Invisible;
            }
        }

        public VisibilityType Visibility { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUri { get; set; }

        public double Range { get; set; }

        public DateTime Start { get; set; }
        public DateTime Available { get; set; }
        public DateTime? Ended { get; set; }

        public string OwnerId { get; set; }
        public User Owner { get; set; }


        public Advertising Advertising { get; set; }
        public List<User> Users { get; set; } = new List<User>();

        protected Siege() { }

        public Siege(VisibilityType visibility, string title, string description, string imageUri, double range, DateTime start, DateTime available, string ownerId) {
            Visibility = visibility;
            Title = title;
            Description = description;
            ImageUri = imageUri;
            Range = range;
            Start = start;
            Available = available;
            OwnerId = ownerId;
        }

        public void Update(VisibilityType visibility, string title, string description, string imageUri, double range, DateTime start, DateTime available) {
            Visibility = visibility;
            Title = title;
            Description = description;
            ImageUri = imageUri;
            Range = range;
            Start = start;
            Available = available;
        }
    }
}