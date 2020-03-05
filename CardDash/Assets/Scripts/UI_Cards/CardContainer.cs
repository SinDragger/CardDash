using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace wyc
{
    public interface ICardContainer
    {

    }
    public class CardContainer : Basic_CardContainer, DataReceiver
    {
        public GameObject Test;//工具
        public GameObject Slot;//工具

        public Color slotDefaultColor;
        public Color slotChoosingColor;

        public int row;//确定行数
        public float deviationValue;//偏移数值——用于和卡牌对比确定对Flag的改变
        //Slot的发光提醒


        public float cardscale;
        public float minCardBorder;
        

        int maxNumber;
        int nowNumber;
        float eachborder;
        // Start is called before the first frame update
        void Start()
        {
            equipsCards = new List<GameObject>();
            weaponsCards = new List<GameObject>();
            UIController.instance.attachRenewList(this, DataChangeType.Init);
            //UIController.instance.cardContainer = this;
            //cards.Add(Slot);
            equipPointer = -1;
            weaponPointer = -1;
        }

        void PrepareInvisible()
        {
            switch (cardContainerType)
            {
                case CardContainerType.Equip:
                    //隐蔽所有Weapon
                    foreach(var temp in weaponsCards)
                    {
                        temp.GetComponent<CardSlot>().SetVisible(false);
                    }
                    foreach (var temp in equipsCards)
                    {
                        temp.GetComponent<CardSlot>().SetVisible(true);
                    }
                    break;
                case CardContainerType.Weapon:
                    foreach (var temp in weaponsCards)
                    {
                        temp.GetComponent<CardSlot>().SetVisible(true);
                    }
                    foreach (var temp in equipsCards)
                    {
                        temp.GetComponent<CardSlot>().SetVisible(false);
                    }
                    break;
            }
        }

        // Update is called once per frame
        void Update()
        {
            InitSlot();
            PrepareInvisible();
        }
        public void InitSlot()
        {
            //获得自己的可用空间与卡牌大小，确定缩放比例与内部间隔
            if (panel != null)
            {
                //Debug.Log("Panel");
                float width = panel.GetComponent<RectTransform>().rect.width - paddingR - paddingL;
                float height = panel.GetComponent<RectTransform>().rect.height - paddingT - paddingB;
                float yfix = paddingB / 2 - paddingT / 2;
                float xfix = paddingL / 2 - paddingR / 2;
                cardHeight = height - paddingT - paddingB;
                cardWidth = (float)GameController.CardX * cardHeight / (float)GameController.CardY;
                cardscale = cardHeight / (float)GameController.CardY;
                maxNumber = (int)width / (int)(cardWidth + minCardBorder);
                nowNumber = maxNumber;
                eachborder = (width - (float)maxNumber * cardWidth) / (maxNumber + 1);
                //float Leftx = this.GetComponent<RectTransform>().position.x;
                //float Righx;
                //Test.GetComponent<RectTransform>().localPosition = (new Vector3(xfix, yfix, 0f));
                //Test.GetComponent<RectTransform>().sizeDelta = (new Vector2(width, height));
                //根据卡牌的个数算上修正值：卡牌数*eachborder+卡*卡牌数-1
                //算中心点偏移
                
                int a = equipsCards.Count;
                if (a < maxNumber)
                {
                    for(int i = 0; i < maxNumber - a; i++)
                    {
                        GameObject temp = Instantiate(Slot, panel.transform);
                        temp.GetComponent<CardSlot>().cardContainer = this;
                        equipsCards.Add(temp);
                        temp = Instantiate(Slot, panel.transform);
                        temp.GetComponent<CardSlot>().cardContainer = this;
                        weaponsCards.Add(temp);
                    }
                }else if(a > maxNumber)
                {
                    for (int i = equipsCards.Count ; i > maxNumber; i--)
                    {
                        GameObject temp = equipsCards[i-1];
                        equipsCards.Remove(temp);
                        Destroy(temp);
                        temp = weaponsCards[i - 1];
                        weaponsCards.Remove(temp);
                        Destroy(temp);
                    }
                }

                for (int i = 0; i < equipsCards.Count; i++)
                {
                    float fixLeft = (i + 1) * eachborder + cardWidth * i + cardWidth / 2;//左起
                    
                    equipsCards[i].GetComponent<RectTransform>().localPosition = (new Vector3(fixLeft - width / 2+xfix,  yfix, 0f));
                    equipsCards[i].GetComponent<RectTransform>().sizeDelta = (new Vector2(cardWidth, cardHeight));
                }

                for (int i = 0; i < weaponsCards.Count; i++)
                {
                    float fixLeft = (i + 1) * eachborder + cardWidth * i + cardWidth / 2;//左起

                    weaponsCards[i].GetComponent<RectTransform>().localPosition = (new Vector3(fixLeft - width / 2 + xfix, yfix, 0f));
                    weaponsCards[i].GetComponent<RectTransform>().sizeDelta = (new Vector2(cardWidth, cardHeight));
                }
                //先装载再
                //根据当前typeSetActive
            }
        }
        void InitAllVisibleCards()
        {
            List<int> target = new List<int>();
            //根据当前决定使用哪个列表来显示
            int minShow;

            target = equips;
            minShow= target.Count < nowNumber ? target.Count : nowNumber;
            CardSlot cardSlot;
            for (int i = 0; i < minShow; i++)
            {
                int temp = i;
                temp += equipFlag;
                if (temp >= equips.Count)
                {
                    temp -= equips.Count;
                }
                cardSlot = equipsCards[i].GetComponent<CardSlot>();
                if (cardSlot.targetCard == null)
                {
                    GameObject b = UIController.instance.CreateNewEquipCard(equips[temp], cardSlot.gameObject);
                    cardSlot.targetCard = b;
                    cardSlot.ResetPosition(cardscale);
                    b.GetComponent<EquipCard>().UpdateData();
                }
            }

            //Debug.Log($"更新卡的数量{minShow}");

            target = weapons;
            minShow = target.Count < nowNumber ? target.Count : nowNumber;
            for (int i = 0; i < minShow; i++)
            {
                int temp = i;
                temp += weaponFlag;
                if (temp >= weapons.Count)
                {
                    temp -= weapons.Count;
                }
                cardSlot = weaponsCards[i].GetComponent<CardSlot>();
                if (cardSlot.targetCard == null)
                {
                    GameObject b = UIController.instance.CreateNewWeaponCard(weapons[temp], cardSlot.gameObject);
                    cardSlot.targetCard = b;
                    cardSlot.ResetPosition(cardscale);
                    b.GetComponent<WeaponCard>().UpdateData();
                }
            }


            target = weapons;
        }

        public void RenewData(List<string> temp)
        {
            Debug.Log("False Using");
        }
        void RenewCards()//更新卡牌的显示挂载
        {
            //根据UI界面的当前指定来更新自己。


            List<int> target=new List<int>();
            //根据当前决定使用哪个列表来显示
            
            switch (cardContainerType)
            {
                case CardContainerType.Equip:
                    target = equips;
                    break;
                case CardContainerType.Weapon:
                    target = weapons;
                    break;
            }
            int minShow = target.Count < nowNumber ? target.Count : nowNumber;
            CardSlot cardSlot;
            for (int i = 0; i < minShow; i++)
            {
                int temp = i;
                switch (cardContainerType)
                {
                    case CardContainerType.Equip:
                        temp += equipFlag;
                        if (temp >= equips.Count)
                        {
                            temp -= equips.Count;
                        }
                         cardSlot = equipsCards[i].GetComponent<CardSlot>();
                        if (cardSlot.targetCard == null)
                        {
                            GameObject b = UIController.instance.CreateNewEquipCard(equips[temp], cardSlot.gameObject);
                            cardSlot.targetCard = b;
                            cardSlot.ResetPosition(cardscale);
                            b.GetComponent<WeaponCard>().UpdateData();
                        }

                        break;
                    case CardContainerType.Weapon:
                        temp += weaponFlag;
                        if (temp >= weapons.Count)
                        {
                            temp -= weapons.Count;
                        }
                         cardSlot = weaponsCards[i].GetComponent<CardSlot>();
                        if (cardSlot.targetCard == null)
                        {
                            GameObject b = UIController.instance.CreateNewWeaponCard(weapons[temp], cardSlot.gameObject);
                            //GameObject b = UIController.instance.CreateNewWeaponCard(weapons[temp]);
                            cardSlot.targetCard = b;
                            cardSlot.ResetPosition(cardscale);
                            b.GetComponent<WeaponCard>().UpdateData();
                            
                        }
                        break;
                }
            }
        }

        public void SwitchCardContainerType()//切换内部成员
        {
            switch (cardContainerType)
            {
                case CardContainerType.Equip:
                    cardContainerType = CardContainerType.Weapon;
                    break;
                case CardContainerType.Weapon:
                    cardContainerType = CardContainerType.Equip;
                    break;
            }
            
            PrepareInvisible();
        }

        public void RequestNewList()//向UIController请求更新装备表信息
        {
            List<int> temp= UIController.instance.GetRealPlayerEquipsId();
            //如果数据更新的列表中没有了原本指向的目标，则flag接着原本的位子，否则为temp中的位置

            equips = temp;
            temp = UIController.instance.GetRealPlayerWeaponsId();
            weapons = temp;
            //Debug.Log($"获取到的武器数量为{temp.Count}");
            //panel.transform.parent = this.transform;

            //GameObject b;
            //b = (GameObject)Resources.Load("Prefabs/WeaponCardPrefab");
            //b = GameController.Instantiate(b);
            //b.GetComponent<WeaponCard>().weaponID = weaponId;
            //b.GetComponent<WeaponCard>().playerName = RaceController.instance.RealPlayer.player.name;
            //RenewCards();
        }

        public void RenewData()
        {
            InitSlot();
            RequestNewList();
            InitAllVisibleCards();
            PrepareInvisible();
            RenewSlotData();
           
        }



        public override bool NextPointer(CardContainerType cardContainerType)
        {
            List<int> temp=null;
            switch (cardContainerType)
            {
                case CardContainerType.Equip:
                    temp = equips;
                    if (temp.Count == 0) { return false; }
                    equipPointer++;
                    if (equipPointer > temp.Count - 1)
                    {
                        equipPointer = 0;
                    }
                    break;
                case CardContainerType.Weapon:
                    temp = weapons;
                    if (temp.Count == 0) { return false; }
                    weaponPointer++;
                    if (weaponPointer > temp.Count - 1)
                    {
                        weaponPointer = 0;
                    }
                    //反馈武器

                    break;
            }

            RenewSlotData();
            //被Input或操作调用，自己变化后，修改UI内容
            return false;
        }

        public override bool FrontPointer(CardContainerType cardContainerType)
        {
            List<int> temp = null;
            switch (cardContainerType)
            {
                case CardContainerType.Equip:
                    temp = equips;
                    if (temp.Count == 0) { return false; }
                    equipPointer--;
                    if (equipPointer < 0)
                    {
                        equipPointer = temp.Count - 1;
                    }
                    break;
                case CardContainerType.Weapon:
                    temp = weapons;
                    if (temp.Count == 0) { return false; }
                    weaponPointer--;
                    if (weaponPointer < 0)
                    {
                        weaponPointer = temp.Count - 1;
                    }
                    break;
            }
            RenewSlotData();
            return false;
        }
        public void ClickBySlot(GameObject target)
        {
            List<GameObject> temp = null;
            switch (cardContainerType)
            {
                case CardContainerType.Equip:
                    temp = equipsCards;
                    break;
                case CardContainerType.Weapon:
                    temp = weaponsCards;
                    break;
            }
            for(int i = 0; i < temp.Count; i++)
            {
                if (target == temp[i])
                {
                    SetPointer(cardContainerType, i);
                }
            }
        }

        public override bool SetPointer(CardContainerType cardContainerType, int flag)
        {
            switch (cardContainerType)
            {
                case CardContainerType.Equip:
                    equipPointer = flag;
                    break;
                case CardContainerType.Weapon:
                    weaponPointer = flag;
                    break;
            }
            RenewSlotData();
            return false;
        }

        public override void RenewSlotData()
        {
            for(int i = 0; i < equipsCards.Count;i++)
            {
                if (i != equipPointer)
                {
                    equipsCards[i].GetComponent<Image>().color = slotDefaultColor;
                }
                else
                {
                    equipsCards[i].GetComponent<Image>().color = slotChoosingColor;

                }
            }
            for(int i = 0; i < weaponsCards.Count;i++)
            {
                if (i != weaponPointer)
                {
                    weaponsCards[i].GetComponent<Image>().color = slotDefaultColor;
                }
                else
                {
                    weaponsCards[i].GetComponent<Image>().color = slotChoosingColor;

                }
            }
            //激活状态、不可使用状态、
        }

    }

    public enum CardContainerType
    {
        Equip,
        Weapon,
        Land,
        Thug
    }


}
