using System;
namespace PaulSchlyter
{
    public class Sol(DateTime day, double longitude)
    {
        private const double DegRad = Math.PI / 180.0;

        public double LocalMeanSolarTimeEpoch = EpochDayLocalMidday(day, longitude);
        public double DistanceToSun => Hypotenuse(OrbitXCoordinate, OrbitYCoordinate);
        public double EquatorialXCoordinate => EclipticRectangularXCoordinate;
        public double EquatorialZCoordinate => EclipticRectangularYCoordinate * Math.Sin(ObliquityOfEcliptic);
        public double EquatorialYCoordinate => EclipticRectangularYCoordinate * Math.Cos(ObliquityOfEcliptic);

        public double ObliquityOfEcliptic => 23.4393 * DegRad - 3.563E-7 * DegRad * LocalMeanSolarTimeEpoch;
        public double EclipticRectangularYCoordinate => DistanceToSun * Math.Sin(TrueSolarLongitude);

        public static readonly DateTime J2000 = new(2000, 1, 1, 0, 0, 0);
        public static double DaysSinceJ2000(DateTime dateTime) => (dateTime - J2000).TotalDays;
        public static double EpochDayLocalMidday(DateTime dateTime, double longitude) => DaysSinceJ2000(dateTime.Date) + 0.5 - longitude % 180 / 360.0;

        public double DeclinationRadians => Math.Atan2(EquatorialZCoordinate, Hypotenuse(EquatorialXCoordinate, EquatorialYCoordinate));
        public double ApparentRadiusDegrees => 0.2666 / DistanceToSun;
        public double EclipticRectangularXCoordinate => DistanceToSun * Math.Cos(TrueSolarLongitude);
        public double OrbitXCoordinate => Math.Cos(EccentricAnomaly) - EarthOrbitEccentricity;
        public double OrbitYCoordinate => Math.Sqrt(1.0 - Square(EarthOrbitEccentricity)) * Math.Sin(EccentricAnomaly);
        public static double Hypotenuse(double x, double y) => Math.Sqrt(Square(x) + Square(y));
        public double RightAscensionRadians => Math.Atan2(EquatorialYCoordinate, EquatorialXCoordinate);


        public static double Square(double value) => value * value;
        public double EccentricAnomaly => MeanAnomaly + EarthOrbitEccentricity * Math.Sin(MeanAnomaly) * (1.0 + EarthOrbitEccentricity * Math.Cos(MeanAnomaly));
        public double EarthOrbitEccentricity => 0.016709 - 1.151E-9 * LocalMeanSolarTimeEpoch;
        public double TrueSolarLongitude => Rev2Pi(TrueAnomaly + MeanLongitudeOfPerihelion);

        public double MeanLongitudeOfPerihelion => 282.9404 * DegRad + 4.70935E-5 * DegRad * LocalMeanSolarTimeEpoch;
        public double TrueAnomaly => Math.Atan2(OrbitYCoordinate, OrbitXCoordinate);
        public double MeanAnomaly => Rev2Pi(356.0470 * DegRad + 0.9856002585 * DegRad * LocalMeanSolarTimeEpoch);

        public static double Rev2Pi(double value)
        {
            const double revolution = Math.PI * 2;
            double result = value % revolution;
            return result < 0 ? result + revolution : result;
        }
    }



    public enum DiurnalResult
    {
        /// <summary>
        /// The diurnal arc is normal; the sun crosses the specified altitude on the specified day.
        /// </summary>
        NormalDay,

        /// <summary>
        /// The sun remains above the specified altitude for all 24h of the specified day. The time
        /// returned is when the sun is closest to the horizon (directly to the south in the
        /// northern hemisphere; to the north in the southern); sunrise is 12 hours before
        /// midday, and sunset is 12 hours after.
        /// </summary>
        SunAlwaysAbove,

        /// <summary>
        /// The sun remains below the specified altitude for all 24h of the specified day. Sunrise
        /// and sunset are both calculated as when the sun is closest to the horizon (directly to
        /// the south in the northern hemisphere; to the north in the southern).
        /// </summary>
        SunAlwaysBelow
    }
    class Calculator
    {
        public double Latitude;
        public double Longitude;
        public DateTime time;
        public Sol SolarPosition;

