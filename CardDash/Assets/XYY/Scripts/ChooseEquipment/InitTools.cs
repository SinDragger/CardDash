using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace xyy{
public static class InitTools 
{
    public static void renewIndexValue(List<GameObject> cards){
        for(int i=0;i<cards.Count;i++){
      cards[i].GetComponent<EquipmentCards>().SetIndex(i);
    }
   } 
   //卡片 Index顺序不变 里面的 Refitting Weapon改变  如果是被移出的 就只改Index
//    public static void reNewCards(MonoBehaviour[] sorts,List<GameObject> cards){
//      for(int i=0;i<sorts.Length;i++){
//        if(!cards[i].GetComponent<EquipmentCards>().getCardStatus()){
         
//        }
//      }
//    }
   public static void renewInformation(List<GameObject> cards,Vector3 startPosition,float cardsDistance){
         renewIndexValue(cards);//更新Index
         int i=0;
         for(int j=0;j<cards.Count;j++){//更新位置 
           if(cards[j].GetComponent<EquipmentCards>().getCardStatus()){
             
           }
           else{
             cards[j].GetComponent<RectTransform>().anchoredPosition3D =startPosition+new Vector3(cardsDistance,0,0)*i;
             i++;
           }
         }
        
   }
   public static void RenewSliderSingleMove(int i,MonoBehaviour s,int cardDistance){//card Distance为预期间隔
     int temp=cardDistance;
     if(i<=6){//5是预期屏幕里放下的卡牌 cardDistance为预期间隔
       temp=cardDistance;}
      else{
        temp=(i-6)*cardDistance;
      }
     s.GetComponent<SldierChange>().sliderSingleMove=temp;
   }
}
}
