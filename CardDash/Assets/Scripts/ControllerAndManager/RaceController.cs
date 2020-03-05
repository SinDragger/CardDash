using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using wyc;

public class RaceController : MonoBehaviour//比赛管理者
{
    public static RaceController instance;


    public RuntimePlayer realPlayer;//真正所控制的玩家
    public List<RuntimePlayer> enemyPlayers;//真实开始进行游戏的玩家们

    public float time;//当前时间
    public float timeShow;//时间系数
    public int state;//阶段
    int turns;//回合数
    float waitSecond;//每回合时间

    int hasDonePlayer;//执行完操作的玩家数量

    int gameState;//游戏总阶段：菜单、准备、进行、结束/结算


    public List<Thug> thugs;
    List<Weapon> weapons;
    List<Equip> equips;
    List<Land> lands;
    List<Characteristic> characteristics;
    List<RuntimePlayer> runtimePlayers;//真实开始进行游戏的玩家们
    List<MotionMessage> totalMotionMessagesPool;//玩家操作栈，记录每个玩家的操作时间点与

    List<DamageMessage> totalDamageMessage;
    List<DamageMessage> thisTurnDamageMessage;


    int deadPeople;


    public static float ACCOUNT_STATE_TIME = 10;
    public static float ACCELERATE_STATE_TIME = 15;
    public static float MOTION_STATE_TIME = 10;
    public static float PREMEDITATE_STATE_TIME = 15;
    public static float COMBAT_STATE_TIME = 10;
    public static float END_STATE_TIME = 10;


    public static float READY_STATE_TIME = 3;
    public static float PRE_STATE_TIME = 2;
    public static float AFTER_STATE_TIME = 1;
    public static float NEXT_TIME = 1;


    public static RaceController Instance()
    {
        if (instance == null)
        {
            instance = new RaceController();
        }
        return instance;
    }
    void Start()
    {
        instance = this;
        totalMotionMessagesPool = new List<MotionMessage>();
        thugs = new List<Thug>();
        weapons = new List<Weapon>();
        equips = new List<Equip>();
        lands = new List<Land>();
        characteristics = new List<Characteristic>();
        enemyPlayers = new List<RuntimePlayer>();
        totalDamageMessage = new List<DamageMessage>();
        thisTurnDamageMessage = new List<DamageMessage>();
        gameState = (int)GAME_STATE.PREPARE;


        //应该由Loading来负责
        //if (GameController.instance)
        //{
        //    InitRace(GameController.instance.playerThug, GameController.instance.enemiesThug, GameController.instance.maxLand);
        //    UIController.instance.LoadPlayerData();
        //}   
    }



    //public RaceController()
    //{
    //    totalMotionMessagesPool = new List<MotionMessage>();
    //    thugs = new List<Thug>();
    //    weapons = new List<Weapon>();
    //    equips = new List<Equip>();
    //    lands = new List<Land>();
    //    characteristics = new List<Characteristic>();
    //    enemyPlayers = new List<RuntimePlayer>();
    //    totalDamageMessage = new List<DamageMessage>();
    //    thisTurnDamageMessage = new List<DamageMessage>();
    //    gameState = (int)GAME_STATE.PREPARE;
    //}
    #region Race_Get
    public int GetState()
    {
        return state;
    }
    public int GetGameState()
    {
        return gameState;
    }
    public int GetTurns()
    {
        return turns;
    }
    public Thug GetThugByName(string name)
    {
        foreach (Thug thug in thugs)
        {
            if (thug.name == name)
            {
                return thug;
            }
        }
        return null;
    }
    public int GetMaxPlayers()
    {

        return runtimePlayers.Count;
    }
    public int GetConfirmPlayers()
    {
        int result = 0;
        foreach (var temp in enemyPlayers)
        {
            if (temp.confirm)
                result++;
        }
        return result;
    }
    //public float GetLeftTime()
    //{
    //    if (waitSecond - time < 0f)
    //        return 0f;
    //    return waitSecond - time;
    //}

    public float GetStateTime()
    {
        return waitSecond;
    }
    public float GetNowTime()
    {
        return time;
    }

    public List<RuntimePlayer> GetRuntimePlayers()
    {
        return runtimePlayers;
    }


    private void showAllPlayerPosition()
    {
        foreach(var a in runtimePlayers)
        {
            Debug.Log(a.player.name+"/" + a.land.position + "/" +a.distance);
        }
    }

    public List<RuntimePlayer> GetRuntimePlayerRank(bool countAlive)
    {
        showAllPlayerPosition();

        List<RuntimePlayer> temp = new List<RuntimePlayer>(runtimePlayers);
        //判断规则
        //完成比赛：时间顺序-未完成比赛-路程顺序：距离下一个地图长度

        temp.Sort((a, b) =>
        {

            if (a.complete.CompareTo(b.complete) != 0)//比较完成比赛
            {
                return b.complete.CompareTo(a.complete);
            }
            else
            {

                if (a.complete)//是否都胜利
                {
                    return b.completeTime.CompareTo(a.completeTime);
                }
                else
                {
                    if (a.land.position.CompareTo(b.land.position) != 0)//比较地形卡位置
                    {
                        return b.land.position.CompareTo(a.land.position);
                    }
                    else
                    {
                        if (a.distance.CompareTo(b.distance) != 0)//比较路程
                        {
                            return b.distance.CompareTo(a.distance);
                        }
                        else
                        {

                        }
                    }
                }
            }
            return 0;
        }
        );
        //foreach (RuntimePlayer p in temp)
        //{
        //    Debug.Log($"{p.player.name}");
        //}
        return temp;
    }
    public RuntimePlayer GetRuntimePlayerByName(string name)
    {
        foreach (RuntimePlayer temp in runtimePlayers)
        {
            if (temp.player.name == name)
            {
                return temp;
            }
        }
        return null;
    }
    public RuntimePlayer GetRuntimePlayerByID(int ID)
    {
        foreach (RuntimePlayer temp in runtimePlayers)
        {
            if (temp.runtimePlayerID == ID)
            {
                return temp;
            }
        }
        return null;
    }
    public Land GetLandById(int landid)
    {
        foreach (Land temp in lands)
        {
            if (temp.landid == landid)
            {
                return temp;
            }
        }
        return null;
    }
    public Equip GetEquipByNameAndID(string name, int equipId)
    {
        foreach (RuntimePlayer temp in runtimePlayers)
        {
            if (temp.player.name == name)
            {
                foreach (Equip e in temp.player.equips)
                {
                    if (e.playerOrderNumber == equipId)
                    {
                        return e;
                    }
                }
            }
        }
        return null;
    }
    public Weapon GetWeaponByNameAndID(string name, int weaponId)
    {
        foreach (RuntimePlayer temp in runtimePlayers)
        {
            if (temp.player.name == name)
            {
                foreach (Weapon w in temp.player.weapons)
                {
                    if (w.playerOrderNumber == weaponId)
                    {
                        return w;
                    }
                }
            }
        }
        return null;
    }

