using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace xyy{

public enum cardsType{
      Refittingcards,
      Weaponcards
}
public class EquipmentCards : MonoBehaviour
{
      cardsType e_cardType;
      int index;//在列表中的初始序号 或者是排序后的序号；
      Refitting re;
      Weapon we;
      bool moveOut;
      GameObject cardSlot;
      void Awake(){
            cardSlot=null;
            re=null;
            we=null;
            moveOut=false;//true为移除
      }
       #region  Set/get
       public GameObject getCardSlot(){
             return cardSlot;
       }
       public void setCardsSlot(GameObject go){
             cardSlot=go;

       }
       public bool getCardStatus(){
      return moveOut;
       }
        public void SetCardStatus(bool b){
          moveOut=b;
       }
       public cardsType GetE_CardType(){
             return e_cardType;
       }
       public void SetE_CardType(cardsType e){
             e_cardType=e;
       }
      public void SetIndex(int I){
            index=I;
      }
     public void SetRefitting(Refitting r){
         re=r;
      }
      public void SetWeapon(Weapon w){
         we=w;
      }
       public Refitting GetRefitting(){
         return re;
      }
      public Weapon GetWeapon(){
         return  we;
         
      }
      #endregion;
      
}
}

      

