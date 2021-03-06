﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Neno.Scripts;

public enum EnemyType
{
    Blue = 0,
    Green,
    Orange,
    Purple,
    Red
}

public class Enemy : MonoBehaviour, IEnemy
{
    public EnemyType type = EnemyType.Red;
    private Player player;

    public bool Accelerating { get; set; }

    public EnemyType EnemyType
    {
        get { return type; }
        set { type = value; }
    }

    [SerializeField]
    private GameObject popParticlePrefab;

    [SerializeField]
    private GameObject selectedMaker;

    [SerializeField]
    private AudioSource selectedVoice;

    private bool Connected = false;

    public bool Combined
    {
        get { return Connected; }
        set { Connected = value; }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Accelerating = false;
        transform.localScale = new Vector3(2, 2, 2);
        transform.DOShakeScale(3f, 0.2f, 0, 10).SetLoops(-1);
        selectedMaker.SetActive(false);
    }

    void Update()
    {
        if (Accelerating)
        {
            return;
        }

        Vector3 direction = player.transform.position - gameObject.transform.position;
        direction = direction.normalized;

        gameObject.transform.position += direction * 0.05f;
    }

    public void Init(EnemyType type)
    {
        this.type = type;
    }

    // 繋がったとき
    public void OnLink()
    {
        transform.DOScale(transform.localScale.x * 1.2f, 0.2f);
        selectedMaker.SetActive(true);
    }

    // 繋がりがなくなったとき
    public void OnUnLink()
    {
        transform.DOScale(1f, 0.2f);
        selectedMaker.SetActive(false);
    }

    // 爆発する
    public void Pop()
    {
        selectedMaker.SetActive(false);
        transform.DOScale(transform.localScale.x * 2f, 0.1f).OnComplete(() =>
        {
            GameObject particle = Instantiate(popParticlePrefab);
            particle.transform.localPosition = transform.position;
            Destroy(particle, 0.5f);
            gameObject.SetActive(false);
            Destroy(this.gameObject, 1f);
        });
    }

    public void Explode()
    {
        EnemyManager.CurrentEnemyNum--;
        Pop();
    }
}
