using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityEffect
{
    [SerializeField] protected int bonus;
    [SerializeField] protected bool usesDice;
    [SerializeField] protected float multiplier = 1;

    [SerializeField] protected bool targetsUser;

    public AbilityEffect() { }
    public AbilityEffect(int bonus, bool usesDice, float mult, bool targetsUser) {
        this.bonus = bonus;
        this.usesDice = usesDice;
        this.multiplier = mult;
        this.targetsUser = targetsUser;
    }

    public abstract void Play(bool isOpponent, int dice);
}