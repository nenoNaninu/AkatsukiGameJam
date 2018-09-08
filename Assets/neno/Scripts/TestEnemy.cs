using System.Collections;
using System.Collections.Generic;
using Neno.Scripts;
using UnityEngine;

public class TestEnemy : MonoBehaviour,IEnemy
{
    private int id = 0;
    int IEnemy.Id
    {
        get { return id; }
        set { id = value; }
    }

    void IEnemy.Explode()
    {
        Destroy(gameObject);
    }
}
