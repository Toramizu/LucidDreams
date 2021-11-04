using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageUI : MonoBehaviour
{
    [SerializeField] Button borrowed;
    [SerializeField] Image image;
    [SerializeField] Sprite defaultImage;

    ImageData data;

    public void Init(ImageData data)
    {
        this.data = data;

        if (data == null || data.Image == null)
            image.sprite = defaultImage;
        else
            image.sprite = data.Image;

        if(data != null)
            borrowed.gameObject.SetActive(data.Source != null && data.Source != "");
    }

    public void OpenBorrowed()
    {
        if (data.Source != null && data.Source != "")
            Application.OpenURL(data.Source);
    }
}
