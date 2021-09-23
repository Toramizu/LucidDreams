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
    
    public virtual void OnAttack(ref int damages, Character current, Character other) { }
    public virtual void OnDefense(ref int damages, Character current, Character other) { }

    public virtual void StartTurn(Character current) { }
    public virtual void EndTurn(Character current) { }
}