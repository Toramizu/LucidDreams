using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DieSlot :  IDie
{
    DieSlotUI SlotUI;

    RolledDie slottedDie;
    public RolledDie SlottedDie {
        get { return slottedDie; }
        set
        {
            slottedDie = value;

            if(condition.Link != null)
            {
                if(slottedDie == null)
                {
                    condition.Link.Count--;
                    if(condition.Link.Count <= 0)
                        condition.Link.Value = 0;
                }
                else
                {
                    condition.Link.Value = slottedDie.Value;
                    condition.Link.Count++;
                }
            }
        }
    }
    public bool Slotted { get { return SlottedDie != null; } }
    public int Value {
        get
        {
            if (SlottedDie == null)
                return 0;
            else
                return SlottedDie.Value;
        }
    }

    Ability ability;

    DiceCondition condition;
    public DiceCondition Condition {
        get { return condition; }
        set { SetCondition(value); }
    }
    public int[] AcceptedValues { get { return condition.AcceptedValues; } }

    public bool IsActive
    {
        get {
            return SlotUI && SlotUI.isActiveAndEnabled;
        }
    }
    public float X
    {
        get
        {
            if (SlotUI)
                return SlotUI.Pos.x;
            else
                return 0f;
        }
    }
    public float Y
    {
        get
        {
            if (SlotUI)
                return SlotUI.Pos.y;
            else
                return 0f;
        }
    }

    public LinkedValue Link
    {
        get { return condition.Link; }
        set
        {
            condition.Link = value;
            SlotUI.ShowLinks(value != null);
        }
    }

    public DieSlot() { }
    public DieSlot(DiceCondition condition, DieSlotUI slotUI, Ability ability)
    {
        this.ability = ability;
        SlotUI = slotUI;
        slotUI.Init(this);

        SetCondition(condition);
        Link = ability.Link;
    }

    public void SetCondition(DiceCondition cond)
    {
        condition = cond;
        SlotUI.SetCondition(cond);
    }

    public bool Check(int die)
    {
        return condition.Check(die);
    }
    
    public void OnDrop(RolledDie die)
    {
        if (die != null && condition.Check(die.Value))
        {
            SlottedDie = die;
            die.CurrentSlot = this;
            ability.Check();
        }
    }
}