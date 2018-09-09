using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_damage : MonoBehaviour {
    public Image damage;
    HP_singleton hp;
    public int damage_num;
    public float damage_time;
    int hp_get;

	// Use this for initialization
	void Start () {
        hp = HP_singleton.Instance;
        D_time();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Enemy"){
            hp_get = hp.getScore();
            hp_get -= damage_num;
            hp.setScore(hp_get);

            Debug.Log("hit");

            damage.enabled = true;

            Invoke("D_time",damage_time);
        }
    }

    void D_time()
    {
        damage.enabled = false;
    }
}
