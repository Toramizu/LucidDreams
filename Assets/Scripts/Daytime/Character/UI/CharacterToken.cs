using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterToken : MonoBehaviour
{
    [SerializeField] Image image;
    public Sprite Sprite
    {
        get { return image.sprite; }
        set { image.sprite = value; }
    }

    public Character Character { get; set; }
    CharacterUI mainUI;

    public void Init(Character chara, CharacterUI ui)
    {
        Character = chara;
        mainUI = ui;
        Sprite = chara.Data.Icon;
    }

    public void OnClick()
    {
        mainUI.ShowCharacter(Character);
    }

    public void Toggle(bool toggle)
    {
        gameObject.SetActive(toggle);
    }
}
