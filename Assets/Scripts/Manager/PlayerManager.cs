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
