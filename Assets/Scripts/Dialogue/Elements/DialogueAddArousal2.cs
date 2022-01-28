using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueAddArousal2 : DialogueElement
{
    [XmlAttribute("Proportion")]
    public float Proportion { get; set; }
    [XmlAttribute("ToOpponent")]
    public bool ToOpponent { get; set; }

    public override bool Play(DialogueUI dialUI)
    {
        if (!ToOpponent)
        {
            int amount = GameManager.Instance.PlayerManager.InflictDamageProportion(Proportion);
            if (amount >= 0)
                GameManager.Instance.Notify("Gained " + amount + " arousal");
            else
                GameManager.Instance.Notify("Lost " + -amount + " arousal");
        } else if(GameManager.Instance.Status == GameStatus.Battle)
        {
            int amount = GameManager.Instance.BattleManager.Opponent.InflictDamageProportion(Proportion);
            GameManager.Instance.BattleManager.Opponent.InflictDamage(amount);
            if (amount >= 0)
                GameManager.Instance.Notify("Opponent gained " + amount + " arousal");
            else
                GameManager.Instance.Notify("Opponent lost " + -amount + " arousal");
        }
        /*switch (GameManager.Instance.Status)
        {

            case GameStatus.Battle:
                if (ToOpponent)
                {
                    int amount = GameManager.Instance.BattleManager.Opponent.InflictDamageProportion(Proportion);
                    GameManager.Instance.BattleManager.Opponent.InflictDamage(amount);
                    if (amount >= 0)
                        GameManager.Instance.Notify("Opponent gained " + amount + " arousal");
                    else
                        GameManager.Instance.Notify("Opponent lost " + -amount + " arousal");
                }
                else
                {
                    int amount = GameManager.Instance.BattleManager.Player.InflictDamageProportion(Proportion);
                    GameManager.Instance.BattleManager.Player.InflictDamage(amount);
                    if (amount >= 0)
                        GameManager.Instance.Notify("Gained " + amount + " arousal");
                    else
                        GameManager.Instance.Notify("Lost " + -amount + " arousal");

                }
                break;
            case GameStatus.Dream:
                int pAmount = GameManager.Instance.PlayerManager.InflictDamageProportion(Proportion);
                if (pAmount >= 0)
                    GameManager.Instance.Notify("Gained " + pAmount + " arousal");
                else
                    GameManager.Instance.Notify("Lost " + -pAmount + " arousal");
                break;
        }*/
        return true;
    }
}
