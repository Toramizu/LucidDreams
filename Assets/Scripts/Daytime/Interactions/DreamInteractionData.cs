using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDreamInteraction", menuName = "Data/Interaction/Dream")]
public class DreamInteractionData : InteractionData
{
    [SerializeField] CharacterData character;
    public CharacterData Character { get { return character; } }

    [SerializeField] DreamData dream;
    public DreamData Dream { get { return dream; } }

    public override Sprite Icon
    {
        get
        {
            if (icon == null)
                return character.Icon;
            else
                return icon;
        }
    }

    public override void OnClick()
    {
        GameManager.Instance.StartDream(dream, character.Succubus);
    }
}