    public RuntimePlayer GetRealPlayer()
    {
        //Debug.Log(RealPlayer.player.name);
        return realPlayer;
    }

    public Equip GetRealPlayerEquip(int playerOrderId)
    {
        foreach (var e in realPlayer.player.equips)
        {
            if (e.playerOrderNumber == playerOrderId) return e;
        }
        return null;
    }
    public List<Equip> GetRealPlayerEquips()
    {
        if (realPlayer != null)
        {
            List<Equip> temp = new List<Equip>();
            foreach (var e in realPlayer.player.equips)
            {
                temp.Add(e);
            }
            return temp;
        }
        return null;
    }
    public Weapon GetRealPlayerWeapon(int playerOrderId)
    {
        foreach (var w in realPlayer.player.weapons)
        {
            if (w.playerOrderNumber == playerOrderId) return w;
        }
        return null;
    }
    public List<Weapon> GetRealPlayerWeapons()
    {
        if (realPlayer != null)
        {
            List<Weapon> temp = new List<Weapon>();
            foreach (var w in realPlayer.player.weapons)
            {
                temp.Add(w);
            }
            return temp;
        }
        return null;
    }

    public RuntimePlayer GetRandomRuntimePlayer()
    {
        //Debug.Log($"players{runtimePlayers.Count}");
        return runtimePlayers.Count == 0 ? null : runtimePlayers[UnityEngine.Random.Range(0, runtimePlayers.Count)];
    }
    #endregion

    #region Race_Init
    public void InitRace(int maxPlayer, int maxLand, int playerId)//旧版本
    {

        Thug thug;
        foreach (ThugSource thugSource in DataController.instance.thugSources)
        {
            if (thugSource.orderNumber == playerId)
            {
                //初始化上边获取的列表
                List<int> temp1 = new List<int>();
                foreach (var t in GameManager.instance.nowCharcter.weapon)
                {
                    temp1.Add(t.Id);
                }
                thugSource.weapons = temp1;

                List<int> temp2 = new List<int>();
                foreach (var t in GameManager.instance.nowCharcter.refitting)
                {
                    temp2.Add(t.Id);
                }
                thugSource.equips = temp2;
            }
            thug = new Thug(thugSource, DataController.instance.weaponSources, DataController.instance.equipSources, DataController.instance.characteristicSources);
            thugs.Add(thug);
        }
        UIController.instance.RecieveLog("——————GameInit——————");
        //Debug.Log("——————GameInit——————");
        RandomLands(maxLand);//地形5
        LoadPlayerToRunTime(maxPlayer, playerId);//4人比赛,全员NPC化

        UIController.instance.RecieveLog($"参赛者：{runtimePlayers.Count}人");

        InitSelfAndEnemies(0);//区分提出一个敌人人物

        //Debug.Log($"真正的武器数量为{realPlayer.player.weapons.Count}");
        turns = 1;//重置为第一回合
        state = (int)STATE.READY_STATE;//初始化回合
        waitSecond = READY_STATE_TIME;//每回合15秒
        //timeShow = 3f;
        gameState = (int)GAME_STATE.RUNNING;
        UIController.instance.DataChange(DataChangeType.Init);
        UIController.instance.DataChange(DataChangeType.State);
        UIController.instance.SetActiveToUICards(false, CardContainerType.Equip);
        UIController.instance.SetActiveToUICards(false, CardContainerType.Weapon);
        Debug.Log("Init Over");
        //禁用武器与改装
    }
    public void InitRace(ThugSource player,List<ThugSource> enemies, int maxLand)//new
    {
        Thug thug;
        thug = new Thug(player, DataController.instance.weaponSources, DataController.instance.equipSources, DataController.instance.characteristicSources);
        thugs.Add(thug);
        foreach (ThugSource thugSource in enemies)
        {
            thug = new Thug(thugSource, DataController.instance.weaponSources, DataController.instance.equipSources, DataController.instance.characteristicSources);
            thugs.Add(thug);//加载入全部人物列表
        }
        UIController.instance.RecieveLog("——————GameInit——————");
        RandomLands(maxLand);//加载地形
        LoadPlayerToRunTime();//全数加载

        UIController.instance.RecieveLog($"参赛者：{runtimePlayers.Count}人");

        InitSelfAndEnemies(0);//区分敌我

        //Debug.Log($"真正的武器数量为{realPlayer.player.weapons.Count}");
        turns = 1;//重置为第一回合

        gameState = (int)GAME_STATE.RUNNING;
        UIController.instance.score.score.Init(runtimePlayers,realPlayer);
        
        UIController.instance.DataChange(DataChangeType.Init);
        UIController.instance.DataChange(DataChangeType.State);
        StateChangeIntoREADY_STATE();
        Debug.Log("Init Over");
    }

