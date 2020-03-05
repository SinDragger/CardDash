using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine.SceneManagement;
using UnityEngine;
//using System;
namespace wyc {

    public delegate void GetMessage();


    public class DataController {
        public List<ThugSource> thugSources;
        public List<ThugSource> enemies;
        public List<WeaponSource> weaponSources;
        public List<EquipSource> equipSources;
        public List<LandSource> landSources;
        public List<CharacteristicSource> characteristicSources;



        public static DataController instance;
        public static DataController Instance()
        {

            if (instance == null)
            {
                instance = new DataController();
            }
            return instance;
        }
        public DataController()
        {
            thugSources = new List<ThugSource>();
            enemies = new List<ThugSource>();
            weaponSources = new List<WeaponSource>();
            equipSources = new List<EquipSource>();
            characteristicSources = new List<CharacteristicSource>();
            landSources = new List<LandSource>();
            //LoadAllData();
        }


        public void ComfirmEnemies(int maxCount)
        {
            maxCount = maxCount > enemies.Count ? enemies.Count : maxCount;
            maxCount =enemies.Count - maxCount ;
            for (; maxCount > 0; maxCount--)
            {
                enemies.Remove(enemies[Random.Range(0, enemies.Count)]);
            }
            Debug.Log("敌人生成");
            foreach(var i in enemies)
            {
                Debug.Log(i.name);
            }
        }

        public List<ThugSource> TestChooseAblePlayers(int number)
        {
            List<ThugSource> temp = new List<ThugSource>();
            for(int i = number<thugSources.Count?number: thugSources.Count; i > 0; i--)
            {
                ThugSource target = thugSources[Random.Range(0, thugSources.Count)];
                if (!temp.Contains(target))
                {
                    temp.Add(target);
                }
                else
                {
                    i++;
                }
            }
            return temp;
        }
        public List<ThugSource> TestChooseAbleRandomPlayers(int number)
        {
            List<ThugSource> temp = new List<ThugSource>();
            for (int i = number ; i > 0; i--)
            {
                //ThugSource target = 
            }

            return temp;

        }

        public List<WeaponSource> TestBuyAbleRandomWeapons(int number)
        {

            List<WeaponSource> temp = new List<WeaponSource>();

            for (int i = number; i > 0; i--)
            {
                WeaponSource target = weaponSources[Random.Range(0, weaponSources.Count)];
                temp.Add(target);
            }

            return temp;
        }
        public List<WeaponSource> BuyAbleWeapons()
        {
            List<WeaponSource> temp = new List<WeaponSource>();
            foreach(var wTemp in weaponSources)
            {
                temp.Add(wTemp);
            }

            //for (int i = number; i > 0; i--)
            //{
            //    WeaponSource target = weaponSources[Random.Range(0, weaponSources.Count)];
            //    temp.Add(target);
            //}

            return temp;
        }
        public List<EquipSource> TestBuyAbleRandomEquips(int number)
        {

            List<EquipSource> temp = new List<EquipSource>();
            for (int i = number; i > 0; i--)
            {
                EquipSource target = equipSources[Random.Range(0, equipSources.Count)];
                temp.Add(target);
            }

            return temp;
        }
        public List<EquipSource> BuyAbleRandomEquips()
        {
            List<EquipSource> temp = new List<EquipSource>();
            foreach (var eTemp in equipSources)
            {
                temp.Add(eTemp);
            }
            return temp;
        }



        public void RemoveEnemy(int id)
        {
            foreach(var temp in enemies)
            {
                if (temp.orderNumber == id)
                {
                    enemies.Remove(temp);
                    return;
                }
            }
        }

