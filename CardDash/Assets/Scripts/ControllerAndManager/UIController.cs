using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using wyc;
public class UIController : MonoBehaviour
{//UI控制者，UI组件向其发出申请，其中转向数据/比赛请求数据，再将其反馈提供。同时负责界面组件的生产
    public static GameObject canvas;

    public static UIController instance;

    public int targetId;
    public int usingWeaponId;
    public int usingEquipId;

    public int UIState;//显示敌人为0，不显示敌人为-1；

    public UI_CardArea_MainGame mainArea;
    public UI_PlayerArea playerArea;
    public UI_CharacShow_Text characShowText;
    public UI_Score_Main score;
    //public Basic_CardContainer cardContainer;//原始的卡牌容器，用于交互
    //public Basic_EnemySlot enemySlotBar;
    //public Basic_MINIGame MINIGame;

    public List<DataReceiver> landRenewList;
    public List<DataReceiver> timeRenewList;
    public List<DataReceiver> stateRenewList;
    public List<DataReceiver> playerRenewList;
    public List<DataReceiver> turnRenewList;

    public List<DataReceiver> deadEffectList;
    public List<DataReceiver> completeEffectList;

    public List<DataReceiver> InitList;
    public List<DataReceiver> logRenewList;

    public List<string> logPool;//消息池，日后会更新

    public List<RuntimePlayer> ranking;
    public void Awake()
    {
        landRenewList = new List<DataReceiver>();
        timeRenewList = new List<DataReceiver>();
        stateRenewList = new List<DataReceiver>();
        playerRenewList = new List<DataReceiver>();
        turnRenewList = new List<DataReceiver>();
        deadEffectList = new List<DataReceiver>();
        completeEffectList = new List<DataReceiver>();
        InitList = new List<DataReceiver>();
        logRenewList = new List<DataReceiver>();
        logPool = new List<string>();
        instance = this;
    }

    private void Start()
    {
        //GameController.instance.InitRace();
        //LoadPlayerData();
    }

    public void LoadPlayerData()
    {
        if (GameController.instance)
        {
            mainArea.InitData(RaceController.instance.realPlayer.runtimePlayerID);
            playerArea.InitData(RaceController.instance.GetRealPlayer(), RaceController.instance.enemyPlayers);
            //GameController.instance.playerThug;
        }
    }

    public void ThugDead(int orderNum)
    {
        playerArea.PlayerDead(orderNum);
    }

    public static UIController Instance()
    {
        if (instance == null)
        {
            //instance = new UIController();

            instance = GameObject.FindObjectOfType<UIController>();
            if (instance == null) return null;
            //设置canvas，以免失误
            RectTransform t = Object.FindObjectOfType<RectTransform>();
            Transform temp = t.transform;
            canvas = temp.gameObject;
            while (temp.parent != null)
            {
                temp = temp.parent;
                t = temp.GetComponent<RectTransform>();
                if (temp != null)
                {
                    canvas = temp.gameObject;
                }
            }
        }
        return instance;
    }
    #region UI_Control
    public void ChangePlayerSpeed(int i)
    {
        RaceController.instance.realPlayer.SetMotionMessage(MOTION_CODE.ACCELERATE_CHANGE, new List<string>() { i.ToString() });
    }
    private void RenewActiveEnemies()
    {
        var temp=RaceController.instance.GetRealPlayer();
        Weapon w = temp.GetUsingWeapon();
        //var avaliableList = new List<RuntimePlayer>();
        //List<RuntimePlayer> avaliableList;
        List<int> able = new List<int>();
        if (w.weaponType == (int)WEAPON_TYPE.MELEE)
        {
            foreach(var enemy in temp.canAttackMelee)
            {
                able.Add(enemy.runtimePlayerID);
            }
        }
        else
        {
            foreach (var enemy in temp.canAttackRange)
            {
                able.Add(enemy.runtimePlayerID);
            }
        }
        Debug.Log(temp.canAttackRange.Count + "/" + temp.canAttackMelee.Count);
        playerArea.RenewChoosableEnemies(able);

        //temp.usingWeapon
    }
    public bool ChangeUsingEquipment(int id,CardContainerType type)
    {
        if (type == CardContainerType.Weapon)
        {
            usingWeaponId = id;
            if (id == -1) { return false;
                //清空可选目标序列
            }
            //开始处理可用武器列表
            Debug.Log("SetUsingWeapon");
            RaceController.instance.GetRealPlayer().SetUsingWeapon(id);
            RenewActiveEnemies();
            return true;

        }else if (type == CardContainerType.Equip)
        {
            usingEquipId = id;
            if (id == -1){return false;}
            return true;
        }
        return false;
    }

