using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComplexButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Image image;
    [SerializeField] TMP_Text text;

    void Start()
    {
        button = GetComponent<Button>();
    }

    public void Toggle(bool toggle)
    {
        button.interactable = toggle;

        if (toggle)
        {
            Color i = image.color;
            i.a = 1f;
            image.color = i;
            Color t = text.color;
            t.a = 1f;
            text.color = t;
        }
        else
        {
            Color i = image.color;
            i.a = .5f;
            image.color = i;
            Color t = text.color;
            t.a = .5f;
            text.color = t;
        }
    }
}
