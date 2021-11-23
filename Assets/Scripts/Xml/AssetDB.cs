using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class AssetDB
{
    static AssetDB instance;
    public static AssetDB Instance
    {
        get
        {
            if (instance == null)
                instance = new AssetDB();

            return instance;
        }
    }

    XmlManager xml;

    public AssetDB()
    {
        xml = new XmlManager();

        Sprites = new SpriteDB();

        Dialogues = new XmlDB<DialogueData>("Dialogues");
        Dialogues.LoadFromXml();

        DreamMaps = new XmlDB<DreamMapData>("DreamMaps");
        DreamMaps.LoadFromXml();

        Succubi = new XmlDB<SuccubusData>("Succubi");
        Succubi.LoadFromXml();

        Characters = new XmlDB<CharacterData>("Characters");
        Characters.LoadFromXml();
    }
    #region Scriptable Objects & Sprites
    public XmlDB<AbilityData> Abilities { get; private set; }
    public void InitAbilities(List<AbilityData> lst)
    {
        Abilities = new XmlDB<AbilityData>("Abilities");
        Abilities.AddRange(lst);
    }
    public XmlDB<DreamData> Dreams { get; private set; }
    public void InitDreams(List<DreamData> lst)
    {
        Dreams = new XmlDB<DreamData>("Dreams");
        Dreams.AddRange(lst);
    }
    public XmlDB<SuccubusData> Succubi { get; private set; }
    public void InitSuccubi(List<SuccubusData> lst)
    {
        Succubi = new XmlDB<SuccubusData>("Succubi");
        Succubi.AddRange(lst);
    }
    public XmlDB<Trait> Traits { get; private set; }
    public void InitTraits(List<Trait> lst)
    {
        Traits = new XmlDB<Trait>("Traits");
        Traits.AddRange(lst);
    }
    public XmlDB<CharacterData> Characters { get; private set; }
    public void InitCharacters(List<CharacterData> lst)
    {
        Characters = new XmlDB<CharacterData>("Characters");
        Characters.AddRange(lst);
    }

    public SpriteDB Sprites { get; set; }
    #endregion

    public XmlDB<DialogueData> Dialogues { get; private set; }
    public XmlDB<DreamMapData> DreamMaps { get; private set; }
}

public class XmlDB<T> where T : XmlAsset
{
    string classId;
    Dictionary<string, T> assets;

    public List<T> ToList()
    {
        return assets.Values.ToList();
    }

    public T this[string id]
    {
        get
        {
            if (assets.ContainsKey(id))
                return assets[id];
            else
                throw new System.Exception(classId + " asset not found : " + id);
        }
        set
        {
            assets[id] = value;
        }
    }

    public XmlDB(string classId)
    {
        this.classId = classId;
        assets = new Dictionary<string, T>();
    }
    /*public XmlDB(string classId, List<T> assets)
    {
        this.classId = classId;
        this.assets = new Dictionary<string, T>();
        foreach (T asset in assets)
            this.assets.Add(asset.ID, asset);
    }*/

    public bool ContainsID(string id)
    {
        return assets.ContainsKey(id);
    }

    public void AddRange(List<T> assets)
    {
        foreach (T asset in assets)
            Add(asset);
    }

    public void Add(T asset)
    {
        assets[asset.ID] = asset;
    }

    #region Xml
    public void SaveToXml()
    {
        try
        {
            SaveXml(assets.Values.ToList());
        }
        catch (Exception e)
        {
            throw new Exception("Error saving " + classId + " : " + e.Message);
        }
    }

    public void LoadFromXml()
    {
        List<T> data;
        try
        {
            data = LoadXml();
        }
        catch (Exception e)
        {
            throw new Exception("Error loading " + classId + " : " + e.Message);
        }
        //List<T> data = LoadXml();
        if (assets == null)
            assets = new Dictionary<string, T>();

        foreach (T d in data)
            assets[d.ID] = d;
    }

    static string contentPath = Application.dataPath + @"/../Xml/";
    static string ext = ".xml";
    string dialoguesPath { get { return contentPath + classId + ext; } }

    void SaveXml(List<T> data)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<T>));
        TextWriter writer = new StreamWriter(dialoguesPath);

        ser.Serialize(writer, data);
        writer.Close();
    }

    List<T> LoadXml()
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<T>));
        FileStream fs = new FileStream(dialoguesPath, FileMode.Open);
        return (List<T>)ser.Deserialize(fs);
    }
    #endregion
}

public interface XmlAsset
{
    string ID { get; }
}

public class SpriteDB
{
    Dictionary<string, Sprite> sprites;

    public Sprite this[string id]
    {
        get
        {
            if (sprites.ContainsKey(id))
                return sprites[id];
            else
                return null;
        }
    }

    public void AddRange(List<Sprite> sprites)
    {
        this.sprites = new Dictionary<string, Sprite>();
        foreach (Sprite s in sprites)
            this.sprites.Add(s.name, s);
    }
}