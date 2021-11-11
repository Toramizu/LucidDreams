using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArousal", menuName = "Data/Dialogue/Arousal2")]
public class DialogueAddArousal2 : DialogueElement
{
    [SerializeField] float proportion;
    [SerializeField] bool toOpponent;

    public override bool Play(DialogueUI dialUI)
    {
        Debug.Log("Add arousal " + GameManager.Instance.Status);
        switch (GameManager.Instance.Status)
        {
            case GameStatus.Battle:
                if (toOpponent)
                {
                    int amount = GameManager.Instance.BattleManager.Opponent.InflictDamageProportion(proportion);
                    GameManager.Instance.BattleManager.Opponent.InflictDamage(amount);
                    if (amount >= 0)
                        GameManager.Instance.Notify("Opponent gained " + amount + " arousal");
                    else
                        GameManager.Instance.Notify("Opponent lost " + -amount + " arousal");
                }
                else
                {
                    int amount = GameManager.Instance.BattleManager.Player.InflictDamageProportion(proportion);
                    GameManager.Instance.BattleManager.Player.InflictDamage(amount);
                    if (amount >= 0)
                        GameManager.Instance.Notify("Gained " + amount + " arousal");
                    else
                        GameManager.Instance.Notify("Lost " + -amount + " arousal");

                }
                break;
            case GameStatus.Dream:
                int pAmount = GameManager.Instance.PlayerManager.InflictDamageProportion(proportion);
                if (pAmount >= 0)
                    GameManager.Instance.Notify("Gained " + pAmount + " arousal");
                else
                    GameManager.Instance.Notify("Lost " + -pAmount + " arousal");
                break;
        }
        return true;
    }
}
