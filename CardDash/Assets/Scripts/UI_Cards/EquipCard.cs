﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace wyc
{
    public class EquipCard : MonoBehaviour
    {
        public string playerName;
        public int EquipID=-1;
        public Image cardPicture;
        public Text name_text;
        public Text price_text;
        public Text weight_Text;
        public Text effect_Text;
        public Text discribe_Text;

        Canvas canvas;
        Scale selfScale;
        RectTransform self;
        //暂存数值
        // Start is called before the first frame update
        void Start()
        {
           // FindCanvas();
            self = this.GetComponent<RectTransform>();
            selfScale = GetComponent<Scale>();
        }
        //void FindCanvas()
        //{
        //    Transform t = this.transform;
        //    Canvas temp;
        //    while (t.parent != null)
        //    {
        //        t = transform.parent;
        //        temp = t.GetComponent<Canvas>();
        //        if (temp != null)
        //        {
        //            canvas = temp;
        //        }
        //    }
        //}

        // Update is called once per frame
        void Update()
        {
        }


        public void UpdateData()
        {
            List<string> data = UIController.instance.GetEquipDataByIDAndPlayerName(EquipID, playerName);
            if (data != null)
            {
                name_text.text = data[0];
                price_text.text = data[2]; //
                effect_Text.text = data[3]; //

                Sprite temp = (Sprite)Resources.Load("Pictures/Equip/" + data[0], typeof(Sprite));
                cardPicture.sprite = temp;
            }
        }
    }

}