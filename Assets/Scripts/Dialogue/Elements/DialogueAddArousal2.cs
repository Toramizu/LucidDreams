using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArousal", menuName = "Data/Dialogue/Arousal2")]
public class DialogueAddArousal2 : DialogueElement
{
    [SerializeField] float percentile;
    [SerializeField] bool toOpponent;

    public override bool Play(DialogueUI dialUI)
    {
        Debug.Log("Add arousal " + GameManager.Instance.Status);
        switch (GameManager.Instance.Status)
        {
            case GameStatus.Battle:
                if (toOpponent)
                {
                    int amount = (int)(GameManager.Instance.BattleManager.Opponent.MaxArousal * percentile);
                    GameManager.Instance.BattleManager.Opponent.InflictDamage(amount);
                    if (amount >= 0)
                        GameManager.Instance.Notify("Opponent gained " + amount + " arousal");
                    else
                        GameManager.Instance.Notify("Opponent lost " + -amount + " arousal");
                }
                else
                {
                    int amount = (int)(GameManager.Instance.BattleManager.Player.MaxArousal * percentile);
                    GameManager.Instance.BattleManager.Player.InflictDamage(amount);
                    if (amount >= 0)
                        GameManager.Instance.Notify("Gained " + amount + " arousal");
                    else
                        GameManager.Instance.Notify("Lost " + -amount + " arousal");

                }
                break;
            case GameStatus.Dream:
                int pAmount = (int)(GameManager.Instance.PlayerManager.Player.MaxArousal * percentile);
                GameManager.Instance.PlayerManager.ReduceArousal(-pAmount, 0);
                if (pAmount >= 0)
                    GameManager.Instance.Notify("Gained " + pAmount + " arousal");
                else
                    GameManager.Instance.Notify("Lost " + -pAmount + " arousal");
                break;
        }
        return true;
    }
}
