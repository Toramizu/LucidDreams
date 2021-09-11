using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleGauge : MonoBehaviour
{
    [SerializeField] Color backgroundColor;
    [SerializeField] Color fillColor;
    [SerializeField] Color textColor;

    [SerializeField] Image background;
    [SerializeField] Transform mask;
    [SerializeField] Image filling;

    [SerializeField] TMP_Text text;

    private void Start()
    {
        background.color = backgroundColor;
        filling.color = fillColor;
        text.color = textColor;
    }

    public void Fill(int current, int max)
    {
        if (max == 0)
            mask.localScale = new Vector3(1, 1, 1);
        else
            mask.localScale = new Vector3((float)current / max, 1, 1);

        text.text = current.ToString() + '/' + max;
    }
}
