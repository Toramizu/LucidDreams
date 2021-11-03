using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceHolder
{
    DiceHolderUI holderUI;

    const int MIN_ROLL = 1;
    const int MAX_ROLL = 6;
    const int MAX_ROLL_PLUS_ONE = MAX_ROLL + 1;

    int diceCount;
    Queue<int> diceQueue;
    const int DICE_QUEUE_COUNT = 100;
    
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
        diceQueue = new Queue<int>();
        for (int i = 0; i < DICE_QUEUE_COUNT; i++)
            diceQueue.Enqueue(Random.Range(MIN_ROLL, MAX_ROLL_PLUS_ONE));
    }

    public void Roll(int amount, DiceCondition condition)
    {
        for (int i = 0; i < amount; i++)
        {
            int roll;
            if(condition == null)
            {
                roll = diceQueue.Dequeue();
                diceQueue.Enqueue(Random.Range(MIN_ROLL, MAX_ROLL_PLUS_ONE));
                //Random.Range(MIN_ROLL, MAX_ROLL_PLUS_ONE);
            }
            else
            {
                foreach (int a in condition.AcceptedValues)
                    Debug.Log(a);
                List<int> v = new List<int>(condition.AcceptedValues);
                v.Remove(0);

                if (v.Count == 0)
                    roll = 0;
                else
                    roll = v[Random.Range(0, v.Count)];

            }

            /*int roll = Random.Range(MIN_ROLL, MAX_ROLL_PLUS_ONE);

            int safety = 0;
            while(condition != null && condition.Check(roll) && safety < 100)
            {
                roll = Random.Range(MIN_ROLL, MAX_ROLL_PLUS_ONE);
                safety++;
            }*/
            Give(roll);
        }
    }

    public void Give(int value)
    {
        if(value > MAX_ROLL)
        {
            Give(value - MAX_ROLL);
            value = MAX_ROLL;
        }

        RolledDie die = new RolledDie(value, diceCount++);
        RolledDice.Add(die);
        if(holderUI != null)
            holderUI.PlaceDie(die);
    }
    
    public void ResetDice()
    {
        RolledDice.Clear();
        if (holderUI != null)
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
        d.diceCount = diceCount;
        d.diceQueue = new Queue<int>(diceQueue);
        /*d.RolledDice = new List<RolledDie>();
        foreach (RolledDie die in RolledDice)
            d.RolledDice.Add(die.Clone());*/
        return d;
    }
}
