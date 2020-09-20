using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Models.Time
{
    public class TimePeriod : IEquatable<TimePeriod>
    {
        private readonly long seconds;
        public int Hours { get; }
        public int Minutes { get; }
        public int Seconds { get; }

        public TimePeriod(int hh, int mm, int ss)
        {
            seconds = 3600 * hh + 60 * mm + ss;
            Hours = (int)(seconds / 3600);
            Minutes = (int)((seconds - (Hours * 3600)) / 60);
            Seconds = (int)(seconds - (Hours * 3600) - (Minutes * 60));
            if ((hh < 0) || (mm < 0) || (ss < 0))
            {
                throw new ArgumentException();
            }
        }
        public TimePeriod(int hh, int mm) : this(hh, mm, default(int)) { }
        public TimePeriod(int ss) : this(default(int), default(int), ss) { }
        public TimePeriod(Time time1, Time time2) : this(time1.Hours - time2.Hours, time1.Minutes - time2.Minutes, time1.Seconds - time2.Seconds) { }
        public TimePeriod(string timePeriod)
        {
            string[] timePeriodTab = timePeriod.Split(':');
            if (timePeriodTab.Length != 3)
            {
                throw new ArgumentException("Wprowadzono błędny format czasu");
            }
            else
            {
                Hours = Convert.ToInt32(timePeriodTab[0]);
                Minutes = Convert.ToInt32(timePeriodTab[1]);
                Seconds = Convert.ToInt32(timePeriodTab[2]);
                if ((Hours < 0) || (Minutes < 0) || (Seconds < 0))
                {
                    throw new ArgumentException();
                }
            }
        }

        public override string ToString()
        {
            return $"{Hours.ToString("000")}:{Minutes.ToString("00")}:{Seconds.ToString("00")}";
        }

        public bool Equals(TimePeriod other) => (Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds);

        public static bool Equals(TimePeriod timePeriod1, TimePeriod timePeriod2) => timePeriod1.Equals(timePeriod2);
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (Object.ReferenceEquals(this, obj)) return true;
            if (obj.GetType() == typeof(TimePeriod)) return Equals(obj);
            else return false;
        }
        public static bool operator ==(TimePeriod timePeriod1, TimePeriod timePeriod2) => Equals(timePeriod1, timePeriod2);

        public static bool operator !=(TimePeriod timePeriod1, TimePeriod timePeriod2) => !Equals(timePeriod1, timePeriod2);

        public static bool operator <(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            if (!(timePeriod1.Hours == timePeriod2.Hours)) return timePeriod1.Hours < timePeriod2.Hours;
            if (!(timePeriod1.Minutes == timePeriod2.Minutes)) return timePeriod1.Minutes < timePeriod2.Minutes;
            if (!(timePeriod1.Seconds == timePeriod2.Seconds)) return timePeriod1.Seconds < timePeriod2.Seconds;

            return false;
        }
        public static bool operator <=(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            if (!(timePeriod1.Hours == timePeriod2.Hours)) return timePeriod1.Hours <= timePeriod2.Hours;
            if (!(timePeriod1.Minutes == timePeriod2.Minutes)) return timePeriod1.Minutes <= timePeriod2.Minutes;
            if (!(timePeriod1.Seconds == timePeriod2.Seconds)) return timePeriod1.Seconds <= timePeriod2.Seconds;

            return true;
        }
        public static bool operator >(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            if (!(timePeriod1.Hours == timePeriod2.Hours)) return timePeriod1.Hours > timePeriod2.Hours;
            if (!(timePeriod1.Minutes == timePeriod2.Minutes)) return timePeriod1.Minutes > timePeriod2.Minutes;
            if (!(timePeriod1.Seconds == timePeriod2.Seconds)) return timePeriod1.Seconds > timePeriod2.Seconds;

            return false;
        }
        public static bool operator >=(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            if (!(timePeriod1.Hours == timePeriod2.Hours)) return timePeriod1.Hours >= timePeriod2.Hours;
            if (!(timePeriod1.Minutes == timePeriod2.Minutes)) return timePeriod1.Minutes >= timePeriod2.Minutes;
            if (!(timePeriod1.Seconds == timePeriod2.Seconds)) return timePeriod1.Seconds >= timePeriod2.Seconds;

            return true;
        }
        public int CompareTo(TimePeriod other)
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
        public TimePeriod TimePeriodPlus(TimePeriod timePeriod)
        {
            checked
            {
                return new TimePeriod(
                    Hours + timePeriod.Hours,
                    Minutes + timePeriod.Minutes,
                    Seconds + timePeriod.Seconds);
            }
        }
        public static TimePeriod TimePlus(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            checked
            {
                return new TimePeriod(
                    timePeriod1.Hours + timePeriod2.Hours,
                    timePeriod1.Minutes + timePeriod2.Minutes,
                    timePeriod1.Seconds + timePeriod2.Seconds);
            }
        }
        public static TimePeriod operator +(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            checked
            {
                return new TimePeriod(
                    timePeriod1.Hours + timePeriod2.Hours,
                    timePeriod1.Minutes + timePeriod2.Minutes,
                    timePeriod1.Seconds + timePeriod2.Seconds);
            }
        }
        public static TimePeriod operator -(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            checked
            {
                if (timePeriod1 > timePeriod2)
                {
                    return new TimePeriod(
                        timePeriod1.Hours - timePeriod2.Hours,
                        timePeriod1.Minutes - timePeriod2.Minutes,
                        timePeriod1.Seconds - timePeriod2.Seconds);
                }
                else
                {
                    throw new ArgumentException("Parametr 'timePeriod1' musi być mniejszy od parametru 'timePeriod2'");
                }
            }
        }
    }
}
