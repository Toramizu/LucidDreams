using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CharacterData : ImageHaver, XmlAsset
{
    [XmlAttribute("ID")]
    public string ID { get; set; }
    [XmlAttribute("DisplayName")]
    public override string Name { get; set; }
    [XmlElement("Background")]
    public string Background { get; set; }

    [XmlIgnore]
    public Sprite Icon { get {
            return AssetDB.Instance.Images[_Icon]; } }
    [XmlAttribute("Icon")]
    public string _Icon { get; set; }

    [XmlAttribute("Succubus")]
    public string _Succubus { get; set; }
    [XmlIgnore]
    public SuccubusData Succubus { get { return AssetDB.Instance.Succubi[_Succubus]; } }

    [XmlElement("Event")]
    public List<ConditionalDialogue> Events { get; set; }

    [XmlElement("Relationship")]
    public List<RelationshipData> Relationships { get; set; }

    [XmlElement("Unlock")]
    public MultCondition Condition { get; set; }
    [XmlIgnore]
    public bool Check { get { return Condition == null || Condition.Check; } }

    [XmlElement("NightUnlock")]
    public MultCondition NightCondition { get; set; }
    [XmlIgnore]
    public bool NightCheck { get { return Check && (NightCondition == null || NightCondition.Check); } }

    [XmlElement("Location")]
    public List<CharacterLocation> Locations { get; set; }

    [XmlElement("Color")]
    public ColorXml _Color { get; set; }
    [XmlIgnore]
    public Color Color {
        get {
            if (_Color == null)
                return Color.white;
            else
                return _Color.Color;
        }
    }

    [XmlElement("Preferences")]
    public CharacterPrefs Preferences { get; set; }

    [XmlIgnore]
    public bool IsImportant { get
        {
            return Relationships != null && Relationships.Count > 0;
        }
    }
}

public class CharacterLocation
{
    [XmlAttribute("Time")]
    public int Time { get; set; }
    [XmlElement("Location")]
    public List<string> Locations { get; set; }
}