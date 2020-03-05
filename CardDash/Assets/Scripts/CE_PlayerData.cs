using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CE_PlayerData : MonoBehaviour
{
    public Text wealthText;
    public Text driveText;
    public Text strengthText;
    public Text healthText;
    public Image playerImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DataUpdate(wyc.ThugSource player)
    {
        if (wealthText != null) { wealthText.text = "Money:" + player.wealth.ToString()+",000"; }
        if (driveText != null) { driveText.text = "Drive:" + player.drive.ToString(); }
        if (strengthText != null) { strengthText.text = "Strength:" + player.strength.ToString() ; }
        if (healthText != null) { healthText.text = "Health:" + player.health.ToString(); }
        if (playerImage != null) {
            //Sprite temp = (Sprite)Resources.Load("Pictures/Thug/" + player.name, typeof(Sprite));
            Sprite temp = (Sprite)Resources.Load("Pictures/Thug/" + player.name, typeof(Sprite));
            playerImage.sprite = temp;
        }
    }
}