    private void InitSelfAndEnemies(int order)
    {

        realPlayer = runtimePlayers[order];
        realPlayer.isNPC = false;
        //Debug.Log("阶段三" + realPlayer.player.name);
        //Debug.Log("阶段三" + realPlayer.player.weapons.Count);
        //Debug.Log("阶段三" + realPlayer.player.equips.Count);
        //取消realPlayer的NPC状态
        foreach (RuntimePlayer runtimePlayer in runtimePlayers)
        {
            if (runtimePlayer != realPlayer)
            {
                enemyPlayers.Add(runtimePlayer);
            }
        }
        //然后根据敌人列表生成对敌画面，如果自己是非操控的服务器端则不用

    }
    private void LoadPlayerToRunTime()//练习模式
    {
        //默认thugs第一个是自己
        runtimePlayers = new List<RuntimePlayer>();
        RuntimePlayer temp = new RuntimePlayer(thugs[0], false, lands[0]);
        temp.runtimePlayerID = 0;
        runtimePlayers.Add(temp);
        for (int i = 1; i < thugs.Count; i++)
        {
            temp = new RuntimePlayer(thugs[i], true, lands[0]);
            temp.runtimePlayerID = i;
            runtimePlayers.Add(temp);
        }

        Debug.Log("阶段二" + runtimePlayers[0].player.name);
        foreach (RuntimePlayer t in runtimePlayers)
        {
            UIController.instance.RecieveLog($"{t.player.name}");
            //temp.player.Print();
        }
    }

    private void LoadPlayerToRunTime(int n, int playerId)
    {//构建n个玩家的参赛
        Thug tTemp;
        int flag;
        for (int i = 0; i < n - 1; i++)
        {
            tTemp = thugs[i];
            flag = Random.Range(i, thugs.Count);
            thugs[i] = thugs[flag];
            thugs[flag] = tTemp;
        }
        int pos = 0;
        for (int i = 0; i < thugs.Count; i++)
        {
            if (thugs[i].source.orderNumber == playerId)
            {
                Debug.Log("阶段0" + thugs[i].name + "/" + thugs[i].source.orderNumber); pos = i; break;
            }
        }
        tTemp = thugs[pos];
        thugs[pos] = thugs[0];
        thugs[0] = tTemp;
        Debug.Log("阶段一" + thugs[0].name);
        Debug.Log(playerId);

        runtimePlayers = new List<RuntimePlayer>();
        for (int i = 0; i < n; i++)
        {
            RuntimePlayer temp = new RuntimePlayer(thugs[i], true, lands[0]);
            temp.runtimePlayerID = i;
            runtimePlayers.Add(temp);
        }

        Debug.Log("阶段二" + runtimePlayers[0].player.name);
        foreach (RuntimePlayer temp in runtimePlayers)
        {
            UIController.instance.RecieveLog($"{temp.player.name}");
            //temp.player.Print();
        }
    }

    private void RandomLands(int n)//构建出随机地形
    {
        //Debug.Log("lands");
        int flag;
        for (int i = 0; i < n; i++)
        {
            flag = UnityEngine.Random.Range(0, DataController.instance.landSources.Count);
            lands.Add(new Land(DataController.instance.landSources[flag]));
        }

        for (int i = 0; i < n; i++)
        {
            lands[i].Print();
            lands[i].position = i;//这一段不一定
            lands[i].landid = i; //它的id等于排列次序
        }
        for (int i = 0; i < n - 1; i++)
        {
            lands[i].nextland.Add(lands[i + 1]);
        }
    }
    #endregion

    #region  Race_Loop
    public void GameLooping()
    {
        CheckNPC();
        GetPlayersMessage();
        UpdateMessage();//获取，然后处理
                        //NPC行为未行动则自动化行动
        time += Time.deltaTime * timeShow;
        if (time > waitSecond)
        {
            time = 0;
            //ConfirmAllPlayersMoveMent();//对未进行操作玩家的操作进行定义并记录
            StateChange();
        }
        UIController.instance.DataChange(DataChangeType.Time);
    }

    private void CheckNPC()
    {
        foreach (RuntimePlayer runTimePlayer in runtimePlayers)
        {
            if (runTimePlayer.isNPC && !runTimePlayer.confirm && !runTimePlayer.complete)
            {//接管未行动的NPC行动
                runTimePlayer.AIcontrol(state, 0);
            }
        }
    }


    private void UpdateMessage()
    {
        bool dataRenew = false;
        if (totalMotionMessagesPool.Count > 0) { dataRenew = true; }
        while (totalMotionMessagesPool.Count > 0)
        {
            RuntimePlayer player = null;
            foreach (RuntimePlayer ptemp in runtimePlayers)
            {
                if (ptemp.GetOrderNum() == totalMotionMessagesPool[0].playerNum)
                {
                    player = ptemp;
                }
            }
            if (player != null)
            {
                switch (totalMotionMessagesPool[0].code)
                {
                    case (int)MOTION_CODE.ACCELERATE_CHANGE:
                        PlayerChangeSpeed(player, int.Parse(totalMotionMessagesPool[0].GetParam(0)));
                        break;
                    case (int)MOTION_CODE.ATTACT_TARGET: break;
                    case (int)MOTION_CODE.CHARACTERISTIC_ACTIVE: break;
                    case (int)MOTION_CODE.ATTACK_TARGET_CHANGE:
                        PlayerChangeAttackTarget(player, FindRuntimePlayerByID(int.Parse(totalMotionMessagesPool[0].GetParam(0))));
                        //PlayerChangeAttackTarget(player, FindRuntimePlayerByName(totalMotionMessagesPool[0].GetParam(0)));
                        break;
                    case (int)MOTION_CODE.COMFIRM:
                        PlayerConfirm(player);
                        break;
                    case (int)MOTION_CODE.EXIT_GAME: break;

                }
            }
            //totalMotionMessagesPool[0]
            totalMotionMessagesPool.Remove(totalMotionMessagesPool[0]);
        }
        if (dataRenew) UIController.instance.DataChange(DataChangeType.RealPlayer);
    }
    #endregion