    public bool SetActiveToUICards(bool flag,CardContainerType type)
    {
        if (!mainArea) return false;
        mainArea.SetActiveToCards(flag, type);
        return true;
    }

    public bool TargetSelect(int playerId)
    {
        if (playerId == -1) return false;
        targetId = playerId;
        Debug.Log($"选取攻击目标为{playerId}号");
        RaceController.instance.realPlayer.SetMotionMessage(MOTION_CODE.ATTACK_TARGET_CHANGE, new List<string>() { playerId.ToString() });
        //向RaceControll获得目标，然后
        //更新旧的UI，清除？
        playerArea.TargetChoosed(playerId);
        return true;
    }
    #endregion
    #region UI_Data
    public bool attachRenewList(DataReceiver dataReceiver, DataChangeType type)
    {
        List<DataReceiver> list = GetDataReceiverList(type);
        if (dataReceiver != null && !list.Contains(dataReceiver))
        {
            list.Add(dataReceiver);
            return true;
        }
        return false;
    }
    public void DataChange(DataChangeType type)
    {
        List<DataReceiver> list = GetDataReceiverList(type);
        foreach (var temp in list)
        {
            temp.RenewData();
        }
    }
    private List<DataReceiver> GetDataReceiverList(DataChangeType type)
    {
        List<DataReceiver> list = null;
        switch (type)
        {
            case DataChangeType.Land:
                list = landRenewList;
                break;

            case DataChangeType.RealPlayer:
                list = playerRenewList;
                //调一下排名顺序
                //ranking = RaceController.instance.GetRuntimePlayerRank(true);
                break;

            case DataChangeType.State:
                list = stateRenewList;
                break;
            case DataChangeType.Time:
                list = timeRenewList;
                break;
            case DataChangeType.Turn:
                list = turnRenewList;
                break;
            case DataChangeType.Dead:
                list = deadEffectList;
                break;
            case DataChangeType.GameComplete:
                list = completeEffectList;
                break;
            case DataChangeType.Init:
                list = InitList;
                break;
            case DataChangeType.Log:
                list = logRenewList;
                break;
        }
        return list;
    }
    #endregion
    #region UI_Get
    public List<string> GetRealPlayerData()
    {
        List<string> temp = new List<string>();
        //找人
        RuntimePlayer p = RaceController.instance.GetRealPlayer();
        temp.Add(p.accelerateSpeed.ToString());
        temp.Add(p.slowSpeed.ToString());
        temp.Add(p.playMINIgame.ToString());
        temp.Add(p.changeSpeed.ToString());
        temp.Add(p.wanted.ToString());
        temp.Add(p.speed.ToString());
        temp.Add(p.player.drive.ToString());


        int difficult = (p.land.complex + (p.speed / 100) + (p.wanted / 3));
        temp.Add(difficult.ToString());

        return temp;
    }
    public List<int> GetRuntimePlayerEquipsIds(int id)
    {
        List<int> temp = new List<int>();
        RuntimePlayer p = RaceController.instance.GetRuntimePlayerByID(id);
        foreach (var i in p.player.equips)
        {
            temp.Add(i.playerOrderNumber);
        }
        return temp;
    }
    public List<int> GetRuntimePlayerWeaponsIds(int id)
    {
        List<int> temp = new List<int>();
        RuntimePlayer p = RaceController.instance.GetRuntimePlayerByID(id);
        foreach (var i in p.player.weapons)
        {
            temp.Add(i.playerOrderNumber);
        }
        return temp;
    }

