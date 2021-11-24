using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterInteraction", menuName = "Data/Interaction/Character")]
public class CharacterInteractionData : InteractionData
{
    [SerializeField] string character;
    //public CharacterData Character { get { return AssetDB.Instance.Characters[character]; } }
    /*[SerializeField] CharacterData character;
    public CharacterData Character { get { return character; } }*/

    public override Sprite Icon
    {
        get {
            if (icon == null)
                return AssetDB.Instance.CharacterDatas[character].Icon;
            else
                return icon;
        }
    }

    public override void OnClick()
    {
        //Debug.Log("Play Character");
        /*InteractionDialogue inter = GameManager.Instance.Assets.GetCharacter(character.ID).GetInteraction();
        inter.Play();*/

        AssetDB.Instance.Characters[character].PlayInteraction();
        //GameManager.Instance.Assets.GetCharacter(character).PlayInteraction();
    }
}
