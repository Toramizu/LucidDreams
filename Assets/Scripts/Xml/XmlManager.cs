using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class XmlManager
{
    public static string contentPath = Application.dataPath + @"/../Xml/";
    public static string ext = ".xml";
    public static string modListPath = contentPath + "ModList" + ext;

    #region Dialogues
    static string dialoguesPath = contentPath + "Dialogues" + ext;
    static string dialoguesFile = "/Dialogues" + ext;

    public void SaveDialogues(List<DialogueData> data)
    {
        XmlSerializer ser = new XmlSerializer(typeof(DialogueXml));
        TextWriter writer = new StreamWriter(dialoguesPath);

        ser.Serialize(writer, new DialogueXml(data));
        writer.Close();
    }

    public List<DialogueData> LoadDialogues()
    {
        XmlSerializer ser = new XmlSerializer(typeof(DialogueXml));
        FileStream fs = new FileStream(dialoguesPath, FileMode.Open);
        return ((DialogueXml)ser.Deserialize(fs)).Dialogues;
    }
    #endregion
    /*#region Loading

    public static void LoadXml()
    {
        ModList mods = LoadModList();
        if (mods == null)
            mods = CreateModList();

        LoadAll(mods.Mods);
    }

    static void LoadAll(List<ModData> mods)
    {
        foreach (ModData mod in mods)
            if (mod.Load)
                AssetManager.Instance.Terrains.Add(LoadTerrains(contentPath + mod.ID), true);
        foreach (ModData mod in mods)
            if (mod.Load)
                AssetManager.Instance.Stats.Add(LoadStats(contentPath + mod.ID), true);
        foreach (ModData mod in mods)
            if (mod.Load)
                AssetManager.Instance.Gauges.Add(LoadGauges(contentPath + mod.ID), true);
        foreach (ModData mod in mods)
            if (mod.Load)
                AssetManager.Instance.Skills.Add(LoadSkills(contentPath + mod.ID), true);
        foreach (ModData mod in mods)
            if (mod.Load)
                AssetManager.Instance.DamageTypes.Add(LoadDamageTypes(contentPath + mod.ID), true);
        foreach (ModData mod in mods)
            if (mod.Load)
                AssetManager.Instance.WeaponTypes.Add(LoadWeaponTypes(contentPath + mod.ID), true);
        foreach (ModData mod in mods)
            if (mod.Load)
                AssetManager.Instance.Items.Add(LoadItems(contentPath + mod.ID), true);
        foreach (ModData mod in mods)
            if (mod.Load)
                AssetManager.Instance.Abilities.Add(LoadAbilities(contentPath + mod.ID), true);
        foreach (ModData mod in mods)
            if (mod.Load)
                AssetManager.Instance.Maps.Add(LoadMaps(contentPath + mod.ID), true);
        foreach (ModData mod in mods)
            if (mod.Load)
                AssetManager.Instance.Events.Add(LoadEvents(contentPath + mod.ID), true);
        foreach (ModData mod in mods)
            if (mod.Load)
                AssetManager.Instance.Traits.Add(LoadTraits(contentPath + mod.ID), true);
        foreach (ModData mod in mods)
            if (mod.Load)
                AssetManager.Instance.Bodies.Add(LoadBodies(contentPath + mod.ID), true);
        foreach (ModData mod in mods)
            if (mod.Load)
                AssetManager.Instance.Races.Add(LoadRaces(contentPath + mod.ID), true);
        foreach (ModData mod in mods)
            if (mod.Load)
                AssetManager.Instance.CharacterDatas.Add(LoadCharacters(contentPath + mod.ID), true);
        foreach (ModData mod in mods)
            if (mod.Load)
                AssetManager.Instance.Scenarios.Add(LoadScenarios(contentPath + mod.ID), true);
    }

    static ModList CreateModList()
    {
        string[] dirs = Directory.GetDirectories(contentPath);
        ModList list = new ModList();
        list.Mods = new List<ModData>();
        foreach (string dir in dirs)
        {
            ModData mod = new ModData();
            mod.ID = Path.GetFileName(dir);
            mod.Load = true;
            Debug.Log(mod.ID);
            list.Mods.Add(mod);
        }

        XmlSerializer ser = new XmlSerializer(typeof(ModList));
        TextWriter writer = new StreamWriter(modListPath);

        ser.Serialize(writer, list);
        writer.Close();
        return list;
    }

    static ModList LoadModList()
    {
        if (!File.Exists(modListPath))
            return null;

        XmlSerializer ser = new XmlSerializer(typeof(ModList));
        FileStream fs = new FileStream(modListPath, FileMode.Open);
        return ((ModList)ser.Deserialize(fs));
    }

    public static void OLDLoadXml()
    {
        string[] dirs = Directory.GetDirectories(contentPath);
        foreach (string dir in dirs)
            ProcessDir(dir);
    }

    static void ProcessDir(string path)
    {
        AssetManager.Instance.Terrains.Add(LoadTerrains(path), true);
        AssetManager.Instance.Stats.Add(LoadStats(path), true);
        AssetManager.Instance.Gauges.Add(LoadGauges(path), true);
        AssetManager.Instance.Skills.Add(LoadSkills(path), true);
        AssetManager.Instance.DamageTypes.Add(LoadDamageTypes(path), true);
        AssetManager.Instance.WeaponTypes.Add(LoadWeaponTypes(path), true);
        AssetManager.Instance.Items.Add(LoadItems(path), true);
        AssetManager.Instance.Abilities.Add(LoadAbilities(path), true);
        AssetManager.Instance.Maps.Add(LoadMaps(path), true);
        AssetManager.Instance.Events.Add(LoadEvents(path), true);
        AssetManager.Instance.Traits.Add(LoadTraits(path), true);
        AssetManager.Instance.Bodies.Add(LoadBodies(path), true);
        AssetManager.Instance.Races.Add(LoadRaces(path), true);
        AssetManager.Instance.CharacterDatas.Add(LoadCharacters(path), true);
        AssetManager.Instance.Scenarios.Add(LoadScenarios(path), true);
    }

    #endregion

    #region Terrains
    public static string terrainsPath = contentPath + "Terrains" + ext;
    public static string terrainsFile = "/Terrains" + ext;

    public static void SaveTerrains(List<HexTerrain> terrains)
    {
        XmlSerializer ser = new XmlSerializer(typeof(TerrainsXml));
        TextWriter writer = new StreamWriter(terrainsPath);

        ser.Serialize(writer, new TerrainsXml(terrains));
        writer.Close();
    }

    public static List<HexTerrain> LoadTerrains()
    {
        XmlSerializer ser = new XmlSerializer(typeof(TerrainsXml));
        FileStream fs = new FileStream(terrainsPath, FileMode.Open);
        return ((TerrainsXml)ser.Deserialize(fs)).Terrains;
    }

    public static List<HexTerrain> LoadTerrains(string path)
    {
        if (!File.Exists(path + terrainsFile))
            return null;

        XmlSerializer ser = new XmlSerializer(typeof(TerrainsXml));
        FileStream fs = new FileStream(path + terrainsFile, FileMode.Open);
        return ((TerrainsXml)ser.Deserialize(fs)).Terrains;
    }
    #endregion

    #region Abilities
    public static string abilitiesPath = contentPath + "Abilities" + ext;
    public static string abilitiesFile = "/Abilities" + ext;

    public static void SaveAbilities(List<Ability> abilities)
    {
        XmlSerializer ser = new XmlSerializer(typeof(AbilitiesXml));
        TextWriter writer = new StreamWriter(abilitiesPath);

        ser.Serialize(writer, new AbilitiesXml(abilities));
        writer.Close();
    }

    public static List<Ability> LoadAbilities()
    {
        XmlSerializer ser = new XmlSerializer(typeof(AbilitiesXml));
        FileStream fs = new FileStream(abilitiesPath, FileMode.Open);
        return ((AbilitiesXml)ser.Deserialize(fs)).Abilities;
    }

    public static List<Ability> LoadAbilities(string path)
    {
        if (!File.Exists(path + abilitiesFile))
            return null;

        XmlSerializer ser = new XmlSerializer(typeof(AbilitiesXml));
        FileStream fs = new FileStream(path + abilitiesFile, FileMode.Open);
        return ((AbilitiesXml)ser.Deserialize(fs)).Abilities;
    }
    #endregion

    #region Skills
    public static string skillsPath = contentPath + "Skills" + ext;
    public static string skillsFile = "/Skills" + ext;

    public static void SaveSkills(List<Skill> skills)
    {
        XmlSerializer ser = new XmlSerializer(typeof(SkillsXml));
        TextWriter writer = new StreamWriter(skillsPath);

        ser.Serialize(writer, new SkillsXml(skills));
        writer.Close();
    }

    public static List<Skill> LoadSkills()
    {
        XmlSerializer ser = new XmlSerializer(typeof(SkillsXml));
        FileStream fs = new FileStream(skillsPath, FileMode.Open);
        return ((SkillsXml)ser.Deserialize(fs)).Skills;
    }

    public static List<Skill> LoadSkills(string path)
    {
        if (!File.Exists(path + skillsFile))
            return null;

        XmlSerializer ser = new XmlSerializer(typeof(SkillsXml));
        FileStream fs = new FileStream(path + skillsFile, FileMode.Open);
        return ((SkillsXml)ser.Deserialize(fs)).Skills;
    }
    #endregion

    #region Characters
    public static string charactersPath = contentPath + "Characters" + ext;
    public static string charactersFile = "/Characters" + ext;

    public static void SaveCharacters(List<CharacterData> characters)
    {
        XmlSerializer ser = new XmlSerializer(typeof(CharactersXml));
        TextWriter writer = new StreamWriter(charactersPath);

        ser.Serialize(writer, new CharactersXml(characters));
        writer.Close();
    }

    public static List<CharacterData> LoadCharacters()
    {
        XmlSerializer ser = new XmlSerializer(typeof(CharactersXml));
        FileStream fs = new FileStream(charactersPath, FileMode.Open);
        return ((CharactersXml)ser.Deserialize(fs)).Characters;
    }

    public static List<CharacterData> LoadCharacters(string path)
    {
        if (!File.Exists(path + charactersFile))
            return null;

        XmlSerializer ser = new XmlSerializer(typeof(CharactersXml));
        FileStream fs = new FileStream(path + charactersFile, FileMode.Open);
        return ((CharactersXml)ser.Deserialize(fs)).Characters;
    }
    #endregion

    #region Maps
    public static string mapsPath = contentPath + "Maps" + ext;
    public static string mapsFile = "/Maps" + ext;

    public static void SaveMaps(List<MapData> maps)
    {
        XmlSerializer ser = new XmlSerializer(typeof(MapsXml));
        TextWriter writer = new StreamWriter(mapsPath);

        ser.Serialize(writer, new MapsXml(maps));
        writer.Close();
    }

    public static List<MapData> LoadMaps()
    {
        XmlSerializer ser = new XmlSerializer(typeof(MapsXml));
        FileStream fs = new FileStream(mapsPath, FileMode.Open);
        return ((MapsXml)ser.Deserialize(fs)).Maps;
    }

    public static List<MapData> LoadMaps(string path)
    {
        if (!File.Exists(path + mapsFile))
            return null;

        XmlSerializer ser = new XmlSerializer(typeof(MapsXml));
        FileStream fs = new FileStream(path + mapsFile, FileMode.Open);
        return ((MapsXml)ser.Deserialize(fs)).Maps;
    }
    #endregion

    #region Events
    public static string eventsPath = contentPath + "Events" + ext;
    public static string eventsFile = "/Events" + ext;

    public static void SaveEvents(List<GameEvent> maps)
    {
        XmlSerializer ser = new XmlSerializer(typeof(EventsXml));
        TextWriter writer = new StreamWriter(eventsPath);

        ser.Serialize(writer, new EventsXml(maps));
        writer.Close();
    }

    public static void SaveEventsDebug(List<GameEvent> maps)
    {
        string eventsPath2 = contentPath + "DEvents" + ext;
        XmlSerializer ser = new XmlSerializer(typeof(EventsXml));
        TextWriter writer = new StreamWriter(eventsPath2);

        ser.Serialize(writer, new EventsXml(maps));
        writer.Close();
    }

    public static List<GameEvent> LoadEvents()
    {
        XmlSerializer ser = new XmlSerializer(typeof(EventsXml));
        FileStream fs = new FileStream(eventsPath, FileMode.Open);
        return ((EventsXml)ser.Deserialize(fs)).Events;
    }

    public static List<GameEvent> LoadEvents(string path)
    {
        if (!File.Exists(path + eventsFile))
            return null;

        XmlSerializer ser = new XmlSerializer(typeof(EventsXml));
        FileStream fs = new FileStream(path + eventsFile, FileMode.Open);
        return ((EventsXml)ser.Deserialize(fs)).Events;
    }
    #endregion

    #region Items
    public static string itemsPath = contentPath + "Items" + ext;
    public static string itemsFile = "/Items" + ext;

    public static void SaveItems(List<BasicItem> items)
    {
        XmlSerializer ser = new XmlSerializer(typeof(ItemsXml));
        TextWriter writer = new StreamWriter(itemsPath);

        ser.Serialize(writer, new ItemsXml(items));
        writer.Close();
    }

    public static List<BasicItem> LoadItems()
    {
        XmlSerializer ser = new XmlSerializer(typeof(ItemsXml));
        FileStream fs = new FileStream(itemsPath, FileMode.Open);
        return ((ItemsXml)ser.Deserialize(fs)).Items;
    }

    public static List<BasicItem> LoadItems(string path)
    {
        if (!File.Exists(path + itemsFile))
            return null;

        XmlSerializer ser = new XmlSerializer(typeof(ItemsXml));
        FileStream fs = new FileStream(path + itemsFile, FileMode.Open);
        return ((ItemsXml)ser.Deserialize(fs)).Items;
    }
    #endregion

    #region Gauges
    public static string gaugesPath = contentPath + "Gauges" + ext;
    public static string gaugesFile = "/Gauges" + ext;

    public static void SaveGauges(List<Gauge> items)
    {
        XmlSerializer ser = new XmlSerializer(typeof(GaugesXml));
        TextWriter writer = new StreamWriter(gaugesPath);

        ser.Serialize(writer, new GaugesXml(items));
        writer.Close();
    }

    public static List<Gauge> LoadGauges()
    {
        XmlSerializer ser = new XmlSerializer(typeof(GaugesXml));
        FileStream fs = new FileStream(gaugesPath, FileMode.Open);
        return ((GaugesXml)ser.Deserialize(fs)).Gauges;
    }

    public static List<Gauge> LoadGauges(string path)
    {
        if (!File.Exists(path + gaugesFile))
            return null;

        XmlSerializer ser = new XmlSerializer(typeof(GaugesXml));
        FileStream fs = new FileStream(path + gaugesFile, FileMode.Open);
        return ((GaugesXml)ser.Deserialize(fs)).Gauges;
    }
    #endregion

    #region Stats
    public static string statsPath = contentPath + "Stats" + ext;
    public static string statsFile = "/Stats" + ext;

    public static void SaveStats(List<Statistic> items)
    {
        XmlSerializer ser = new XmlSerializer(typeof(StatssXml));
        TextWriter writer = new StreamWriter(statsPath);

        ser.Serialize(writer, new StatssXml(items));
        writer.Close();
    }

    public static List<Statistic> LoadStats()
    {
        XmlSerializer ser = new XmlSerializer(typeof(StatssXml));
        FileStream fs = new FileStream(statsPath, FileMode.Open);
        return ((StatssXml)ser.Deserialize(fs)).Stats;
    }

    public static List<Statistic> LoadStats(string path)
    {
        if (!File.Exists(path + statsFile))
            return null;

        XmlSerializer ser = new XmlSerializer(typeof(StatssXml));
        FileStream fs = new FileStream(path + statsFile, FileMode.Open);
        return ((StatssXml)ser.Deserialize(fs)).Stats;
    }
    #endregion

    #region Traits
    public static string traitsPath = contentPath + "Traits" + ext;
    public static string traitsFile = "/Traits" + ext;

    public static void SaveTraits(List<Trait> items)
    {
        XmlSerializer ser = new XmlSerializer(typeof(TraitsXml));
        TextWriter writer = new StreamWriter(traitsPath);

        ser.Serialize(writer, new TraitsXml(items));
        writer.Close();
    }

    public static List<Trait> LoadTraits()
    {
        XmlSerializer ser = new XmlSerializer(typeof(TraitsXml));
        FileStream fs = new FileStream(traitsPath, FileMode.Open);
        return ((TraitsXml)ser.Deserialize(fs)).Traits;
    }

    public static List<Trait> LoadTraits(string path)
    {
        if (!File.Exists(path + traitsFile))
            return null;

        XmlSerializer ser = new XmlSerializer(typeof(TraitsXml));
        FileStream fs = new FileStream(path + traitsFile, FileMode.Open);
        return ((TraitsXml)ser.Deserialize(fs)).Traits;
    }
    #endregion

    #region Weapon Types
    public static string WeaponTypesPath = contentPath + "WeaponTypes" + ext;
    public static string WeaponTypesFile = "/WeaponTypes" + ext;

    public static void SaveWeaponTypes(List<WeaponType> items)
    {
        XmlSerializer ser = new XmlSerializer(typeof(WeaponTypesXml));
        TextWriter writer = new StreamWriter(WeaponTypesPath);

        ser.Serialize(writer, new WeaponTypesXml(items));
        writer.Close();
    }

    public static List<WeaponType> LoadWeaponTypes()
    {
        XmlSerializer ser = new XmlSerializer(typeof(WeaponTypesXml));
        FileStream fs = new FileStream(WeaponTypesPath, FileMode.Open);
        return ((WeaponTypesXml)ser.Deserialize(fs)).WeaponTypes;
    }

    public static List<WeaponType> LoadWeaponTypes(string path)
    {
        if (!File.Exists(path + WeaponTypesFile))
            return null;

        XmlSerializer ser = new XmlSerializer(typeof(WeaponTypesXml));
        FileStream fs = new FileStream(path + WeaponTypesFile, FileMode.Open);
        return ((WeaponTypesXml)ser.Deserialize(fs)).WeaponTypes;
    }
    #endregion

    #region Damage Types
    public static string DamageTypesPath = contentPath + "DamageTypes" + ext;
    public static string DamageTypesFile = "/DamageTypes" + ext;

    public static void SaveDamageTypes(List<DamageType> items)
    {
        XmlSerializer ser = new XmlSerializer(typeof(DamageTypesXml));
        TextWriter writer = new StreamWriter(DamageTypesPath);

        ser.Serialize(writer, new DamageTypesXml(items));
        writer.Close();
    }

    public static List<DamageType> LoadDamageTypes()
    {
        XmlSerializer ser = new XmlSerializer(typeof(DamageTypesXml));
        FileStream fs = new FileStream(DamageTypesPath, FileMode.Open);
        return ((DamageTypesXml)ser.Deserialize(fs)).DamageTypes;
    }

    public static List<DamageType> LoadDamageTypes(string path)
    {
        if (!File.Exists(path + DamageTypesFile))
            return null;

        XmlSerializer ser = new XmlSerializer(typeof(DamageTypesXml));
        FileStream fs = new FileStream(path + DamageTypesFile, FileMode.Open);
        return ((DamageTypesXml)ser.Deserialize(fs)).DamageTypes;
    }
    #endregion

    #region Scenarios
    public static string ScenariosPath = contentPath + "Scenarios" + ext;
    public static string ScenariosFile = "/Scenarios" + ext;

    public static void SaveScenarios(List<Scenario> items)
    {
        XmlSerializer ser = new XmlSerializer(typeof(ScenariosXml));
        TextWriter writer = new StreamWriter(ScenariosPath);

        ser.Serialize(writer, new ScenariosXml(items));
        writer.Close();
    }

    public static List<Scenario> LoadScenarios()
    {
        XmlSerializer ser = new XmlSerializer(typeof(ScenariosXml));
        FileStream fs = new FileStream(ScenariosPath, FileMode.Open);
        return ((ScenariosXml)ser.Deserialize(fs)).Scenarios;
    }

    public static List<Scenario> LoadScenarios(string path)
    {
        if (!File.Exists(path + ScenariosFile))
            return null;

        XmlSerializer ser = new XmlSerializer(typeof(ScenariosXml));
        FileStream fs = new FileStream(path + ScenariosFile, FileMode.Open);
        return ((ScenariosXml)ser.Deserialize(fs)).Scenarios;
    }
    #endregion

    #region Bodies
    public static string BodiesPath = contentPath + "Bodies" + ext;
    public static string BodiesFile = "/Bodies" + ext;

    public static void SaveBodies(List<Body> items)
    {
        XmlSerializer ser = new XmlSerializer(typeof(BodiesXml));
        TextWriter writer = new StreamWriter(BodiesPath);

        ser.Serialize(writer, new BodiesXml(items));
        writer.Close();
    }

    public static List<Body> LoadBodies()
    {
        XmlSerializer ser = new XmlSerializer(typeof(BodiesXml));
        FileStream fs = new FileStream(BodiesPath, FileMode.Open);
        return ((BodiesXml)ser.Deserialize(fs)).Bodies;
    }

    public static List<Body> LoadBodies(string path)
    {
        if (!File.Exists(path + BodiesFile))
            return null;

        XmlSerializer ser = new XmlSerializer(typeof(BodiesXml));
        FileStream fs = new FileStream(path + BodiesFile, FileMode.Open);
        return ((BodiesXml)ser.Deserialize(fs)).Bodies;
    }
    #endregion

    #region Races
    public static string RacesPath = contentPath + "Races" + ext;
    public static string RacesFile = "/Races" + ext;

    public static void SaveRaces(List<Race> items)
    {
        XmlSerializer ser = new XmlSerializer(typeof(RacesXml));
        TextWriter writer = new StreamWriter(RacesPath);

        ser.Serialize(writer, new RacesXml(items));
        writer.Close();
    }

    public static List<Race> LoadRaces()
    {
        XmlSerializer ser = new XmlSerializer(typeof(RacesXml));
        FileStream fs = new FileStream(RacesPath, FileMode.Open);
        return ((RacesXml)ser.Deserialize(fs)).Races;
    }

    public static List<Race> LoadRaces(string path)
    {
        if (!File.Exists(path + RacesFile))
            return null;

        XmlSerializer ser = new XmlSerializer(typeof(RacesXml));
        FileStream fs = new FileStream(path + RacesFile, FileMode.Open);
        return ((RacesXml)ser.Deserialize(fs)).Races;
    }
    #endregion

    #region Dungeon
    public static string DungeonsPath = contentPath + "Dungeons" + ext;

    public static void SaveDungeons(List<OLD_Dungeon> items)
    {
        XmlSerializer ser = new XmlSerializer(typeof(DungeonsXml));
        TextWriter writer = new StreamWriter(DungeonsPath);

        ser.Serialize(writer, new DungeonsXml(items));
        writer.Close();
    }

    public static List<OLD_Dungeon> LoadDungeons()
    {
        XmlSerializer ser = new XmlSerializer(typeof(DungeonsXml));
        FileStream fs = new FileStream(DungeonsPath, FileMode.Open);
        return ((DungeonsXml)ser.Deserialize(fs)).Dungeons;
    }
    #endregion

    #region KeyBinds
    public static string KeysPath = contentPath + "KeyCodes" + ext;
    public static string KeysFile = "KeyCodes" + ext;

    public static void SaveKeys(KeyMapping keys)
    {
        XmlSerializer ser = new XmlSerializer(typeof(KeyMapping));
        TextWriter writer = new StreamWriter(KeysPath);

        ser.Serialize(writer, keys);
        writer.Close();
    }

    public static KeyMapping LoadKeys()
    {
        XmlSerializer ser = new XmlSerializer(typeof(KeyMapping));
        FileStream fs = new FileStream(KeysPath, FileMode.Open);
        return ((KeyMapping)ser.Deserialize(fs));
    }
    #endregion
}

public class TerrainsXml
{
    [XmlElement("Terrain")]
    public List<HexTerrain> Terrains { get; set; }

    public TerrainsXml() { Terrains = new List<HexTerrain>(); }
    public TerrainsXml(List<HexTerrain> terrains) { Terrains = terrains; }
}
public class AbilitiesXml
{
    [XmlElement("Ability", typeof(MagicAbility))]
    [XmlElement("WeaponAbility", typeof(WeaponAbility))]
    public List<Ability> Abilities { get; set; }

    public AbilitiesXml() { Abilities = new List<Ability>(); }
    public AbilitiesXml(List<Ability> abilities) { Abilities = abilities; }
}
public class SkillsXml
{
    [XmlElement("Skill")]
    public List<Skill> Skills { get; set; }

    public SkillsXml() { Skills = new List<Skill>(); }
    public SkillsXml(List<Skill> skills) { Skills = skills; }
}
public class CharactersXml
{
    [XmlElement("Character")]
    public List<CharacterData> Characters { get; set; }

    public CharactersXml() { Characters = new List<CharacterData>(); }
    public CharactersXml(List<CharacterData> characters) { Characters = characters; }
}
public class MapsXml
{
    [XmlElement("Map")]
    public List<MapData> Maps { get; set; }

    public MapsXml() { Maps = new List<MapData>(); }
    public MapsXml(List<MapData> map) { Maps = map; }
}
public class EventsXml
{
    [XmlElement("Dialogue", typeof(Dialogue))]
    [XmlElement("Encounter", typeof(Encounter))]
    [XmlElement("Dungeon", typeof(Dungeon))]
    public List<GameEvent> Events { get; set; }

    public EventsXml() { Events = new List<GameEvent>(); }
    public EventsXml(List<GameEvent> gEvent) { Events = gEvent; }
}
public class ItemsXml
{
    [XmlElement("Weapon", typeof(Weapon))]
    [XmlElement("Armor", typeof(Armor))]
    [XmlElement("Belt", typeof(ShieldBelt))]
    [XmlElement("Food", typeof(FoodItem))]
    [XmlElement("Item", typeof(BasicItem))]
    public List<BasicItem> Items { get; set; }

    public ItemsXml() { Items = new List<BasicItem>(); }
    public ItemsXml(List<BasicItem> items) { Items = items; }
}
public class GaugesXml
{
    public List<Gauge> Gauges { get; set; }

    public GaugesXml() { Gauges = new List<Gauge>(); }
    public GaugesXml(List<Gauge> items) { Gauges = items; }
}
public class StatssXml
{
    public List<Statistic> Stats { get; set; }

    public StatssXml() { Stats = new List<Statistic>(); }
    public StatssXml(List<Statistic> items) { Stats = items; }
}
public class TraitsXml
{
    [XmlElement("StackTrait", typeof(StackableTrait))]
    [XmlElement("EvoTrait", typeof(EvolvingTrait))]
    [XmlElement("Trait")]
    public List<Trait> Traits { get; set; }

    public TraitsXml() { Traits = new List<Trait>(); }
    public TraitsXml(List<Trait> items) { Traits = items; }
}
public class WeaponTypesXml
{
    [XmlElement("WeaponType")]
    public List<WeaponType> WeaponTypes { get; set; }

    public WeaponTypesXml() { WeaponTypes = new List<WeaponType>(); }
    public WeaponTypesXml(List<WeaponType> items) { WeaponTypes = items; }
}
public class DamageTypesXml
{
    [XmlElement("DamageType")]
    public List<DamageType> DamageTypes { get; set; }

    public DamageTypesXml() { DamageTypes = new List<DamageType>(); }
    public DamageTypesXml(List<DamageType> items) { DamageTypes = items; }
}
public class ScenariosXml
{
    [XmlElement("Scenario")]
    public List<Scenario> Scenarios { get; set; }

    public ScenariosXml() { Scenarios = new List<Scenario>(); }
    public ScenariosXml(List<Scenario> items) { Scenarios = items; }
}
public class BodiesXml
{
    [XmlElement("Body")]
    public List<Body> Bodies { get; set; }

    public BodiesXml() { Bodies = new List<Body>(); }
    public BodiesXml(List<Body> items) { Bodies = items; }
}
public class RacesXml
{
    [XmlElement("Race")]
    public List<Race> Races { get; set; }

    public RacesXml() { Races = new List<Race>(); }
    public RacesXml(List<Race> items) { Races = items; }
}
public class DungeonsXml
{
    [XmlElement("Dungeon")]
    public List<OLD_Dungeon> Dungeons { get; set; }

    public DungeonsXml() { Dungeons = new List<OLD_Dungeon>(); }
    public DungeonsXml(List<OLD_Dungeon> items) { Dungeons = items; }
}

public class ModList
{
    [XmlElement("Module")]
    public List<ModData> Mods { get; set; }
}

public class ModData
{
    [XmlAttribute("ID")]
    public string ID { get; set; }
    [XmlAttribute("Load")]
    public bool Load { get; set; }*/
}

public class DialogueXml
{
    [XmlElement("Dialogue")]
    public List<DialogueData> Dialogues { get; set; }

    public DialogueXml() { Dialogues = new List<DialogueData>(); }
    public DialogueXml(List<DialogueData> dialogues) { Dialogues = dialogues; }
}