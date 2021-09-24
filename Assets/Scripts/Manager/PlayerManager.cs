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

    [SerializeField] Character player;
    [SerializeField] TMP_Text crystalsText;

    public List<AbilityData> Abilities { get; set; }
    //public List<AbilityData> StoredAbilities { get; set; } = new List<AbilityData>();
    /*{
        get {
            List<AbilityData> abis = new List<AbilityData>();
            foreach(Ability abi in player.Abilities)
                abis.Add(abi.)

            return null;
        }
        set {
            for (int i = 0; i < value.Count; i++)
                player.SetAbility(value[i], i);
        }
    }*/

    public void SetPlayer(CharacterData data)
    {
        player.LoadCharacter(data);
        Abilities = data.Abilities;
        //EquipedAbilities = data.Abilities;
    }

    public void LearnAbility(AbilityData data, int cost)
    {
        crystals -= cost;
        Debug.Log("Learned " + data.Title);
    }

    public void IncreaseMaxArousal(int amount, int cost)
    {
        crystals -= cost;
        player.MaxArousal += amount;
        player.Arousal = player.MaxArousal;
    }
}
