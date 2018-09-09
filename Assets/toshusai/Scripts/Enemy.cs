using System.Collections;
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
    public GameObject popParticlePrefab;

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
        transform.DOScale(1.2f, 0.2f);
    }

    // 繋がりがなくなったとき
    public void OnUnLink()
    {
        transform.DOScale(1f, 0.2f);
    }

    // 爆発する
    public void Pop()
    {
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
