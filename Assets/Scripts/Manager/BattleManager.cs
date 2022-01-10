using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueUI;

public class BattleManager : Window
{
    [SerializeField] SuccubusUI opponentUI;
    public Succubus Opponent { get { return opponentUI.Character; } }
    [SerializeField] SuccubusUI playerUI;
    public Succubus Player { get { return playerUI.Character; } }

    [SerializeField] EndPanel endPanel;
    [SerializeField] TraitTooltip tooltip;

    DialogueAction onWin;
    DialogueAction onLoss;

    public bool PlayerTurn { get; private set; }
    public Succubus GetCharacter(bool current)
    {
            if (PlayerTurn == current)
                return Player;
            else
                return Opponent;
    }

    public Succubus Other(Succubus current)
    {
        if (current == Opponent)
            return Player;
        else
            return Opponent;
    }

    public void StartBattle(SuccubusData oData, List<AbilityData> pAbis, DialogueAction onWin, DialogueAction onLoss)
    {
        this.onWin = onWin;
        this.onLoss = onLoss;
        StartBattle(oData, pAbis);
    }

    public void StartBattle(SuccubusData oData, List<AbilityData> pAbis)
    {
        FadeIn();
        endPanel.FadeOut();
        Opponent.LoadCharacter(oData);
        Player.Reset(pAbis);
        PlayerTurn = false;
        NextRound();
    }

    public bool CheckBattleStatus()
    {
        if (Opponent.Finished)
        {
            EndBattle(true, Opponent.Edged);
            return true;
        }
        else if (Player.Finished)
        {
            EndBattle(false, false);
            return true;
        }
        return false;
    }

    public void EndBattle(bool victory, bool edged)
    {
        if (victory)
        {
            endPanel.Victory(Opponent.Crystals, Opponent, edged);
            onWin?.Invoke();
        }
        else
        {
            endPanel.Loss();
            onLoss?.Invoke();
        }

        onWin = null;
        onLoss = null;
    }

    public void NextRound()
    {
        if (PlayerTurn)
        {
            EndRound();
        }
        else
        {
            Opponent.EndTurn();
            PlayerTurn = true;
            Player.StartTurn();
        }
    }

    public void EndRound()
    {
        if (PlayerTurn)
        {
            Player.EndTurn();
            PlayerTurn = false;
            Opponent.StartTurn();

            Opponent.PlayTurn();
        }

        //NextRound();
    }

    public void InflictsDamage(int amount, bool targetsCurrent, bool ignoreTraits)
    {
        Succubus user;
        if (PlayerTurn)
            user = Player;
        else
            user = Opponent;

        Succubus target;
        if (PlayerTurn == targetsCurrent)
            target = Player;
        else
            target = Opponent;

        int total = CalculateDamages(amount, user, target, ignoreTraits);

        target.InflictDamage(total);
        //CheckBattleStatus();
    }

    public int CalculateDamages(int amount, Succubus user, Succubus target, bool ignoreTraits)
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
            Player.Traits.AddTrait(trait, amount);
        else
            Opponent.Traits.AddTrait(trait, amount);
    }

    public void Roll(int amount, DiceCondition condition)
    {
        if (PlayerTurn)
            Player.Roll(amount, condition);
        else
            Opponent.Roll(amount, condition);
    }

    public void Give(int value)
    {
        if (PlayerTurn)
            Player.Give(value);
        else
            Opponent.Give(value);
    }

    public void ResetDicePosition()
    {
        Player.ResetDicePosition();
    }

    public void ToggleOpponentAbilities(bool toggle)
    {
        if (PlayerTurn)
        {
            Player.ToggleAbilities(!toggle);
            Opponent.ToggleAbilities(toggle);
        }
    }

    public DiceHolder RolledDice()
    {
        if (PlayerTurn)
            return Player.Dice;
        else
            return Opponent.Dice;
    }

    public List<Ability> Abilities(bool current)
    {
        if (PlayerTurn && current)
            return Player.Abilities;
        else
            return Opponent.Abilities;
    }

    public void ShowTooltip(Trait trait)
    {
        tooltip.Open(trait);
    }

    #region Cheats
    public void FullHeal()
    {
        Player.FullHeal();
    }
    #endregion
}
