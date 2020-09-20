using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Models.Time
{
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        public int Hours { get; }
        public int Minutes { get; }
        public int Seconds { get; }

        public Time(int hh, int mm, int ss)
        {
            Hours = hh % 24;
            Minutes = mm % 60;
            Seconds = ss % 60;
            if ((Hours < 0) || (Minutes < 0) || (Seconds < 0))
            {
                throw new ArgumentException();
            }
        }

        public Time(int hh, int mm) : this(hh, mm, default(int)) { }
        public Time(int hh) : this(hh, default(int), default(int)) { }
        public Time(string time)
        {
            string[] timeTab = time.Split(':');
            if (timeTab.Length != 3)
                throw new ArgumentException("Wprowadzono błędny format czasu");
            else
            {
                Hours = Convert.ToInt32(timeTab[0]) % 24;
                Minutes = Convert.ToInt32(timeTab[1]) % 60;
                Seconds = Convert.ToInt32(timeTab[2]) % 60;
                if ((Hours < 0) || (Minutes < 0) || (Seconds < 0))
                {
                    throw new ArgumentException();
                }
            }
        }

        public override string ToString()
        {
            return $"{Hours.ToString("00")}:{Minutes.ToString("00")}:{Seconds.ToString("00")}";
        }

        public bool Equals(Time other) => (Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds);

        public static bool Equals(Time time1, Time time2) => time1.Equals(time2);

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (Object.ReferenceEquals(this, obj)) return true;
            if (obj.GetType() == typeof(Time)) return Equals(obj);
            else return false;
        }

        public static bool operator ==(Time time1, Time time2) => Equals(time1, time2);

        public static bool operator !=(Time time1, Time time2) => !Equals(time1, time2);

        public static bool operator <(Time time1, Time time2)
        {
            if (!(time1.Hours == time2.Hours)) return time1.Hours < time2.Hours;
            if (!(time1.Minutes == time2.Minutes)) return time1.Minutes < time2.Minutes;
            if (!(time1.Seconds == time2.Seconds)) return time1.Seconds < time2.Seconds;

            return false;
        }
        public static bool operator <=(Time time1, Time time2)
        {
            if (!(time1.Hours == time2.Hours)) return time1.Hours <= time2.Hours;
            if (!(time1.Minutes == time2.Minutes)) return time1.Minutes <= time2.Minutes;
            if (!(time1.Seconds == time2.Seconds)) return time1.Seconds <= time2.Seconds;

            return true;
        }
        public static bool operator >(Time time1, Time time2)
        {
            if (!(time1.Hours == time2.Hours)) return time1.Hours > time2.Hours;
            if (!(time1.Minutes == time2.Minutes)) return time1.Minutes > time2.Minutes;
            if (!(time1.Seconds == time2.Seconds)) return time1.Seconds > time2.Seconds;

            return false;
        }
        public static bool operator >=(Time time1, Time time2)
        {
            if (!(time1.Hours == time2.Hours)) return time1.Hours >= time2.Hours;
            if (!(time1.Minutes == time2.Minutes)) return time1.Minutes >= time2.Minutes;
            if (!(time1.Seconds == time2.Seconds)) return time1.Seconds >= time2.Seconds;

            return true;
        }
        public int CompareTo(Time other)
        {
            if (Hours.Equals(other.Hours))
            {
                if (Minutes.Equals(other.Minutes))
                {
                    return Seconds - other.Seconds;
                }
                else
                {
                    return Minutes - other.Minutes;
                }
            }
            else
            {
                return Hours - other.Hours;
            }
        }
        public Time TimePlus(TimePeriod timePeriod)
        {
            return new Time(
                Hours + timePeriod.Hours,
                Minutes + timePeriod.Minutes,
                Seconds + timePeriod.Seconds);
        }
        public static Time TimePlus(Time time, TimePeriod timePeriod)
        {
            return new Time(
                time.Hours + timePeriod.Hours,
                time.Minutes + timePeriod.Minutes,
                time.Seconds + timePeriod.Seconds);
        }
        public static Time operator +(Time time1, Time time2)
        {
            checked
            {
                return new Time(
                    time1.Hours + time2.Hours,
                    time1.Minutes + time2.Minutes,
                    time1.Seconds + time2.Seconds);
            }
        }
        public static Time operator -(Time time1, Time time2)
        {
            checked
            {
                if (time1 > time2)
                {
                    return new Time(
                        time1.Hours - time2.Hours,
                        time1.Minutes - time2.Minutes,
                        time1.Seconds - time2.Seconds);
                }
                else
                {
                    throw new ArgumentException("Parametr 'time1' musi być większy od parametru 'time2'");
                }
            }
        }
    }
}
