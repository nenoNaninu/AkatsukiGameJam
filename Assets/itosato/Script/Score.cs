using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Text Score_text;
    int s_n;
    Score_singleton score;

    // Use this for initialization
    void Start()
    {
        score = Score_singleton.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        s_n = score.getScore();
        Score_text.text = "Score : " + s_n.ToString();

    }
}
