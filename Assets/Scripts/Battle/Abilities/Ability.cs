using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text description;

    [SerializeField] List<DieSlot> DiceSlots;

    [SerializeField] TMP_Text countText;
    int? count;
    public int? Count {
        get {return count; }
        set { count = value;
            if(count == null)
                countText.text = "";
            else if (count <= 0)
                countText.text = "0";
            else
                countText.text = value.ToString();
        }
    }

    private void Start()
    {
        foreach (DieSlot slot in DiceSlots)
            slot.Ability = this;
    }

    public void Init(AbilityData data)
    {
        title.text = data.Title;
        description.text = data.Description;

        for (int i = 0; i < DiceSlots.Count; i++)
        {
            if (i < data.Conditions.Count)
            {
                DiceSlots[i].gameObject.SetActive(true);
                DiceSlots[i].SetCondition(data.Conditions[i].ToCondition());
            }
            else
                DiceSlots[i].gameObject.SetActive(false);
        }

        if (data.Total <= 0)
            Count = null;
        else
            Count = data.Total;
    }

    public void Check()
    {
        foreach (DieSlot slot in DiceSlots)
        {
            if (!slot.isActiveAndEnabled || !slot.Slotted)
                return;
        }
        PlayAbility();
    }

    void PlayAbility()
    {
        int total = 0;
        foreach (DieSlot slot in DiceSlots)
            total += slot.Value;
        Debug.Log(total);
    }
}
