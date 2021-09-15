using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    [SerializeField] List<AbilityData> abilities;
    public Dictionary<string, AbilityData> Abilities { get; private set; }

    [SerializeField] List<DreamData> dreams;
    public Dictionary<string, DreamData> Dreams { get; private set; }
    
    [SerializeField] List<CharacterData> succubi;
    public Dictionary<string, CharacterData> Succubi { get; private set; }

    [SerializeField] List<Trait> traits;
    public Dictionary<string, Trait> Traits { get; private set; }

    void Start()
    {
        Abilities = new Dictionary<string, AbilityData>();
        foreach (AbilityData abi in abilities)
            Abilities.Add(abi.Title, abi);

        Dreams = new Dictionary<string, DreamData>();
        foreach (DreamData dream in dreams)
            Dreams.Add(dream.ID, dream);

        Succubi = new Dictionary<string, CharacterData>();
        foreach (CharacterData succu in succubi)
            Succubi.Add(succu.Name, succu);

        Traits = new Dictionary<string, Trait>();
        foreach (Trait trait in traits)
            Traits.Add(trait.ID, trait);
    }
}
