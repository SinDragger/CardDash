using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LoadingPanel : MonoBehaviour
{
    List<UI_CardSlot_2> slots;
    List<UI_PlayerData_MINI> players;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void Init(List<wyc.ThugSource> data)
    {
        slots = new List<UI_CardSlot_2>();
        players = new List<UI_PlayerData_MINI>();
        for (int i = 0; i < transform.childCount; i++)
        {
            var temp = transform.GetChild(i);
            players.Add(temp.GetComponent<UI_PlayerData_MINI>());
            slots.Add(temp.GetComponent<UI_CardSlot_2>());
            slots[i].RenewData(data[i]);
            players[i].Renew("NPC" + i + "号");
        }
        players[0].Renew("你");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
