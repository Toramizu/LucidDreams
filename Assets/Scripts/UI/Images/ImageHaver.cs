using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ImageHaver : ScriptableObject
{
    public abstract string Name { get; }

    [SerializeField] ImageSet images;
    public ImageSet Images { get { return images; } }
    public ImageData Image { get { return images.Default; } }
}
[System.Serializable]
public class ImageSet
{
    [SerializeField] List<ImageData> images;

    Dictionary<string, ImageData> imagesDict;

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

    public void Add(ImageData d) { images.Add(d); }

    void Init()
    {
        imagesDict = new Dictionary<string, ImageData>();
        foreach (ImageData i in images)
            imagesDict.Add(i.ID, i);
    }
}