using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class BattleAI
{
    const int AI_SAMPLE = 10;//000;

    public List<DieToSlot> FindNextAction(Character user, Character other)
    {
        AITry bestTry = null;

        for(int i = 0; i < AI_SAMPLE; i++)
        {
            AITry currentTry = new AITry(user, other);
            RandomTry(currentTry);
            if (bestTry == null || bestTry.AIValue < currentTry.AIValue)
                bestTry = currentTry;
        }

        return bestTry.Plays;
    }

    void RandomTry(AITry aiTry)
    {
        List<Ability> abis = (List<Ability>)aiTry.User.Abilities;
        List<RolledDie> dice = (List<RolledDie>)aiTry.User.Dice.RolledDice;

        while(abis.Count > 0 && dice.Count > 0)
        {
            Ability abi = abis[Random.Range(0, abis.Count)];
            List<DieToSlot> results = TryAbility(aiTry, abi, dice);
            if (results == null || abi.RemainingUses < 1)
                abis.Remove(abi);
            if (results != null)
                aiTry.Plays.AddRange(results);
        }
    }

    List<DieToSlot> TryAbility(AITry aiTry, Ability abi, List<RolledDie> dice)
    {
        if (dice.Count < abi.Slots.Count)
            return null;

        List<RolledDie> currentDice = new List<RolledDie>(dice);
        List<DieToSlot> placed = new List<DieToSlot>();

        if (abi.Locked)
        {
            RolledDie d = currentDice[Random.Range(0, currentDice.Count)];
            placed.Add(new DieToSlot(d, abi.LockSlot));
            currentDice.Remove(d);
        }

        int total = 0;
        if (abi.Count == null)
        {
            foreach (DieSlot slot in abi.Slots)
            {
                List<RolledDie> okDice = currentDice.Where(d => slot.AcceptedValues.Contains(d.Value)).ToList();

                if (okDice.Count == 0)
                    return null;

                RolledDie die = okDice[Random.Range(0, okDice.Count)];
                placed.Add(new DieToSlot(die, slot));
                currentDice.Remove(die);
                total += die.Value;
            }
        }
        else
        {
            List<DieSlot> slots = new List<DieSlot>(abi.Slots);
            while (abi.Count > total && slots.Count > 0)
            {
                DieSlot slot = slots[0];

                List<RolledDie> okDice = currentDice.Where(d => slot.AcceptedValues.Contains(d.Value)).ToList();

                if (okDice.Count == 0)
                    slots.Remove(slot);
                else
                {
                    RolledDie die = okDice[Random.Range(0, okDice.Count)];
                    placed.Add(new DieToSlot(die, slot));
                    currentDice.Remove(die);
                    total += die.Value;
                }
            }
        }

        if(abi.Count == null || abi.Count < 0)
            abi.PlayAbility(aiTry.User, aiTry.Other, total);

        foreach (DieToSlot dts in placed)
            dice.Remove(dts.Die);

        return placed;
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

class AITry
{
    public Character User { get; set; }
    public Character Other { get; set; }
    public List<DieToSlot> Plays { get; set; }
    float? aiValue = null;
    public float AIValue
    {
        get
        {
            if (aiValue == null)
                aiValue = Other.AIValue - User.AIValue;

            return aiValue.Value;
        }
    }

    public AITry(Character user, Character other)
    {
        User = user.Clone();
        Other = other.Clone();
        Plays = new List<DieToSlot>();
    }
}