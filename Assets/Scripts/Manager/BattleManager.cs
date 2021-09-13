using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] Opponent opponent;
    [SerializeField] Player player;
    [SerializeField] DiceHolder dice;

    public void SetPlayer(PlayerData data)
    {
        player.LoadPlayer(data);
    }

    public void StartBattle(OpponentData oData)
    {
        gameObject.SetActive(true);
        opponent.LoadOpponent(oData);
        NextRound();
    }

    public void CheckBattleStatus()
    {
        if(opponent.Finished)
        {
            Debug.Log("Battle Won!");
        }else if (player.Finished)
        {
            Debug.Log("Game Over...");
        }
    }

    public void SetAbility(AbilityData data, int slot)
    {
        player.SetAbility(data, slot);
    }

    public void NextRound()
    {
        Roll(player.Rolls, true);
    }

    /*public void CheckEndRound()
    {
        if (player.NoAbilityRemaining || dice.NoADiceRemaining)
            EndRound();
    }*/

    public void EndRound()
    {
        dice.ResetDice();
        player.NewRound();

        Debug.Log("Opponent turn");
        NextRound();
    }

    public void InflictsDamage(int amount, bool toOpponent)
    {
        if (toOpponent)
            opponent.InflictDamage(amount);
        else
            player.InflictDamage(amount);
    }

    public void Roll(int amount, bool reset)
    {
        dice.Roll(amount, reset);
    }
}