        public void LoadAllData()//全部读取模式
        {
            string path = Application.dataPath + "/GameData";
            //UIController.instance.RecieveLog($"读取数据路径{path}");
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            FileInfo[] files = directoryInfo.GetFiles("*.json");
            foreach (FileInfo file in files)
            {
                //UIController.instance.RecieveLog($"读取数据{file.Name}");
                StreamReader sr = new StreamReader(file.FullName);

                if (file.Name.Contains("thug"))
                    while (!sr.EndOfStream)
                    {
                        thugSources.Add(JsonUtility.FromJson<ThugSource>(sr.ReadLine()));
                        enemies.Add(thugSources[thugSources.Count-1]);
                    }

                if (file.Name.Contains("weapon"))
                    while (!sr.EndOfStream)
                    {
                        weaponSources.Add(JsonUtility.FromJson<WeaponSource>(sr.ReadLine()));
                    }

                if (file.Name.Contains("equip"))
                    while (!sr.EndOfStream)
                    {
                        //                        Debug.Log(JsonUtility.FromJson<EquipSource>(sr.ReadLine()).name);
                        equipSources.Add(JsonUtility.FromJson<EquipSource>(sr.ReadLine()));
                    }

                if (file.Name.Contains("lands"))
                    while (!sr.EndOfStream)
                    {
                        landSources.Add(JsonUtility.FromJson<LandSource>(sr.ReadLine()));
                    }
                if (file.Name.Contains("characteristics"))
                    while (!sr.EndOfStream)
                    {
                        characteristicSources.Add(JsonUtility.FromJson<CharacteristicSource>(sr.ReadLine()));
                    }
                sr.Close();
            }
            foreach (ThugSource t in thugSources)
            {
                if (t != null)
                    t.Print();
            }
            foreach (CharacteristicSource t in characteristicSources)
            {
                
                //Debug.Log(t.GetName());
                //Debug.Log(CharacteristicsController.PrintCharacteristicDescirbe(t));

            }
        }
        public CharacteristicSource GetCharacteristicSource(int id)
        {
            foreach(var temp in characteristicSources)
            {
                if (temp.orderNumber == id)
                    return temp;
            }
            return null;
        }
        public ThugSource GetThugSourceById(int id)
        {
            foreach (var temp in thugSources) {
                if (temp.orderNumber == id) return temp;
            }
            return null;
        }
        public void SaveData()
        {
            string path = Application.dataPath + "/GameData";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string[] tempStrings;
            tempStrings = new string[thugSources.Count];
            for (int i = 0; i < thugSources.Count; i++)
            {
                tempStrings[i] = JsonUtility.ToJson(thugSources[i]);
            }
            File.WriteAllLines(path + "/thugs.json", tempStrings);

            tempStrings = new string[weaponSources.Count];
            for (int i = 0; i < weaponSources.Count; i++)
            {
                tempStrings[i] = JsonUtility.ToJson(weaponSources[i]);

            }
            File.WriteAllLines(path + "/weapons.json", tempStrings);

            tempStrings = new string[equipSources.Count];
            for (int i = 0; i < equipSources.Count; i++)
            {
                tempStrings[i] = JsonUtility.ToJson(equipSources[i]);
            }
            File.WriteAllLines(path + "/equips.json", tempStrings);

            tempStrings = new string[landSources.Count];
            for (int i = 0; i < landSources.Count; i++)
            {
                tempStrings[i] = JsonUtility.ToJson(landSources[i]);
            }
            File.WriteAllLines(path + "/lands.json", tempStrings);

            tempStrings = new string[characteristicSources.Count];
            for (int i = 0; i < characteristicSources.Count; i++)
            {
                tempStrings[i] = JsonUtility.ToJson(characteristicSources[i]);
            }
            File.WriteAllLines(path + "/characteristics.json", tempStrings);
        }
        public CharacteristicSource GetCharacter( int id)
        {
            foreach(var temp in characteristicSources)
            {
                if (temp.orderNumber == id)
                {
                    return temp;
                }
            }
            return null;
        }
    }

    public interface DataReceiver
    {
        void RenewData();//默认的调用，自动寻找UI_Controller并去获取数据内容
        void RenewData(List<string> temp);//不向UI获取，而由调用者传入 更新的数据
    }
    public enum DataChangeType
    {
        Init,//初始化参数表
        Time,//根据时间变化而变化
        State,//根据阶段变化而变化
        Turn,//根据回合变化而变化
        Land,//根据地形变化而变化
        RealPlayer,//根据玩家数据变化而变化
        Dead,
        GameComplete,
        Log,//日志信息
    }

