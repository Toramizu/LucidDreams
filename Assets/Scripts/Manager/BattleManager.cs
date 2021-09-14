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
        opponent.Traits.EndTurn(opponent);
        player.Traits.StartTurn(player);
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

        player.Traits.EndTurn(player);
        opponent.Traits.StartTurn(opponent);

        Debug.Log("Opponent turn");
        player.NewRound();
        NextRound();
    }

    public void InflictsDamage(int amount, bool toOpponent, bool fromOpponent)
    {
        Character target;
        if (toOpponent)
            target = opponent;
        else
            target = player;
        Character user;
        if (fromOpponent)
            user = opponent;
        else
            user = player;


        user.Traits.OnAttack(ref amount, user, target);
        target.Traits.OnDefense(ref amount, target, user);

        target.InflictDamage(amount);
    }

    public void AddTrait(Trait trait, int amount, bool toOpponent)
    {
        if (toOpponent)
            opponent.Traits.AddTrait(trait, amount);
        else
            player.Traits.AddTrait(trait, amount);
    }

    public void Roll(int amount, bool reset)
    {
        dice.Roll(amount, reset);
    }

    public void Give(int value)
    {
        dice.Give(value);
    }

    public void ResetDicePosition()
    {
        dice.ResetDicePosition();
    }
}
