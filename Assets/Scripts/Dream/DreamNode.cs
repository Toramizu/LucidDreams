using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreamNode : MonoBehaviour
{
    [SerializeField] Coordinate coo;
    public Coordinate Coordinate { get { return coo; } }
    [SerializeField] Vector3 leftPos;
    [SerializeField] Vector3 rightPos;

    DreamNodeData data;
    DreamToken currentToken;

    public void Init(Coordinate coo, DreamManager manager)
    {
        this.coo = coo;
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
        GameManager.Instance.DreamManager.Clicked(this);
    }

    public void OnEnter()
    {
        if (currentToken != null)
        {
            GameManager.Instance.StartBattle(currentToken.Character);
            currentToken = null;
        }
    }

    public void PlaceAt(DreamToken token, bool left)
    {
        if (left)
            token.transform.position = transform.position + leftPos;
        else
        {
            token.transform.position = transform.position + rightPos;
            currentToken = token;
        }
    }

    public void MoveTo(DreamToken token)
    {
        iTween.MoveTo(token.gameObject, iTween.Hash(
            "x", transform.position.x + leftPos.x,
            "y", transform.position.y + leftPos.y,
            "time", .8f,
            "easeType", iTween.EaseType.easeOutSine,
            "onComplete", "OnEnter",
            "onCompleteTarget", gameObject
            ));
    }

    public void Clear()
    {
        currentToken = null;
    }
}
