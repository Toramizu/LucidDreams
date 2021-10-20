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
    Character player { get { return playerUI.Character; } }
    [SerializeField] TMP_Text crystalsText;
    [SerializeField] SimpleGauge dreamGauge;

    List<AbilityData> abilities;
    public List<AbilityData> Abilities {
        get { return abilities; }
        set { abilities = new List<AbilityData>(value); }
    }

    public void SetPlayer(CharacterData data)
    {
        player.LoadCharacter(data);
        Abilities = data.Abilities;
        //EquipedAbilities = data.Abilities;
    }

    public void LearnAbility(AbilityData data, int cost)
    {
        crystals -= cost;
        //Debug.Log("Learned " + data.Title);
        abilities.Add(data);
    }

    public void IncreaseMaxArousal(int amount, int cost)
    {
        crystals -= cost;
        player.MaxArousal += amount;
        player.Arousal = 0;
        UpdateGauge();
    }

    public void UpdateGauge()
    {
        dreamGauge.Fill(player.Arousal, player.MaxArousal);
    }
}
