using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using wyc;
public class UI_EnemySlot : MonoBehaviour, DataReceiver,IBeginDragHandler, IDragHandler,IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int playerId;

    public Text weaponText;
    public Text wantedText;
    public Text meleeText;
    public Text speedText;
    public Text killsText;
    public Text attackedText;
    public Text rangeText;
    public Text healthText;
    public Text nameText;

    public string weapon;
    public int wanted;
    public int melee;
    public int speed;
    public int kills;
    public int attacked;
    public int range;
    public int health;
    public int healthMax;
    public string name;

    public Image healthImage;
    public Image selectImage;
    public Image headIcon;


    public float flodWidth;
    public float unflodWidth;
    public bool isFlod;

    public float targetWidth;
    public float nowWidth;

    public UI_EnemySlotBar slotBar;
    public EnemySlotSideType type;

    public EnemySlotSideType playerState;

    public RectTransform self;


    public float targetX=0f;
    bool reach;
    bool isDraging;
    public bool isSelect;

    void Awake()
    {
        self = GetComponent<RectTransform>();
        UIController.instance.attachRenewList(this, DataChangeType.State);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RenewWidth();
        CheckSelectRightfull();
        if (reach && Input.GetMouseButtonDown(1))
        {
            SelectSlot();
        }
    }
    private bool CheckSelectRightfull()
    {
        if (isSelect && range == 2)
        {
            slotBar.CheckTargetGoingOn(this);
        }
        return false;
    }

    private void RenewWidth()
    {
        nowWidth = self.rect.width;
        //Debug.Log(nowWidth);
        if (nowWidth != targetWidth)
        {
            if (nowWidth < targetWidth)
            {
                nowWidth += slotBar.unFlodSpeed * Time.deltaTime;
                if (nowWidth > targetWidth)
                {
                    nowWidth = targetWidth;
                }
            }
            else
            {
                nowWidth -= slotBar.unFlodSpeed * Time.deltaTime;
                if (nowWidth < targetWidth)
                {
                    nowWidth = targetWidth;
                }
            }
            self.sizeDelta=new Vector2(nowWidth, self.sizeDelta.y);
            slotBar.UpdateSlotsPosition();
        }
    }

    public void SetFlod(bool flod)
    {
        isFlod = flod;
        if (flod)
        {
            targetWidth = flodWidth;
            //self.sizeDelta=new Vector2(flodWidth, self.sizeDelta.y);
        }
        else
        {
            targetWidth = unflodWidth;
            //self.sizeDelta = new Vector2(unflodWidth, self.sizeDelta.y);
        }
    }
    public void ChangeFlod()
    {
        if(!isDraging)
        SetFlod(!isFlod);
    }

    public void RenewData(List<string> temp)
    {
    }
    public void RenewData()
    {
        if(playerState == EnemySlotSideType.OutGame|| playerState == EnemySlotSideType.Complete)
        {
            return;
        }

        List<string> temp=UIController.instance.GetRuntimePlayerSlotData(playerId);
        weapon= temp[0];
        wanted = int.Parse(temp[1]);
        melee = int.Parse(temp[2]);
        speed = int.Parse(temp[3]);
        kills = int.Parse(temp[4]);
        attacked = int.Parse(temp[5]);
        range = int.Parse(temp[6]);
        health = int.Parse(temp[7]);
        healthMax = int.Parse(temp[8]);
        name = temp[9];
        EnemySlotSideType t = (EnemySlotSideType)int.Parse(temp[10]);
        if ((t < EnemySlotSideType.OutGame)){
            switch (t)
            {
                case EnemySlotSideType.None:
                    if(playerState== EnemySlotSideType.Down)
                    {
                        playerState = t;
                    }
                    break;
                case EnemySlotSideType.Down:
                    if (playerState == EnemySlotSideType.None)
                    {
                        playerState = t;
                    }
                    break;
            }
        }
        else
        {
            playerState = t;
        }
        if (playerState == EnemySlotSideType.OutGame)
        {
            SetFlod(true);
            slotBar.SetSlotToBack(this);
        }
        if (weaponText != null) { weaponText.text = weapon; }
        if (wantedText != null) { wantedText.text = wanted.ToString(); }
        if (meleeText != null) { meleeText.text = melee.ToString(); }
        if (speedText != null) { speedText.text = speed.ToString(); }
        if (killsText != null) { killsText.text = kills.ToString(); }
        if (attackedText != null) { attackedText.text = attacked.ToString(); }
        if (rangeText != null) { rangeText.text = range.ToString(); }
        if (healthText != null) { healthText.text = health.ToString(); }
        if (nameText != null) { nameText.text = name; }

        if (health == healthMax)
        {
            healthImage.color=new Color(19f/255f,161f/255f,0f);
        }
        else if (health > healthMax / 3)
        {
            healthImage.color = new Color(150f / 255f, 161f / 255f, 0f);

        }
        else if(health > 0)
        {
            healthImage.color = new Color(161/ 255f, 39f / 255f, 0f);
        }
        else
        {
            healthImage.color = new Color(102f / 255f, 102f / 255f,102f / 255f);
        }
        UpdateStateColor();
    }
    public void UpdateStateColor()
    {
        switch (playerState)
        {
            case EnemySlotSideType.None:
                selectImage.color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
                break;
            case EnemySlotSideType.Target:
                selectImage.color = new Color(254f / 255f, 154f / 255f, 114f / 255f);
                break;
            case EnemySlotSideType.OutGame:
                selectImage.color = new Color(80f / 255f, 80f / 255f, 80f / 255f);
                break;
            case EnemySlotSideType.Down:
                selectImage.color = new Color(184f / 255f, 94f / 255f, 224f / 255f);
                break;
            case EnemySlotSideType.Complete:
                selectImage.color = new Color(252f / 255f, 255f / 255f, 141f / 255f);
                break;
            case EnemySlotSideType.RealPlayer:
                selectImage.color = new Color(100f / 255f, 101f / 255f, 255f / 255f);
                break;
        }
        if (isSelect)
        {
            selectImage.color = new Color(254f / 255f, 0f / 255f, 0f / 255f);
        }
    }
    Vector2 startPosition;
    Vector2 offset;
    public void OnDrag(PointerEventData eventData)
    {
        offset=  eventData.position- startPosition ;
        startPosition= eventData.position;
        slotBar.BarScroll(offset.x);
        //Debug.Log("拖动了外层"+offset.x);


    }
    void SelectSlot()
    {
        if (isSelect)
        {
            slotBar.CheckTargetGoingOn(this);
            return;
        }
        if (range == 2)
        {
            return;
        }
        //if () return;:range<2.但好像没成功
        switch (playerState)//target不会触发
        {
            case EnemySlotSideType.None:
            case EnemySlotSideType.Down:
                slotBar.SelectSlot(this);
                break;
        }

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition=eventData.position;
        isDraging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDraging = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        reach = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        reach = false;
    }
}