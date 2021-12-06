using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightPrepUI : Window, CharacterDisplayer
{
    [SerializeField] string defaultSuccubus = "Maid";
    public NightPreps NightPreps { get; private set; } = new NightPreps();

    [SerializeField] CharacterListUI charaList;

    public NightStat Default
    {
        get
        {
            return NightPreps[defaultSuccubus];
        }
    }
    public override void Open()
    {
        FadeIn();
        charaList.Clear();

        List<Character> charas = AssetDB.Instance.Characters.ToList();
        charaList.Open(charas, this);
    }

    public void DisplayCharacter(Character chara)
    {
        Debug.Log(chara.Data.Name);
    }

    public void DisplayNext()
    {
        charaList.DisplayNext();
    }

    public void DisplayPrevious()
    {
        charaList.DisplayPrevious();
    }
}
public class NightPreps
{
    NightStat all = new NightStat(null);
    Dictionary<string, NightStat> characters = new Dictionary<string, NightStat>();

    public NightStat this[string id]
    {
        get { return characters[id]; }
    }

    public void AddArousalBonus(string character, int value, bool mod)
    {
        NightStat stat = GetStat(character);

        if (stat == null)
            return;
        else if (mod)
            stat.ArousalMod *= value;
        else
            stat.ArousalBonus += value;
    }

    public void AddRelationBonus(string character, int relation, int value, bool mod)
    {
        NightStat stat = GetStat(character);

        if (stat == null)
            return;
        else if (mod)
            stat.RelationMod[relation] *= value;
        else
            stat.RelationBonus[relation] += value;
    }

    NightStat GetStat(string character)
    {
        if (character == null)
            return all;
        else
        {
            if (!characters.ContainsKey(character))
            {
                Character chara = AssetDB.Instance.Characters[character];
                if (chara == null)
                {
                    GameManager.Instance.Notify("Character not found : " + character);
                    return null;
                }
                else
                    characters.Add(character, new NightStat(chara));
            }

            return characters[character];
        }
    }

    public void ResetAll()
    {
        all.Reset();
        foreach (NightStat stat in characters.Values)
            stat.Reset();
    }
}

public class NightStat
{
    public Character Character { get; set; }
    public SuccubusData Succubus { get { return Character.Data.Succubus; } }
    public int ArousalBonus { get; set; }
    public float ArousalMod { get; set; }

    public List<int> RelationBonus { get; set; }
    public List<float> RelationMod { get; set; }

    public NightStat(Character character)
    {
        Character = character;
        Reset();
    }

    public void Reset()
    {
        ArousalBonus = 0;
        ArousalMod = 1f;

        RelationBonus = new List<int>() { 0, 0, 0 };
        RelationMod = new List<float>() { 1f, 1f, 1f };
    }
}