    #region Race_StateChange

    //private void StateChange_New()//切换回合
    //{
    //    //先完善掉未操作者的操作.?
    //    ResetStateData();
    //    switch (state)
    //    {
    //        case (int)STATE.READY_STATE:
    //            StateChangeIntoREADY_STATE();
    //            break;
    //        case (int)STATE.PRE_STATE:
    //            StateChangeIntoMOTION_STATE();
    //            break;
    //        case (int)STATE.AFTER_STATE:
    //            StateChangeIntoPREMEDITATE_STATE();
    //            break;
    //        case (int)STATE.NEXT_STATE:
    //            StateChangeNextTurn_N();
    //            break;
    //    }
    //    UIController.instance.DataChange(DataChangeType.State);
    //    //Debug.Log("计数"+realPlayer.canAttackMelee.Count);
    //    UIController.instance.DataChange(DataChangeType.RealPlayer);
    //    //a
    //    Debug.Log("STATECHANGETO：" + ((STATE)state).ToString());
    //}
    private void StateChange()//切换回合
    {
        //先完善掉未操作者的操作.?
        ResetStateData();
        switch (state)
        {
            case (int)STATE.ACCOUNT_STATE:
                StateChangeIntoACCELERATE_STATE();
                break;
            case (int)STATE.ACCELERATE_STATE:
                StateChangeIntoMOTION_STATE();
                break;
            case (int)STATE.MOTION_STATE:
                StateChangeIntoPREMEDITATE_STATE();
                break;
            case (int)STATE.PREMEDITATE_STATE:
                StateChangeIntoCOMBAT_STATE();
                break;
            case (int)STATE.COMBAT_STATE:
                StateChangeIntoEND_STATE();
                break;
            case (int)STATE.END_STATE:
                StateChangeNextTurn();
                break;


            case (int)STATE.READY_STATE:
                StateChangeIntoPRE_STATE();
                break;
            case (int)STATE.PRE_STATE:
                StateChangeIntoAFTER_STATE();
                break;
            case (int)STATE.AFTER_STATE:
                StateChangeIntoNEXT_STATE();
                break;
            case (int)STATE.NEXT_STATE:
                StateChangeNextTurn();
                break;
        }
        UIController.instance.DataChange(DataChangeType.State);
        //Debug.Log("计数"+realPlayer.canAttackMelee.Count);
        UIController.instance.DataChange(DataChangeType.RealPlayer);
        //a
        Debug.Log("STATECHANGETO：" + ((STATE)state).ToString());
    }

    private void PlayerChangeSpeed(RuntimePlayer player, int speed)
    {
        if (player.CheckSelfActive())
        {
            player.changeSpeed = speed;
            string temp = null;
            switch (speed)
            {
                case -1:
                    temp = "减速";
                    break;
                case 0:
                    temp = "匀速";
                    break;
                case 1:
                    temp = "加速";
                    break;
            }
            temp = $"{player.player.name}准备{temp}";
            UIController.instance.RecieveLog(temp);
        }
    }

    private void PlayerChangeAttackTarget(RuntimePlayer attacker, RuntimePlayer defender)
    {
        attacker.target = defender;
    }
    private void ResetStateData()
    {
        foreach (RuntimePlayer temp in runtimePlayers)
        {
            temp.confirm = false;
        }
    }

    private void StateChangeIntoREADY_STATE()
    {
        //启用改装牌
        //UIController.instance.SetActiveToUICards(true, CardContainerType.Equip);
        UIController.instance.SetActiveToUICards(true, CardContainerType.Equip);
        UIController.instance.SetActiveToUICards(true, CardContainerType.Weapon);
        waitSecond = READY_STATE_TIME;
        state = (int)STATE.READY_STATE;
    }
    private void StateChangeIntoPRE_STATE()
    {
        UIController.instance.SetActiveToUICards(false, CardContainerType.Equip);
        ProcessChangingSpeed();
        CheckGameConfirm(p => { return p.isPlayerPlayMINIGame(PlayMiniGameType.Drive); });
        ProcessMoving();//处理移动
        UIController.instance.DataChange(DataChangeType.Land);//所处地形数据更新
        CountAttackers();
        waitSecond = PRE_STATE_TIME;
        state = (int)STATE.PRE_STATE;
    }
    private void StateChangeIntoAFTER_STATE()
    {
        UIController.instance.SetActiveToUICards(false, CardContainerType.Weapon);
        UIController.instance.playerArea.ClearChooseAble();//暂时高耦合
        //禁用武器牌
        ProcessHealthBeforeCombat();
        ProcessAttackingTarget();//处理攻击目标合法性，并发送开展小游戏。
        waitSecond = AFTER_STATE_TIME;
        state = (int)STATE.AFTER_STATE;
    }
    private void StateChangeIntoNEXT_STATE()
    {
        ProcessAttackingDamage();
        ClearPlayersRouteAndTarget();
        waitSecond = NEXT_TIME;
        state = (int)STATE.NEXT_STATE;
    }
    private void StateChangeIntoACCELERATE_STATE()
    {
        //启用改装牌
        UIController.instance.SetActiveToUICards(true, CardContainerType.Equip);
        waitSecond = ACCELERATE_STATE_TIME;
        state = (int)STATE.ACCELERATE_STATE;
    }
    private void StateChangeIntoMOTION_STATE()
    {
        //应用加减速
        ProcessChangingSpeed();
        CheckGameConfirm(p => { return p.isPlayerPlayMINIGame(PlayMiniGameType.Drive); });//检查是否玩小游戏，不玩的直接置confirm
                                                                                          //给玩的发送请求，提前可关闭
        waitSecond = MOTION_STATE_TIME;
        state = (int)STATE.MOTION_STATE;//开始玩小游戏，影响之后是否可攻击
    }