    public class CharacteristicsController//特性管理者，对局类特性激活RaceController,全局类激活GameController
    {
        public static CharacteristicsController instance;
        public static int thresholdCheck=0;
        public static int thresholdEffect=1;
        public static CharacteristicsController Instance()
        {
            if (instance == null)
            {
                instance = new CharacteristicsController();
            }
            return instance;
        }
        public static string PrintCharacteristicDescirbe(CharacteristicSource source)
        {
            string temp = "";
            string temp2 = "";
            int checkI;//检查用的I
            int tInt = 0;//可被影响的数字
            bool isCheck=false;//在检索
            bool newStart=true;
            for(int i=0;i<source.CHEffects.Count;i++)
            {
                CHEffect che = source.CHEffects[i];
                if (!newStart)
                {
                    if (che.effectType < thresholdCheck)
                    {
                        if (!isCheck)
                        {
                            temp += ',';
                        }
                        else
                        {
                            temp += '且';
                        }
                        isCheck = true;
                    }
                    else if (che.effectType > thresholdEffect)
                    {
                        if (!isCheck)
                        {
                            temp += '并';
                        }
                        isCheck = false;
                    }

                }
                newStart = false;//认为判断从开始出发
                switch ((EFFECT_TYPE)che.effectType)
                {
                    case EFFECT_TYPE.EFFECT_BUYING:
                        tInt += int.Parse(che.effect);
                        if (tInt == -99)
                        {
                            temp += $"无法购买武器与改装";
                        }
                        else if(tInt == 99)
                        {
                            temp += $"无视武器改装购买数量限制";
                        }
                        else
                        {
                            temp2 = tInt < 0 ? "-" : "+";
                            temp += $"武器改装购买{temp2}{tInt}次";
                        }
                        break;
                    case EFFECT_TYPE.NONE:
                        temp += "。";
                        newStart = true;//认为判断回归开始
                        break;
                    case EFFECT_TYPE.CHECK_SELF_HEALTH:
                        temp += "如果生命值";
                        temp += printCheckValue(che.effect);
                        break;
                }
            }
            return temp;
        }

        static string printCheckValue(string temp)
        {
            string temp2="";
            char c;
            bool isB=false;
            c = temp[0];
            if (c == '0')
            {
                temp2 += "不为零";
                return temp2;
            }
            else if (c == '+')
            {
                temp2 += "大于";
            }
            else if (c == '-')
            {
                temp2 += "小于";
            }
            if (temp[1] == '%')
            {
                isB = true;
            }
            if (isB)
            {
                temp2+=temp.Substring(2)+"%";
            }
            else
            {
                temp2 += temp.Substring(1);

            }
            return temp2;
        }





    }

    public class InputController//输入管理者
    {
        public static InputController instance;

        public static KeySet[] DefaultSetting = {
            new KeySet(KeyCode.Return, KeyBoardInputCode.Comfirm, (int)AssisInput.LeftAlt | (int)AssisInput.LeftCtrl),
            new KeySet(KeyCode.RightArrow, KeyBoardInputCode.NextCard, 0),
            new KeySet(KeyCode.LeftArrow, KeyBoardInputCode.PrevCard, 0),
            new KeySet(KeyCode.UpArrow, KeyBoardInputCode.Accelerate, 0),
            new KeySet(KeyCode.DownArrow, KeyBoardInputCode.Slow, 0),

            new KeySet(KeyCode.A, KeyBoardInputCode.TestA, (int)AssisInput.LeftShift),
            new KeySet(KeyCode.B, KeyBoardInputCode.TestB, (int)AssisInput.RightShift),
            //new KeySet(KeyCode., KeyBoardInputCode., ),
        };

        public List<KeySet> normal;//通用设置
        public List<KeySet> game;//主游戏设置
        public List<KeySet> shoot;//射击设置
        public List<KeySet> drive;//驾驶设置
        public List<KeySet> melee;//近战设置

        public static InputController Instance()
        {
            if (instance == null)
            {
                instance = new InputController();
            }
            return instance;
        }

        InputController()
        {
            normal = new List<KeySet>();
            game = new List<KeySet>();
            shoot = new List<KeySet>();
            drive = new List<KeySet>();
            melee = new List<KeySet>();
            //初始化设定
            SetIntoDefault();
        }

