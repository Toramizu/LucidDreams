using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    [SerializeField] List<AbilityData> abilities;
    public Dictionary<string, AbilityData> Abilities { get; private set; }

    [SerializeField] List<DreamData> dreams;
    public Dictionary<string, DreamData> Dreams { get; private set; }

    [SerializeField] List<DreamMapData> maps;
    public Dictionary<string, DreamMapData> Maps { get; private set; }

    [SerializeField] List<SuccubusData> succubi;
    public Dictionary<string, SuccubusData> Succubi { get; private set; }

    [SerializeField] List<Trait> traits;
    public Dictionary<string, Trait> Traits { get; private set; }

    [SerializeField] List<CharacterData> charactersData;
    public Dictionary<string, CharacterData> CharacterDatas { get; private set; }
    
    void Start()
    {
        Abilities = new Dictionary<string, AbilityData>();
        foreach (AbilityData abi in abilities)
            Abilities.Add(abi.Title, abi);

        Dreams = new Dictionary<string, DreamData>();
        foreach (DreamData dream in dreams)
            Dreams.Add(dream.ID, dream);

        Maps = new Dictionary<string, DreamMapData>();
        foreach (DreamMapData map in maps)
            Maps.Add(map.ID, map);

        Succubi = new Dictionary<string, SuccubusData>();
        foreach (SuccubusData succu in succubi)
            Succubi.Add(succu.Name, succu);

        Traits = new Dictionary<string, Trait>();
        foreach (Trait trait in traits)
            Traits.Add(trait.ID, trait);

        CharacterDatas = new Dictionary<string, CharacterData>();
        foreach (CharacterData chara in charactersData)
            CharacterDatas.Add(chara.ID, chara);
    }



    Dictionary<string, Character> Characters { get; set; } = new Dictionary<string, Character>();

    public Character GetCharacter(string id)
    {
        if (!Characters.ContainsKey(id))
        {
            if (!CharacterDatas.ContainsKey(id))
            {
                Debug.LogError("Character " + id + " not found");
                return null;
            }

            Character chara = new Character(CharacterDatas[id]);
            Characters.Add(id, chara);
            return chara;
        }
        else
            return Characters[id];
    }
}