    public delegate bool GameConfirm(RuntimePlayer player);

    private void CheckGameConfirm(GameConfirm gameConfirm)
    {
        foreach (RuntimePlayer player in runtimePlayers)
        {
            if (player.CheckSelfActive())
            {
                if (gameConfirm(player))
                {
                    PlayerConfirm(player);
                }
            }
        }
    }

    private void PlayerConfirm(RuntimePlayer player)
    {
        if (!player.confirm)
        {
            player.confirm = true;
            //削减时间
            IndentationTime();
        }
    }
    private void IndentationTime()//缩进时间
    {
        //time += 2.0f;
    }
    private void GetPlayersMessage()
    {
        foreach (RuntimePlayer temp in runtimePlayers)
        {
            if (temp.motionMessagesPool.Count > 0)
            {
                foreach (MotionMessage mtemp in temp.motionMessagesPool)
                {
                    totalMotionMessagesPool.Add(mtemp);
                }
                temp.motionMessagesPool.Clear();
            }
        }
    }

    private void ProcessChangingSpeed()
    {
        foreach (RuntimePlayer player in runtimePlayers)
        {
            if (player.CheckSelfActive())
            {
                string temp;
                switch (player.changeSpeed)
                {
                    case -1:
                        player.speed -= player.slowSpeed;
                        temp = $"{player.player.name}进行了减速{player.slowSpeed}公里/小时,现时速{player.speed}公里/小时";
                        UIController.instance.RecieveLog(temp);

                        break;
                    case 0:
                        break;
                    case 1:
                        player.speed += player.accelerateSpeed;
                        temp = $"{player.player.name}进行了加速{player.slowSpeed}公里/小时,现时速{player.speed}公里/小时";
                        UIController.instance.RecieveLog(temp);
                        break;
                }
            }
        }

    }

    private void StateChangeIntoPREMEDITATE_STATE()
    {
        UIController.instance.SetActiveToUICards(false, CardContainerType.Equip);
        UIController.instance.SetActiveToUICards(true, CardContainerType.Weapon);
        //禁用改装牌，可以启用武器牌
        //收集确认玩小游戏的情况，先不写
        ClearPlayersRouteAndTarget();
        ProcessMoving();//处理移动
        UIController.instance.DataChange(DataChangeType.Land);//所处地形数据更新
        CountAttackers();

        waitSecond = PREMEDITATE_STATE_TIME;
        state = (int)STATE.PREMEDITATE_STATE;
    }
    private void ProcessMoving()
    {
        foreach (RuntimePlayer player in runtimePlayers)
        {
            //oldpositon和newposition

            if (player.CheckSelfActive())
            {
                //player.playMINIgame & (int)(PlayMiniGameType.Drive);
                if (player.isPlayerPlayMINIGame(PlayMiniGameType.Drive))
                {
                    int temp;
                    int temp2 = CheckPlayerDriving(player, out temp);
                    //Debug.Log($"{player.player.name}投掷出了{temp}总和{temp2}");
                    if (temp2 < 0)
                    {
                        string s = $"{player.player.name}狠狠得撞上了没有来得及处理的障碍，怦然巨响";
                        UIController.instance.RecieveLog(s);
                        playerDown(player);
                        player.oldLand = player.land;
                        player.oldDistance = player.distance;
                        continue;
                    }
                }

                player.oldLand = player.land;
                player.oldDistance = player.distance;
                player.distance += player.speed / 10;
                player.route.Add(player.land);
                while (player.distance > player.land.length)
                {
                    if (player.land.nextland.Count == 0)//跑到尽头
                    {
                        //计算时间 绝对player.speed / 10移动距离player.distance-player.speed / 10初始距离
                        float movelength = player.speed / 10;
                        float originlength = player.distance - movelength;
                        float costTime = (player.land.length - (player.distance - movelength)) / movelength;//<1f
                        PlayerCompleteRace(player, turns + costTime);
                        break;
                        //runtimePlayers.Remove(player);
                    }
                    else
                    {
                        player.distance -= player.land.length;
                        player.land = player.land.nextland[0];//默认1
                        player.route.Add(player.land);//加入之后行驶到的
                        string s = $"{player.player.name}驶入了{player.land.name}";
                        UIController.instance.RecieveLog(s);
                        int temp;
                        int temp2 = CheckPlayerDriving(player, out temp);
                        //Debug.Log($"{player.player.name}投掷出了{temp}总和{temp2}");
                        if (temp2 < 0)
                        {
                            UIController.instance.RecieveLog($"{player.player.name}在变道时未经注意，撞了上去");
                            player.distance = 0;
                            playerDown(player);
                            player.oldLand = player.land;
                            player.oldDistance = player.distance;
                        }
                        else
                        {
                            DecreaseWanted(player);
                        }
                    }
                }
                if (!player.complete)
                {
                    UIController.instance.RecieveLog($"{player.player.name}于{player.land.name}的{player.distance}距离");
                }

            }
        }
    }
    private void DecreaseWanted(RuntimePlayer player)
    {
        int decreaseWanted = player.speed / 100;
        int tempWanted = player.wanted - decreaseWanted;

        player.wanted = tempWanted < 0 ? 0 : tempWanted;
    }
    private void PlayerCompleteRace(RuntimePlayer player, float completeTime)
    {

        player.distance = player.land.length;
        player.complete = true;
        //修改完成时间
        player.completeTime = completeTime;
        Debug.Log(completeTime);
        UIController.instance.RecieveLog($"{player.player.name}已经抵达终点");

    }

