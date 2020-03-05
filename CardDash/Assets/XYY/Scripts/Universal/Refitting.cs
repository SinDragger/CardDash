using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace xyy{
public class Refitting 
{
   public int type{get;set;}
    public int money {get;set;}
   public  int effectllevel{get;set;}
   public string speciality{get;set;}

        public int Id;
        public void setID(int id)
        {
            this.Id = id;
        }

}
public class Tire:Refitting{
     public Tire(int m, int ef,string sp,int t=0){//轮胎 刹车 引擎 点火系统 底盘悬挂 供油油箱 加速
         money=m;
         effectllevel=ef;
         speciality=sp;
         type=t;
    }
}
public class Braking:Refitting{
    public Braking(int m, int ef,string sp,int t=1){//轮胎 刹车 引擎 点火系统 底盘悬挂 供油油箱 加速
         money=m;
         effectllevel=ef;
         speciality=sp;
         type=t;
    }
}
public class Engine:Refitting{
    public Engine(int m, int ef,string sp,int t=2){//轮胎 刹车 引擎 点火系统 底盘悬挂 供油油箱 加速
         money=m;
         effectllevel=ef;
         speciality=sp;
         type=t;
    }
}
public class FireSysytem:Refitting{
    public FireSysytem(int m, int ef,string sp,int t=3){//轮胎 刹车 引擎 点火系统 底盘悬挂 供油油箱 加速
         money=m;
         effectllevel=ef;
         speciality=sp;
         type=t;
    }
}
public class Suspension:Refitting{
    public Suspension(int m, int ef,string sp,int t=4){//轮胎 刹车 引擎 点火系统 底盘悬挂 供油油箱 加速
         money=m;
         effectllevel=ef;
         speciality=sp;
         type=t;
    }
}
public class OilTank:Refitting{
    public OilTank(int m, int ef,string sp,int t=5){//轮胎 刹车 引擎 点火系统 底盘悬挂 供油油箱 加速
         money=m;
         effectllevel=ef;
         speciality=sp;
         type=t;
    }
}
public class SpeedUp:Refitting{
   public SpeedUp(int m, int ef,string sp,int t=6){//轮胎 刹车 引擎 点火系统 底盘悬挂 供油油箱 加速
         money=m;
         effectllevel=ef;
         speciality=sp;
         type=t;
    }
}
}

