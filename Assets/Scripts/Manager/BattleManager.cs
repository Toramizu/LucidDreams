using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] Opponent opponent;
    [SerializeField] Player player;

    public void StartBattle(OpponentData oData)
    {
        gameObject.SetActive(true);
        opponent.LoadOpponent(oData);
    }

    public void InflictDamage(int amount)
    {
        if (opponent.InflictDamage(amount))
        {
            //Victory
        }
        else
        {
            //Opponent's turn
        }
    }

    public void SetAbility(AbilityData data, int slot)
    {
        player.SetAbility(data, slot);
    }
}
