using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace xyy{
public class Weapon 
{
   public int type{get;set;}//0 近战 1远程
   public int price{get;set;}
   public int damage{get;set;}
   public int threatening{get;set;}
   public string  speciality{get;set;}

        public int Id;
        public void setID(int id)
        {
            this.Id = id;
        }
    }
public class CloseCombt: Weapon{
   public CloseCombt(int p, int d, int th,string sp,int t=0){
         price=p;
         damage=d;
         threatening=th;
         speciality=sp;
         type=t;
    }

}
public class RangedAttack:Weapon{
  public  RangedAttack(int p, int d, int th,string sp,int t=1){
         price=p;
         damage=d;
         threatening=th;
         speciality=sp;
         type=t;
    }
}
}
