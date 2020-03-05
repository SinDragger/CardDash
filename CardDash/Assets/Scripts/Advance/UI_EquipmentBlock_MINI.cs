using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using wyc;
public class UI_EquipmentBlock_MINI : MonoBehaviour
{
    public Text equipmentName;
    public Image typeImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init(Weapon w)
    {
        if (typeImage)
        {
            Sprite temp = (Sprite)Resources.Load("Pictures/UI_2/人物_装备栏_武器", typeof(Sprite));
            typeImage.sprite = temp;
        }
        if (equipmentName)
        {
            equipmentName.text = w.name;
        }
    }
    public void Init(Equip e)
    {
        if (typeImage)
        {
            Sprite temp = (Sprite)Resources.Load("Pictures/UI_2/人物_装备栏_改装", typeof(Sprite));
            typeImage.sprite = temp;
        }
        if (equipmentName)
        {
            equipmentName.text = e.name;
        }

    }
}
