using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityUI : Hidable
{
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text description;

    /*[SerializeField] List<DieSlot> DiceSlots;
    public List<DieSlot> Slots { get { return DiceSlots; } }*/
    [SerializeField] List<DieSlotUI> dieSlots;

    [SerializeField] TMP_Text countText;

    [SerializeField] GameObject lockTransform;
    [SerializeField] LockSlot lockSlot;
    public LockSlot LockSlot { get { return lockSlot; } }

    public AbilityData Data { get; set; }
    Ability ability;


    /*protected override void Awake()
    {
        base.Awake();
        foreach (DieSlot slot in DiceSlots)
            slot.Ability = ability;
    }*/

    public void Init(AbilityData data, Ability ability)
    {
        gameObject.SetActive(true);

        title.text = data.Title;
        Data = data;
        this.ability = ability;

        lockTransform.SetActive(false);

        foreach (DieSlotUI slot in dieSlots)
            slot.gameObject.SetActive(false);

        /*if (data.EqualDice)
            link = new LinkedValue();
        else
            link = null;

        for (int i = 0; i < DiceSlots.Count; i++)
        {
            if (i < data.Conditions.Count)
            {
                DiceSlots[i].gameObject.SetActive(true);
                DiceSlots[i].SetCondition(data.Conditions[i].ToCondition()); ;
                DiceSlots[i].Linked = link;
            }
            else
                DiceSlots[i].gameObject.SetActive(false);
        }

        if (data.Total <= 0)
            Count = null;
        else
            Count = data.Total;

        effects.Clear();
        foreach (EffectData effect in data.Effects)
            effects.Add(effect.ToEffect());

        Locked = false;
        Used = 0;

        ResetAbility();*/
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
            countText.gameObject.SetActive(true);
        }
    }

    public void LockAbility(bool toggle)
    {
        lockTransform.SetActive(toggle);
    }
}
