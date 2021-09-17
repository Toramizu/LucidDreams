using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreamToken : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Sprite defaultImage;

    CharacterData chara;
    public CharacterData Character
    {
        get { return chara; }
        set
        {
            chara = value;
            if (chara.Image == null)
                image.sprite = defaultImage;
            else
                image.sprite = chara.Image;
        }
    }
}
