using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace xyy{
public class CardSlot : MonoBehaviour
{
    
  bool slotStatus;
    [SerializeField]
   cardsType bCcards;
   #region  get/set
   public cardsType getSlotType(){
       return bCcards;
   }
   public void setSlotType(cardsType ct){
       bCcards=ct;
   }
   public bool getSlotStatus(){
       return slotStatus;
   }
   public void setSlotStatus(bool b){
       slotStatus=b;
   }
   #endregion 
   void Awake(){
      slotStatus=false;
   }
}
}