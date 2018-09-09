using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour {
    public Text HP_text;
    int hp_n;
    HP_singleton hp;

    // Use this for initialization
    void Start () {
        hp = HP_singleton.Instance;
    }
	
	// Update is called once per frame
	void Update () {
        hp_n = hp.getScore();
        HP_text.text = "HP : " + hp_n.ToString();

	}
}
