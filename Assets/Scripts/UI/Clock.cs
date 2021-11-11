using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] List<Sprite> timeImages;

    TimeSlot currentTime;
    public TimeSlot Time
    {
        get { return currentTime; }
        set
        {
            currentTime = value;

            ParseTime();

            image.sprite = timeImages[(int)currentTime];
        }
    }

    void ParseTime()
    {
        if((int) currentTime > timeImages.Count)
        {
            int c = (int)currentTime;
            currentTime = (TimeSlot)(c % timeImages.Count);
        }
    }

    public void AdvanceTime()
    {
        Time++;
    }
}

public enum TimeSlot
{
    Morning,
    Afternoon,
    Evening,
}
