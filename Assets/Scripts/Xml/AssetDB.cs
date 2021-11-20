using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        LoadDialogues();
        //Dialogues = new XmlDB<DialogueData2>("Dialogue");
    }

    public XmlDB<DialogueData> Dialogues { get; private set; }

    public void SaveDialogues()
    {
        try
        {
            xml.SaveDialogues(Dialogues.ToList());
        } catch(Exception e)
        {
            throw new Exception("Error saving Dialogues : " + e.Message);
        }
    }

    public void LoadDialogues()
    {
        try
        {
            List<DialogueData> lst = xml.LoadDialogues();
            Dialogues = new XmlDB<DialogueData>("Dialogue", lst);
        }
        catch (Exception e)
        {
            throw new Exception("Error loading Dialogues : " + e.Message);
        }
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
    public XmlDB(string classId, List<T> assets)
    {
        this.classId = classId;
        this.assets = new Dictionary<string, T>();
        foreach (T asset in assets)
            this.assets.Add(asset.ID, asset);
    }
}

public interface XmlAsset
{
    string ID { get; }
}
