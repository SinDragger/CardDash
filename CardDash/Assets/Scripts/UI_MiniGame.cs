using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MiniGame : Basic_MINIGame
{
    //public bool isActive = false;
    //public Animation drive;
    //public bool driveActive;

    //public Animation melee;
    //public bool meleeActive;

    //public Animation shoot;
    //public bool shootActive;
    //public List<Animation> targets;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void ReverseAndPlayAnimation(int type)
    {
        Animation target = null;
        bool flag=true;
        switch (type)
        {
            case 0:
                driveActive = !driveActive;
                flag = driveActive;
                target = drive;
                break;
            case 1:
                meleeActive = !meleeActive;
                flag = meleeActive;
                target = melee;
                break;
            case 2:
                shootActive = !shootActive;
                flag = shootActive;
                target = shoot;
                break;
        }
        if (flag)
        {
            target.Play("insert");
        }
        else
        {
            target.Play("desert");
        }
    }
    public override void SetAllTo(bool toFlag)
    {
        if (driveActive != toFlag)
        {
            ReverseAndPlayAnimation(0);
        }
        if (meleeActive != toFlag)
        {
            ReverseAndPlayAnimation(1);
        }
        if (shootActive != toFlag)
        {
            ReverseAndPlayAnimation(2);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
