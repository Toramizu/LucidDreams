using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceHolder : MonoBehaviour
{
    [SerializeField] int minRoll;
    [SerializeField] int maxRoll;
    [SerializeField] RolledDie diePrefab;
    [SerializeField] float dieSize = 100f;

    List<RolledDie> rolledDice = new List<RolledDie>();

    public bool NoADiceRemaining
    {
        get
        {
            foreach (RolledDie die in rolledDice)
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
        {
            Give(Random.Range(minRoll, maxRoll + 1));
            /*RolledDie die = Instantiate(diePrefab, transform, false);
            die.gameObject.SetActive(true);

            PlaceDie(die);

            rolledDice.Add(die);
            die.Value = Random.Range(minRoll, maxRoll + 1);*/
        }
    }

    public void Give(int value)
    {
        RolledDie die = Instantiate(diePrefab, transform, false);
        die.gameObject.SetActive(true);

        PlaceDie(die);

        rolledDice.Add(die);
        die.Value = value;
    }

    void PlaceDie(RolledDie die)
    {
        die.Position = new Vector3(rolledDice.Count * dieSize, 0, 0);
        //die.transform.localPosition = new Vector3(rolledDice.Count * dieSize, 0, 0);
    }

    public void ResetDice()
    {
        foreach (RolledDie die in rolledDice)
            Destroy(die.gameObject);

        rolledDice.Clear();
    }

    public void ResetDicePosition()
    {
        foreach (RolledDie die in rolledDice)
            die.ResetPosition();
    }
}
