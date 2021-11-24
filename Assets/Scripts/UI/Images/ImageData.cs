using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[System.Serializable]
public class ImageData
{
    [XmlAttribute("ID")]
    public string ID { get; set; }
    [XmlIgnore]
    public Sprite Image { get { return AssetDB.Instance.Images[_Image]; } }
    [XmlAttribute("ImageID")]
    public string _Image { get; set; }
    [XmlAttribute("Source")]
    public string Source { get; set; }
}
