using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class BattleAI
{
    [SerializeField] int sample = 10;//000;

    List<SimpleDie> sDice;
    List<SimpleAbility> sAbilities;

    public List<DieToSlot> FindNextAction(List<Ability> abilities, List<RolledDie> dice, Character user, Character target)
    {
        //Copy Dice
        sDice = new List<SimpleDie>();
        foreach (RolledDie die in dice)
            sDice.Add(new SimpleDie(die));

        //Copy Abilities
        sAbilities = new List<SimpleAbility>();
        foreach (Ability abi in abilities)
            if(abi.IsActive)
                sAbilities.Add(new SimpleAbility(abi));

        //Tries
        AIData best = new AIData();
        List<DieToSlot> bestTry = null;

        for (int i = 0; i < sample; i++)
        {
            AIData current = new AIData(user, target);
            List<DieToSlot> currentTry = RandomTry(current);

            current.FinalCheck();

            if (current.AIValue > best.AIValue)
            {
                best = current;
                bestTry = currentTry;
            }
        }

        //Debug.Log("Final : " + best.AIValue);
        /*foreach (DieToSlot s in bestTry)
            Debug.Log(s.Die.Value);*/
        return bestTry;
    }

    List<DieToSlot> RandomTry(AIData current)
    {
        List<DieToSlot> placedDice = new List<DieToSlot>();
        //Create new lists & copy SipleAbilities
        RandomList<SimpleDie> dice = new RandomList<SimpleDie>(sDice);
        RandomList<SimpleAbility> abilities = new RandomList<SimpleAbility>();
        foreach (SimpleAbility abi in sAbilities)
            abilities.Add(new SimpleAbility(abi));

        while(dice.Count > 0 && abilities.Count > 0)
        {
            //Pick a random Ability and try to fill it up
            SimpleAbility abi = abilities.GetRandom();
            if (TryAbility(abi, dice, placedDice, current))
                abilities.Remove(abi);
        }
        
        return placedDice;
    }

    //Return true if ability should be removed
    bool TryAbility(SimpleAbility abi, RandomList<SimpleDie> dice, List<DieToSlot> placedDice, AIData current)
    {
        //Not enough dice to fill ability
        if (dice.Count < abi.SlotCount)
            return true;

        List<DieToSlot> dieSlots = new List<DieToSlot>();
        List<SimpleDie> usedDice = new List<SimpleDie>();
        RandomList<SimpleDie> usableDice = new RandomList<SimpleDie>(dice);
        List<SimpleSlot> slots = abi.sSlots.OrderBy(x => Random.Range(float.MinValue, float.MaxValue)).ToList();

        if (abi.Locked)
        {
            SimpleDie die = usableDice.GetRandom();
            dieSlots.Add(new DieToSlot(die.Die, abi.LockSlot));
            usableDice.Remove(die);
            abi.Locked = false;
        }

        int diceTotal = 0;
        foreach (SimpleSlot slot in abi.sSlots)
        {
            RandomList<SimpleDie> okDice = new RandomList<SimpleDie>(usableDice.Where(d => slot.Values.Contains(d.Value)));

            if (okDice.Count == 0)
                return true;

            SimpleDie die = okDice.GetRandom();
            dieSlots.Add(new DieToSlot(die.Die, slot.Slot));
            usableDice.Remove(die);
            usedDice.Add(die);
            diceTotal += die.Value;
        }

        foreach(SimpleDie used in usedDice)
            dice.Remove(used);
        foreach (DieToSlot ds in dieSlots)
            placedDice.Add(ds);

        current.AIValue += abi.GetAIValue(diceTotal, current);
        abi.Uses--;
        abi.Used++;
        return abi.Uses <= 0;
    }
}

public class SimpleCharacter
{
    public Character Chara { get; private set; }
    public int HP { get; set; }
    public Dictionary<Trait, int> Traits { get; set; } = new Dictionary<Trait, int>();

    public SimpleCharacter(Character chara)
    {
        Chara = chara;
        HP = chara.Arousal;
        Traits = chara.Traits.ToSimpleDictionary;
    }

    public float CheckValue()
    {
        float val = 0;

        if (HP >= Chara.MaxArousal)
            val -= 1000;

        foreach (Trait t in Traits.Keys)
            val += t.AIValue * Traits[t];


        return val;
    }

    public void InflictDamage(int amount)
    {

    }

    public void ApplyTrait(Trait t, int amount)
    {
        if (Traits.ContainsKey(t))
            Traits[t] += amount;
        else
            Traits.Add(t, amount);

        if (Traits[t] > t.MaxStack)
            Traits[t] = t.MaxStack;
    }
}


class SimpleAbility
{
    public Ability Ability { get; private set; }
    public bool Locked { get; set; }
    public int Uses { get; set; }
    public int Used { get; set; }

    public List<SimpleSlot> sSlots { get; set; }
    public int SlotCount
    {
        get
        {
            if (Locked)
                return sSlots.Count + 1;
            else
                return sSlots.Count;
        }
    }

    public LockSlot LockSlot { get { return Ability.LockSlot; } }

    public SimpleAbility(Ability ability)
    {
        Ability = ability;
        Locked = ability.Locked;
        Uses = ability.RemainingUses;
        Used = ability.Used;

        sSlots = new List<SimpleSlot>();
        foreach (DieSlot slot in ability.Slots)
            if(slot.IsActive)
                sSlots.Add(new SimpleSlot(slot));
    }
    public SimpleAbility(SimpleAbility ability)
    {
        Ability = ability.Ability;
        Locked = ability.Locked;
        Uses = ability.Uses;
        Used = ability.Used;

        sSlots = new List<SimpleSlot>(ability.sSlots);
    }

    public float GetAIValue(int dice, AIData current)
    {
        return Ability.GetAIValue(dice, current);
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

    public SimpleSlot(DieSlot dieSlot)
    {
        Slot = dieSlot;
        Values = dieSlot.Condition.AcceptedValues;
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
    public SimpleCharacter User { get; private set; }
    public SimpleCharacter Target { get; private set; }
    /*public Character User { get; set; }
    public int UserHP { get; set; }
    public Character Target { get; set; }
    public int TargetHP { get; set; }*/


    public AIData()
    {
        AIValue = float.MinValue;
    }
    public AIData(Character user, Character target)
    {
        User = new SimpleCharacter(user);
        Target = new SimpleCharacter(target);
    }

    public void FinalCheck()
    {
        AIValue += User.CheckValue();
        AIValue -= Target.CheckValue();
        /*if (User.HP > User. < 0)
            AIValue -= 10000;
        if (TargetHP < 0)
            AIValue += 1000;*/
    }

    public void InflictDamage(int amount, bool targetsUser)
    {
        foreach(Trait t in User.Traits.Keys)
        {
            //t.OnAttack(ref amount, User, )
        }
    }
}