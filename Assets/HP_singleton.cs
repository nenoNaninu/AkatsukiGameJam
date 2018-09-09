using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_singleton : MonoBehaviour {

    public Text HpText;
    private static HP_singleton Score;
    private int h_num = 0;

    public static HP_singleton Instance
    {
        get
        {
            if (Score == null)
            {
                GameObject obj = new GameObject("HP_singleton");
                Score = obj.AddComponent<HP_singleton>();
            }
            return Score;
        }
        set
        {
        }
    }

    public void setScore(int h)
    {
        this.h_num = h;
    }
    public int getScore()
    {
        return this.h_num;
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HpText.text = "HP : " + this.h_num.ToString();
    }
}
