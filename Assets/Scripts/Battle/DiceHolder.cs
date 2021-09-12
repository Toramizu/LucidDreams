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

    public void Roll(int amount, bool reset)
    {
        if (reset)
            ResetDice();

        for(int i = 0; i < amount; i++)
        {
            RolledDie die = Instantiate(diePrefab, transform, false);
            die.gameObject.SetActive(true);

            PlaceDie(die);

            rolledDice.Add(die);
            die.Value = Random.Range(minRoll, maxRoll + 1);
        }
    }

    void PlaceDie(RolledDie die)
    {
        die.transform.localPosition = new Vector3(rolledDice.Count * dieSize, 0, 0);
    }

    void ResetDice()
    {
        foreach (RolledDie die in rolledDice)
            Destroy(die.gameObject);

        rolledDice.Clear();
    }
}
