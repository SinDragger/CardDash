using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace xyy{
public class HaveBuyedCards : MonoBehaviour
{
   public static List<Weapon> buyedWeapon=new List<Weapon>();
   public static List<Refitting> buyedRefitting=new List<Refitting>();
   public List<GameObject> cardSlots;

      public static HaveBuyedCards singleton=null;
    [SerializeField]
    int refittingNumber;
    void Awake(){
       if(singleton==null){//单例模式
      
           singleton=this;}
        else  if(singleton!=this){
      
            Destroy(gameObject);
        };
    }
    public GameObject getFreeWeaponSlot(){
      for(int i=refittingNumber;i<cardSlots .Count;i++){
          if(!cardSlots[i].GetComponent<CardSlot>().getSlotStatus()){
              return cardSlots[i];
          }
      }
      return null;
    }
    public GameObject getFreeRefittingSlot(){
      for(int i=0;i<refittingNumber;i++){
          if(!cardSlots[i].GetComponent<CardSlot>().getSlotStatus()){
              return cardSlots[i];
          }
      }
      return null;
    }
}
}