        private void InstallDefaultToInputList(ref int flag,List<KeySet> target)
        {
            for (; flag < DefaultSetting.Length; flag++)
            {
                if (DefaultSetting[flag].keyInputCode == KeyBoardInputCode.None)
                {
                    flag++;
                    break;
                }
                target.Add(DefaultSetting[flag]);
            }
        }

        public bool SetIntoDefault()
        {
            normal.Clear();
            game.Clear();
            shoot.Clear();
            drive.Clear();
            melee.Clear();

            int i = 0;
            InstallDefaultToInputList(ref i, normal);
            InstallDefaultToInputList(ref i, game);
            InstallDefaultToInputList(ref i, shoot);
            InstallDefaultToInputList(ref i, drive);
            InstallDefaultToInputList(ref i, melee);



            return false;
        }
        public void CheckInput(int gameScene)
        {
            List<KeySet> temp = GetTargetSet(gameScene);
            foreach(KeySet k in temp)
            {
                if (k.CheckActive())
                {
                    //检查该按键是否在该过程中被激活
                    //激活它的Motion操作
                    ActMotionCode(k.keyInputCode);
                }
            }

        }


        public bool ActMotionCode(KeyBoardInputCode code)
        {
            switch (code)
            {
                case KeyBoardInputCode.None:
                    break;
                //case KeyBoardInputCode.NextCard:
                //    //扔给UI界面
                //    UIController.instance.cardContainer.NextPointer(UIController.instance.cardContainer.cardContainerType);
                //    break;
                //case KeyBoardInputCode.PrevCard:
                //    //扔给UI界面
                //    UIController.instance.cardContainer.FrontPointer(UIController.instance.cardContainer.cardContainerType);
                    break;
                case KeyBoardInputCode.Comfirm:
                    //扔给UI界面
                    RaceController.instance.realPlayer.SetMotionMessage(MOTION_CODE.COMFIRM,null);
                    break;
                case KeyBoardInputCode.Accelerate:
                    {
                        switch (RaceController.instance.realPlayer.changeSpeed)
                        {
                            case -1:
                                RaceController.instance.realPlayer.SetMotionMessage(MOTION_CODE.ACCELERATE_CHANGE, new List<string>() { "0" });
                                UIController.instance.DataChange(DataChangeType.RealPlayer);
                                break;
                            case 0:
                                RaceController.instance.realPlayer.SetMotionMessage(MOTION_CODE.ACCELERATE_CHANGE, new List<string>() { "1" });
                                UIController.instance.DataChange(DataChangeType.RealPlayer);
                                break;
                            case 1:
                                return false;
                                break;
                        }
                    }
                    break;
                case KeyBoardInputCode.Slow:
                    {
                        switch (RaceController.instance.realPlayer.changeSpeed)
                        {
                            case -1:
                                return false;
                            case 0:
                                RaceController.instance.realPlayer.SetMotionMessage(MOTION_CODE.ACCELERATE_CHANGE, new List<string>() { "-1" });
                                UIController.instance.DataChange(DataChangeType.RealPlayer);
                                break;
                            case 1:
                                RaceController.instance.realPlayer.SetMotionMessage(MOTION_CODE.ACCELERATE_CHANGE, new List<string>() { "0" });
                                UIController.instance.DataChange(DataChangeType.RealPlayer);
                                break;
                        }
                    }
                    break;
                case KeyBoardInputCode.TestA:
                    Debug.Log("TestA_Active");
                    break;
                case KeyBoardInputCode.TestB:
                    Debug.Log("TestB_Active");
                    break;
                    //进入消息池|改变客户端配置内容
            }
            return true;
        }


        private List<KeySet> GetTargetSet(int gameScene)
        {
            List<KeySet> temp=null;
            switch (gameScene)
            {
                case 0:temp = normal;break;
                case 1:temp = game;break;
                case 2:temp = shoot; break;
                case 3:temp = drive; break;
                case 4:temp = melee; break;
            }
            return temp;
        }

        public class KeySet
        {
            public int assis;
            public KeyCode keyCode;
            public KeyBoardInputCode keyInputCode;

