using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ability : Hidable
{
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text description;

    [SerializeField] List<DieSlot> DiceSlots;

    [SerializeField] TMP_Text countText;
    [SerializeField] bool isOpponent;

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

    AbilityData data;
    public int Uses { get; set; }
    List<AbilityEffect> effects = new List<AbilityEffect>();

    protected override void Start()
    {
        base.Start();
        foreach (DieSlot slot in DiceSlots)
            slot.Ability = this;
    }

    public void Init(AbilityData data)
    {
        this.data = data;
        title.text = data.Title;
        description.text = GameManager.Instance.Parser.ParseDescription(data.Description);

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

        effects.Clear();
        foreach (EffectData effect in data.Effects)
            effects.Add(effect.ToEffect());

            ResetAbility();
    }

    public void ResetAbility()
    {
        Show();
        Uses = data.Uses;

        if (Count <= 0)
            Count = data.Total;
    }

    public void Check()
    {
        if (count == null)
        {
            foreach (DieSlot slot in DiceSlots)
            {
                if (slot.isActiveAndEnabled && !slot.Slotted)
                    return;
            }
            PlayAbility();
        }
        else
        {
            foreach (DieSlot slot in DiceSlots)
                if(slot.Slotted)
                {
                    Count -= slot.SlottedDie.Value;
                    slot.SlottedDie.gameObject.SetActive(false);
                }

            if(count <= 0)
                PlayAbility();
        }
    }

    void PlayAbility()
    {
        int total = 0;
        foreach (DieSlot slot in DiceSlots)
        {
            if (slot.SlottedDie != null)
            {
                total += slot.Value;
                slot.SlottedDie.gameObject.SetActive(false);
            }
        }

        foreach (AbilityEffect effect in effects)
            effect.Play(isOpponent, total);

        //GameManager.Instance.BattleManager.HitOpponent(total);
        //Debug.Log(total);

        Uses--;
        if (Uses <= 0)
            Hide();
    }
}
