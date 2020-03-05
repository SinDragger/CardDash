using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace xyy{
public class Character 
{
   public int id{get;set;}
   public string characterName{get;set;}
   public int health{get;set;}
   public int money{get;set;}
   public int damage{get;set;}
   public int driveAbility{get;set;}
   public string specialAbility{get;set;}
   public List<Weapon> weapon;
   public List<Refitting> refitting;
   public Character(int id,string characterName,int health,int money,int damage,int driveAbility,string specialAbility){
       this.id=id;
       this.characterName=characterName;
       this.health=health;
       this.money=money ;
       this.damage=damage;
       this.driveAbility=driveAbility;
       this.specialAbility=specialAbility;
       weapon=new List<Weapon>();
       refitting=new List<Refitting>();
   }
 }
}
