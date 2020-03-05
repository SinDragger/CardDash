using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance=null;
   // public xyy.CharacterInit characterInitScript;
   public int mainCharacterId;
   #region data
    public xyy.Character [] characters={new xyy.Character(1,"谢尔盖",8,20000,3,4,"null"),
                                        new xyy.Character(2,"李东辉",6,40000,1,7,"null"),
                                        new xyy.Character(3,"卡洛尼",5,20000,1,5,"null"),
                                        new xyy.Character(4,"亚伦",8,50000,2,4,"null"),
                                        new xyy.Character(5,"布伦特",8,30000,1,6,"null"),
                                        new xyy.Character(6,"奥马利安",8,25000,0,6,"null"),
                                        new xyy.Character(7,"芙拉维娅",8,60000,1,5,"null"),
                                        new xyy.Character(8,"杜林",8,30000,1,5,"null")};//角色数据
    public Sprite[] characterPictures;
    public xyy.Refitting[] refittings={new xyy.Tire(10000,1,"null")  ,
                                        new xyy.Braking(10000,1,"null") , 
                                        new xyy.Engine(20000,1,"null") ,
                                        new xyy.FireSysytem(10000,1,"null") ,
                                        new xyy.Suspension(10000,1,"null") ,
                                        new xyy.OilTank(10000,1,"null"),
                                        new xyy.SpeedUp(5000,1,"null")  };
    public xyy.Weapon[] weapons={ new xyy.CloseCombt(0,1,1,"null"),
                                  new xyy.CloseCombt(1000,1,1,"null"),
                                  new xyy.CloseCombt(1000,2,2,"null"),
                                  new xyy.CloseCombt(5000,1,1,"null"),
                                  new xyy.CloseCombt(2000,3,2,"null"),
                                  new xyy.CloseCombt(2000,2,2,"null"),
                                  new xyy.RangedAttack(1000,1,1,"null"),
                                  new xyy.RangedAttack(5000,2,3,"null"),
                                  new xyy.RangedAttack(10000,3,3,"null"),
                                  new xyy.RangedAttack(12000,3,4,"null"),
                                  new xyy.RangedAttack(20000,4,4,"null")};
    #endregion
    public  int GameState=0;//0为start
    public xyy.Character nowCharcter;
    //  public xyy.Refitting getRefittingInfo(int index){
    //        return refittings[index];
    // }
    // public xyy.Weapon getWeaponInfo(int index){
    //   return weapons[index];
    // }
  void Awake(){
    System.Random rd=new System.Random(System .DateTime.Now.Millisecond);//随机化种子
    mainCharacterId=rd.Next(0,8);
 nowCharcter=characters[mainCharacterId];
   GameState=0;
      if(instance==null){//单例模式
           instance=this;}
        else  if(instance!=this){
            Destroy(gameObject);
        };
      DontDestroyOnLoad(gameObject);
  }

}
