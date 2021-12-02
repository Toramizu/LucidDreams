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
        set
        {
            currentTime = value;

            ParseTime();

            image.sprite = timeImages[currentTime];
        }
    }

    public int NightTime
    {
        get { return timeImages.Count - 1; }
    }

    void ParseTime()
    {
        if(currentTime >= timeImages.Count)
        {
            int c = currentTime;
            currentTime = c % timeImages.Count;
        }
    }

    public int AdvanceTime(int amount)
    {
        Time += amount;
        return currentTime;
    }

    public int NewDay()
    {
        Time = 0;
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
