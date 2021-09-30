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

    [SerializeField] Image image;
    [SerializeField] Image nextMapImage;
    /*NodeContent type;
    readonly Dictionary<NodeContent, Color> colors = new Dictionary<NodeContent, Color>()
    {
        {NodeContent.None, Color.white},
        {NodeContent.Succubus, Color.red},
        {NodeContent.Exit, Color.black},
        {NodeContent.Shop, Color.cyan},
    };*/

    //DreamToken currentToken;

    CharacterData cData;
    ShopData sData;
    DreamData next;

    public void Init(Coordinate coo, DreamManager manager)
    {
        this.coo = coo;
        rightToken.gameObject.SetActive(false);
        rightToken.transform.position = transform.position + rightPos;
        nextMapImage.gameObject.SetActive(false);
    }

    public void Init()
    {
        Toggle(true);
    }

    public void Toggle(bool toggle)
    {
        image.enabled = toggle;
        GetComponent<Button>().enabled = toggle;
        nextMapImage.gameObject.SetActive(false);
    }

    public void OnClick()
    {
        GameManager.Instance.DreamManager.Clicked(this);
    }

    public void OnEnter()
    {
        Debug.Log(coo);
        if(cData != null)
        {
            if (cData != null)
            {
                GameManager.Instance.StartBattle(cData);
                rightToken.transform.localScale = new Vector3(.5f, .5f, .5f);
                cData = null;
            }
        }
        else if(sData != null)
        {
            GameManager.Instance.DreamManager.OpenShop(sData);
        } else if(next != null)
        {
            GameManager.Instance.DreamManager.ContinueDream(next);
        }

        /*switch (type)
        {
            case NodeContent.Shop:
                GameManager.Instance.DreamManager.OpenShop(sData);
                break;

            case NodeContent.Succubus:
            case NodeContent.Boss:
                if (cData != null)
                {
                    GameManager.Instance.StartBattle(cData);
                    rightToken.transform.localScale = new Vector3(.5f, .5f, .5f);
                    cData = null;
                }
                break;

            case NodeContent.Exit:
                Debug.Log("Next map");
                GameManager.Instance.DreamManager.ContinueDream(next);
                break;
        }*/

        /*if (cData != null)
        {
            GameManager.Instance.StartBattle(cData);
            rightToken.transform.localScale = new Vector3(.5f, .5f, .5f);
            cData = null;
        } else if(sData != null)
        {
            GameManager.Instance.DreamManager.OpenShop(sData);
        }*/
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
        rightToken.transform.localScale = new Vector3(1f, 1f, 1f);
        image.color = Color.red;
    }

    public void SetShop(ShopData data)
    {
        sData = data;
        rightToken.gameObject.SetActive(true);
        rightToken.SetShop();
        image.color = Color.cyan;
    }

    public void SetExit(DreamData next)
    {
        this.next = next;
        nextMapImage.gameObject.SetActive(true);
        image.color = Color.gray;
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
