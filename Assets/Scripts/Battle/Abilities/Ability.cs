using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Ability// : Hidable
{
    AbilityUI abiUI;

    //[SerializeField] TMP_Text title;
    //[SerializeField] TMP_Text description;

    [SerializeField] List<DieSlot> DiceSlots = new List<DieSlot>();
    public List<DieSlot> Slots { get { return DiceSlots; } }

    //[SerializeField] TMP_Text countText;

    int? count;
    public int? Count {
        get {return count; }
        set {
            count = value;
            if (abiUI != null)
                abiUI.SetCount(value);
        }
    }

    public LinkedValue Link { get; private set; }

    public AbilityData Data { get; private set; }
    public int RemainingUses { get; set; }
    List<AbilityEffect> effects = new List<AbilityEffect>();

    public int Used { get; private set; }

    public LockSlot LockSlot { get { return abiUI.LockSlot; } }
    bool locked;
    public bool Locked {
        get { return locked; }
        set
        {
            locked = value;
            abiUI.LockAbility(value);
        }
    }

    public Ability(AbilityData data, AbilityUI abiUI)
    {
        Init(data, abiUI);
        /*Data = data;
        this.abiUI = abiUI;

        if(abiUI != null)
            abiUI.Init(data, this);*/
    }

    public bool IsActive {
        get
        {
            return abiUI != null && abiUI.isActiveAndEnabled;
        }
    }

    public void Init(AbilityData data, AbilityUI abiUI)
    {
        Data = data;

        this.abiUI = abiUI;
        if (abiUI != null)
            abiUI.Init(data, this);
        
        if (data.EqualDice)
            Link = new LinkedValue();
        else
            Link = null;

        DiceSlots.Clear();
        for(int i = 0; i < data.Conditions.Count; i++)
            DiceSlots.Add(new DieSlot(data.Conditions[i], abiUI.GetDieSlot(i), this));

        /*for (int i = 0; i < DiceSlots.Count; i++)
        {
            if (i < data.Conditions.Count)
            {
                DiceSlots[i].gameObject.SetActive(true);
                DiceSlots[i].SetCondition(data.Conditions[i].ToCondition()); ;
                DiceSlots[i].Linked = link;
            }
            else
                DiceSlots[i].gameObject.SetActive(false);
        }*/

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
        abiUI.Show();
        if (Data.Uses == -1)
            RemainingUses = int.MaxValue;
        else
            RemainingUses = Data.Uses;
        if (Link != null)
        {
            Link.Value = 0;
            Link.Count = 0;
        }

        if (Count <= 0)
            Count = Data.Total;

        RefreshDescr();
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
                slot.SlottedDie.Hide();
                //slot.SlottedDie.gameObject.SetActive(false);
            }
        }

        foreach (AbilityEffect effect in effects)
            effect.CheckAndPlay(total, this);
        
        RemainingUses--;
        Used++;

        if (RemainingUses <= 0)
            abiUI.Hide();
        else
            RefreshDescr();
    }

    public void RefreshDescr()
    {
        if(abiUI != null)
            abiUI.SetDescription(
                GameManager.Instance.Parser.ParseAbilityDescription(this, 
                GameManager.Instance.BattleManager.GetCharacter(true)));
    }

    public /*List<RolledDie>*/ void TryFill(List<RolledDie> dice, Dictionary<RolledDie, IDie> toPlace) // TODO : Manage locked dice
    {
        if (dice.Count == 0) return;

        //Keep dice fitting conditions
        Dictionary<RolledDie, IDie> placed = new Dictionary<RolledDie, IDie>();
        int? count = this.count;

        foreach (IDie slot in DiceSlots)
        {
            if (slot.IsActive)
            {
                foreach (RolledDie die in dice)
                {
                    //If a die fits
                    if (slot.Check(die.Value) && !placed.ContainsKey(die))
                    {
                        //Keep die & go to next slot
                        placed.Add(die, slot);

                        if(count != null)
                        {
                            count -= die.Value;
                            if (count <= 0)
                                break;
                        }
                        else
                            break;
                    }
                    else
                    {
                        //If no die found, skip ability
                        return; //dice;
                    }
                }
            }
        }


        //Place die in slot & Remove placed dice from remaining
        foreach(RolledDie die in placed.Keys)
        {
            toPlace.Add(die, placed[die]);
            dice.Remove(die);
        }
    }

    public float GetAIValue(int dice, AIData current)
    {
        float val = 0;

        foreach (AbilityEffect effect in effects)
            effect.GetAIValue(dice, current, this);

        return val;
    }
}

public enum AbilityStatus
{
    Weak = -1,
    Normal = 0,
    Strong = 1
}
