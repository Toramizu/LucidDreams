using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceHolder : MonoBehaviour
{
    [SerializeField] int minRoll;
    [SerializeField] int maxRoll;
    [SerializeField] RolledDie diePrefab;
    [SerializeField] float dieSize = 100f;
    [SerializeField] bool rightToLeft;
    [SerializeField] int dicePerRow = 10;

    public List<RolledDie> RolledDice { get; private set; } = new List<RolledDie>();

    public bool NoADiceRemaining
    {
        get
        {
            foreach (RolledDie die in RolledDice)
                if (die.isActiveAndEnabled)
                    return false;

            return true;
        }
    }

    public void Roll(int amount, bool reset)
    {
        if (reset)
            ResetDice();

        for(int i = 0; i < amount; i++)
            Give(Random.Range(minRoll, maxRoll + 1));
    }

    public void Give(int value)
    {
        RolledDie die = Instantiate(diePrefab, transform, false);
        die.gameObject.SetActive(true);

        PlaceDie(die);

        RolledDice.Add(die);
        die.Value = value;
    }

    void PlaceDie(RolledDie die)
    {
        float y = (RolledDice.Count / dicePerRow) * dieSize;        
        float x = (RolledDice.Count % dicePerRow) * dieSize;

        if (rightToLeft)
            die.Position = new Vector3(-x, -y, 0);
        else
            die.Position = new Vector3(x, y, 0);
    }

    public void ResetDice()
    {
        foreach (RolledDie die in RolledDice)
            Destroy(die.gameObject);

        RolledDice.Clear();
    }

    public void ResetDicePosition()
    {
        foreach (RolledDie die in RolledDice)
            die.ResetPosition();
    }
}
