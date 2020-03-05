using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace wyc
{
    public class Cards
    {
    }

    public class Player//玩家的数据
    {
        public List<Thug> thugs;//拥有的打手们
    }

    [System.Serializable]
    public class ThugSource
    {
        public int orderNumber;//序号
        public string name;
        public int health;
        public int strength;
        public int wealth;
        public int drive;
        public int sex;
        public List<int> characteristics;
        public List<int> weapons;
        public List<int> equips;
        public ThugSource()
        {
            name = "thugtext";
            //health = UnityEngine.Random.Range(8, 10);
            //strength = UnityEngine.Random.Range(1, 3);
            //wealth = UnityEngine.Random.Range(20, 40);
            //drive = UnityEngine.Random.Range(4, 6);
            characteristics = new List<int>();
            weapons = new List<int>();
            equips = new List<int>();
        }
        public ThugSource(Thug thug, int orderNumber)
        {
            this.orderNumber = orderNumber;
            name = thug.name;
            health = thug.health_Souce;
            strength = thug.strength_Souce;
            wealth = thug.wealth_Souce;
            drive = thug.drive_Souce;
            characteristics = new List<int>();
            weapons = new List<int>();
            equips = new List<int>();
            //导入数据
        }
        public void Print()
        {
            Debug.Log($"{name}:health {health}、strength {strength}、drive {drive}、wealth ${wealth},000");
        }
        public static string RandomName()
        {
            string name = "";
            switch (UnityEngine.Random.Range(1, 3 + 1))
            {
                case 1: name = "Tom"; break;
                case 2: name = "JACK"; break;
                case 3: name = "Jhon"; break;
            }
            return name;

        }
    }
    public class Thug
    {
        public ThugSource source;
        public string name;
        public int health;
        public int health_Souce;
        public int strength;
        public int strength_Souce;
        public int wealth;
        public int wealth_Souce;
        public int drive;
        public int drive_Souce;
        public List<Characteristic> characteristics;
        public List<Weapon> weapons;    
        public List<Equip> equips;

        //是否npc控制 是否执行小游戏
        //可执行操作：加减速的调整控制-1,0,1 攻击目标改变 攻击目标选择 自身卡牌启用

        public Thug()
        {
        }
        public Thug(ThugSource source, List<WeaponSource> weaponsData, List<EquipSource> equipsData, List<CharacteristicSource> characteristicsData)//根据数据创建人物
        {
            this.source = source;
            name = source.name;
            health = source.health;
            health_Souce = source.health;
            strength = source.strength;
            strength_Souce = source.strength;
            wealth = source.wealth;
            wealth_Souce = source.wealth;
            drive = source.drive;
            drive_Souce = source.drive;
            characteristics = new List<Characteristic>();
            weapons = new List<Weapon>();
            equips = new List<Equip>();
            foreach (int temp in source.weapons)
            {
                foreach (WeaponSource weapon in weaponsData)
                {
                    if (weapon.orderNumber == temp)
                    {
                        weapons.Add(new Weapon(weapon, characteristicsData));
                    }
                }
            }
            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].playerOrderNumber = i;
            }

            foreach (int temp in source.equips)
            {
                foreach (EquipSource equip in equipsData)
                {
                    if (equip.orderNumber == temp)
                    {
                        equips.Add(new Equip(equip, characteristicsData));
                    }
                }
            }
            for (int i = 0; i < equips.Count; i++)
            {
                equips[i].playerOrderNumber = i;
            }


            foreach (int temp in source.characteristics)
            {
                foreach (CharacteristicSource characteristic in characteristicsData)
                {
                    if (characteristic.orderNumber == temp)
                    {
                        characteristics.Add(new Characteristic(characteristic));
                    }
                }
            }
            for (int i = 0; i < characteristics.Count; i++)
            {
                characteristics[i].playerOrderNumber = i;
            }

        }
        public ThugSource FoldToSouce(int orderNumber)
        {
            ThugSource source = new ThugSource(this, orderNumber);
            return source;
        }//打包回数据

        public void Print()
        {
            Debug.Log($"{name}:health {health}、strength {strength}、drive {drive}、wealth ${wealth},000");
            foreach (Weapon weapon in weapons)
            {
                Debug.Log($"{name}:{weapon.name}");
            }
        }
    }
    [System.Serializable]
    public class LandSource//地形
    {
        public int orderNumber;//序号
        public string name;
        public int complex;//地形复杂度
        public int length;//长度
                          //public List<int> characteristics;车技项

        public void Print()
        {
            Debug.Log($"{name}:{complex}/{length}");
        }
    }
    public class Land//地形
    {
        LandSource source;
        //public int orderNumber;//序号
        public int landid;
        public List<Land> nextland;//改变成可选
        public int position;//位置
        public string name;
        public int complex;//地形复杂度
        public int length;//长度
                          //public List<int> characteristics;车技项

        public Land(LandSource lSource)
        {
            source = lSource;
            name = lSource.name;
            complex = lSource.complex;
            length = lSource.length;
            nextland = new List<Land>();
            landid = 0;
        }


        public void Print()
        {
            Debug.Log($"{name}:{complex}/{length}");
        }
    }
    [System.Serializable]
    public class EquipSource//改装
    {
        public int orderNumber;//序号
        public string name;//名称
        public int equipType;//改装类型
        public int price;//价格
        public int effectLevel;//效果等级
        public List<int> characteristics;
        public EquipSource()
        {
            characteristics = new List<int>();
        }
        public string GetCardType()
        {
            string temp = "";
            switch ((EQUIP_TYPE)equipType)
            {
                case EQUIP_TYPE.TIRE:
                    temp = "轮胎";
                    break;
                case EQUIP_TYPE.ENGINE:
                    temp = "引擎";
                    break;
                case EQUIP_TYPE.IGNITION:
                    temp = "点火";
                    break;
                case EQUIP_TYPE.CONSUME:
                    temp = "消耗";
                    break;
                case EQUIP_TYPE.BRAKE:
                    temp = "刹车";
                    break;
                case EQUIP_TYPE.CHASSIS:
                    temp = "悬挂";
                    break;
            }
            return temp;
        }

    }
    public class Equip//改装
    {
        EquipSource source;
        public int orderNumber;//序号,系统序号
        public int playerOrderNumber;//玩家内排序序号
        public string name;//名称
        public int equipType;//改装类型
        public int price;//价格
        public int effectLevel;//效果等级
        public List<Characteristic> characteristics;
        public Equip(EquipSource eSource, List<CharacteristicSource> characteristicsData)
        {
            characteristics = new List<Characteristic>();

            source = eSource;
            name = eSource.name;
            equipType = eSource.equipType;
            effectLevel = eSource.effectLevel;
            price = eSource.price;
            characteristics = new List<Characteristic>();
            playerOrderNumber = -1;
            foreach (int temp in eSource.characteristics)
            {
                foreach (CharacteristicSource characteristicSource in characteristicsData)
                {
                    if (characteristicSource.orderNumber == temp)
                    {
                        characteristics.Add(new Characteristic(characteristicSource));
                    }
                }
            }
        }
        public string GetCardType()
        {
            return source.GetCardType();
        }

    }
    [System.Serializable]
    public class WeaponSource//武器
    {
        public int orderNumber;//序号
        public string name;
        public int weaponType;//武器类型，远程/近战
        public int damage;//伤害
        public int wanted;//通缉值
        public int price;//价格
        public List<int> characteristics;
        public WeaponSource()
        {
            characteristics = new List<int>();
        }
        public string GetCardType()
        {
            string temp="";
            switch ((WEAPON_TYPE)weaponType)
            {
                case WEAPON_TYPE.LONG_RANGE:
                    temp = "远程";
                    break;
                case WEAPON_TYPE.MELEE:
                    temp = "近战";
                    break;
            }
            return temp;
        }
    }
    public class Weapon//武器
    {
        WeaponSource source;
        public int orderNumber;//序号
        public int playerOrderNumber;//玩家内排序序号
        public string name;
        public int weaponType;//武器类型，远程/近战
        public int damage;//伤害
        public int wanted;//通缉值
        public int price;//价格
        public List<Characteristic> characteristics;
        public Weapon(WeaponSource wSource, List<CharacteristicSource> characteristicsData)
        {
            source = wSource;
            wanted = wSource.wanted;
            name = wSource.name;
            weaponType = wSource.weaponType;
            damage = wSource.damage;
            characteristics = new List<Characteristic>();
            price = wSource.price;  
            playerOrderNumber = -1;
            foreach (int temp in wSource.characteristics)
            {
                foreach (CharacteristicSource characteristicSource in characteristicsData)
                {
                    if (characteristicSource.orderNumber == temp)
                    {
                        characteristics.Add(new Characteristic(characteristicSource));
                    }
                }
            }
        }
        public string GetCardType()
        {
            return source.GetCardType();
        }
    }
    [System.Serializable]
    public class CharacteristicSource
    {
        public int orderNumber;//序号
        public string name;//名称
        public int characteristicType;//特性类别，代表触发时间点。复杂类型的推荐用‘复杂’类型
        public string Describe;//描述
        public string[] CHtemp;//存储参数的数组（8个：冷却时间、使用次数、叠加层数、经验计数+4个辅助暂存区）
        public List<CHEffect> CHEffects;
        public CharacteristicSource()
        {
            CHtemp = new string[8];
            for (int i = 0; i < 8; i++)
            {
                this.CHtemp[i] = "";
            }
            CHEffects = new List<CHEffect>();
        }
        public CharacteristicSource(int orderNumber, string name, int characteristicType, string[] CHtemp, List<CHEffect> CHEffects)
        {
            this.CHtemp = new string[8];
            for (int i = 0; i < CHtemp.Length; i++)
            {
                this.CHtemp[i] = CHtemp[i];
            }
            this.CHEffects = CHEffects;
            this.orderNumber = orderNumber;
            this.characteristicType = characteristicType;
        }
        public string GetName()
        {
            return name;
        }
        public string GetMaxCooldown()
        {
            return CHtemp[0];
        }
        public string GetMaxUse()
        {
            return CHtemp[1];
        }
        public string GetMaxOverlying()
        {
            return CHtemp[2];
        }
        public string GetMaxExperiance()
        {
            return CHtemp[3];
        }
        public string PrintDescribe()
        {

            return "";
        }


    }
    public class Characteristic
    {
        CharacteristicSource source;
        public int playerOrderNumber;//序号
        public string name;//名称
        public int characteristicType;//特性类别，代表触发时间点。复杂类型的推荐用‘复杂’类型
        public string[] CHtemp;//存储参数的数组（8个：冷却时间、使用次数、叠加层数、经验计数+4个辅助暂存区）
        public List<CHEffect> CHEffects;
        public Characteristic(CharacteristicSource cSource)
        {
            CHtemp = new string[8];
            source = cSource;
            name = cSource.name;
            characteristicType = cSource.characteristicType;
            CHEffects = new List<CHEffect>();

            for (int i = 0; i < cSource.CHtemp.Length; i++)
            {
                CHtemp[i] = cSource.CHtemp[i];
            }
            foreach (CHEffect temp in cSource.CHEffects)
            {
                CHEffects.Add(temp.Clone());
            }
        }
    }
    
    [System.Serializable]
    public class CHEffect
    {
        public int effectType;//效果数组，负为检测项，正为施加效果
        public string effect;//效果

        public CHEffect()
        {
        }
        public CHEffect(EFFECT_TYPE et, string effect)
        {
            effectType = (int)et;
            this.effect = effect;
        }
        public CHEffect Clone()
        {
            CHEffect temp = new CHEffect();
            temp.effect = effect;
            temp.effectType = effectType;
            return temp;
        }
    }

    public enum PlayMiniGameType
    {
        Drive=1,
        Shoot=2,
        Combat=4,
    }

    public class RuntimePlayer
    {//实时后的玩家
        public int runtimePlayerID;
        public Thug player;//玩家实际数据
        public bool isNPC;//是否NPC化
        public int playerStatus;//玩家状态：在线、离线、暂停|死亡·在线、死亡·离线
                                //public List<RunTimePlayer> livePlayers;//玩家列表
        public List<RuntimePlayer> canAttackRange;//可攻击玩家列表
        public List<RuntimePlayer> canAttackMelee;//可攻击玩家列表

        public int playMINIgame;//是否玩小游戏
        public int gameStatus;//小游戏当前的状态

        Weapon usingWeapon;//当前使用的武器

        public Land land;//所位于的地图是哪张
        public int distance;//当前的位移距离
        public int speed;//当前的速度
        public int changeSpeed;//-1减速 0不变 1加速
        public RuntimePlayer target;//攻击目标
        public int wanted;//通缉值
        public int accelerateSpeed;//加速数值
        public int slowSpeed;//减速数值

        public int healthBeforeCombat;//每回合战斗前的生命值

        public Land oldLand;//上回合的地形卡
        public int oldDistance;//上回合的位置
        public List<Land> route;//行驶的路径过程

        public List<Characteristic> status;//状态组

        public List<RuntimePlayer> killedPeople;//杀过的人
        public List<RuntimePlayer> attackedPeople;//攻击过的人
        public List<RuntimePlayer> realEnemy;//攻击过自己的人
        public RuntimePlayer killer;//杀死自己者的源头：地形杀算自杀

        public bool complete;//完成比赛
        public float completeTime;//具体完成时间
        public int down;//翻车
        public bool confirm;//是否确认结束
        public List<MotionMessage> motionMessagesPool;//需要发送的操作信息池，每回合尝试发送一遍，直至收到回传确认信息。队列形式
        public RuntimePlayer(Thug thug, bool isNPC, Land land)
        {
            motionMessagesPool = new List<MotionMessage>();
            canAttackRange = new List<RuntimePlayer>();
            canAttackMelee = new List<RuntimePlayer>();
            killedPeople = new List<RuntimePlayer>();
            attackedPeople = new List<RuntimePlayer>();
            route = new List<Land>();
            player = thug;
            this.land = land;//放置于目标地点
            oldLand = land;
            speed = 100;
            oldDistance = 0;
            distance = 0;
            accelerateSpeed = 50;
            slowSpeed = 50;
            this.isNPC = isNPC;
            playMINIgame = 0;
            runtimePlayerID = 0;
            completeTime = 0;
            if (player.weapons.Count >= 1)
            {
                usingWeapon = player.weapons[0];
            }
        }

        public void SetUsingWeapon(Weapon w)
        {
            usingWeapon = w;
        }
        public void SetUsingWeapon(int weaponId)
        {
            foreach(Weapon w in player.weapons)
            {
                if (w.playerOrderNumber == weaponId)
                {
                    SetUsingWeapon(w);
                }
            }
        }

        public Weapon GetUsingWeapon()
        {
            return usingWeapon;
        }
        public string GetUsingWeaponName()
        {
            if (usingWeapon == null)
            {
                return "";
            }
            else
            {
                return usingWeapon.name;
            }
        }

        public bool CheckSelfActive()
        {
            return down == 0 && !complete && (playerStatus != (int)PLAYER_STATUS_TYPE.OUT_GAME && playerStatus != (int)PLAYER_STATUS_TYPE.DEAD && playerStatus != (int)PLAYER_STATUS_TYPE.DYING);
        }

        public bool isPlayerPlayMINIGame(PlayMiniGameType gameType)
        {
            if (((int)gameType & playMINIgame) > 0)
            {
                return true;
            }
            return false;
        }

        public void setPlayerPlayMINIGame(PlayMiniGameType gameType,bool flag)
        {
            if (flag)
            {
                playMINIgame = playMINIgame | (int)gameType;
            }
            else
            {
                
                playMINIgame = playMINIgame & ~(int)gameType;
            }
        }

        //*需要修改
        public void AIcontrol(int state, int AIlevel)//根据当前回合进行判断行为
        {
            //Debug.Log("Thinking");
            if (CheckSelfActive())
            {
                switch (state)
                {
                    case (int)STATE.ACCOUNT_STATE:
                        //默认确认
                        break;
                    case (int)STATE.ACCELERATE_STATE:
                        //判断速度与难度
                        AI_CONTROL_SPEED();
                        break;
                    case (int)STATE.MOTION_STATE:
                        break;
                    case (int)STATE.PREMEDITATE_STATE:
                        //确定攻击目标，从当前持用武器开始
                        AI_CONTROL_TARGET();
                        break;
                    case (int)STATE.COMBAT_STATE:
                        break;
                    case (int)STATE.END_STATE:
                        break;



                    case (int)STATE.READY_STATE:
                        AI_CONTROL_SPEED();
                        break;
                    case (int)STATE.PRE_STATE:
                        AI_CONTROL_TARGET();
                        break;
                    case (int)STATE.AFTER_STATE:
                        break;
                    case (int)STATE.NEXT_STATE:
                        break;
                }
                SetMotionMessage(MOTION_CODE.COMFIRM, null);
            }
            else if (down > 0)
            {
                SetMotionMessage(MOTION_CODE.COMFIRM, null);
            }
        }
        private void AI_CONTROL_SPEED()
        {
            if (speed >= 200 && player.drive <= (land.complex + (speed / 100) + (wanted / 3)) - 3)
            {
                List<string> temp = new List<string>();
                temp.Add("-1");
                SetMotionMessage(MOTION_CODE.ACCELERATE_CHANGE, temp);
            }
            else
            {
                List<string> temp = new List<string>();
                temp.Add("1");
                SetMotionMessage(MOTION_CODE.ACCELERATE_CHANGE, temp);
            }
        }

        private void AI_CONTROL_TARGET()
        {
            if (usingWeapon.weaponType == (int)WEAPON_TYPE.MELEE)
            {
                if (canAttackMelee.Count > 0)
                {
                    target = canAttackMelee[Random.Range(0, canAttackMelee.Count - 1)];
                    List<string> temp = new List<string>();
                    temp.Add(target.runtimePlayerID.ToString());
                    SetMotionMessage(MOTION_CODE.ATTACK_TARGET_CHANGE, temp);
                }
            }
            else if (usingWeapon.weaponType == (int)WEAPON_TYPE.LONG_RANGE)
            {
                if (canAttackRange.Count > 0)
                {
                    target = canAttackRange[Random.Range(0, canAttackMelee.Count - 1)];
                    List<string> temp = new List<string>();
                    temp.Add(target.runtimePlayerID.ToString());
                    SetMotionMessage(MOTION_CODE.ATTACK_TARGET_CHANGE, temp);
                }
            }
        }

            public void SetMotionMessage(MOTION_CODE motionCode, List<string> motionParams)
        {//定义自己的
         //检查是否重复的，重复的置换。包括comfirm，通过MOTION_CODE。<=Comfirm为唯一，需要被置换。>则只替换重复的
         
            //检查角色状态，忽略无效操作
            switch (motionCode)
            {
                case MOTION_CODE.COMFIRM:
                    if (confirm) { return; }
                    break;
            }

            if((int)motionCode <= (int)MOTION_CODE.COMFIRM)
            {
                //检索正确性
            }

            motionMessagesPool.Add(new MotionMessage(this, (int)motionCode, motionParams));
        }//

        public int GetOrderNum()
        {
            return player.source.orderNumber;
        }
        public bool CheckOrderNum(int orderNum)
        {
            return orderNum == player.source.orderNumber;
        }
        public bool isUnable()
        {
            return playerStatus == (int)PLAYER_STATUS_TYPE.OUT_GAME || playerStatus == (int)PLAYER_STATUS_TYPE.DEAD;
        }
    }

}
