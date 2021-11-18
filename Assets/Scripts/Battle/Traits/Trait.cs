using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trait : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }
    [SerializeField] string description;
    public string Description { get { return description; } }
    [SerializeField] protected Sprite icon;
    public Sprite Icon { get { return icon; } }
    [SerializeField] Trait reverseTrait;
    public Trait ReversrTrait { get { return reverseTrait; } }
    [SerializeField] int maxStack = 0;
    public int MaxStack { get { return maxStack; } }

    [SerializeField] float aiValue;
    public float AIValue { get { return aiValue; } }
    
    public virtual void OnAttack(ref int damages, Succubus current, Succubus other, int stack) { }
    public virtual void OnDefense(ref int damages, Succubus current, Succubus other, int stack) { }

    public virtual void StartTurn(Succubus current, int stack) { }
    public virtual void EndTurn(Succubus current, int stack) { }
}