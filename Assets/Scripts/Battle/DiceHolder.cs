using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceHolder// : MonoBehaviour
{
    DiceHolderUI holderUI;

    const int MIN_ROLL = 1;
    const int MAX_ROLL_PLUS_ONE = 7;

    /*[SerializeField] int minRoll;
    [SerializeField] int maxRoll;
    [SerializeField] RolledDie diePrefab;
    [SerializeField] RolledDieUI dieUIPrefab;
    [SerializeField] float dieSize = 100f;
    [SerializeField] bool rightToLeft;
    [SerializeField] int dicePerRow = 10;*/

    public List<RolledDie> RolledDice { get; private set; } = new List<RolledDie>();

    public DiceHolder(DiceHolderUI holderUI)
    {
        this.holderUI = holderUI;
    }

    public bool NoADiceRemaining
    {
        get
        {
            foreach (RolledDie die in RolledDice)
                if (die.IsActive)
                    return false;

            return true;
        }
    }

    public void Roll(int amount, bool reset, DiceCondition condition)
    {
        //Debug.Log("Rolling " + amount);
        if (reset)
            ResetDice();

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
        RolledDie die = new RolledDie(value);
        RolledDice.Add(die);
        holderUI.PlaceDie(die);

        /*RolledDie die = Instantiate(dieUIPrefab, transform, false);
        die.gameObject.SetActive(true);

        holderUI.PlaceDie(die);

        RolledDice.Add(die);
        die.Value = value;*/
    }

    /*void PlaceDie(RolledDie die)
    {
        float y = (RolledDice.Count / dicePerRow) * dieSize;        
        float x = (RolledDice.Count % dicePerRow) * dieSize;

        if (rightToLeft)
            die.Position = new Vector3(-x, -y, 0);
        else
            die.Position = new Vector3(x, y, 0);
    }*/

    public void ResetDice()
    {
        RolledDice.Clear();
        holderUI.ResetDice();
        /*foreach (RolledDie die in RolledDice)
            Destroy(die.gameObject);

        RolledDice.Clear();*/
    }

    public void ResetDicePosition()
    {
        holderUI.ResetDicePosition();
        /*foreach (RolledDie die in RolledDice)
            die.ResetPosition();*/
    }
}