    private void CountAttackers()
    {
        for (int i = 0; i < runtimePlayers.Count; i++)
        {
            RuntimePlayer player1 = runtimePlayers[i];
            //如果之前在前面，现在在后面，或之前在后面，现在在前面，则交错，认为攻击目标
            if (!player1.CheckSelfActive())//不看摔车的
                continue;
            for (int n = i + 1; n < runtimePlayers.Count; n++)
            {
                RuntimePlayer player2 = runtimePlayers[n];
                if (!player2.CheckSelfActive())//不看摔车的
                { continue; }
                //获得了2个player，开始判断重叠lands
                if (CheckPlayerCanMeleeAttack(player1, player2))
                {
                    player1.canAttackMelee.Add(player2);
                    player2.canAttackMelee.Add(player1);
                    //Debug.Log($"{player1.player.name} {player2.player.name} Melee");
                }
                if (CheckPlayerCanRangeAttack(player1, player2))
                {
                    player1.canAttackRange.Add(player2);
                    player2.canAttackRange.Add(player1);
                    //Debug.Log($"{player1.player.name} {player2.player.name} Range");
                }
            }
        }
    }

    private bool CheckPlayerCanMeleeAttack(RuntimePlayer attacker, RuntimePlayer defender)
    {
        List<Land> checkpoint = new List<Land>();
        Dictionary<Land, int> hashMap = new Dictionary<Land, int>();
        for (int i = 0; i < attacker.route.Count; i++)
        {
            for (int n = 0; n < defender.route.Count; n++)
            {
                if (defender.route[n] == attacker.route[i])
                {
                    checkpoint.Add(defender.route[n]);
                }
            }
        }
        //取得重合地区
        foreach (Land l in checkpoint)
        {
            //统计两者到此的（头）的总路程
            float aDistance = 0, dDistance = 0;
            float aEnterTime = 0, dEnterTime = 0;
            float aExitTime = 0, dExitTime = 0;
            for (int i = 0; l != attacker.route[i]; i++)
            {
                aDistance += attacker.route[i].length;
            }
            for (int i = 0; l != defender.route[i]; i++)
            {
                dDistance += defender.route[i].length;
            }
            aEnterTime = aDistance / attacker.speed;
            dEnterTime = dDistance / defender.speed;
            aExitTime = (aDistance + l.length) / attacker.speed;
            dExitTime = (dDistance + l.length) / defender.speed;
            if ((aEnterTime - dEnterTime) >= 0 && (aEnterTime - dEnterTime) <= 0 || (aEnterTime - dEnterTime) <= 0 && (aEnterTime - dEnterTime) >= 0)//2者正负不一致或时间点一致
            {
                return true;//确认可攻击
            }
        }
        //attacker.route;
        return false;
    }
    private bool CheckPlayerCanRangeAttack(RuntimePlayer attacker, RuntimePlayer defender)
    {
        //中途位置大幅度交错
        int aF = attacker.route[0].position;
        int aL = attacker.route[attacker.route.Count - 1].position;
        int dF = defender.route[0].position;
        int dL = defender.route[defender.route.Count - 1].position;

        if (Mathf.Abs(aF - dF) < 2)
        {
            return true;
        }
        if (Mathf.Abs(aL - dL) < 2)
        {
            return true;
        }
        if ((aF < dF && aL > dL) || (aF > dF && aL < dL))
        {
            return true;
        }
        //存在近战可以的范围
        return false;
    }

    private void ClearPlayersRouteAndTarget()
    {
        foreach (RuntimePlayer player in runtimePlayers)
        {
            player.route.Clear();
            player.canAttackMelee.Clear();
            player.canAttackRange.Clear();
        }
    }

    private void playerDown(RuntimePlayer player)
    {
        player.speed = 0;
        player.down = 2;//翻车2回合
        HurtByLand(player);//该玩家受摔倒伤害
    }
    private void HurtByLand(RuntimePlayer player)
    {
        int LandHurt = 1;
        player.player.health -= LandHurt;//当前只扣1格血
        if (player.player.health <= 0)
        {
            //重伤出局
            PlayerInjuryOut(player);
        }
    }
    private void PlayerInjuryOut(RuntimePlayer player)
    {
        player.playerStatus = (int)PLAYER_STATUS_TYPE.OUT_GAME;

        UIController.instance.RecieveLog($"{player.player.name}重伤出局");
    }

    private int CheckPlayerDriving(RuntimePlayer player, out int dice)
    {
        dice = Random.Range(1, 7);
        int n = player.player.drive + dice - (player.land.complex + (player.speed / 100) + (player.wanted / 3));
        return n;
    }

    private void StateChangeIntoCOMBAT_STATE()
    {
        UIController.instance.SetActiveToUICards(false, CardContainerType.Weapon);
        UIController.instance.playerArea.ClearChooseAble();//暂时高耦合
        //禁用武器牌
        ProcessHealthBeforeCombat();
        ProcessAttackingTarget();//处理攻击目标合法性，并发送开展小游戏。
        waitSecond = COMBAT_STATE_TIME;
        state = (int)STATE.COMBAT_STATE;
    }

