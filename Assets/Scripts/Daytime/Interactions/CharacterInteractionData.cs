using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterInteraction", menuName = "Data/Interaction/Character")]
public class CharacterInteractionData : InteractionData
{
    [SerializeField] CharacterData character;
    public CharacterData Character { get { return character; } }

    public override Sprite Icon
    {
        get {
            if (icon == null)
                return character.Icon;
            else
                return icon;
        }
    }

    public override void OnClick()
    {
        //Debug.Log("Play Character");
        InteractionData inter = GameManager.Instance.Assets.GetCharacter(character.ID).GetInteraction();
        inter.OnClick();
    }
}
