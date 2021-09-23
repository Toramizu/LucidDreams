using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TraitUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image icon;
    [SerializeField] TMP_Text text;

    Trait trait;

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
        trait = t;
        icon.sprite = t.Icon;
        Amount = amount;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.Instance.BattleManager.ShowTooltip(trait);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.Instance.BattleManager.ShowTooltip(null);
    }
}
