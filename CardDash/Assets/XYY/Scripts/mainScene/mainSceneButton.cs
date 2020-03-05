using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace xyy{
public class mainSceneButton : MonoBehaviour
{
  public static mainSceneButton singleton =null;
  Canvas canvas;
  [SerializeField]
   GameObject MenuPrefab;//带遮挡面版 就是 shelterpanel

    void Awake(){

      if(singleton==null){//单例模式
           singleton=this;}
        else  if(singleton !=this){
            Destroy(gameObject);
        };
      
  }
  #region MenuButton
     public void showMenu(){
       GameObject clone=Instantiate(MenuPrefab,Vector3.zero ,this.transform .rotation ,canvas .transform );
       clone.GetComponent<RectTransform>().anchoredPosition3D=Vector3.zero;
     }
     public void defeat(){
       Debug.Log("You lose");
     }
  #endregion

}
}