        public static DateTime LocalMidday(DateTime dateTime, double longitude) => dateTime.Date.AddDays(0.5 - longitude % 180.0 / 360.0);
        public DateTime Day => time.Date;
        readonly DateTime TimeSunAtLongitude;
        readonly TimeSpan halfDay;
        public static DiurnalResult diurnalResult = DiurnalResult.NormalDay;

        private const double DegRad = Math.PI / 180.0;
        public static double RadiansToHours(double radians) => 12 / Math.PI * radians;
        public static double Rev1Pi(double value) => value % Math.PI;
        private static double Rev2Pi(double value)
        {
            double result = value % (2 * Math.PI);
            return result < 0 ? result + 2 * Math.PI : result;
        }
        public static double GMST0Radians(double epochDay) => Rev2Pi(Math.PI + (356.0470 + 282.9404 + (0.9856002585 + 4.70935E-5) * epochDay) * DegRad);
        public double LocalSiderealTimeRadians => Rev2Pi(GMST0Radians(SolarPosition.LocalMeanSolarTimeEpoch) + Math.PI + Longitude * DegRad);

        const double AstronomicalHorizon = -18.0;
        const double CivilHorizon = -6.0;
        const double NauticalHorizon = -12.0;
        const double NominalHorizon = -35.0 / 60.0;

        public double HorizonDegrees => NominalHorizon - SolarPosition.ApparentRadiusDegrees;
        public (DiurnalResult, double) DiurnalArcRadians(double altitude)
        {
            double cosineOfArc =
                (Math.Sin(altitude * DegRad)
                -
                  Math.Sin(Latitude * DegRad) * Math.Sin(SolarPosition.DeclinationRadians))
                  /
                  (Math.Cos(Latitude * DegRad) * Math.Cos(SolarPosition.DeclinationRadians));

            return cosineOfArc switch
            {
                >=  1.0 => (DiurnalResult.SunAlwaysBelow, 0),
                <= -1.0 => (DiurnalResult.SunAlwaysAbove, Math.PI),
                _ => (DiurnalResult.NormalDay, Math.Acos(cosineOfArc))
            };
        }
        public static TimeSpan ArcRadiansToTimeSpan(double radiansOfArc) => TimeSpan.FromHours(RadiansToHours(radiansOfArc));
        public Calculator(double latitude, double longitude, DateTime date)
        {
            Latitude = latitude;
            Longitude = longitude;
            time = LocalMidday(date, Longitude);
            SolarPosition = new Sol(Day, Longitude);



            TimeSunAtLongitude = new DateTime(Day.Year, Day.Month, Day.Day, 0, 0, 0, DateTimeKind.Utc)
                   .AddHours(12.0 - RadiansToHours(Rev1Pi(LocalSiderealTimeRadians - SolarPosition.RightAscensionRadians)));

            (DiurnalResult result, double arcRadians) = DiurnalArcRadians(HorizonDegrees);
            diurnalResult = result;
            halfDay = ArcRadiansToTimeSpan(arcRadians);
        }
        public DateTime Sunrise
        {
            get
            {
                if (halfDay.TotalDays == 0.0) return DateTime.MinValue;
                if (halfDay.TotalDays == 0.5) return DateTime.MinValue;
                return TimeZoneInfo.ConvertTimeFromUtc(TimeSunAtLongitude - halfDay, TimeZoneInfo.Local);
            }
        }

        public DateTime Sunset
        {
            get
            {
                if (halfDay.TotalDays == 0.0) return DateTime.MinValue;
                if (halfDay.TotalDays == 0.5) return DateTime.MinValue;
                return TimeZoneInfo.ConvertTimeFromUtc(TimeSunAtLongitude + halfDay, TimeZoneInfo.Local);
            }
        }

        public static (DateTime rise, DateTime set, DiurnalResult result) Get(double latitude, double longitude, DateTime date)
        {
            Calculator calculator = new(latitude, longitude, date);
            return (calculator.Sunrise, calculator.Sunset, diurnalResult);
        }
    }
}
