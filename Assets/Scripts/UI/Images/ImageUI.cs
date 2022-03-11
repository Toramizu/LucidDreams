using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageUI : MonoBehaviour
{
    [SerializeField] Button borrowed;
    [SerializeField] protected Image image;
    [SerializeField] Sprite defaultImage;

    public RectTransform RectTransform { get { return image.rectTransform; } }

    ImageData data;

    public void Init(ImageData data)
    {
        Toggle(true);
        this.data = data;

        if (data == null || data.Image == null)
        {
            image.gameObject.SetActive(false);
            //image.sprite = defaultImage;
        }
        else
        {
            image.gameObject.SetActive(true);
            image.sprite = data.Image;
            borrowed.gameObject.SetActive(data.Source != null && data.Source != "");
        }

        /*if(data != null)
            borrowed.gameObject.SetActive(data.Source != null && data.Source != "");*/
    }

    public void OpenBorrowed()
    {
        if (data.Source != null && data.Source != "")
            Application.OpenURL(data.Source);
    }

    public virtual void Toggle(bool toggle)
    {
        image.gameObject.SetActive(toggle);
    }
}