    public List<string> GetRuntimePlayerSlotData(int id)
    {
        List<string> temp = new List<string>();
        RuntimePlayer p = RaceController.instance.GetRealPlayer();
        //List<RuntimePlayer> rp = RaceController.instance.GetRuntimePlayers();
        RuntimePlayer rpp = RaceController.instance.GetRuntimePlayerByID(id);
        temp.Add(rpp.GetUsingWeaponName());
        temp.Add(rpp.wanted.ToString());
        temp.Add(rpp.player.strength.ToString());
        temp.Add(rpp.speed.ToString());
        temp.Add(rpp.killedPeople.Count.ToString());
        if (rpp.attackedPeople.Contains(p))
        {
            temp.Add("1");
        }
        else
        {
            temp.Add("0");
        }
        if (p.canAttackMelee.Contains(rpp))
        {
            temp.Add("0");
        }
        else if (p.canAttackRange.Contains(rpp))
        {
            temp.Add("1");
        }
        else
        {
            temp.Add("2");
        }

        temp.Add(rpp.player.health.ToString());
        temp.Add(rpp.player.health_Souce.ToString());
        temp.Add(rpp.player.name.ToString());

        if (rpp == p)
        {
            temp.Add(((int)EnemySlotSideType.RealPlayer).ToString());
        }
        else if (rpp.complete)
        {
            temp.Add(((int)EnemySlotSideType.Complete).ToString());
        }
        else if (rpp.isUnable())
        {
            temp.Add(((int)EnemySlotSideType.OutGame).ToString());
        }
        else if (rpp.down > 0)
        {
            temp.Add(((int)EnemySlotSideType.Down).ToString());
        }
        else
        {
            temp.Add(((int)EnemySlotSideType.None).ToString());
        }



        //temp.Add(rp[i].killedPeople.Count.ToString());


        return temp;
    }

    public List<int> GetRealPlayerLandFBStatus()
    {

        List<int> temp = new List<int>();
        RuntimePlayer p = RaceController.instance.GetRealPlayer();
        Land land = p.land;
        int countF = 0;
        int countB = 0;
        float pDistance = p.distance;
        foreach (RuntimePlayer enemy in RaceController.instance.enemyPlayers)
        {
            if (enemy.land == land)
            {
                if (enemy.distance <= pDistance)
                {
                    countB++;
                }
                else
                {
                    countF++;
                }
            }
        }
        temp.Add(countB);
        temp.Add(countF);
        //if()
        //一个个塞数据
        return temp;

    }

    public int GetRealPlayerLandID()
    {
        RuntimePlayer p = RaceController.instance.GetRealPlayer();
        int temp = p.land.landid;
        return temp;
    }

    public int GetRealPlayerID()
    {
        RuntimePlayer p = RaceController.instance.GetRealPlayer();
        if (p != null)
        {
            int temp = p.runtimePlayerID;
            return temp;
        }
        return -1;
    }
    public int GetRealPlayerLandLength()
    {
        RuntimePlayer p = RaceController.instance.GetRealPlayer();
        int temp = p.land.length;
        return temp;
    }
    public int GetRealPlayerDistance()
    {
        RuntimePlayer p = RaceController.instance.GetRealPlayer();
        int temp = p.distance;
        return temp;
    }
    public List<string> GetPlayerData(string name)//申请获取一次数据，然后去向race现场拿一次数据
    {

        List<string> temp = new List<string>();
        //找人
        RuntimePlayer p = RaceController.instance.GetRuntimePlayerByName(name);
        temp.Add(p.player.strength.ToString());
        temp.Add(p.player.drive.ToString());
        temp.Add(p.player.health.ToString());
        temp.Add(p.player.wealth.ToString());
        temp.Add(p.player.name);
        foreach (Characteristic c in p.player.characteristics)
        {
            temp.Add(c.name);
            temp.Add(c.characteristicType.ToString());
        }
        //一个个塞数据
        return temp;
    }
    public List<string> GetPlayerData(int ID)//申请获取一次数据，然后去向race现场拿一次数据|分离Origin数据与ChangeAble数据
    {

        List<string> temp = new List<string>();
        //找人
        RuntimePlayer p = RaceController.instance.GetRuntimePlayerByID(ID);
        temp.Add(p.player.strength.ToString());
        temp.Add(p.player.drive.ToString());
        temp.Add(p.player.health.ToString());
        temp.Add(p.player.wealth.ToString());
        temp.Add(p.player.name);
        foreach (Characteristic c in p.player.characteristics)
        {
            temp.Add(c.name);
            temp.Add(c.characteristicType.ToString());
        }
        //一个个塞数据
        return temp;
    }
    public List<int> GetCenterMapData(int ID)//获取中心地图数据——包括主角数据更新与地图数据更新
    {
        //主角名字、地图数量、#(地图ID)、当前阶段
        //

        List<int> temp = new List<int>();
        //找人
        RuntimePlayer p = RaceController.instance.GetRuntimePlayerByID(ID);
        Land l = p.land;
        int landCount = l.nextland.Count;
        temp.Add(landCount);
        foreach (var n in l.nextland)
        {
            //landCount++;
            temp.Add(n.landid);
        }


        return temp;
    }

