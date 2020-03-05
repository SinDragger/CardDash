using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Score : MonoBehaviour
{
    public GameObject prefab;
    List<UI_ScoreSideBar> list;
    RectTransform self;
    public float offset;//间隙长度
    public Text title;
    float targetHeight;
    float selfHeight;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Init(List<wyc.RuntimePlayer> players, wyc.RuntimePlayer rp)
    {
        list = new List<UI_ScoreSideBar>();
        list.Add(prefab.GetComponent<UI_ScoreSideBar>());
        prefab.SetActive(true);
        prefab.GetComponent<UI_ScoreSideBar>().SetToTitle();
        self = GetComponent<RectTransform>();
        //Debug.Log("ShowOne");
        targetHeight = prefab.GetComponent<RectTransform>().rect.height;
        selfHeight = GetComponent<RectTransform>().rect.height;
        GameObject temp;
        for (int i = 0; i < players.Count; i++)
        {
            temp = Instantiate(prefab, transform);
            temp.SetActive(true);
            var e = temp.GetComponent<UI_ScoreSideBar>();
            list.Add(e);
            e.SetDetail(players[i].player.name, players[i].killedPeople.Count, players[i].killedPeople.Count * 10 + (i < 3 ? (3 - i) : 0) * 10);
            if (players[i].player.health < 0) { e.SetDead(false); }
            else if (players[i].player.health == 0) { e.SetDead(true); }
            //Debug.Log("ShowOne");
            if (rp == players[i])
            {
                e.HighLightSelf();
                if (title) { title.text = "第" + (i+1) + "名"; }
                //将名次也设立
            }
        }
        positionRenew();
    }
    public void positionRenew()
    {
        float totalHeight;//设定总长度
        float nextPosition;
        totalHeight = targetHeight + (targetHeight + offset) * (list.Count - 1);
        float y = transform.position.y + selfHeight / 2;//上方长度
        RectTransform r = GetComponent<RectTransform>();
        GetComponent<RectTransform>().sizeDelta = new Vector2(r.rect.width, totalHeight);
        selfHeight = totalHeight;
        for (int i = 0; i < list.Count; i++)
        {
            nextPosition = selfHeight / 2 - targetHeight / 2 - (targetHeight + offset) * i;

            list[i].GetComponent<RectTransform>().localPosition = new Vector3(list[i].GetComponent<RectTransform>().localPosition.x, nextPosition);
        }
    }
}
