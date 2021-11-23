using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DreamMapData : XmlAsset
{
    [XmlAttribute("ID")]
    public string ID { get; set; }

    [XmlElement("Node")]
    public List<DreamNodeData> Nodes { get; set; }
}

[System.Serializable]
public class DreamNodeData
{
    [XmlIgnore]
    Vector3 position;
    [XmlIgnore]
    public Vector3 Position { get { return position; } }
    [XmlAttribute("X")]
    public float X
    {
        get { return position.x; }
        set
        {
            if (position == null)
                position = new Vector3();
            position.x = value;
        }
    }
    [XmlAttribute("Y")]
    public float Y
    {
        get { return position.y; }
        set
        {
            if (position == null)
                position = new Vector3();
            position.y = value;
        }
    }

    [XmlAttribute("Content")]
    public NodeContent Content { get; set; }

    [XmlElement("Link")]
    public List<int> NodeLinks { get; set; }

    public DreamNodeData() { }
    public DreamNodeData(NodeContent content, Vector3 position)
    {
        Content = content;
        this.position = position;
    }
}

public enum NodeContent
{
    None,
    Succubus,
    Boss,
    Start,
    Exit,
    Shop,
    Meditation,
}