using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] Character opponent;
    [SerializeField] Character player;

    bool playerTurn;

    public void SetPlayer(CharacterData data)
    {
        player.LoadCharacter(data);
    }

    public void StartBattle(CharacterData oData)
    {
        gameObject.SetActive(true);
        opponent.LoadCharacter(oData);
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

    public void NextRound()
    {
        opponent.EndTurn();
        player.StartTurn();
        playerTurn = true;
    }

    public void EndRound()
    {
        playerTurn = false;
        player.EndTurn();
        opponent.StartTurn();

        opponent.PlayTurn();

        NextRound();
    }

    public void InflictsDamage(int amount, bool targetsUser)
    {
        Character user;
        if (playerTurn)
            user = player;
        else
            user = opponent;

        Character target;
        if (playerTurn == targetsUser)
            target = player;
        else
            target = opponent;


        user.Traits.OnAttack(ref amount, user, target);
        target.Traits.OnDefense(ref amount, target, user);

        target.InflictDamage(amount);
    }

    public void AddTrait(Trait trait, int amount, bool targetsUser)
    {
        if (playerTurn == targetsUser)
            player.Traits.AddTrait(trait, amount);
        else
            opponent.Traits.AddTrait(trait, amount);
    }

    public void Roll(int amount, bool reset)
    {
        if (playerTurn)
            player.Roll(amount, reset);
        else
            opponent.Roll(amount, reset);
    }

    public void Give(int value)
    {
        if (playerTurn)
            player.Give(value);
        else
            opponent.Give(value);
    }

    public void ResetDicePosition()
    {
        player.ResetDicePosition();
    }
}
