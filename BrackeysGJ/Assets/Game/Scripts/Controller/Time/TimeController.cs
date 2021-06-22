using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using Game.Helper;
using DG.Tweening;

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
        private Sprite[] _seasonImageList = null;
        private Sprite[] SeasonImageList => _seasonImageList;

        [SerializeField]
        private Transform _arrow = null;
        private Transform Arrow => _arrow;

        [SerializeField]
        private SeasonWheelVfx _wheelVfx = null;
        private SeasonWheelVfx WheelVfx => _wheelVfx;

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

            MoveArrow(Calendar.Month, false);
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

        public void ResumeTime()
        {
            TimePaused = false;
        }

        public void PassDay(int finalHour)
        {
            Debug.Log("PassDay called");

            Calendar.JumpTo(0, 0, finalHour, Calendar.Day + 1, Calendar.Month, Calendar.Year);
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
            Debug.Log("Nova season: " + GetSeason().ToString());
            OnChangeSeasonListeners.ForEach(x => x?.Invoke());
            //fazer virar a setinha doida
            MoveArrow(Calendar.Month);
        }

        private void MoveArrow(int season, bool anim = true)
        {
            var angle = 45 + (90 * (-season + 1));
            var finalRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            var time = anim == true ? 0.3f : 0f;

            Sequence seq = DOTween.Sequence();
            seq.Append(Arrow.DORotateQuaternion(finalRotation, time));
            seq.Join(WheelVfx.SetCurrent(season - 1));
            seq.Append(Arrow.DOPunchRotation(-Arrow.transform.forward * 10, 0.3f, 1, 10));
            seq.Play();
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