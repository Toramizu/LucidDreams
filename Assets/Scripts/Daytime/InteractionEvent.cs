using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

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
    public ColorXml _BackgroundColor { get; set; }
    [XmlIgnore]
    public Color BackgroundColor { get { return _BackgroundColor.Color; } }
    [XmlElement("IconColor")]
    public ColorXml _IconColor { get; set; }
    [XmlIgnore]
    public Color IconColor { get { return _IconColor.Color; } }
    [XmlElement("Icon")]
    public string _Icon { get; set; }
    [XmlIgnore]
    public Sprite Icon { get { return AssetDB.Instance.Images[_Icon]; } }
}
