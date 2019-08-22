using System;

namespace papuff.domain.Helpers {
    public static class LocationHelper {

        public static double DistanceTo(this Coordinate local, Coordinate target) {
            return DistanceTo(local, target, UnitOfLength.Kilometers);
        }

        public static double DistanceTo(this Coordinate local, Coordinate target, UnitOfLength unitOfLength) {
            var localRad = Math.PI * local.Latitude / 180;
            var targetRad = Math.PI * target.Latitude / 180;
            var theta = local.Longitude - target.Longitude;
            var thetaRad = Math.PI * theta / 180;

            double dist =
                Math.Sin(localRad) * Math.Sin(targetRad) + Math.Cos(localRad) *
                Math.Cos(targetRad) * Math.Cos(thetaRad);

            dist = Math.Acos(dist);

            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            return unitOfLength.FromMiles(dist);
        }
    }

    public class Coordinate {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public Coordinate(double latitude, double longitude) {
            Latitude = latitude;
            Longitude = longitude;
        }
    }

    public class UnitOfLength {

        public static UnitOfLength Kilometers = new UnitOfLength(1.609344);
        public static UnitOfLength NauticalMiles = new UnitOfLength(0.8684);
        public static UnitOfLength Miles = new UnitOfLength(1);

        private readonly double _miles;

        private UnitOfLength(double miles) {
            _miles = miles;
        }

        public double FromMiles(double input) {
            return input * _miles;
        }
    }
}
