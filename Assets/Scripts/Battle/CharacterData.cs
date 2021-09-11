using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : ScriptableObject
{
    [SerializeField] protected string sName;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    [SerializeField] protected Sprite image;
    public Sprite Image
    {
        get { return image; }
        set { image = value; }
    }
    [SerializeField] protected int maxArousal;
    public int MaxArousal
    {
        get { return maxArousal; }
        set { maxArousal = value; }
    }
    [SerializeField] protected string source;
    public string Source
    {
        get { return source; }
        set { source = value; }
    }
}
