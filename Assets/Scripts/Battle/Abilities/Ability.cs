using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Ability
{
    public AbilityUI AbiUI { get; private set; }

    [SerializeField] List<DieSlot> DiceSlots = new List<DieSlot>();
    public List<DieSlot> Slots { get { return DiceSlots; } }

    int? count;
    public int? Count {
        get {return count; }
        set {
            count = value;
            if (AbiUI != null)
                AbiUI.SetCount(value);
        }
    }

    public LinkedValue Link { get; private set; }

    public AbilityData Data { get; private set; }
    public int RemainingUses { get; set; }
    List<AbilityEffect> effects = new List<AbilityEffect>();

    public int Used { get; private set; }

    public LockSlot LockSlot { get; private set; }
    bool locked;
    public bool Locked {
        get { return locked; }
        set
        {
            locked = value;
            AbiUI.LockAbility(value);
        }
    }

    public Ability() { }
    public Ability(AbilityData data, AbilityUI abiUI)
    {
        Init(data, abiUI);
    }

    public bool IsActive {
        get
        {
            return AbiUI != null && AbiUI.isActiveAndEnabled;
        }
    }

    public void Init(AbilityData data, AbilityUI abiUI)
    {
        Data = data;

        this.AbiUI = abiUI;
        if (abiUI != null)
            abiUI.Init(data, this);
        
        if (data.EqualDice)
            Link = new LinkedValue();
        else
            Link = null;

        DiceSlots.Clear();
        for(int i = 0; i < data.Conditions.Count; i++)
            DiceSlots.Add(new DieSlot(data.Conditions[i], abiUI.GetDieSlot(i), this));

        LockSlot = new LockSlot(this);
        
        if (data.Total <= 0)
            Count = null;
        else
            Count = data.Total;

        effects.Clear();
        foreach (EffectData effect in data.Effects)
            effects.Add(effect.ToEffect());

        Locked = false;
        Used = 0;

        ResetAbility();
    }

    public void ResetAbility()
    {
        AbiUI.Open();
        if (Data.Uses == -1)
            RemainingUses = int.MaxValue;
        else
            RemainingUses = Data.Uses;
        if (Link != null)
        {
            Link.Value = 0;
            Link.Count = 0;
        }

        if (count != null && Count <= 0)
            Count = Data.Total;

        RefreshDescription();
    }

    public void Check()
    {
        if (count == null)
        {
            foreach (DieSlot slot in DiceSlots)
            {
                if (slot.IsActive && !slot.Slotted)
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
                    slot.SlottedDie.Hide();
                    //slot.SlottedDie.gameObject.SetActive(false);
                }

            if(count <= 0)
            {
                PlayAbility();
                Count = Data.Total;
            }
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
                slot.SlottedDie.Hide();
                //slot.SlottedDie.gameObject.SetActive(false);
            }
        }

        PlayAbility(
            GameManager.Instance.BattleManager.GetCharacter(true),
            GameManager.Instance.BattleManager.GetCharacter(false),
            total);

        if (AbiUI != null)
            RefreshDescription();
    }

    public void PlayAbility(Character user, Character other, int dice)
    {
        foreach (AbilityEffect effect in effects)
            effect.CheckAndPlay(user, other, dice, this);

        RemainingUses--;
        Used++;

        if (AbiUI != null)
        {
            if (RemainingUses <= 0)
                AbiUI.Close();
            else
                RefreshDescription();
        }
    }

    public void RefreshDescription()
    {
        if(AbiUI != null)
        {
            AbiUI.SetDescription(
                GameManager.Instance.Parser.ParseAbilityDescription(this,
                GameManager.Instance.BattleManager.GetCharacter(true)));
            AbiUI.SetUses(RemainingUses);
        }
    }

    public Ability Clone()
    {
        Ability a = new Ability();
        a.Data = Data;
        a.count = count;

        if (a.Data.EqualDice)
            Link = new LinkedValue();
        else
            Link = null;

        a.Used = Used;
        a.RemainingUses = RemainingUses;


        a.effects = effects;
        a.DiceSlots = DiceSlots;
        a.locked = locked;
        a.LockSlot = LockSlot;
        return a;
    }
}

public enum AbilityStatus
{
    Weak = -1,
    Normal = 0,
    Strong = 1
}
