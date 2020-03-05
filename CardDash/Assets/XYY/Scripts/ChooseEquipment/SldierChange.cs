using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//[RequireComponent(typeof(UnityEngine .UI.Slider))]
public class SldierChange : MonoBehaviour

{
   public GameObject sliderGameObject;
   public int verticalOrHorizontal; //0 v 1 h
   public float sliderSingleMove;
   public void ChangeGameObject(){
       Vector3 direction=new Vector3(0,0,0);
       switch(verticalOrHorizontal){
        case 0:
          direction=new Vector3(0,1,0);
          break;
        case 1:
          direction=new Vector3(-1,0,0);//根据实际情况变化
          break;
        default:
           Debug.Log("不要乱输入方向数值");
           break ;
       }
        sliderGameObject.GetComponent<RectTransform>().anchoredPosition3D=this.GetComponent<Slider>().value*direction*sliderSingleMove;
   }
}