    private void ProcessHealthBeforeCombat()
    {
        EffectAllPlayers(p => { p.healthBeforeCombat = p.player.health; });
    }
    private void ProcessAttackingTarget()
    {
        bool isLegal = false;//该攻击目标合法
        foreach (RuntimePlayer player in runtimePlayers)
        {
            if (player.CheckSelfActive())
            {
                if (player.target != null)
                {
                    foreach (RuntimePlayer canAttack in player.canAttackMelee)
                    {
                        if (canAttack == player.target)
                        {
                            isLegal = true;
                            break;
                        }
                    }
                    if (!isLegal)
                    {
                        foreach (RuntimePlayer canAttack in player.canAttackMelee)
                        {
                            if (canAttack == player.target)
                            {
                                isLegal = true;
                                break;
                            }
                        }
                    }
                    if (!isLegal)
                    {
                        AttackTargetIsFalse(player);
                    }
                    else
                    {
                        if (player.isPlayerPlayMINIGame(PlayMiniGameType.Combat))
                        {
                            //发送小游戏
                        }
                        //不发送，默认处理
                    }

                }
            }
        }
    }

    private void AttackTargetIsFalse(RuntimePlayer player)
    {
        player.target = null;
    }

    private void StateChangeIntoEND_STATE()
    {
        //统计小游戏进程与处理伤害反馈
        ProcessAttackingDamage();
        waitSecond = END_STATE_TIME;
        state = (int)STATE.END_STATE;
    }

    private void ProcessAttackingDamage()
    {
        foreach (RuntimePlayer player in runtimePlayers)
        {
            //oldpositon和newposition

            if (player.CheckSelfActive() && player.target != null)
            {
                ProcessTryAttackResult(player);
                if (!player.isPlayerPlayMINIGame(PlayMiniGameType.Combat) && !player.isPlayerPlayMINIGame(PlayMiniGameType.Shoot))
                {
                    int temp;
                    int basicDamage = CheckPlayerAttacking(player, out temp);
                    //第一层的纯粹基础伤害
                    //伤害加成A
                    //伤害加成B
                    //伤害加成C
                    if (basicDamage > 0)//拥有伤害
                    {
                        int damage = basicDamage;
                        PlayerDamagePlayer(player, player.target, damage);
                    }
                    else
                    {
                        UIController.instance.RecieveLog($"{player.player.name}用{player.GetUsingWeaponName()}攻击了{player.target.player.name}时失手了");
                    }
                }

            }
        }
    }
    private void ProcessTryAttackResult(RuntimePlayer player)
    {
        if (!player.attackedPeople.Contains(player.target))
        {
            player.attackedPeople.Add(player.target);
        }
        player.wanted += player.GetUsingWeapon().wanted;
        //Debug.Log(player.player.name + "通缉值" + player.wanted);
    }
    private void PlayerDamagePlayer(RuntimePlayer attacker, RuntimePlayer defender, int damage)
    {
        defender.player.health -= damage;
        thisTurnDamageMessage.Add(new DamageMessage(attacker, defender, attacker.GetUsingWeapon(), damage));
        UIController.instance.RecieveLog($"{attacker.player.name}用{attacker.GetUsingWeaponName()}攻击了{defender.player.name}，造成了{damage}点伤害");


        if (defender.player.health <= 0)
        {
            PlayerDying(attacker, defender);
        }
    }

    private void PlayerDying(RuntimePlayer attacker, RuntimePlayer defender)
    {
        //死亡回避以外


        defender.playerStatus = (int)PLAYER_STATUS_TYPE.DYING;
    }

    private int CheckPlayerAttacking(RuntimePlayer player, out int dice)
    {
        dice = Random.Range(1, 101);
        //dice修正
        int diceFix = 0;
        int total = dice + diceFix;
        //根据当前武器
        if (player.GetUsingWeapon().weaponType == (int)WEAPON_TYPE.MELEE)
        {
            if (total > 50)
            {
                return player.GetUsingWeapon().damage + player.player.strength;
            }
            else
            {
                return 0;
            }
        }

        if (player.GetUsingWeapon().weaponType == (int)WEAPON_TYPE.LONG_RANGE)
        {
            if (total > 33)
            {
                return player.GetUsingWeapon().damage;
            }
            else
            {
                return 0;
            }
        }

        return -1;//特殊情况
    }
    private void StateChangeNextTurn()
    {
        CountDamageMessageAndKilling();
        ProcessPlayersDown();
        UIController.instance.playerArea.ClearAll();//暂时高耦合

        if (CheckGameComplete())
        {
            gameState = (int)GAME_STATE.ENDING;
            Debug.Log("比赛结束");
            UIController.instance.RecieveLog($"排名揭晓，胜负已分，竞赛结束");
            UIController.instance.DataChange(DataChangeType.GameComplete);
            return;
        }


        turns++;
        UIController.instance.RecieveLog($"第{turns}回合");
        if (state == (int)STATE.NEXT_STATE)
        {
            StateChangeIntoREADY_STATE();
        }
        else
        {
        }

        UIController.instance.DataChange(DataChangeType.Turn);
    }

    private void StateChangeNextTurn_N()
    {
        CountDamageMessageAndKilling();
        ProcessPlayersDown();
        UIController.instance.playerArea.ClearAll();//暂时高耦合

        if (CheckGameComplete())
        {
            gameState = (int)GAME_STATE.ENDING;
            Debug.Log("比赛结束");
            UIController.instance.RecieveLog($"排名揭晓，胜负已分，竞赛结束");
            UIController.instance.DataChange(DataChangeType.GameComplete);
            return;
        }


        turns++;
        UIController.instance.RecieveLog($"第{turns}回合");
        waitSecond = READY_STATE_TIME;
        state = (int)STATE.READY_STATE;

        UIController.instance.DataChange(DataChangeType.Turn);
    }

