using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreamToken : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Sprite defaultImage;

    [SerializeField] Sprite shopImage;
    
    public void SetCharacter(SuccubusData chara)
    {
        if (chara.Image == null)
            image.sprite = defaultImage;
        else
            image.sprite = chara.Image.Image;
    }

    public void SetShop()
    {
        image.sprite = shopImage;
    }
}
