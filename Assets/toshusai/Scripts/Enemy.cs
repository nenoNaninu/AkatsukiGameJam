using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Neno.Scripts;

public enum EnemyType{
    Blue,
    Green,
    Orange,
    Purple,
    Red
}

public class Enemy : MonoBehaviour,IEnemy
{
    public EnemyType type = EnemyType.Red;

    public EnemyType EnemyType
    {
        get { return type; }
        set { type = value; }
    }

    [SerializeField]
    public GameObject popParticlePrefab;

    [SerializeField]
    private AudioSource selectedVoice;

    private bool Connected = false;

    public bool Combined
    {
        get { return Connected; }
        set { Connected = value; }
    }

    public void Init(EnemyType type)
    {
        this.type = type;
    }

    // 繋がったとき
    public void OnLink(){
        transform.DOScale(1.2f, 0.2f);
    }

    // 繋がりがなくなったとき
    public void OnUnLink(){
        transform.DOScale(1f, 0.2f);
    }

    // 爆発する
    public void Pop(){
        transform.DOScale(2f, 0.1f).OnComplete(() =>
        {
            GameObject particle = Instantiate(popParticlePrefab);
            particle.transform.localPosition = transform.position;
            Destroy(this.gameObject);
        });
    }

    public void Explode()
    {
        Pop();
    }
}
