using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityUI : Hidable
{
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text description;

    [SerializeField] List<DieSlotUI> dieSlots;

    [SerializeField] TMP_Text countText;

    [SerializeField] GameObject lockTransform;
    [SerializeField] LockSlot lockSlot;
    public LockSlot LockSlot { get { return lockSlot; } }

    public AbilityData Data { get; set; }
    Ability ability;

    public void Init(AbilityData data, Ability ability)
    {
        if (data == null)
            return;

        gameObject.SetActive(true);

        title.text = data.Title;
        Data = data;
        this.ability = ability;
        SetCount(data.Total);

        lockTransform.SetActive(false);

        foreach (DieSlotUI slot in dieSlots)
            slot.gameObject.SetActive(false);
    }

    public void Remove()
    {
        gameObject.SetActive(false);
    }

    public DieSlotUI GetDieSlot(int i)
    {
        if (dieSlots.Count > i)
            return dieSlots[i];
        else
            return null;
    }

    public void SetDescription(string description)
    {
        this.description.text = description;
    }

    public void SetCount(int? count)
    {
        if (count == null)
        {
            countText.gameObject.SetActive(false);
        }
        else if (count <= 0)
        {
            countText.gameObject.SetActive(true);
            countText.text = "0";
        }
        else
        {
            countText.text = count.ToString();
            countText.gameObject.SetActive(true);
        }
    }

    public void LockAbility(bool toggle)
    {
        lockTransform.SetActive(toggle);
    }
}
