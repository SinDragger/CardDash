using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace xyy{
public class CharacterInit : MonoBehaviour
{

    public Sprite [] CharacterPictures;
    private Character [] characters={new Character(1,"谢尔盖",8,20000,3,4,null),
                                        new Character(2,"李东辉",6,40000,1,7,null),
                                        new Character(3,"卡洛尼",5,20000,1,5,null),
                                        new Character(4,"亚伦",8,50000,2,4,null),
                                        new Character(5,"布伦特",8,30000,1,6,null),
                                        new Character(6,"奥马利安",8,25000,0,6,null),
                                        new Character(7,"芙拉维娅",8,60000,1,5,null),
                                        new Character(8,"杜林",8,30000,1,5,null)};
   private Character  nowCharacter;
   private int nowCharacterId;
   public Vector3 c_StartPosition;
   private Vector3 anchoredChange;//
   public GameObject buttonExample;// 最上面的人物选择
   public Vector3 imagePosition;//角色卡在的位置
   public GameObject ImageExample;//中间靠左 大的人物图片显示
   public float c_PositionDistance;// 人物选择之间的距离
   public GameObject leftChange;//左切换按钮
   public GameObject rightChange;//右切换按钮
   public Vector3 leftChangePosition;//左按钮位置 右按钮位置 和人物卡相对称；
   private GameObject characterCanvas;// Canvas的选择
   void Awake(){
       nowCharacterId=1;
       nowCharacter=characters[nowCharacterId];
   }
   public void SceneSetUp(){//初始化场景
       GameManager.instance.GameState=2;//正在生成
         characterCanvas=GameObject.FindGameObjectWithTag("Canvas");
       for(int i=0;i<5;i++){
           //c_StartPosition+new Vector3(i*c_PositionDistance,0,0)
       GameObject clone=Instantiate(buttonExample,Vector3.zero,buttonExample .transform .rotation,characterCanvas.transform);
       clone .GetComponent<RectTransform>().anchoredPosition3D=c_StartPosition+new Vector3(i*c_PositionDistance,0,0);
       clone.GetComponent<Button>().image.sprite=CharacterPictures[i];
       }
       ImageExample= Instantiate (ImageExample,imagePosition,ImageExample.transform .rotation,characterCanvas.transform );
       ImageExample .GetComponent<RectTransform>().anchoredPosition3D=imagePosition;
       Debug.Log(nowCharacterId);
       ImageExample.GetComponent<Image>().sprite=CharacterPictures[nowCharacterId-1];
       
   }
}
}
