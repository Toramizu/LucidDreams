using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;

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

    public ColorXml() { }
    public ColorXml(Color color)
    {
        this.color = color;
    }
}