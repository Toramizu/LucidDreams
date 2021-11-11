using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] int crystals;
    public int Crystals {
        get { return crystals; }
        set {
            crystals = value;
            crystalsText.text = value.ToString();
        }
    }

    [SerializeField] CharacterUI playerUI;
    public Character Player { get { return playerUI.Character; } }
    [SerializeField] TMP_Text crystalsText;
    [SerializeField] SimpleGauge dreamGauge;
    [SerializeField] TMP_Text diceText;

    List<AbilityData> abilities;
    public List<AbilityData> Abilities {
        get { return abilities; }
        set { abilities = new List<AbilityData>(value); }
    }

    public int HealthIncrement
    {
        get { return Player.Data.MaxArousal / 2; }
    }

    public void SetPlayer(CharacterData data)
    {
        Player.LoadCharacter(data);
        Abilities = data.Abilities;
        //EquipedAbilities = data.Abilities;
        diceText.text = data.Dice.ToString();
    }

    public void LearnAbility(AbilityData data, int cost)
    {
        Crystals -= cost;
        //Debug.Log("Learned " + data.Title);
        abilities.Add(data);
    }

    public void Meditate()
    {
        Player.Arousal = 0;
        UpdateGauge();
    }

    public void ReduceArousal(int amount, int cost)
    {
        Crystals -= cost;
        Player.Arousal -= amount;
        UpdateGauge();
    }

    public int InflictDamageProportion(float proportion)
    {
        return Player.InflictDamageProportion(proportion);
    }

    public void IncreaseMaxArousal(int amount, int cost)
    {
        Crystals -= cost;
        Player.MaxArousal += amount;
            //amount;
        //player.Arousal = 0;
        UpdateGauge();
    }

    public void AddDie(int amount, int cost)
    {
        Crystals -= cost;
        Player.Rolls++;
        diceText.text = Player.Rolls.ToString();
    }

    public void UpdateGauge()
    {
        dreamGauge.Fill(Player.Arousal, Player.MaxArousal);
    }
}
