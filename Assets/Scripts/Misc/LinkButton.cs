using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkButton : MonoBehaviour
{
    [SerializeField] string link;

    public void OnClick()
    {
        if (link != null && link != "")
            Application.OpenURL(link);
    }
}
