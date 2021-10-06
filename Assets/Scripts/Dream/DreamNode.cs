using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreamNode : MonoBehaviour, PathNode
{
    [SerializeField] Coordinate coo;
    public Coordinate Coordinate { get { return coo; } }
    [SerializeField] DreamToken rightToken;
    [SerializeField] Vector3 leftPos;
    [SerializeField] Vector3 rightPos;

    [SerializeField] protected Image image;
    [SerializeField] Image nextMapImage;

    //public List<NodeLink> CellLinks { get; set; } = new List<NodeLink>();
    //public List<DreamNode> Neighbours { get; set; } = new List<DreamNode>();
    [SerializeField] List<DreamNode> neighbours = new List<DreamNode>();
    //{ get; set; }

    protected GridManager manager;

    CharacterData cData;
    ShopData sData;
    DreamData next;

    NodeContent type;
    public NodeContent Type {
        get { return type; }
        set { SetType(value); }
    }

    #region Pathfinding
    public bool PathStop { get { return cData != null; } }

    public int PCost
    {
        get {
            if (cData == null)
                return 1;
            else
                return 100;
        }
    }

    public List<PathNode> PNeighbours { get; private set; } = new List<PathNode>();
    #endregion

    public void Init(Coordinate coo, GridManager manager)
    {
        this.manager = manager;
        this.coo = coo;
        rightToken.gameObject.SetActive(false);
        rightToken.transform.position = transform.position + rightPos;
        nextMapImage.gameObject.SetActive(false);
    }

    public void Init(Vector3 position, GridManager manager)
    {
        this.manager = manager;
        transform.localPosition = position;
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

    public virtual void OnClick()
    {
        manager.Clicked(this);
        //GameManager.Instance.DreamManager.Clicked(this);
    }

    public void OnEnter()
    {
        //Debug.Log(coo);
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
    }

    public void PlacePlayer(DreamToken token)
    {
        token.transform.position = transform.position + leftPos;
    }

    public void SetCharacter(CharacterData data, bool boss)
    {
        cData = data;
        rightToken.gameObject.SetActive(true);
        rightToken.SetCharacter(data);
        rightToken.transform.localScale = new Vector3(1f, 1f, 1f);
        if(boss)
            SetType(NodeContent.Boss);
        else
            SetType(NodeContent.Succubus);
    }

    public void SetShop(ShopData data)
    {
        sData = data;
        rightToken.gameObject.SetActive(true);
        rightToken.SetShop();
        SetType(NodeContent.Shop);
    }

    public void SetExit(DreamData next)
    {
        this.next = next;
        nextMapImage.gameObject.SetActive(true);
        SetType(NodeContent.Exit);
    }

    public void SetType(NodeContent type)
    {
        this.type = type;
        SetColor();
    }

    public void SetColor()
    {
        switch (Type)
        {
            case NodeContent.None:
            default:
                image.color = Color.white;
                break;

            case NodeContent.Succubus:
                image.color = Color.magenta;
                break;

            case NodeContent.Boss:
                image.color = Color.red;
                break;

            case NodeContent.Shop:
                image.color = Color.cyan;
                break;

            case NodeContent.Start:
                image.color = Color.green;
                break;

            case NodeContent.Exit:
                image.color = Color.gray;
                break;
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

    public float TokenXPos { get { return transform.position.x + leftPos.x; } }
    public float TokenYPos { get { return transform.position.y + leftPos.y; } }

    public void AddNeighbour(DreamNode node)
    {
        neighbours.Add(node);
        PNeighbours.Add(node);
        node.neighbours.Add(this);
        node.PNeighbours.Add(this);
    }

    public void RemoveNeighbour(DreamNode node)
    {
        neighbours.Remove(node);
        PNeighbours.Remove(node);
        node.neighbours.Remove(this);
        node.PNeighbours.Remove(this);
    }

    public void Clear()
    {
        rightToken.gameObject.SetActive(false);
    }
}
