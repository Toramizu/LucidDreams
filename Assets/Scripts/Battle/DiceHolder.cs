using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceHolder
{
    DiceHolderUI holderUI;

    const int MIN_ROLL = 1;
    const int MAX_ROLL_PLUS_ONE = 7;

    int diceCount;
    
    public List<RolledDie> RolledDice { get; private set; } = new List<RolledDie>();
    public List<int> SimpleDiceList
    {
        get
        {
            List<int> l = new List<int>();
            foreach (RolledDie d in RolledDice)
                l.Add(d.Value);
            return l;
        }
    }

    public DiceHolder() { }
    public DiceHolder(DiceHolderUI holderUI)
    {
        this.holderUI = holderUI;
    }

    public void Roll(int amount, DiceCondition condition)
    {
        for(int i = 0; i < amount; i++)
        {
            int roll = Random.Range(MIN_ROLL, MAX_ROLL_PLUS_ONE);

            int safety = 0;
            while(condition != null && condition.Check(roll) && safety < 100)
            {
                roll = Random.Range(MIN_ROLL, MAX_ROLL_PLUS_ONE);
                safety++;
            }

            Give(roll);
        }
    }

    public void Give(int value)
    {
        RolledDie die = new RolledDie(value, diceCount++);
        RolledDice.Add(die);
        holderUI.PlaceDie(die);
    }
    
    public void ResetDice()
    {
        RolledDice.Clear();
        holderUI.ResetDice();
        diceCount = 0;
    }

    public void ResetDicePosition()
    {
        holderUI.ResetDicePosition();
    }

    public DiceHolder Clone()
    {
        DiceHolder d = new DiceHolder();
        d.RolledDice = new List<RolledDie>(RolledDice);
        /*d.RolledDice = new List<RolledDie>();
        foreach (RolledDie die in RolledDice)
            d.RolledDice.Add(die.Clone());*/
        return d;
    }
}
