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

    void ParseTime()
    {
        if(currentTime >= timeImages.Count)
        {
            int c = currentTime;
            currentTime = c % timeImages.Count;
        }
    }

    public int AdvanceTime()
    {
        Time++;
        return currentTime;
    }
}

public enum TimeSlot
{
    Morning,
    Afternoon,
    Evening,
}
