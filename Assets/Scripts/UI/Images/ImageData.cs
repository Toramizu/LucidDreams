using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ImageData
{
    [SerializeField] protected string id;
    public string ID
    {
        get { return id; }
        set { id = value; }
    }
    [SerializeField] protected Sprite image;
    public Sprite Image
    {
        get { return image; }
        set { image = value; }
    }
    [SerializeField] protected string source;
    public string Source
    {
        get { return source; }
        set { source = value; }
    }
}
