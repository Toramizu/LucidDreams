using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NightPrepUI : Window, CharacterDisplayer
{
    [SerializeField] string defaultSuccubus = "Maid";
    public NightPreps NightPreps { get; private set; } = new NightPreps();

    NightStat displayed;
    [SerializeField] Image charaImage;
    [SerializeField] TMP_Text charaName;
    [SerializeField] Image charaBackground;
    [SerializeField] Image succuImage;
    [SerializeField] TMP_Text succuName;
    [SerializeField] Image succuBackground;
    [SerializeField] float backgroundColorFading;

    [SerializeField] TMP_Text summary;

    [SerializeField] CharacterListUI charaList;

    public NightStat Default
    {
        get
        {
            return NightPreps[defaultSuccubus];
        }
    }

    public override void FadeIn()
    {
        base.FadeIn();
        charaList.Clear();

        List<Character> charas = AssetDB.Instance.Characters.ToList().Where(c => c.NightCheck).ToList(); ;
        charaList.Open(charas, this);

        if (displayed == null)
            DisplayCharacter(charas[0]);
        else
            DisplayCharacter(displayed.Character);
    }

    public void DisplayCharacter(Character chara)
    {
        displayed = NightPreps[chara.ID];

        if (displayed == null)
        {
            NightPreps.AddCharacter(chara);
            displayed = NightPreps[chara.ID];
        }

        charaImage.sprite = chara.Data.Image.Image;
        charaName.text = chara.Data.Name;
        succuImage.sprite = chara.Data.Succubus.Image.Image;
        succuName.text = chara.Data.Succubus.Name;

        if (chara.Data.Color != null)
        {
            Color color = chara.Data.Color.Color;
            Color faded = new Color();
            faded.r = color.r + (1f - color.r) * backgroundColorFading;
            faded.g = color.g + (1f - color.g) * backgroundColorFading;
            faded.b = color.b + (1f - color.b) * backgroundColorFading;
            faded.a = 1;

            charaBackground.color = faded;
            succuBackground.color = faded;
        }
        else
        {
            charaBackground.color = Color.white;
            succuBackground.color = Color.white;
        }

        summary.text = displayed.Summary();
    }

    public void DisplayNext()
    {
        charaList.DisplayNext();
    }

    public void DisplayPrevious()
    {
        charaList.DisplayPrevious();
    }

    public override void FadeOut()
    {
        base.FadeOut();
        GameManager.Instance.NextDay();
    }

    public void StartNight()
    {
        GameManager.Instance.StartDream(displayed);
    }
}

public class NightPreps
{
    NightStat all = new NightStat(null);
    Dictionary<string, NightStat> characters = new Dictionary<string, NightStat>();

    public NightStat this[string id]
    {
        get {
            if (characters.ContainsKey(id))
                return characters[id];
            else
                return null;
        }
    }

    public void AddCharacter(Character chara)
    {
        characters[chara.ID] = new NightStat(chara);
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

    public int FinalArousal
    {
        get { return (int)(Succubus.MaxArousal * ArousalMod + .5f) + ArousalBonus; }
    }

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

    public string Summary()
    {
        SuccubusData sData = Succubus;
        StringBuilder txt = new StringBuilder();

        int arousal = FinalArousal;
        txt.Append("Arousal : ").Append(arousal);
        if (ArousalBonus != 0 || ArousalMod != 1f)
        {
            txt.Append("(");

            if (ArousalMod != 1f)
            {
                txt.Append("*").Append(ArousalMod);
                if (ArousalBonus != 0)
                    txt.Append(" ");
            }

            if (ArousalBonus != 0)
                txt.Append("+").Append(ArousalBonus);

            txt.Append(")");
        }

        txt.Append("\nDice : ").Append(sData.Dice);

        if (RelationBonus[0] != 0 || RelationMod[0] != 1f)
        {
            txt.Append("\nFriendship : ");

            if (RelationMod[0] != 1f)
            {
                txt.Append("*").Append(RelationMod[0]);
                if (RelationBonus[0] != 0)
                    txt.Append(" ");
            }

            if (RelationBonus[0] != 0)
                txt.Append("+").Append(RelationBonus[0]);
        }

        if (RelationBonus[1] != 0 || RelationMod[1] != 1f)
        {
            txt.Append("\nLove : ");

            if (RelationMod[1] != 1f)
            {
                txt.Append("*").Append(RelationMod[1]);
                if (RelationBonus[1] != 0)
                    txt.Append(" ");
            }

            if (RelationBonus[1] != 0)
                txt.Append("+").Append(RelationBonus[1]);
        }

        if (RelationBonus[2] != 0 || RelationMod[2] != 1f)
        {
            txt.Append("\nLoss : ");

            if (RelationMod[2] != 1f)
            {
                txt.Append("*").Append(RelationMod[2]);
                if (RelationBonus[2] != 0)
                    txt.Append(" ");
            }

            if (RelationBonus[2] != 0)
                txt.Append("+").Append(RelationBonus[2]);
        }

        return txt.ToString();
    }

}