            public KeySet(KeyCode k, KeyBoardInputCode m,int i)
            {
                keyCode = k;
                keyInputCode = m;
                assis = i;
            }
            
            private bool CheckAssisCode(AssisInput checkAssis)
            {
                switch (checkAssis)
                {
                    case AssisInput.LeftCtrl:
                        return Input.GetKey(KeyCode.LeftControl);
                    case AssisInput.LeftShift:
                        return Input.GetKey(KeyCode.LeftShift);
                    case AssisInput.LeftAlt:
                        return Input.GetKey(KeyCode.LeftAlt);
                    case AssisInput.RightCtrl:
                        return Input.GetKey(KeyCode.RightControl);
                    case AssisInput.RightShift:
                        return Input.GetKey(KeyCode.RightShift);
                    case AssisInput.RightAlt:
                        return Input.GetKey(KeyCode.RightAlt);
                }
                return false;
            }

            public bool CheckActive()
            {
                System.Array assisArray = System.Enum.GetValues(typeof(AssisInput));
                if (!Input.GetKeyDown(keyCode))
                {
                    return false;
                }
                foreach (var checkAssis in assisArray)
                {
                    if ((assis & (int)checkAssis) != 0)
                    {
                        if (!CheckAssisCode((AssisInput)checkAssis))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (CheckAssisCode((AssisInput)checkAssis))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }
    }
    public enum AssisInput
    {
        LeftCtrl    =1,
        LeftShift   =2,
        LeftAlt     =4,
        RightCtrl   =8,
        RightShift  =16,
        RightAlt    =32
    }
    public enum KeyBoardInputCode
    {
        None,
        Comfirm,//确认
        Accelerate,//加速
        //速度不变？好像不需要
        Slow,//减速
        //选择下方卡牌槽1
        //选择下方卡牌槽2
        //选择下方卡牌槽3
        //选择下方卡牌槽4
        //选择下方卡牌槽5
        //选择下方卡牌槽6
        NextCard,//下方卡牌槽 上一张/下一张|切换时自动取消需要指定目标的激活
        PrevCard,
        //切换卡牌槽页面
        //卡牌槽 激活（单击为选择，按住为激活）

        //选择上方人物第一位
        //上方人物槽 上一个/下一个
        //将目标人物标记为目标
        //按住式查看/切换式查看 *2 

        //切换 未来路径地形卡检视

        //启动人物自律行动与取消
        TestA,
        TestB,
    }

    public class GameController : MonoBehaviour
    {

        public static GameController instance;
        public int gameScene;
        public int playerThugId;
        public ThugSource playerThug;
        public List<ThugSource> enemiesThug;

        public static GameController Instance()
        {
            if (instance == null)
            {
                instance = new GameController();
            }
            return instance;
        }



        float lockedTime = 3;//

        public static int CardX = 518;//卡牌的宽
        public static int CardY = 911;//卡牌的高

        public int maxPlayer;//当前该局的最大玩家数量
        public int maxLand;//当前该局的最大地形卡数量

        int gameType;//游戏状态：0单机模式 1客户端 2服务器

        //public event GetMessage Messenger;

        private void AllControllerInstance()
        {
            DataController.Instance();
            UIController.Instance();
            RaceController.Instance();
            InputController.Instance();
        }

        public void RenewPlayerEquipAndWeapons(List<EquipSource> equips,List<WeaponSource> weapons)
        {
            ThugSource player = DataController.instance.GetThugSourceById(playerThugId);
            player.weapons.Clear();
            foreach(var temp in weapons)
            {
                player.weapons.Add(temp.orderNumber);
            }
            player.equips.Clear();
            foreach(var temp in equips)
            {
                player.equips.Add(temp.orderNumber);
            }
        }

        void Awake()
        {
            if (!instance) { instance = this; }
            else { DestroyImmediate(this.gameObject); return; }
            gameType = 0;
            playerThugId = -1;
            DontDestroyOnLoad(gameObject);
            AllControllerInstance();
        }
        public void LoadData()
        {
            DataController.instance.LoadAllData();
            //DataController.instance.SaveData();
        }
        public void InitRace()
        {
            Debug.Log(playerThugId);
            //RaceController.instance.InitRace(maxPlayer, maxLand, playerThugId);
            RaceController.instance.InitRace(playerThug, enemiesThug, maxLand);
        }
        public void SaveData()
        {
            DataController.instance.SaveData();
        }

        //public void SetGameScene(int order)//设定数据的内容，环境触发
        //{

        //}

        public void GameScenceChange(int order)//目标界面
        {

            if (order == 2)
            {
                //Debug.Log(DataController.instance.GetThugSourceById(playerThugId).name);
                DataController.instance.ComfirmEnemies(maxPlayer-1);
            }
            //GameManager.instance.mainCharacterId = nowCharacterId;
            SceneManager.LoadScene(order);
        }

        #region xyy_needed
        //————————————前方界面专用————————————
        //注：特性只有序号，自行申请具体数据
        public List<ThugSource> ChooseAblePlayers(int number)//返回一定量可选的人物 并配置初始人物列表
        {
            //Debug.Log("内容载入");
            List<ThugSource> temp = new List<ThugSource>();
            List<ThugSource> temp2 = new List<ThugSource>();
            DataController.instance.thugSources.ForEach(i => temp.Add(i));


            int count=number>temp.Count?temp.Count:number;
            for (; count > 0; count--)
            {
                int i = Random.Range(0, temp.Count);
                temp2.Add(temp[i]);
                temp.Remove(temp[i]);
            }
            foreach(var a in temp2)
            {
                //Debug.Log(a.name);
            }

            return temp2;
        }
        public void EnemyPoolRemovePlayer(int id)
        {
            DataController.instance.RemoveEnemy(id);
        }
        public List<ThugSource> ChooseAbleRandomPlayers(int number)
        {
            List<ThugSource> temp = new List<ThugSource>();

            return temp;
        }

        public void PrepareNPCRace(int num)
        {
            enemiesThug = RandomNPCEnemies(playerThugId, num);
        }

        public List<ThugSource> RandomNPCEnemies(int playerId,int number)
        {
            List<ThugSource> temp = new List<ThugSource>();
            if (DataController.instance.thugSources.Count < number )
            {
                return null;
            }
            DataController.instance.thugSources.ForEach(i => {
                if (i.orderNumber == playerId) { }
                else { temp.Add(i); }
            });
            while (temp.Count>number)
            {
                temp.Remove(temp[Random.Range(0, temp.Count)]);//删除直到只剩一个
            }
            return temp;
        }

        public List<WeaponSource> BuyAbleRandomWeapons(int number)
        {
            List<WeaponSource> from = new List<WeaponSource>(DataController.instance.weaponSources);
            List<WeaponSource> temp = new List<WeaponSource>();
            for(int i = 0; i < number; i++)
            {
                var n = from[Random.Range(0, from.Count)];
                temp.Add(n);
                from.Remove(n);
            }
            return temp;
        }
        public List<EquipSource> BuyAbleRandomEquips(int number)
        {

            List<EquipSource> from = new List<EquipSource>(DataController.instance.equipSources);
            List<EquipSource> temp = new List<EquipSource>();
            for (int i = 0; i < number; i++)
            {
                var n = from[Random.Range(0, from.Count)];
                temp.Add(n);
                from.Remove(n);
            }
            return temp;
        }

        //回传数据


        #endregion

        void Start()
        {
            //DataController.instance.LoadAllData();
            //RaceController.instance.InitRace(maxPlayer, maxLand);
            //DataController.instance.SaveData();

            //数据库展开成关系实例
            //阶段一：导入完人物
            //a.transform.parent = canvas.transform;

            //阶段二：初始化，开始回合逻辑
            //RuntimePlayer t = RaceController.instance.GetRandomRuntimePlayer();
            //UIController.instance.CreateNewRuntimePlayerCard(t.player.name);
            //Land l = RaceController.instance.GetLandById(0);
            //UIController.instance.CreateNewLandCard(l.landid);
            //Equip e = RaceController.instance.GetEquipByNameAndID(t.player.name, t.player.equips[0].playerOrderNumber);
            //UIController.instance.CreateNewEquipCard(t.player.name,e.playerOrderNumber);
            //Weapon w = RaceController.instance.GetWeaponByNameAndID(t.player.name, t.player.weapons[0].playerOrderNumber);
            //UIController.instance.CreateNewWeaponCard(t.player.name,w.playerOrderNumber);

            //Equip e = RaceController.instance.GetLandById(0);
            //UIController.instance.CreateNewLandCard(l.landid);

            //ThugSource thug = new ThugSource();
            ////thugSources.Add(thug);
            //Weapon weapon = new Weapon();
            //weapons.Add(weapon);
            //Equip equip = new Equip();
            //equips.Add(equip);
            //Land land = new Land();
            //lands.Add(land);
            //Characteristic characteristic = new Characteristic();
            //characteristics.Add(characteristic);
            //List<CHEffect> temp=new List<CHEffect>();
            //temp.Add(new CHEffect());
            //CharacteristicSource s = new CharacteristicSource(0, "男性毁灭者", (int)CHARACTERISTIC_TYPE.COMBAT, new string[] { "", "", "", "", "", "", "", "" }, temp);
            //characteristicSources.Add(s);


            //Debug.Log(System.DateTime.Now);
            //MotionMessage a=new MotionMessage();
            //a.show();
            wyc.GameController.instance.LoadData();
        }



        // Update is called once per frame
        void Update()
        {
            //增加游戏未结束
            if (RaceController.instance.GetGameState() == (int)GAME_STATE.RUNNING)
            {
                RaceController.instance.GameLooping();
                InputController.instance.CheckInput(0);
            }
            //Object a=null;
            //Instantiate(a);
        }

        public void ShowText(string s)
        {
            Debug.Log(s);
        }

        public void CreateObject(Object o, Transform t)
        {

        }

    }


    public class Status {//状态类

    }

    public class MotionMessage {//操作信息类
        int year;
        int month;
        int day;
        int hour;
        int minute;
        int second;

        public int playerNum;//玩家识别码
        public int code;//操作码
        public List<string> motionParams;//操作参数表
        public MotionMessage(RuntimePlayer player, int motionCode, List<string> motionParams)//必须有操作人与方式
        {

            this.motionParams = new List<string>();
            year = System.DateTime.Now.Year;
            month = System.DateTime.Now.Month;
            day = System.DateTime.Now.Day;
            hour = System.DateTime.Now.Hour;
            minute = System.DateTime.Now.Minute;
            second = System.DateTime.Now.Second;
            code = motionCode;
            playerNum = player.GetOrderNum();
            if (motionParams != null)
            {
                foreach (string temp in motionParams)
                {
                    this.motionParams.Add(temp);
                }
            }
            //playerNum = player.player.;
        }
        public void show()
        {
            Debug.Log($"{year}/{month}/{day} {hour}:{minute}.{second}");
        }
        public string GetParam(int flag)
        {
            if (motionParams.Count > flag)
            {
                return motionParams[flag];
            }
            return null;
        }
    }
    public enum EFFECT_TYPE//默认check链为&&方式，在check循环之前使用EFFECT_TEMP_CHANGE进行辅助值改变进行!与||运算。
    {
        CHECK_SELF_FALL,//自身坠落
        CHECK_SELF_COMBAT,//自身进入战斗
        CHECK_SELF_DAMAGE,//自身受伤
        CHECK_SELF_DAMAGE_MELEE,//自身受近战伤
        CHECK_SELF_DAMAGE_RANGE,//自身受远程伤
        //—————数值变化—————
        CHECK_SELF_HEALTH = -4,
        CHECK_ENEMY_HEALTH = -3,
        CHECK_SELF_SPEED = -2,//INT。"0"不为0。“-”小于效果值的绝对值。“+”大于效果值的绝对值，“%”为百分比。
        CHECK_ENEMY_SPEED = -1,//INT。"0"比较双方。“-”小于效果值的绝对值。“+”大于效果值的绝对值。
        CHECK_TEMP = -1,//INT。检查辅助值。
        NONE = 0,//INT，效果值代表持续与建立状态。0即时,"-"代表特殊阶段与触发点，“+”持续回合数|代表句号。结束check环的作用，清除辅助值需要手动且必须。
        
        EFFECT_TEMP_CHANGE,//INT。暂存辅助值改变
        EFFECT_TEMP_EQUAL, //INT。暂存辅助值改变


        EFFECT_TIME_CHANGE,//INT。作用于NONE计算持续回合时的改变量
        EFFECT_SELF_SPEED,
        EFFECT_SELF_WEALTH,
        EFFECT_SELF_STRENGTH,
        EFFECT_SELF_DRIVE,
        EFFECT_ENEMY_SPEED,
        EFFECT_ENEMY_WEALTH,
        EFFECT_ENEMY_STRENGTH,
        EFFECT_ENEMY_DRIVE,

        EFFECT_COMBAT_MELEE_DODGE,//近战闪避
        EFFECT_FALL_IMMUNE,//坠落伤害免疫
        //—————————人物被动特性———————————
        EFFECT_BUYING,//影响购买数量
    }
    //战斗狂暴：当血量<4时，近战能力+1。当血量<2时，近战能力+1。
    //[-4,5,0][-4,1,1]|
    //[-4,-4]、[5,1]、[0,0][-4,-2]、[5,1]、[0,0]
    //地形卡，损失血量，造成伤害，减免伤害...
    public enum CHARACTERISTIC_TYPE
    {
        NORMAL,//普通，在对局时才起效
        COMPLEX,//复合，默认全加载
        PASSIVE,//被动，在人物加载时生效。
        ONCE,//一次性，每回合清空

        DRIVE,//驾驶
        COMBAT,//战斗
        //人物4特性
        EACH_TRUN,//每回合
        ACCELERATE,//每加速
    }
    public enum EQUIP_TYPE
    {
        CONSUME,//消耗，特性激活
        TIRE,//轮胎 驾驶
        BRAKE,//刹车 减速
        ENGINE,//引擎 加速
        IGNITION,//点火 初速
        CHASSIS,//底盘 车技
    }
    public enum WEAPON_TYPE
    {
        LONG_RANGE,//远程
        MELEE//近战
    }
    public enum STATE {
        ACCOUNT_STATE,//结算阶段#。回合能力启动 10s|5s 已经可以进行加减速阶段的行为
        ACCELERATE_STATE,//加减速阶段 10s|5s 用于确定
        MOTION_STATE,//移动阶段*小游戏 15|10s 已经可以进行预谋阶段的行为
        PREMEDITATE_STATE,//预谋阶段| 10|5s 用于确定
        COMBAT_STATE,//混战阶段*小游戏|开始、伤害、结算、结束 10s|5s
        END_STATE,//结束阶段# 10s|5s
        //—————新的——————
        READY_STATE,//准备阶段 解锁所有可以进行的行为 20s 
        PRE_STATE,//预处理阶段 移动结算，锁住可进行的行为（更改行为吃减值）
        AFTER_STATE,//后处理阶段 动画与小游戏阶段
        NEXT_STATE,//下一阶段 
        
    }
    public enum MOTION_CODE {//操作码
        //————— 唯一 ———————
        NONE,//没有，切换
        EXIT_GAME,//退出游戏
        ATTACT_TARGET,//设置攻击目标
        ACCELERATE_CHANGE,//加速变换
        ATTACK_TARGET_CHANGE,//攻击目标变换
        COMFIRM,//确定
        //————— 复数 ————————
        //小游戏的操作
        CHARACTERISTIC_ACTIVE,//谁
    }
    //public int playerStatus;//玩家状态：在线、离线、暂停|死亡·在线、死亡·离线
    public enum PLAYER_STATUS_TYPE
    {
        ONLINE,
        OFFLINE,
        STOP,
        DYING,
        DEAD,
        OUT_GAME

    }
    public enum GAME_STATE
    {
        PREPARE,//准备中
        RUNNING,//运行中
        ENDING,//结束
    }
    public class DamageMessage
    {
        public RuntimePlayer attacker;
        public RuntimePlayer deffender;
        public Weapon weapon;
        public int damage;
        public DamageMessage(RuntimePlayer attacker,RuntimePlayer deffender,Weapon weapon,int damage)
        {
            this.attacker = attacker;
            this.deffender = deffender;
            this.weapon = weapon;
            this.damage = damage;
        }
    }
}