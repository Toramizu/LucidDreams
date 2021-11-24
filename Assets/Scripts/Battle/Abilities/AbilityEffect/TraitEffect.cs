using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class TraitEffect : AbilityEffect
{
    [XmlIgnore]
    public Trait Trait {
        get { return AssetDB.Instance.Traits[_Trait]; }
        set { _Trait = value.ID; }
    }
    [XmlAttribute("Trait")]
    public string _Trait { get; set; }

    public TraitEffect() { }
    public TraitEffect(EffectData data) : base(data)
    {
        _Trait = data.StringValue;
    }

    public override void Play(Succubus user, Succubus other, int dice, Ability abi)
    {
        if (TargetsUser)
            user.Traits.AddTrait(Trait, Value(dice, abi));
        else
            other.Traits.AddTrait(Trait, Value(dice, abi));
    }

    public override AbilityEffect Clone()
    {
        TraitEffect e = new TraitEffect();
        e.Trait = Trait;
        return e;
    }
}
