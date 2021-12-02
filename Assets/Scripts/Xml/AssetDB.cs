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

    public AssetDB()
    {
        Images = new SpriteDB();
        Images.LoadImages();

        Abilities = new XmlDB<AbilityData>("Abilities");
        Abilities.LoadFromXml();

        Dialogues = new XmlDB<DialogueData>("Dialogues");
        Dialogues.LoadFromXml();

        Dreams = new XmlDB<DreamData>("Dreams");
        Dreams.LoadFromXml();

        DreamMaps = new XmlDB<DreamMapData>("DreamMaps");
        DreamMaps.LoadFromXml();

        Succubi = new XmlDB<SuccubusData>("Succubi");
        Succubi.LoadFromXml();

        Locations = new XmlDB<LocationData>("Locations");
        Locations.LoadFromXml();

        CharacterDatas = new XmlDB<CharacterData>("Characters");
        CharacterDatas.LoadFromXml();
        Characters = new XmlDB<Character>("Characters");
        LoadCharacters();

        Traits = new XmlDB<Trait>("Traits");
        Traits.AddRange(GameManager.Instance.Assets.Traits);
    }

    public SpriteDB Images { get; private set; }

    public XmlDB<AbilityData> Abilities { get; private set; }
    public XmlDB<CharacterData> CharacterDatas { get; private set; }
    public XmlDB<DialogueData> Dialogues { get; private set; }
    public XmlDB<DreamData> Dreams { get; private set; }
    public XmlDB<DreamMapData> DreamMaps { get; private set; }
    public XmlDB<LocationData> Locations { get; private set; }
    public XmlDB<SuccubusData> Succubi { get; private set; }

    public XmlDB<Trait> Traits { get; private set; }

    public XmlDB<Character> Characters { get; private set; }
    void LoadCharacters()
    {
        //ToDo : Load from save
        foreach (CharacterData data in CharacterDatas.ToList())
            Characters.Add(new Character(data));
    }
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
            if(id == null) throw new System.Exception(classId + " asset was null...");
            else if (assets.ContainsKey(id))
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
        try
        {
            LoadDirXml(contentPath);
            string[] dirs = Directory.GetDirectories(contentPath);
            foreach (string dir in dirs)
                LoadDirXml(dir + "/");
        }
        catch (Exception e)
        {
            throw new Exception("Error loading " + classId + " : " + e.Message);
        }
    }

    void LoadDirXml(string path)
    {
        List<T> data = LoadXml(path);
        
        if (data == null)
            return;

        if (assets == null)
            assets = new Dictionary<string, T>();

        foreach (T d in data)
            assets[d.ID] = d;
    }

    static string contentPath = Application.dataPath + @"/../Content/Xml/";
    static string ext = ".xml";
    string fileName { get { return classId + ext; } }

    void SaveXml(List<T> data)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<T>));
        TextWriter writer = new StreamWriter(contentPath + fileName);

        ser.Serialize(writer, data);
        writer.Close();
    }

    List<T> LoadXml(string path)
    {
        if (!File.Exists(path + fileName))
            return null;

        XmlSerializer ser = new XmlSerializer(typeof(List<T>));
        FileStream fs = new FileStream(path + fileName, FileMode.Open);
        List<T> assets = (List<T>)ser.Deserialize(fs);
        fs.Close();
        return assets;
    }
    #endregion
}

public interface XmlAsset
{
    string ID { get; }
}

public class SpriteDB
{
    static string imagesPath = Application.dataPath + @"/../Content/Images/";
    static List<string> imgExts = new List<string>() { ".png", ".jpg" };

    Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

    public Sprite this[string id]
    {
        get
        {
            if (id != null && sprites.ContainsKey(id))
                return sprites[id];
            else
                return null;
        }
    }

    public int Count
    {
        get { return sprites.Count; }
    }

    public void AddRange(List<Sprite> sprites)
    {
        foreach (Sprite s in sprites)
            this.sprites.Add(s.name, s);
    }

    public void LoadImages()
    {
        try
        {
            AddRange(GetSprites(imagesPath));
            string[] dirs = Directory.GetDirectories(imagesPath);
            foreach (string dir in dirs)
                AddRange(GetSprites(dir));
        }
        catch (Exception e)
        {
            throw new Exception("Error loading Images : " + e.Message);
        }
    }

    List<Sprite> GetSprites(string path)
    {
        List<Sprite> icons = new List<Sprite>();
        DirectoryInfo d = new DirectoryInfo(path);

        foreach (FileInfo file in d.GetFiles())
        {
            Texture2D texture = new Texture2D(64, 64, TextureFormat.ARGB32, false);
            texture.LoadImage(File.ReadAllBytes((file.FullName)));

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            sprite.name = Path.GetFileNameWithoutExtension(file.Name);
            icons.Add(sprite);
        }

        return icons;
    }
}