    private bool CheckGameComplete()
    {
        //bool check = true;
        int count = 1;
        foreach (RuntimePlayer player in runtimePlayers)
        {
            if (!player.isUnable() && !player.complete)
            {
                count--;
                if (count < 0) return false;
                //check = false;

            }
        }
        return true;

    }
    private void CountDamageMessageAndKilling()
    {


        foreach (var player in runtimePlayers)
        {
            if (player.playerStatus == (int)PLAYER_STATUS_TYPE.DYING)//如果是濒死角色，开始检索本回合伤害值
            {
                //统计本回合造成伤害的玩家与造成的伤害值与是否是关键性杀伤伤害
                int originHealth = player.healthBeforeCombat;
                List<AttackerDamage> attackers = new List<AttackerDamage>();
                //List<int> thisTurnDamage = new List<int>();
                foreach (DamageMessage damageMessage in thisTurnDamageMessage)
                {
                    if (damageMessage.deffender == player)
                    {
                        bool find = false;
                        for (int i = 0; i < attackers.Count; i++)
                        {
                            if (attackers[i].attacker == damageMessage.attacker)
                            {
                                find = true;
                                attackers[i].damage += damageMessage.damage;
                                break;
                            }
                        }
                        if (!find)
                        {
                            attackers.Add(new AttackerDamage(damageMessage.attacker, damageMessage.damage));
                        }
                    }
                }
                foreach (var attacker in attackers)
                {
                    if (attacker.damage > originHealth)
                    {
                        attacker.isDeadlyAttack = true;
                    }
                }
                //扫完
                attackers.Sort((a, b) =>
                {
                    if (a.isDeadlyAttack.CompareTo(b.isDeadlyAttack) != 0)
                    {
                        return a.isDeadlyAttack.CompareTo(b.isDeadlyAttack);
                    }
                    else
                    {
                        if (a.isFavourAttack.CompareTo(b.isFavourAttack) != 0)
                        {
                            return a.isFavourAttack.CompareTo(b.isFavourAttack);
                        }
                        else
                        {
                            if (a.damage.CompareTo(b.damage) != 0)
                            {
                                return a.damage.CompareTo(b.damage);
                            }
                            else
                            {
                                //当前判断到伤害截止

                            }
                        }
                    }
                    return 0;
                });

                if (attackers.Count > 0)
                {
                    PlayerKilling(attackers[attackers.Count - 1].attacker, player);
                    PlayerDie(player);
                }
                //其他情况：地形杀、自杀

            }
        }


        foreach (var i in thisTurnDamageMessage)
        {
            totalDamageMessage.Add(i);
        }
        thisTurnDamageMessage.Clear();
        //totalDamageMessage.a
    }

    class AttackerDamage
    {
        public RuntimePlayer attacker;
        public int damage;
        public bool isFavourAttack;
        public bool isDeadlyAttack;
        public AttackerDamage(RuntimePlayer attacker, int damage)
        {
            this.attacker = attacker;
            this.damage = damage;
            isFavourAttack = false;
            isDeadlyAttack = false;
        }
    }
    private bool PlayerKilling(RuntimePlayer attacker, RuntimePlayer defender)
    {
        //玩家A把玩家D杀了
        attacker.killedPeople.Add(defender);
        //Debug.Log($"{attacker.player.name}重创了{defender.player.name}，杀人数为{attacker.killedPeople.Count}");
        UIController.instance.RecieveLog($"{attacker.player.name}重创了{defender.player.name}，杀人数为{attacker.killedPeople.Count}");

        return true;
    }
    private bool PlayerDie(RuntimePlayer player)
    {
        player.playerStatus = (int)PLAYER_STATUS_TYPE.DEAD;
        UIController.instance.RecieveLog($"{player.player.name}死了");
        //Debug.Log($"{realPlayer.player.name}检索");
        player.speed = 0;
        deadPeople++;

        UIController.instance.ThugDead(player.runtimePlayerID);

        if (player == realPlayer)
        {
            UIController.instance.DataChange(DataChangeType.Dead);
        }
        return true;
    }

    #endregion

    private void ProcessPlayersDown()
    {
        foreach (RuntimePlayer player in runtimePlayers)
        {
            if (!player.complete)
            {
                if (player.down == 2)
                {
                    player.down--;
                    UIController.instance.RecieveLog($"{player.player.name}艰难得爬起来准备上车");
                    //Debug.Log($"{player.player.name}艰难得爬起来准备上车");

                }
                else if (player.down == 1)
                {
                    player.down--;
                    player.speed = 100;//赋予初速
                    UIController.instance.RecieveLog($"{player.player.name}引擎再度轰鸣！");
                    //Debug.Log();
                }
            }
        }
    }

    public delegate void PlayerStatusChange(RuntimePlayer player);
    private void EffectAllPlayers(PlayerStatusChange doingToPlayer)
    {
        foreach (RuntimePlayer player in runtimePlayers)
        {
            doingToPlayer(player);
        }
    }

    public RuntimePlayer FindRuntimePlayerByName(string name)
    {
        if (name != null)
        {
            foreach (RuntimePlayer player in runtimePlayers)
            {
                if (name.CompareTo(player.player.name) == 0)
                {
                    return player;
                }
            }
        }
        return null;
    }
    public RuntimePlayer FindRuntimePlayerByID(int playerId)
    {
        if (playerId != -1)
        {
            foreach (RuntimePlayer player in runtimePlayers)
            {
                if (player.runtimePlayerID == playerId)
                {
                    return player;
                }
            }
        }
        return null;
    }

}
