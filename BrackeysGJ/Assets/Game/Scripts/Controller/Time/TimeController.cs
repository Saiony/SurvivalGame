using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using Game.Helper;

namespace Game.Scripts.Controller.Time
{
    public class TimeController : MonoBehaviour, ICalendarListener
    {
        public FarmCalendar Calendar { get; private set; }

        [SerializeField]
        private TextMeshProUGUI _timeText = null;
        private TextMeshProUGUI TimeText => _timeText;

        [SerializeField]
        private TextMeshProUGUI _dayText = null;
        private TextMeshProUGUI DayText => _dayText;

        [SerializeField]
        private TextMeshProUGUI _ampmText = null;
        private TextMeshProUGUI AMPMText => _ampmText;

        [SerializeField]
        private Image _seasonImage = null;
        private Image SeasonImage => _seasonImage;

        [SerializeField]
        private Sprite[] _seasonImageList = null;
        private Sprite[] SeasonImageList => _seasonImageList;

        [SerializeField]
        [Range(0, 60)]
        private float _timeUpdateFrequency = 0;
        private float TimeUpdateFrequency => _timeUpdateFrequency;

        [SerializeField]
        [Range(0, 60)]
        private int _secsToUpdate = 0;
        private int SecsToUpdate => _secsToUpdate;

        private bool TimePaused { get; set; }

        public static TimeController Instance = null;

        private List<Action> OnChangeDayListeners { get; set; }
        private List<Action> OnChangeSeasonListeners { get; set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            OnChangeDayListeners = new List<Action>();
            OnChangeSeasonListeners = new List<Action>();
        }

        private void Start()
        {
            Calendar = new FarmCalendar(1, 4, 15, 22, 4, 2);
            InvokeRepeating("PassTime", TimeUpdateFrequency, TimeUpdateFrequency);

            Calendar.OnChangeMinute += OnMinuteChanged;
            Calendar.OnChangeHour += OnHourChanged;
            Calendar.OnChangeDay += OnDayChanged;
            Calendar.OnChangeMonth += OnSeasonChanged;
            Calendar.OnChangeYear += OnYearChanged;

            OnHourChanged();
            OnDayChanged();
            OnSeasonChanged();
        }

        private void PassTime()
        {
            if (!TimePaused)
                Calendar.IncrementTime(0, 0, SecsToUpdate);
        }

        public void PauseTime()
        {
            TimePaused = true;
        }

        public void UnpauseTime()
        {
            TimePaused = false;
        }

        public void PassDay(int finalHour)
        {
            Debug.Log("PassDay called");
            Calendar.SetTime(0, 0, finalHour, Calendar.Day, Calendar.Month, Calendar.Year);
            Calendar.IncrementTime(24, 0, 0);
        }

        public void OnMinuteChanged()
        {
            var hourFormated = Calendar.Hour > 12 ? Calendar.Hour - 12 : Calendar.Hour;
            TimeText.text = hourFormated.ToString("D2") + ":" + Calendar.Minute.ToString("D2");
        }

        public void OnHourChanged()
        {
            var hourFormated = Calendar.Hour > 12 ? Calendar.Hour - 12 : Calendar.Hour;
            TimeText.text = hourFormated.ToString("D2") + ":" + Calendar.Minute.ToString("D2");
            AMPMText.text = Calendar.Hour > 12 ? "PM" : "AM";
        }

        public void OnDayChanged()
        {
            var dayOfTheWeek = WeekDays[(Calendar.Day % 7)];
            DayText.text = Calendar.Day.ToString() + " " + dayOfTheWeek;
            Debug.Log("DayChanged to: " + Calendar.Day.ToString());

            OnChangeDayListeners.ForEach(x => x?.Invoke());
        }

        public void OnSeasonChanged()
        {
            SeasonImage.sprite = SeasonImageList[Calendar.Month - 1];
            OnChangeSeasonListeners.ForEach(x => x?.Invoke());
        }

        public void OnYearChanged()
        {
        }

        public void SubscribeDayChanged(Action action)
        {
            OnChangeDayListeners.Add(action);
        }

        public void SubscribeSeasonChanged(Action action)
        {
            OnChangeSeasonListeners.Add(action);
        }

        public SeasonType GetSeason()
        {
            switch (Calendar.Month)
            {
                case 1:
                    return SeasonType.Spring;
                case 2:
                    return SeasonType.Summer;
                case 3:
                    return SeasonType.Autumn;
                case 4:
                    return SeasonType.Winter;
                default:
                    return SeasonType.Unknown;
            }
        }

        private string[] WeekDays =
        {
            "Sun",
            "Mon",
            "Tue",
            "Wed",
            "Thu",
            "Fri",
            "Sat"
        };
    }

}