using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreamNode : MonoBehaviour
{
    [SerializeField] Coordinate coo;

    DreamManager manager;
    DreamNodeData data;

    public void Init(Coordinate coo, DreamManager manager)
    {
        this.coo = coo;
        this.manager = manager;
    }

    public void Init(DreamNodeData data)
    {
        Toggle(true);
    }

    public void Toggle(bool toggle)
    {
        GetComponent<Image>().enabled = toggle;
        GetComponent<Button>().enabled = toggle;
    }

    public void OnClick()
    {
        Debug.Log(coo);
    }
}
