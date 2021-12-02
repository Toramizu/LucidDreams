using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;
using static DialogueUI;

public abstract class InteractionEvent
{
    public abstract void Play();

    [XmlAttribute("ID")]
    public string ID { get; set; }

    [XmlAttribute("Name")]
    public string Name { get; set; }

    [XmlIgnore]
    Vector2 position = new Vector2();
    [XmlIgnore]
    public Vector2 Position { get { return position; } }
    [XmlAttribute("PosX")]
    public float X { get { return position.x; } set { position.x = value; } }
    [XmlAttribute("PosY")]
    public float Y { get { return position.y; } set { position.y = value; } }

    [XmlElement("BgColor")]
    public ColorXml _BackgroundColor { get; set; } = new ColorXml();
    [XmlIgnore]
    public Color BackgroundColor { get { return _BackgroundColor.Color; } }
    [XmlElement("IconColor")]
    public ColorXml _IconColor { get; set; } = new ColorXml();
    [XmlIgnore]
    public Color IconColor { get { return _IconColor.Color; } }
    [XmlAttribute("Icon")]
    public string _Icon { get; set; }
    [XmlIgnore]
    public Sprite Icon { get { return AssetDB.Instance.Images[_Icon]; } }

    [XmlAttribute("TimeSpent"), DefaultValue(0)]
    public int TimeSpent { get; set; }
    
    public void EndEvent()
    {
        if (TimeSpent > 0)
            GameManager.Instance.DayManager.AdvanceTime(TimeSpent);
    }
}

public class ConditionalDialogue
{
    [XmlAttribute("Time")]
    public int Time { get; set; }

    [XmlElement("Condition")]
    public MultCondition Condition { get; set; }
    [XmlIgnore]
    public bool Check { get { return Condition == null || Condition.Check; } }

    [XmlAttribute("Dialogue")]
    public string _Dialogue { get; set; }
    [XmlIgnore]
    public DialogueData Dialogue { get { return AssetDB.Instance.Dialogues[_Dialogue]; } }

    public void Play(DialogueAction action)
    {
        GameManager.Instance.StartDialogue(Dialogue, action);
    }
}