using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace wyc
{
    public class LandCard : MonoBehaviour
    {
        public int landID=-1;
        public Image cardPicture;
        public Text name_text;
        public Text distance_Text;
        public Text complex_Text;
        public Text discribe_Text;

        Canvas canvas;
        Scale selfScale;
        RectTransform self;
        //暂存数值
        // Start is called before the first frame update
        void Start()
        {
            //FindCanvas();
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

        void Update()
        {
            
        }

        public void UpdateData()
        {
            List<string> data = UIController.instance.GetLandDataByID(landID);
            //载入属性
            if (data != null)
            {
                name_text.text = data[0];
                //player.strength.ToString();
                distance_Text.text = data[1];
                complex_Text.text = data[2];
                //discribe_Text.text = data[3]; 
                Sprite temp = (Sprite)Resources.Load("Pictures/Land/" + data[0], typeof(Sprite));
                cardPicture.sprite = temp;
            }
        }

    }

}