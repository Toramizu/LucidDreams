using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] string crystals = "Crystals";
    public int Crystals {
        get { return Flags.Instance.GetFlag(crystals); }
        set {
            Flags.Instance.SetFlag(crystals, value);
            crystalsText.text = value.ToString();
        }
    }

    [SerializeField] SuccubusUI playerUI;
    public Succubus Player { get {
            if (GameManager.Instance.Status == GameStatus.Day)
                return null;
            else
                return playerUI.Character;
        }
    }
    public Succubus PlayerSuccubus { get { return playerUI.Character; } }
    [SerializeField] TMP_Text crystalsText;
    [SerializeField] SimpleGauge dreamGauge;
    [SerializeField] TMP_Text diceText;

    List<AbilityData> abilities;
    public List<AbilityData> Abilities {
        get { return abilities; }
        set { abilities = new List<AbilityData>(value); }
    }

    public Inventory Inventory { get; private set; } = new Inventory();

    public int HealthIncrement
    {
        get { return PlayerSuccubus.Data.MaxArousal / 2; }
    }

    public void SetPlayer(SuccubusData data)
    {
        PlayerSuccubus.LoadCharacter(data);
        Abilities = data.Abilities;
        //EquipedAbilities = data.Abilities;
        diceText.text = data.Dice.ToString();
        UpdateGauge();
    }

    public void SetPlayer(NightStat stats)
    {
        SuccubusData data = stats.Succubus;
        PlayerSuccubus.LoadCharacter(stats);
        Abilities = data.Abilities;
        diceText.text = data.Dice.ToString();
        UpdateGauge();
    }

    public void LearnAbility(AbilityData data, int cost)
    {
        Crystals -= cost;
        //Debug.Log("Learned " + data.Title);
        abilities.Add(data);
    }

    public void Meditate()
    {
        PlayerSuccubus.Arousal = 0;
        UpdateGauge();
    }

    public void ReduceArousal(int amount, int cost)
    {
        Crystals -= cost;
        PlayerSuccubus.Arousal -= amount;
        UpdateGauge();
    }

    public int InflictDamageProportion(float proportion)
    {
        return PlayerSuccubus.InflictDamageProportion(proportion);
    }

    public void IncreaseMaxArousal(int amount, int cost)
    {
        Crystals -= cost;
        PlayerSuccubus.MaxArousal += amount;
            //amount;
        //player.Arousal = 0;
        UpdateGauge();
    }

    public void AddDie(int amount, int cost)
    {
        Crystals -= cost;
        PlayerSuccubus.Rolls++;
        diceText.text = PlayerSuccubus.Rolls.ToString();
    }

    public void UpdateGauge()
    {
        dreamGauge.Fill(PlayerSuccubus.Arousal, PlayerSuccubus.MaxArousal);
    }
}
