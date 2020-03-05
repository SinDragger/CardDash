using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using wyc;
public class Game_Main_Loading : MonoBehaviour
{
    public static Game_Main_Loading instance;
    public UI_LoadingBar loadingBar;
    public UI_LoadingPanel loadingPanel;
    public float timeBarSpeed;
    public List<ThugSource> players;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //获得GameController的5个对敌
        players = new List<ThugSource>();
        players.Add(GameController.instance.playerThug);
        GameController.instance.enemiesThug.ForEach(i => players.Add(i));
        loadingPanel.Init(players);
        loadingBar.Init(timeBarSpeed);
        //开始载入
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CloseSelf()
    {
        gameObject.SetActive(false);
        if (GameController.instance)
        {
            GameController.instance.InitRace();
            //RaceController.instance.InitRace(GameController.instance.playerThug, GameController.instance.enemiesThug, GameController.instance.maxLand);
            UIController.instance.LoadPlayerData();
        }
    }
}
