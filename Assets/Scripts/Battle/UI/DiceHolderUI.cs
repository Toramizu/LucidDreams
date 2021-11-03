using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceHolderUI : MonoBehaviour
{
    [SerializeField] RolledDieUI diePrefab;
    [SerializeField] float dieSize = 100f;
    [SerializeField] bool rightToLeft;
    [SerializeField] int dicePerRow = 10;

    public List<RolledDieUI> RolledDice { get; private set; } = new List<RolledDieUI>();

    public void PlaceDie(RolledDie die)
    {
        RolledDieUI dieUI = Instantiate(diePrefab, transform, false);
        RolledDice.Add(dieUI);
        dieUI.gameObject.SetActive(true);

        dieUI.Init(die);
        die.DieUI = dieUI;

        float y = (RolledDice.Count / dicePerRow) * dieSize;
        float x = (RolledDice.Count % dicePerRow) * dieSize;

        if (rightToLeft)
            dieUI.Position = new Vector3(-x, -y, 0);
        else
            dieUI.Position = new Vector3(x, y, 0);

        GameManager.Instance.Sound.DiceRoll();
    }

    public void ResetDice()
    {
        foreach (RolledDieUI die in RolledDice)
            Destroy(die.gameObject);

        RolledDice.Clear();
    }

    public void ResetDicePosition()
    {
        foreach (RolledDieUI die in RolledDice)
            die.ResetPosition();
    }

    public RolledDie GetDieByID(int id)
    {
        foreach (RolledDieUI die in RolledDice)
        {
            if (die.Die.ID == id)
                return die.Die;
        }

        return null;
    }
}