    public int GetRaceState()
    {
        int state = RaceController.instance.GetState();
        //string temp=null;
        return state;
    }
    public int GetRaceTurn()
    {
        int turn = RaceController.instance.GetTurns();
        return turn;
    }
    public int GetRaceConfirmPlayers()
    {
        int temp = RaceController.instance.GetConfirmPlayers();
        return temp;
    }
    public int GetRaceMaxPlayers()
    {
        int temp = RaceController.instance.GetMaxPlayers();
        return temp;
    }
    public float GetRaceLeftTime()
    {
        float temp = RaceController.instance.GetStateTime() - RaceController.instance.GetNowTime();
        return temp < 0f ? 0f : temp;
    }
    public float GetRaceStateTime()
    {
        return RaceController.instance.GetStateTime();
    }
    public float GetRaceNowTime()
    {
        return RaceController.instance.GetNowTime() > RaceController.instance.GetStateTime() ? RaceController.instance.GetStateTime() : RaceController.instance.GetNowTime();
    }

    public List<string> GetLandDataByID(int landid)//申请获取一次数据，然后去向race现场拿一次数据
    {

        List<string> temp = new List<string>();
        //找地形
        Land l = RaceController.instance.GetLandById(landid);
        temp.Add(l.name.ToString());
        temp.Add(l.length.ToString());
        temp.Add(l.complex.ToString());
        temp.Add(l.position.ToString());
        return temp;
    }
    public List<int> GetRealPlayerEquipsId()
    {
        List<int> temp = new List<int>();
        List<Equip> equips = RaceController.instance.GetRealPlayerEquips();
        foreach (var e in equips)
        {
            temp.Add(e.playerOrderNumber);
        }
        return temp;
    }

    public List<int> GetRealPlayerWeaponsId()
    {
        List<int> temp = new List<int>();
        List<Weapon> weapons = RaceController.instance.GetRealPlayerWeapons();
        foreach (var w in weapons)
        {
            temp.Add(w.playerOrderNumber);
        }
        return temp;
    }

    public List<string> GetEquipDataByIDAndPlayerName(int equipId, string playerName)//申请获取一次数据，然后去向race现场拿一次数据
    {

        List<string> temp = new List<string>();
        Equip e = RaceController.instance.GetEquipByNameAndID(playerName, equipId);
        if (e != null)
        {
            temp.Add(e.name.ToString());
            temp.Add(e.equipType.ToString());
            temp.Add(e.price.ToString());
            temp.Add(e.effectLevel.ToString());
            foreach (Characteristic c in e.characteristics)
            {
                temp.Add(c.name);
                temp.Add(c.characteristicType.ToString());
            }
            return temp;
        }
        return null;
    }
    public List<string> GetWeaponDataByIDAndPlayerName(int weaponId, string playerName)//申请获取一次数据，然后去向race现场拿一次数据
    {

        List<string> temp = new List<string>();
        Weapon e = RaceController.instance.GetWeaponByNameAndID(playerName, weaponId);
        if (e != null)
        {
            temp.Add(e.name.ToString());
            temp.Add(e.damage.ToString());
            temp.Add(e.price.ToString());
            temp.Add(e.wanted.ToString());
            foreach (Characteristic c in e.characteristics)
            {
                temp.Add(c.name);
                temp.Add(c.characteristicType.ToString());
            }
            return temp;
        }
        return null;
    }
    #endregion
    private bool RealPlayerDead()
    {
        GameObject.FindGameObjectWithTag("DeadEffect").SetActive(true);
        return false;
    }


    public void RecieveUICode(List<int> data)//*需要被替换，old
    {
        if (data.Count == 0) return;
        switch ((UI_Code)data[0])
        {
            case UI_Code.SELECT_TARGET:
                targetId = data[1];
                break;
        }
    }

