using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueAddArousal : DialogueElement
{
    [XmlAttribute("Amount")]
    public int Amount { get; set; }
    [XmlAttribute("ToOpponent"), DefaultValue(false)]
    public bool ToOpponent { get; set; }

    public override bool Play(DialogueUI dialUI)
    {
        if (!ToOpponent)
        {
            GameManager.Instance.PlayerManager.ReduceArousal(-Amount, 0);
            if (Amount >= 0)
                GameManager.Instance.Notify("Gained " + Amount + " arousal");
            else
                GameManager.Instance.Notify("Lost " + -Amount + " arousal");
        }
        else if (GameManager.Instance.Status == GameStatus.Battle)
        {
            GameManager.Instance.BattleManager.Opponent.InflictDamage(Amount);
            if (Amount >= 0)
                GameManager.Instance.Notify("Opponent gained " + Amount + " arousal");
            else
                GameManager.Instance.Notify("Opponent lost " + -Amount + " arousal");
        }
        /*switch (GameManager.Instance.Status)
        {
            case GameStatus.Battle:
                if (ToOpponent)
                {
                    GameManager.Instance.BattleManager.Opponent.InflictDamage(Amount);
                    if (Amount >= 0)
                        GameManager.Instance.Notify("Opponent gained " + Amount + " arousal");
                    else
                        GameManager.Instance.Notify("Opponent lost " + -Amount + " arousal");
                }
                else
                {
                    GameManager.Instance.BattleManager.Player.InflictDamage(Amount);
                    if (Amount >= 0)
                        GameManager.Instance.Notify("Gained " + Amount + " arousal");
                    else
                        GameManager.Instance.Notify("Lost " + -Amount + " arousal");

                }
                break;
            case GameStatus.Dream:
                GameManager.Instance.PlayerManager.ReduceArousal(-Amount, 0);
                if (Amount >= 0)
                    GameManager.Instance.Notify("Gained " + Amount + " arousal");
                else
                    GameManager.Instance.Notify("Lost " + -Amount + " arousal");
                break;
        }*/
        return true;
    }
}
