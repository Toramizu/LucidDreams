using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] Opponent opponent;
    [SerializeField] Character player;

    [SerializeField] EndPanel endPanel;
    [SerializeField] TraitTooltip tooltip;

    public bool PlayerTurn { get; private set; }
    
    public void StartBattle(CharacterData oData, List<AbilityData> pAbis)
    {
        gameObject.SetActive(true);
        opponent.LoadCharacter(oData);
        player.LoadAbilities(pAbis);
        PlayerTurn = false;
        NextRound();
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void CheckBattleStatus()
    {
        if(opponent.Finished)
        {
            //GameManager.Instance.EndBattle(opponent);
            endPanel.Victory(opponent.Crystals);
        }else if (player.Finished)
        {
            GameManager.Instance.NextDay();
        }
    }

    public void NextRound()
    {
        if (PlayerTurn)
        {
            EndRound();
        }
        else
        {
            opponent.EndTurn();
            PlayerTurn = true;
            player.StartTurn();
        }
    }

    public void EndRound()
    {
        if (PlayerTurn)
        {
            player.EndTurn();
            PlayerTurn = false;
            opponent.StartTurn();

            opponent.PlayTurn();
        }

        //NextRound();
    }

    public void InflictsDamage(int amount, bool targetsCurrent, bool ignoreTraits)
    {
        Character user;
        if (PlayerTurn)
            user = player;
        else
            user = opponent;

        Character target;
        if (PlayerTurn == targetsCurrent)
            target = player;
        else
            target = opponent;

        if (!ignoreTraits)
        {
            user.Traits.OnAttack(ref amount, user, target);
            target.Traits.OnDefense(ref amount, target, user);
        }

        target.InflictDamage(amount);
        CheckBattleStatus();
    }

    public void AddTrait(Trait trait, int amount, bool targetsUser)
    {
        if (PlayerTurn == targetsUser)
            player.Traits.AddTrait(trait, amount);
        else
            opponent.Traits.AddTrait(trait, amount);
    }

    public void Roll(int amount, bool reset, DiceCondition condition)
    {
        if (PlayerTurn)
            player.Roll(amount, reset, condition);
        else
            opponent.Roll(amount, reset, condition);
    }

    public void Give(int value)
    {
        if (PlayerTurn)
            player.Give(value);
        else
            opponent.Give(value);
    }

    public void ResetDicePosition()
    {
        player.ResetDicePosition();
    }

    public void ToggleOpponentAbilities(bool toggle)
    {
        if (PlayerTurn)
        {
            player.ToggleAbilities(!toggle);
            opponent.ToggleAbilities(toggle);
        }
    }

    public DiceHolder RolledDice()
    {
        if (PlayerTurn)
            return player.Dice;
        else
            return opponent.Dice;
    }

    public List<Ability> Abilities(bool current)
    {
        if (PlayerTurn && current)
            return player.Abilities;
        else
            return opponent.Abilities;
    }

    public void ShowTooltip(Trait trait)
    {
        tooltip.Open(trait);
    }
}
