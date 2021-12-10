using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] List<Sprite> timeImages;

    int currentTime;
    public int Time
    {
        get { return currentTime; }
        private set
        {
            currentTime = value;

            ParseTime();

            image.sprite = timeImages[currentTime];
        }
    }

    public int Day { get; private set; } = 1;
    public string DayS { get { return days[Day % days.Length]; } }
    string[] days = new string[] { "Moonday", "Waterday", "Aetherday", "Sunday", "Fireday", "Earthday" };
    /*Moonday			Selen-
    Waterday		Hydr-
    Aetherday		Aether-
    Sunday			Heli-
    Fireday			Pyr-
    Earthday		Ge-*/

    public string Date { get { return DayS + " " + Day; } }

    public int NightTime
    {
        get { return timeImages.Count - 1; }
    }

    void ParseTime()
    {
        if(currentTime >= timeImages.Count)
        {
            Day += currentTime / timeImages.Count;
            currentTime = currentTime % timeImages.Count;
        }
    }

    public bool AdvanceTime(int amount)
    {
        int currentDay = Day;
        Time += amount;
        return Day > currentDay;
    }

    public int NewDay()
    {
        Time = 0;
        Day++;
        return 0;
    }
}

public enum TimeSlot
{
    Morning,
    Afternoon,
    Evening,
    Night,
}
