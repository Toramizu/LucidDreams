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

    [XmlElement("Event")]
    public List<ConditionalDialogue> Events { get; set; }

    [XmlAttribute("Parent")]
    public string Parent { get; set; }

    public override void Play()
    {
        List<ConditionalDialogue> evnts = Events.Where(e => e.Check).ToList();

        if (evnts.Count > 0)
        {
            ConditionalDialogue evnt = evnts[Random.Range(0, evnts.Count)];
            GameManager.Instance.StartDialogue(evnt.Dialogue, EndEvent);
        }
    }
}

public class ColorXml
{
    Color32 color = new Color32(255, 255, 255, 255);
    public Color32 Color { get { return color; } }
    [XmlAttribute("R")]
    public byte R { get { return color.r; } set { color.r = value; } }
    [XmlAttribute("G")]
    public byte G { get { return color.g; } set { color.g = value; } }
    [XmlAttribute("B")]
    public byte B { get { return color.b; } set { color.b = value; } }
    [XmlAttribute("A"), DefaultValue(255)]
    public byte A { get { return color.a; } set { color.a = value; } }
}