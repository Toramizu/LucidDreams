using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class CharacterPrefs
{
    [XmlElement("Preference")]
    public List<Preference> Preferences { get; set; }
    
    public Preference ItemPreference(Item item)
    {
        List<Preference> prefs = Preferences.Where(i => i.Item == item.ID && i.Check).ToList();
        if (prefs.Count > 0)
            return prefs[0];
        else
        {
            Preference p = new Preference();

            p.Item = item.ID;
            p.Type = PrefEnum.NoThanks;
            p.Description = "I'm not interested, thank you...";
            p.Consumed = false;

            return p;
        }
    }
}

public class Preference
{
    [XmlAttribute("Item")]
    public string Item { get; set; }
    [XmlAttribute("Type")]
    public PrefEnum Type { get; set; }
    [XmlElement("Gain")]
    public List<RelationGain> Gains { get; set; }
    [XmlElement("Description")]
    public string Description { get; set; }
    [XmlAttribute("Consumed")]
    public bool Consumed { get; set; } = true;
    [XmlElement("Condition")]
    public Condition Condition { get; set; }

    [XmlIgnore]
    public bool Check { get { return Condition == null || Condition.Check; } }
}

public class RelationGain
{
    [XmlAttribute("Relationship")]
    public int Relationship { get; set; }
    [XmlAttribute("Points")]
    public int Points { get; set; }
}

public enum PrefEnum
{
    Love,
    Like,
    NoThanks,
    Dislike,
    Wierd,
}