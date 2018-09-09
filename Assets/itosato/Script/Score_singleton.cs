using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_singleton : MonoBehaviour {


    private static Score_singleton Score;
    private int s_num = 0;

    public static Score_singleton Instance{
        get{
            if (Score == null){
                GameObject obj = new GameObject("Score_singleton");
                Score = obj.AddComponent<Score_singleton>();
            }
            return Score;
        }
        set{
        }
    }

    public void setScore(int s){
        this.s_num = s;
    }
    public int getScore(){
        return this.s_num;
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
