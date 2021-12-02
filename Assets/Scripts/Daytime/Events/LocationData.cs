using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class LocationData : InteractionEvent, XmlAsset
{
    [XmlElement("Condition")]
    public MultCondition Condition { get; set; }
    [XmlIgnore]
    public bool Check { get { return Condition == null || Condition.Check; } }

    /*[XmlElement("Event")]
    public List<ConditionalDialogue> Events { get; set; }*/

    [XmlElement("TimeSlot")]
    public List<LocationTimeSlot> TimeSlots { get; set; }

    [XmlAttribute("Parent")]
    public string Parent { get; set; }

    public override void Play()
    {
        int time = GameManager.Instance.Time;

        List<ConditionalDialogue> evnts = new List<ConditionalDialogue>();
        foreach (LocationTimeSlot slot in TimeSlots)
            if(slot.Time == time)
                evnts = slot.Events.Where(e => e.Check).ToList();
        //List<ConditionalDialogue> evnts = Events.Where(e => e.Check).ToList();

        if (evnts.Count > 0)
        {
            ConditionalDialogue evnt = evnts[Random.Range(0, evnts.Count)];
            GameManager.Instance.StartDialogue(evnt.Dialogue, EndEvent);
        }
    }
}

public class LocationTimeSlot
{
    [XmlAttribute("Time")]
    public int Time { get; set; }

    [XmlElement("Event")]
    public List<ConditionalDialogue> Events { get; set; }
}