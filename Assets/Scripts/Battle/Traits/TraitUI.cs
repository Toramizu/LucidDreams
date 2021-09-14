using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TraitUI : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TMP_Text text;

    int amount;
    public int Amount
    {
        get { return amount; }
        set
        {
            amount = value;
            if (amount <= 1)
                text.text = "";
            else
                text.text = value.ToString();
        }
    }

    public void Init(Trait t, int amount)
    {
        icon.sprite = t.Icon;
        Amount = amount;
    }
}
