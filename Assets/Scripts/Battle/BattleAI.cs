using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class BattleAI
{
    [SerializeField] int sample = 10000;

    List<SimpleSlot> sSlots;
    List<SimpleDie> sDice;
    
    public List<DieToSlot> FindNextAction(List<Ability> abilities, List<RolledDie> dice, Character user, Character target)
    {
        Debug.Log("Start AI");
        sSlots = new List<SimpleSlot>();
        foreach (Ability ability in abilities)
        {
            if (ability.isActiveAndEnabled)
            {
                SlotSharedData cd = new SlotSharedData(ability.Count, ability.Uses);
                cd.Ability = ability;

                if (ability.Locked)
                {
                    SimpleSlot lok = new SimpleSlot(ability.LockSlot);
                    sSlots.Add(lok);

                    foreach (DieSlot slot in ability.Slots)
                        if(slot.isActiveAndEnabled)
                            new SimpleSlot(slot, lok, cd);
                }
                else
                {
                    foreach (DieSlot slot in ability.Slots)
                        if (slot.isActiveAndEnabled)
                            sSlots.Add(new SimpleSlot(slot, null, cd));
                }
            }
        }

        sDice = new List<SimpleDie>();
        foreach (RolledDie die in dice)
            sDice.Add(new SimpleDie(die));

        AIData best = new AIData();
        List<DieToSlot> bestTry = null;

        for(int i = 0; i < sample; i++)
        {
            AIData current = new AIData(user, target);
            List<DieToSlot> currentTry = RandomTry(current);
            //int value = Random.Range(int.MinValue, int.MaxValue);

            current.FinalCheck();
            //Debug.Log(current.AIValue);

            if(current.AIValue > best.AIValue)
            {
                best = current;
                bestTry = currentTry;
            }
        }

        //Debug.Log("Final : " + best.AIValue);
        return bestTry;
    }

    List<DieToSlot> RandomTry(AIData current)
    {
        List<DieToSlot> placedDice = new List<DieToSlot>();

        Queue<SimpleDie> dice = new Queue<SimpleDie>(sDice.OrderBy(x => Random.Range(float.MinValue, float.MaxValue)));
        List<SimpleSlot> slots = sSlots.OrderBy(x => Random.Range(float.MinValue, float.MaxValue)).ToList();
        foreach (SimpleSlot slot in slots)
            slot.Reset();

        while (slots.Count > 0 && dice.Count > 0)
        {
            SimpleDie die = dice.Dequeue();
            GetSlot(die, slots, placedDice, current);
        }


        return placedDice;
    }

    void GetSlot(SimpleDie die, List<SimpleSlot> slots, List<DieToSlot> placed, AIData current)
    {
        List<SimpleSlot> okSlots = slots.Where(ok => ok.Values.Contains(die.Value)).ToList();

        if (okSlots.Count > 0)
        {
            SimpleSlot slot = okSlots[Random.Range(0, okSlots.Count)];

            slot.Current = die.Die.Value;
            placed.Add(new DieToSlot(die.Die, slot.Slot));

            if(slot.CoundownDie(die.Value))
            {
                slots.Remove(slot);
                slot.Shared.Check(slots, current);
                if (slot.Locking != null)
                {
                    slots.AddRange(slot.Locking);
                }
            }
        }
    }
}

class SimpleSlot
{
    public IDie Slot { get; private set; }
    public List<SimpleSlot> Locking { get; private set; } = new List<SimpleSlot>();
    public int[] Values { get; private set; }
    public SlotSharedData Shared { get; private set; }
    public int Current { get; set; }

    public SimpleSlot(LockSlot lockSlot)
    {
        Slot = lockSlot;
        Values = new int[] { 0, 1, 2, 3, 4, 5, 6 };
        Shared = new SlotSharedData(null, 0);
    }

    public SimpleSlot(DieSlot dieSlot, SimpleSlot lockSlot, SlotSharedData shared)
    {
        Slot = dieSlot;
        Values = dieSlot.Condition.AcceptedValues;
        if(lockSlot != null)
            lockSlot.Locking.Add(this);
        Shared = shared;
        shared.Slots.Add(this);
    }

    public void Reset()
    {
        Current = -1;
        Shared.Reset();
    }

    public bool CoundownDie(int value)
    {
        if (Shared.Countdown == null)
            return true;
        else
        {
            Shared.Countdown -= value;
            return Shared.Countdown <= 0;
        }
    }
}

class SlotSharedData
{
    public Ability Ability { get; set; }
    public int? Countdown { get; set; }
    int? baseCountdown;
    public int Uses { get; private set; }
    int baseUses;

    public List<SimpleSlot> Slots { get; set; } = new List<SimpleSlot>();

    public SlotSharedData(int? countdown, int uses) {
        baseCountdown = countdown;
        baseUses = uses;
    }

    public void Check(List<SimpleSlot> slots, AIData current)
    {
        foreach (SimpleSlot slot in Slots)
            if (slot.Current <= 0) return;

        int dice = 0;
        foreach (SimpleSlot slot in Slots)
            dice += slot.Current;
        current.AIValue += Ability.GetAIValue(dice, current);

        Uses -= 1;
        if (Uses > 0)
        {
            foreach (SimpleSlot slot in Slots)
                slot.Current = -1;

            slots.AddRange(Slots);
        }
    }

    public void Reset()
    {
        Countdown = baseCountdown;
        Uses = baseUses;
    }
}

class SimpleDie
{
    public RolledDie Die { get; private set; }
    public int Value { get; private set; }

    public SimpleDie(RolledDie die)
    {
        Die = die;
        Value = die.Value;
    }
}

public class DieToSlot
{
    public RolledDie Die { get; set; }
    public IDie Slot { get; set; }

    public DieToSlot() { }
    public DieToSlot(RolledDie die, IDie slot) {
        Die = die;
        Slot = slot;
    }
}

public class AIData
{
    public float AIValue { get; set; }
    public Character User { get; set; }
    public int UserHP { get; set; }
    public Character Target { get; set; }
    public int TargetHP { get; set; }

    public AIData()
    {
        AIValue = float.MinValue;
    }
    public AIData(Character user, Character target)
    {
        User = user;
        UserHP = user.Missing;
        Target = target;
        TargetHP = target.Missing;
    }

    public void FinalCheck()
    {
        if (UserHP < 0)
            AIValue -= 10000;
        if (TargetHP < 0)
            AIValue += 1000;
    }
}