using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public abstract class ImageHaver
{
    [XmlIgnore]
    public abstract string Name { get; set; }

    [XmlElement("Images")]
    public ImageSet Images { get; set; }
    [XmlIgnore]
    public ImageData Image { get { return Images.Default; } }
}

[System.Serializable]
public class ImageSet
{

    [XmlIgnore]
    Dictionary<string, ImageData> imagesDict;
    [XmlElement("Image")]
    public List<ImageData> Images { get; set; }

    [XmlIgnore]
    public ImageData this[string id]
    {
        get
        {
            if (imagesDict == null) Init();

            if (imagesDict.ContainsKey(id))
                return imagesDict[id];
            else
                return Default;
        }
    }

    [XmlIgnore]
    public ImageData Default
    {
        get
        {
            if (imagesDict == null) Init();

            if (imagesDict.ContainsKey(""))
                return imagesDict[""];
            else
                return null;
        }
    }

    public void Add(ImageData d) { Images.Add(d); }

    void Init()
    {
        imagesDict = new Dictionary<string, ImageData>();
        foreach (ImageData i in Images)
            imagesDict.Add(i.ID, i);
    }
}