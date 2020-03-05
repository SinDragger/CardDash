using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerData_MINI : MonoBehaviour
{
    public Text playerName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Renew(string s)
    {
        if (playerName) { playerName.text = s; }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
