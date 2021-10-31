using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArousal", menuName = "Data/Dialogue/Arousal")]
public class DialogueAddArousal : DialogueElement
{
    [SerializeField] int amount;
    [SerializeField] bool toOpponent;

    public override bool Play(DialogueUI dialUI)
    {
        Debug.Log("Add arousal " + GameManager.Instance.Status);
        switch (GameManager.Instance.Status)
        {
            case GameStatus.Battle:
                if (toOpponent)
                {
                    GameManager.Instance.BattleManager.Opponent.InflictDamage(amount);
                    if (amount >= 0)
                        GameManager.Instance.Notify("Opponent gained " + amount + " arousal");
                    else
                        GameManager.Instance.Notify("Opponent lost " + -amount + " arousal");
                }
                else
                {
                    GameManager.Instance.BattleManager.Player.InflictDamage(amount);
                    if (amount >= 0)
                        GameManager.Instance.Notify("Gained " + amount + " arousal");
                    else
                        GameManager.Instance.Notify("Lost " + -amount + " arousal");

                }
                break;
            case GameStatus.Dream:
                GameManager.Instance.PlayerManager.ReduceArousal(-amount, 0);
                if (amount >= 0)
                    GameManager.Instance.Notify("Gained " + amount + " arousal");
                else
                    GameManager.Instance.Notify("Lost " + -amount + " arousal");
                break;
        }
        return true;
    }
}