    //归属UI,之后打包成Message发给池子
    public void RecieveLog(string log)
    {
        logPool.Add(log);
        Debug.Log(log);
        //
        DataChange(DataChangeType.Log);
    }
    #region UI_CreateCard
    public void CreateNewCard(string name, int type)
    {
        //根据类型去生产
    }
    public void CreateNewRuntimePlayerCard(RuntimePlayer runtimePlayer)
    {
        //准备一份模板，对其进行实现
        GameObject b;
        b = (GameObject)Resources.Load("Prefabs/PlayerCardPrefab");
        b = GameController.Instantiate(b, canvas.transform);
        b.GetComponent<PlayerCard>().runtimePlayerID = runtimePlayer.runtimePlayerID;
        //b.GetComponent<PlayerCard>().playerName= "保加利亚妖王";
        b.GetComponent<PlayerCard>().UpdateData();
    }
    public void CreateNewLandCard(int landId)
    {
        //准备一份模板，对其进行实现
        GameObject b;
        b = (GameObject)Resources.Load("Prefabs/LandCardPrefab");
        b = GameController.Instantiate(b, canvas.transform);
        b.GetComponent<LandCard>().landID = landId;
    }
    public GameObject CreateNewEquipCard(string runtimePlayer, int equipId)
    {
        //准备一份模板，对其进行实现
        GameObject b;
        b = (GameObject)Resources.Load("Prefabs/EquipCardPrefab");
        b = GameController.Instantiate(b, canvas.transform);
        b.GetComponent<EquipCard>().EquipID = equipId;
        b.GetComponent<EquipCard>().playerName = runtimePlayer;
        return b;
    }
    public GameObject CreateNewEquipCard(int equipId)
    {
        //准备一份模板，对其进行实现
        GameObject b;
        b = (GameObject)Resources.Load("Prefabs/EquipCardPrefab");
        b = GameController.Instantiate(b, canvas.transform);
        b.GetComponent<EquipCard>().EquipID = equipId;
        b.GetComponent<EquipCard>().playerName = RaceController.instance.realPlayer.player.name;
        return b;
    }
    public GameObject CreateNewEquipCard(int equipId, GameObject target)//默认给自己创造的
    {
        //准备一份模板，对其进行实现
        GameObject b;
        b = (GameObject)Resources.Load("Prefabs/EquipCardPrefab");
        b = GameController.Instantiate(b, target.transform);
        b.GetComponent<EquipCard>().EquipID = equipId;
        b.GetComponent<EquipCard>().playerName = RaceController.instance.realPlayer.player.name;
        return b;
    }
    public GameObject CreateNewWeaponCard(string runtimePlayer, int weaponId)
    {
        //准备一份模板，对其进行实现
        GameObject b;
        b = (GameObject)Resources.Load("Prefabs/WeaponCardPrefab");
        b = GameController.Instantiate(b, canvas.transform);
        b.GetComponent<WeaponCard>().weaponID = weaponId;
        b.GetComponent<WeaponCard>().playerName = runtimePlayer;
        return b;
    }
    public GameObject CreateNewWeaponCard(int weaponId, GameObject target)//默认给自己创造的
    {
        //准备一份模板，对其进行实现
        GameObject b;
        b = (GameObject)Resources.Load("Prefabs/WeaponCardPrefab");
        b = GameController.Instantiate(b, target.transform);
        b.GetComponent<WeaponCard>().weaponID = weaponId;
        b.GetComponent<WeaponCard>().playerName = RaceController.instance.realPlayer.player.name;
        return b;
    }
    public GameObject CreateNewWeaponCard(int weaponId)//默认给自己创造的
    {
        //准备一份模板，对其进行实现
        GameObject b;
        b = (GameObject)Resources.Load("Prefabs/WeaponCardPrefab");
        b = GameController.Instantiate(b, canvas.transform);
        b.GetComponent<WeaponCard>().weaponID = weaponId;
        b.GetComponent<WeaponCard>().playerName = RaceController.instance.realPlayer.player.name;
        return b;
    }
    #endregion

}
public enum UI_Code//在UI层面可执行的操作列表：
{
    SELECT_TARGET,//选择目标
    ACTIVE_ITEM,//启用武器/改装能力
                //启用地形车技
    Chat,//发送消息
    EquipGame,//退出游戏
              //暂停申请
}
