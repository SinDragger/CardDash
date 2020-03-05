using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace xyy{
public class EnlargeDrag : MonoBehaviour
{
    Ray cameraRay;
    RaycastHit hit;
    LayerMask enlargeLayer;
    public GameObject shelterPanel;
     Vector2 mySizeDelata;
     Vector2 maxSizeDelata;
    Vector2 minSizeDelata;
   void Awake(){
       shelterPanel=CEButtons.singleton .ShelterPanel;
       Destroy(GetComponent<Drag>());
        Destroy(GetComponent<EquipmentCards>());
        enlargeLayer=1<<11;
        mySizeDelata=this.GetComponent<RectTransform>().sizeDelta;
        maxSizeDelata=new Vector2((float)Screen.height /mySizeDelata .y*mySizeDelata.x,Screen .height );//宽高比不变
        minSizeDelata=0.8f*mySizeDelata;
   }
   void Update(){
       Vector2 tempSizeDelata=this.GetComponent<RectTransform>().sizeDelta;
       Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
       if(Input.GetAxis("Mouse ScrollWheel")>0 ){
           tempSizeDelata+=new Vector2 (maxSizeDelata.x-tempSizeDelata .x,maxSizeDelata .y-tempSizeDelata.y)*(1-(tempSizeDelata.x)/maxSizeDelata.x)/5.0f;
          if(tempSizeDelata.x>=maxSizeDelata.x){
              tempSizeDelata=maxSizeDelata;
          }
       }else if(Input.GetAxis("Mouse ScrollWheel")<0){
            tempSizeDelata-=new Vector2 (tempSizeDelata.x-minSizeDelata .x,tempSizeDelata.y-minSizeDelata .y)*(1-minSizeDelata .x/tempSizeDelata .x)/5.0f;
           if(tempSizeDelata.x<=minSizeDelata.x){
               tempSizeDelata=minSizeDelata;
           }
       }
       this.GetComponent<RectTransform>().sizeDelta=tempSizeDelata;
       if(Input.GetMouseButtonDown(0)){//按下的时候修改碰撞体大小 点到图片外面 退出 放大图片 
           mySizeDelata=this.GetComponent<RectTransform>().sizeDelta;
           this.GetComponent<BoxCollider>().size=new Vector3(mySizeDelata.x,mySizeDelata .y,1);
        
        cameraRay=Camera.main.ScreenPointToRay(Input .mousePosition);
       if(!Physics.Raycast(cameraRay,out hit,1000,enlargeLayer)){//this.gameObject .layer
                 
               MyDestroy();

              }
       }
   }
   void MyDestroy(){
        CEButtons.singleton .setEnlargeStatus(false);
       
        StartCoroutine(destroyWait());
   }
   IEnumerator destroyWait(){
    
       yield return new WaitForSeconds(0.02f);
       Destroy(this.gameObject);
        shelterPanel.SetActive(false);
   }
}
}
