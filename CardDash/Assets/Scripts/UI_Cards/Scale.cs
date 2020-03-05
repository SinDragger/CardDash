using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scale : MonoBehaviour
{
    // Start is called before the first frame update
    public float scale;
    float oldScale;
    public RectTransform self;
    //public List<Vector2> ori;
    //public List<Vector3> pos;
    //public List<Text> tex;
    //public List<int> fontsize;
    void Start()
    {
        self = GetComponent<RectTransform>();
        //foreach(RectTransform temp in GetComponentInChildren<RectTransform>())
        //{
        //    Debug.Log("get");
        //    self.Add(temp);
        //    ori.Add(temp.sizeDelta);
        //    pos.Add(temp.localPosition);
        //    Text newText = temp.GetComponent<Text>();
        //    if (newText != null)
        //    {
        //        tex.Add(newText);
        //        fontsize.Add(newText.fontSize);
        //    }
        //}


    }

    // Update is called once per frame
    void Update()
    {
        if (oldScale != scale)
        {
            self.localScale = new Vector3(scale, scale, 1);
            //for(int i = 0; i < self.Count; i++)
            //{
            //    self[i].sizeDelta = new Vector2(ori[i].x * scale, ori[i].y * scale);
            //    self[i].localPosition = new Vector3(pos[i].x * scale, pos[i].y * scale, pos[i].z * scale);
            //}
            //for (int i = 0; i < tex.Count; i++)
            //{
            //    tex[i].fontSize = (int)(fontsize[i] * scale);
            //}
        }
        oldScale = scale;
    }
}
