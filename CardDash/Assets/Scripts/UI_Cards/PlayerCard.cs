using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace wyc
{
    public class PlayerCard : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        //Thug player;
        //public string playerName;
        public int runtimePlayerID;
        int chCount;
        public Image cardPicture;
        public Image ch_icon1;
        public Image ch_icon2;
        public Image ch_icon3;

        public Text characteristic_text1;
        public Text characteristic_text2;
        public Text characteristic_text3;

        public Text name_text;
        public Text strength_text;
        public Text drive_text;
        public Text health_text;
        public Text money_text;


        Scale selfScale;
        RectTransform self;
        //暂存数值
        // Start is called before the first frame update
        void Start()
        {
            chCount = 0;
            //FindCanvas();
            self = this.GetComponent<RectTransform>();
            selfScale = GetComponent<Scale>();
        }


        // Update is called once per frame
        void Update()
        {
        }

        public void UpdateData()
        {
            //List<string> data=UIController.instance.GetPlayerData(playerName);
            List<string> data=UIController.instance.GetPlayerData(runtimePlayerID);
            //载入属性
            //Debug.Log("数据数量" + data.Count);
            strength_text.text = data[0];
                //player.strength.ToString();
            drive_text.text = data[1]; //;
            health_text.text = data[2]; //player.health.ToString();
            money_text.text = data[3]; //;
            name_text.text = data[4]; //player.name;


            Sprite temp = (Sprite)Resources.Load("Pictures/Thug/"+data[4], typeof(Sprite));
            cardPicture.sprite = temp;
                //载入特性
                //if (chCount != player.characteristics.Count)
                //{
            for (int i = 0; i < 3; i++)
            {
                if (2 * i < data.Count - 5)
                //player.characteristics.Count
                {
                    //Debug.Log($"{data[5 + 2*i]}, {data[6 + 2 * i]}");
                    Load(i, data[5 + 2 * i], data[6 + 2 * i]);
                    //player.characteristics[i]
                }
                else
                {
                    Load(i, null, null);
                }
                //player.characteristics.Count
                //加载3个
            }
            //}
        }

        public void UpdateData(ThugSource target)//快速载入
        {
            //List<string> data=UIController.instance.GetPlayerData(playerName);
            //List<string> data = UIController.instance.GetPlayerData(runtimePlayerID);
            //载入属性
            //Debug.Log("数据数量" + data.Count);
            strength_text.text = target.strength.ToString();
            //player.strength.ToString();
            drive_text.text = target.drive.ToString();
            health_text.text = target.health.ToString();
            money_text.text = target.wealth.ToString()+",000";
            name_text.text = target.name;


            Sprite temp = (Sprite)Resources.Load("Pictures/Thug/" + target.name, typeof(Sprite));
            cardPicture.sprite = temp;
            //载入特性
            //if (chCount != player.characteristics.Count)
            //{
            //target.characteristics 特性表，查询特性
            
            for (int i = 0; i < 3; i++)
            {
                if (i < target.characteristics.Count)
                //player.characteristics.Count
                {
                    //Debug.Log($"{data[5 + 2*i]}, {data[6 + 2 * i]}");
                    Load(i, DataController.instance.GetCharacter(target.characteristics[i]));
                    //player.characteristics[i]
                }
                else
                {
                    //Debug.Log("None");
                    Load(i, null);
                }
                //player.characteristics.Count
                //加载3个
            }
            //}
        }

        void Load(int order, CharacteristicSource c)//Characteristic target
        {
            Sprite temp = null;
            //先验证是否为人物特性
            string s="";
            if (c == null)
            {
                temp = (Sprite)Resources.Load("Pictures/Card/character_icon_small_transparent", typeof(Sprite));
                
            }
            else
            {
                s = c.name;
                switch (c.characteristicType)
                {
                    //case (int)CHARACTERISTIC_TYPE.COMBAT:
                    //    temp = (Sprite)Resources.Load("Pictures/Card/character_icon_small_combat", typeof(Sprite));
                    //    break;
                    //case (int)CHARACTERISTIC_TYPE.DRIVE:
                    //    temp = (Sprite)Resources.Load("Pictures/Card/character_icon_small_drive", typeof(Sprite));
                    //    break;
                    //case (int)CHARACTERISTIC_TYPE.EQUIP:
                    //    temp = (Sprite)Resources.Load("Pictures/Card/character_icon_small_equip", typeof(Sprite));
                    //    break;
                    //case (int)CHARACTERISTIC_TYPE.PERSONAL:
                    //    temp = (Sprite)Resources.Load("Pictures/Card/character_icon_small_combat", typeof(Sprite));
                    //    break;
                }
            }
            switch (order)
            {
                case 0:
                    ch_icon1.sprite = temp;
                    characteristic_text1.text = s;
                    break;
                case 1:
                    ch_icon2.sprite = temp;
                    characteristic_text2.text = s;
                    break;
                case 2:
                    ch_icon3.sprite = temp;
                    characteristic_text3.text = s;
                    break;
            }
        }
        void Load(int order,string character,string characterType)//Characteristic target
        {
            Sprite temp=null;
            //先验证是否为人物特性
            if (character == null)
            {
                temp = (Sprite)Resources.Load("Pictures/Card/character_icon_small_transparent", typeof(Sprite));
                return;
            }
            else
            {
                switch (int.Parse(characterType))
                {
                    //case (int)CHARACTERISTIC_TYPE.COMBAT:
                    //    temp = (Sprite)Resources.Load("Pictures/Card/character_icon_small_combat", typeof(Sprite));
                    //    break;
                    //case (int)CHARACTERISTIC_TYPE.DRIVE:
                    //    temp = (Sprite)Resources.Load("Pictures/Card/character_icon_small_drive", typeof(Sprite));
                    //    break;
                    //case (int)CHARACTERISTIC_TYPE.EQUIP:
                    //    temp = (Sprite)Resources.Load("Pictures/Card/character_icon_small_equip", typeof(Sprite));
                    //    break;
                    //case (int)CHARACTERISTIC_TYPE.PERSONAL:
                    //    temp = (Sprite)Resources.Load("Pictures/Card/character_icon_small_combat", typeof(Sprite));
                    //    break;
                }
            }
            switch(order)
            {
                case 0:
                    ch_icon1.sprite = temp;
                    characteristic_text1.text = character;
                    break;
                case 1:
                    ch_icon2.sprite = temp;
                    characteristic_text2.text = character;
                    break;
                case 2:
                    ch_icon3.sprite = temp;
                    characteristic_text3.text = character;
                    break;
            }
        }
        //public void SetPlayer(Thug thug)
        //{
        //    if (thug != null)
        //    {
        //        Debug.Log($"名字：{thug.name}");
        //        playerName = thug.name;
        //        player = thug;
        //        cardPicture.sprite = (Sprite)Resources.Load("Pictures/Thug/" + thug.name, typeof(Sprite));
        //        if (player == null)
        //        {
        //            Debug.Log("赋值无效");
        //        }
        //        UpdateData();
        //    }
        //}

        public void OnPointerDown(PointerEventData eventData)
        {
            //Debug.Log("点击了");
            //this.transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            //Debug.Log("在拖");
            //selfScale.scale = 0.25f;
            //self.position = new Vector3(self.position.x+ eventData.delta.x, self.position.y + eventData.delta.y, 0);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //Debug.Log("鼠标拾起");
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //Debug.Log("拖拽结束");
            //selfScale.scale = 0.2f;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //Debug.Log("鼠标进来");
            //selfScale.scale = 0.2f;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //Debug.Log("鼠标移除");
            //selfScale.scale = 0.17f;
        }
    }

}