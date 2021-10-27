using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Window
{
    [SerializeField] CharacterUI opponentUI;
    Character opponent { get { return opponentUI.Character; } }
    [SerializeField] CharacterUI playerUI;
    Character player { get { return playerUI.Character; } }

    [SerializeField] EndPanel endPanel;
    [SerializeField] TraitTooltip tooltip;

    public bool PlayerTurn { get; private set; }
    public Character GetCharacter(bool current)
    {
            if (PlayerTurn == current)
                return player;
            else
                return opponent;
    }

    public Character Other(Character current)
    {
        if (current == opponent)
            return player;
        else
            return opponent;
    }
    
    public void StartBattle(CharacterData oData, List<AbilityData> pAbis)
    {
        Open();
        endPanel.Close();
        opponent.LoadCharacter(oData);
        player.Reset(pAbis);
        PlayerTurn = false;
        NextRound();
    }

    public void RefreshDescr()
    {
        //foreach (Ability abi in GetCharacter(true).Abilities)
          //  abi.RefreshDescr();
    }

    public bool CheckBattleStatus()
    {
        if (opponent.Finished)
        {
            EndBattle(true);
            return true;
        }
        //endPanel.Victory(opponent.Crystals, opponent.Data);
        else if (player.Finished)
        {
            EndBattle(false);
            return true;
        }
        //GameManager.Instance.NextDay();
        return false;
    }

    public void EndBattle(bool victory)
    {
        if (victory)
            endPanel.Victory(opponent.Crystals, opponent.Data);
        else
            endPanel.Loss();
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

        int total = CalculateDamages(amount, user, target, ignoreTraits);

        target.InflictDamage(total);
        //CheckBattleStatus();
    }

    public int CalculateDamages(int amount, Character user, Character target, bool ignoreTraits)
    {
        if (!ignoreTraits)
        {
            user.Traits.OnAttack(ref amount, user, target);
            target.Traits.OnDefense(ref amount, target, user);
        }

        return amount;
    }

    public void AddTrait(Trait trait, int amount, bool targetsUser)
    {
        if (PlayerTurn == targetsUser)
            player.Traits.AddTrait(trait, amount);
        else
            opponent.Traits.AddTrait(trait, amount);
    }

    public void Roll(int amount, DiceCondition condition)
    {
        if (PlayerTurn)
            player.Roll(amount, condition);
        else
            opponent.Roll(amount, condition);
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

    #region Cheats
    public void FullHeal()
    {
        player.FullHeal();
    }
    #endregion
}
