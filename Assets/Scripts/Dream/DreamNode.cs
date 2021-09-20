using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreamNode : MonoBehaviour
{
    [SerializeField] Coordinate coo;
    public Coordinate Coordinate { get { return coo; } }
    [SerializeField] DreamToken rightToken;
    [SerializeField] Vector3 leftPos;
    [SerializeField] Vector3 rightPos;

    //DreamToken currentToken;

    CharacterData cData;
    ShopData sData;

    public void Init(Coordinate coo, DreamManager manager)
    {
        this.coo = coo;
        rightToken.gameObject.SetActive(false);
        rightToken.transform.position = transform.position + rightPos;
    }

    public void Init()
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
        if (cData != null)
        {
            GameManager.Instance.StartBattle(cData);
            rightToken.transform.localScale = new Vector3(.5f, .5f, .5f);
            cData = null;
        } else if(sData != null)
        {
            GameManager.Instance.DreamManager.OpenShop(sData);
        }
    }

    public void PlacePlayer(DreamToken token)
    {
        token.transform.position = transform.position + leftPos;
    }

    public void SetCharacter(CharacterData data)
    {
        cData = data;
        rightToken.gameObject.SetActive(true);
        rightToken.SetCharacter(data);
    }

    public void SetShop(ShopData data)
    {
        sData = data;
        rightToken.gameObject.SetActive(true);
        rightToken.SetShop();
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
        rightToken.gameObject.SetActive(false);
    }
}
