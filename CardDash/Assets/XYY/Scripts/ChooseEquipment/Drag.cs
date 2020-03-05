
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace xyy
{
    public class Drag: MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {      

    GameObject canvas;
        RectTransform position;
        cardsType cT;
         float doubleClick;
         bool hasdoubleClick;
         LayerMask cardSortLayer=1<<9;
        RaycastHit hit;
        GameObject father;
        Ray cameraRay;
        EquipmentCards thisCard;
        public GameObject shelterPanel;
       // public LayerMask thisCardLayer;//this.gameobject.layer 就不行？？？
        //public LayerMask test;

        //暂存数值
        // Start is called before the first frame update
        void Start()
        {
            cameraRay=Camera.main.ScreenPointToRay(Input .mousePosition);
            thisCard=this.GetComponent<EquipmentCards>();
            hasdoubleClick=false;
            father=this.transform .parent.gameObject;
           cT=thisCard.GetE_CardType();
           canvas=GameObject.FindGameObjectWithTag("Canvas");
           // FindCanvas();
            position = this.GetComponent<RectTransform>();
           doubleClick=Time.time;
          
        }
       
        // Update is called once per frame
        void Update()
        {
           
            
        }
        #region  private one
            
 
        private void emptyCardSlot(GameObject slot){
                   slot.GetComponent<CardSlot>().setSlotStatus(false);
                   thisCard.setCardsSlot(null);
        }
        private void plusCapacity(){
          this.transform .SetParent(father.transform);
            GameObject slot=thisCard.getCardSlot();
          if(thisCard.getCardStatus()&&slot!=null){
                  emptyCardSlot(slot);
                 if(cT==cardsType.Refittingcards){
                    HaveBuyedCards.buyedRefitting.Remove(thisCard.GetRefitting());
                   
                    RefittingInit.actualCapacity+=1;
                  Debug.Log(  RefittingInit.actualCapacity);
                 // RefittingInit.singleton .renewCards(); boom warning
                 }
                 else{
                    HaveBuyedCards.buyedWeapon.Remove(thisCard.GetWeapon());

                    WeaponInit.actualCapacity+=1;
                   Debug.Log(  WeaponInit.actualCapacity);
                    //  WeaponInit.singleton .renewCards(); boom warning
                 }
                 thisCard.SetCardStatus(false);
              }
               if(cT==cardsType.Refittingcards){
                
                RefittingInit.singleton .renewCards();
           }
              else {
                  Debug.Log("?????");
                 WeaponInit.singleton.renewCards();
        }
        }
        private bool FreeObject(GameObject fo){
               if(fo!=null){
                     return true;
                 }
                 return false;
        }
        private bool freePosition(){
            GameObject freeObject;
            if(cT==cardsType.Refittingcards){
                 freeObject=HaveBuyedCards.singleton.getFreeRefittingSlot();
                 return FreeObject(freeObject);
            }
            else{
                 freeObject=HaveBuyedCards.singleton.getFreeWeaponSlot();
                 return FreeObject(freeObject);
            }
        }
        private void reduceCapacity(Vector3 position,bool getFree){
                this.transform .SetParent(canvas.transform);
            GameObject freeObject=null;
           thisCard.SetCardStatus(true);
              if(cT==cardsType.Refittingcards){
                HaveBuyedCards.buyedRefitting.Add(thisCard.GetRefitting());
                Debug.Log(HaveBuyedCards.buyedRefitting.Count);
                if ( RefittingInit.actualCapacity!=0){
                   RefittingInit.actualCapacity-=1;
                  }
                  if(getFree){
                       freeObject=HaveBuyedCards.singleton.getFreeRefittingSlot();
                        if(  freeObject!=null){
                         setCardSlot(freeObject);
                     Debug.Log("???");
                      position=  freeObject.transform .position ;}
                  }
              }
             else {
                HaveBuyedCards.buyedWeapon.Add(thisCard.GetWeapon());
                if (WeaponInit.actualCapacity!=0){
                    WeaponInit.actualCapacity-=1;
                 }
                  if(getFree){
                      freeObject=HaveBuyedCards.singleton.getFreeWeaponSlot();
                      if(freeObject!=null){
                      setCardSlot(freeObject);
                   
                      position=freeObject.transform .position ;}
                  }
           }
             if(freeObject!=null||!getFree){
                   this.GetComponent<RectTransform>().position=position+new Vector3(0,1.0f,0);
             }             
        }
        private void setCardSlot(GameObject cardSlot){
            cardSlot.transform .GetComponent<CardSlot>().setSlotStatus(true);
                      thisCard.setCardsSlot(cardSlot);
        }
        #endregion
        public void OnPointerDown(PointerEventData eventData)
        {
            if(!CEButtons .singleton .getEnlargeStatus()&&eventData.button == PointerEventData.InputButton.Right){//生成放大的图片
                    shelterPanel.SetActive(true);
                    GameObject clone=Instantiate(this.gameObject,Vector3.zero,this.transform.rotation ,shelterPanel.transform );
                    clone.GetComponent<RectTransform>().anchoredPosition3D=Vector3.zero;
                    clone.layer=11;// 11 EnlargeCard
                    clone.AddComponent<EnlargeDrag>();
                    clone.GetComponent<RectTransform>().sizeDelta*=2.5f;//2.5倍放大是预设值
                    CEButtons.singleton.setEnlargeStatus(true);

              
            }
          else if(eventData.button == PointerEventData.InputButton.Left){
            
             if(Time.time-doubleClick<=0.35f){
                  if(!thisCard.getCardStatus()){ //卡在生成的位置上
                  if(freePosition()){
                 thisCard.SetCardStatus(true);
              reduceCapacity(Vector3.zero,true);
              hasdoubleClick=true;
                  }
                  }
                 else{//卡在卡槽上
                 Debug.Log("comein");
                     hasdoubleClick=true;
                     plusCapacity();
                 }

             }
            
             else{
                 hasdoubleClick=false;
               doubleClick=Time.time ;
             }
            Debug.Log("点击了");
        }
        }
   
        public void OnDrag(PointerEventData eventData)
        {
          this.transform.parent=canvas.transform;
            Debug.Log("在拖");
            this.GetComponent<RectTransform>().position += new Vector3(eventData.delta.x*0.15f,eventData.delta.y*0.15f, 0);
            
        }
       
      public void OnPointerUp(PointerEventData eventData)
       {
         cameraRay=Camera.main.ScreenPointToRay(Input .mousePosition);
     
    
        if(!hasdoubleClick){//双击状态不用检测 鼠标抬起时候状态
        if(Physics.Raycast(cameraRay,out hit,1000,cardSortLayer) ){
                Debug.Log(hit.transform .name);
            if(hit.transform.GetComponent<CardSlot>().getSlotType()==cT&&!hit.transform.GetComponent<CardSlot>().getSlotStatus()){
                Debug.Log(hit.transform .name);
                hit.transform .GetComponent<CardSlot>().setSlotStatus(true);
                  setCardSlot(hit.transform.gameObject);
             reduceCapacity(hit.transform .position,false);
              }
           }
            else{//原先在卡槽里 被移出之后
            if(thisCard.getCardStatus()){
            plusCapacity();}
            this.transform .SetParent(father.transform);
                //  this.transform .parent=father .transform;
         
          thisCard.SetCardStatus(false);
          }
        }
 if(cT==cardsType.Refittingcards){
                 RefittingInit.singleton .renewCards();
           }
              else {
                 WeaponInit.singleton.renewCards();
        }

        //(this.GetComponent<EquipmentCards>().getCardStatus()){//确实被移出了   
        //        
           //  }
          //   else{//移出到不对的地方
          
           // }
            //Debug.Log("鼠标抬起");
        }

        public void OnEndDrag(PointerEventData eventData)//一定是在OnDrag被触发以后 才有用 鼠标抬起只要点击鼠标 再抬起来 都会触发；
        {
                
            //Debug.Log("拖拽结束");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {

            //Debug.Log("鼠标进来");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //Debug.Log("鼠标移除");
        }
       
            


   
    }

}
