using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum EnemyType{
    Test,
    Test2,
}

public class Enemy : MonoBehaviour
{
    public EnemyType type = EnemyType.Test;

    [SerializeField]
    public GameObject popParticlePrefab;

    [SerializeField]
    private AudioSource selectedVoice;

    private void Start()
    {

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
}
