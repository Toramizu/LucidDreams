using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDreamInteraction", menuName = "Data/Interaction/Dream")]
public class DreamInteractionData : InteractionData
{
    [SerializeField] string character;
    /*[SerializeField] CharacterData character;
    public CharacterData Character { get { return character; } }*/

    [SerializeField] string dream;
    public DreamData Dream { get { return AssetDB.Instance.Dreams[dream]; } }

    public override Sprite Icon
    {
        get
        {
            if (icon == null)
                return AssetDB.Instance.CharacterDatas[character].Icon;
            else
                return icon;
        }
    }

    public override void OnClick()
    {
        GameManager.Instance.StartDream(Dream, AssetDB.Instance.CharacterDatas[character].Succubus);
    }
}