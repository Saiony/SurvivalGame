using System;
using UnityEngine;

namespace Game.Scripts.Controller.Time
{
    public class FarmCalendar
    {
        public int Second { get; private set; }
        public int Minute { get; private set; }
        public int Hour { get; private set; }

        public int Day { get; private set; }
        public int Month { get; private set; }
        public int Year { get; private set; }

        public event Action OnChangeMinute;
        public event Action OnChangeHour;
        public event Action OnChangeDay;
        public event Action OnChangeMonth;
        public event Action OnChangeYear;

        public FarmCalendar()
        {
            Second = 0;
            Minute = 0;
            Hour = 0;

            Day = 1;
            Month = 1;
            Year = 1;
        }

        public FarmCalendar(int second, int minute, int hour, int day, int month, int year) : this()
        {
            SetTime(second, minute, hour, day, month, year);
        }

        public FarmCalendar(int day, int month, int year) : this()
        {
            Day = day;
            Month = month;
            Year = year;
        }

        public void SetTime(int second, int minute, int hour, int day, int month, int year)
        {
            Second = second;
            Minute = minute;
            Hour = hour;

            Day = day;
            Month = month;
            Year = year;

            OnChangeMinute?.Invoke();
            OnChangeHour?.Invoke();
            OnChangeDay?.Invoke();
            OnChangeMonth?.Invoke();
            OnChangeYear?.Invoke();
        }

        public void IncrementTime(int hour, int minute, int second)
        {
            IncrementHour(hour);
            IncrementMinute(minute);
            IncrementSecond(second);
        }


        #region Increment
        private void IncrementSecond(int second)
        {
            Second += second;
            if (Second < 60)
                return;

            IncrementMinute(Second / 60);
            Second %= 60;
        }

        private void IncrementMinute(int minute)
        {
            Minute += minute;
            if (Minute < 60)
            {
                OnChangeMinute?.Invoke();
                return;
            }

            IncrementHour(Minute / 60);
            Minute %= 60;
            OnChangeMinute?.Invoke();
        }

        private void IncrementHour(int hour)
        {
            Hour += hour;
            if (Hour <= 23)
            {
                OnChangeHour?.Invoke();
                return;
            }

            IncrementDay(Hour / 24);
            Hour %= 24;
            OnChangeHour?.Invoke();
        }

        private void IncrementDay(int day)
        {
            Day += day;
            if (Day <= 30)
            {
                OnChangeDay?.Invoke();
                return;
            }

            IncrementMonth(Day / 30);
            Day %= 30;
            OnChangeDay?.Invoke();
        }

        private void IncrementMonth(int month)
        {
            Month += month;
            if (Month <= 4)
            {
                OnChangeMonth?.Invoke();
                return;
            }

            IncrementYear(Month / 4);
            Month %= 4;
            OnChangeMonth?.Invoke();
        }

        private void IncrementYear(int year)
        {
            Year += year;
            OnChangeYear?.Invoke();
        }
        #endregion Increment
    }
}

public interface ICalendarListener
{
    void OnMinuteChanged();
    void OnHourChanged();
    void OnDayChanged();
    void OnMonthChanged();
    void OnYearChanged();
}