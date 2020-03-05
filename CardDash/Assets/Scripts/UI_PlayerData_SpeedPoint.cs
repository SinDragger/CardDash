using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayerData_SpeedPoint : MonoBehaviour
{
    public float startRotation;
    public float endRotation;
    public float nowRotation;
    public float targetRotation;
    public float rotationSpeed;
    public float maxSpeed;

    public float randomDeviation;//正负幅度
    public GameObject pointer;

    // Start is called before the first frame update
    void Start()
    {
        targetRotation = startRotation;
        nowRotation = startRotation;
        if (pointer != null)
        {
            pointer.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, startRotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePointer(RandomDeviation(targetRotation), FixedRotationSpeed(rotationSpeed));
    }
    void UpdatePointer(float targetRotation,float rotationSpeed)
    {
        //更新nowRotation;
        if (nowRotation != targetRotation)
        {
            if (Mathf.Abs(nowRotation - targetRotation) < 1f)
            {
                return;
            }

            if (nowRotation < targetRotation)
            {
                nowRotation += rotationSpeed*Time.deltaTime;
                if (nowRotation > targetRotation)
                {
                    nowRotation = targetRotation;
                }
            }
            else
            {
                nowRotation -= rotationSpeed * Time.deltaTime;
                if (nowRotation < targetRotation)
                {
                    nowRotation = targetRotation;
                }

            }
            if (pointer != null)
            {
                pointer.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, nowRotation);
                //pointer.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, targetRotation);

            }
        }
    }
    float FixedRotationSpeed(float rotationSpeed)
    {
        if(nowRotation != targetRotation)
        {
            return rotationSpeed + Mathf.Pow(Mathf.Abs((nowRotation - targetRotation) / Mathf.Abs(startRotation - endRotation)),2f) * rotationSpeed*100f;
        }
        return rotationSpeed;
        
    }
    float RandomDeviation(float targetRotation)
    {
        float temp=targetRotation;
        if (targetRotation == startRotation)
        {
            return startRotation;
        }
        temp += Random.Range(-randomDeviation, randomDeviation);
        return temp;
    }
    public bool SetNewRotation(float nowSpeed)
    {
        float cen = nowSpeed / maxSpeed;
        if (cen > 1f)
        {
            cen = 1f;
        }
        targetRotation = startRotation + (endRotation - startRotation) * cen;
        return false;
    }
}
