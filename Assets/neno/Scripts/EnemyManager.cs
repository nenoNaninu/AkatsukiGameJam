using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Neno.Scripts
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> enemyList;//5種類

        [SerializeField] private float interval = 0.5f;

        private Player player;
        private float timeStamp = 0;
        private const int restrictionEnemyNum = 150;
        public static int CurrentEnemyNum { get; set; }

        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            //最初に200体くらいポップさっせる
            for (int i = 0; i < 100; i++)
            {
                Vector3 popPosition = CreatePosition();

                int enemyType = (int)Random.Range(0.0f, 5.0f);
                IEnemy enemy = Instantiate(enemyList[enemyType], popPosition, Quaternion.LookRotation(player.transform.position - popPosition)).GetComponent<IEnemy>();
                enemy.EnemyType = (EnemyType)Enum.ToObject(typeof(EnemyType), enemyType);
            }
            CurrentEnemyNum = 100;
        }

        // Update is called once per frame

        void Update()
        {
            if (timeStamp + interval < Time.time)
            {
                if (CurrentEnemyNum < restrictionEnemyNum)
                {
                    Vector3 popPosition = CreatePosition();
                    int enemyType = (int)Random.Range(0.0f, 5.0f);

                    IEnemy enemy = Instantiate(enemyList[enemyType], popPosition, Quaternion.LookRotation(player.transform.position - popPosition)).GetComponent<IEnemy>();
                    enemy.EnemyType = (EnemyType)Enum.ToObject(typeof(EnemyType), enemyType);
                    CurrentEnemyNum++;
                }
            }
        }

        Vector3 CreatePosition()
        {
            float x = Random.Range(-100f, 100f);
            float y = Random.Range(-100f, 100f);
            float z = Random.Range(50f, 100f);

            Vector3 position = new Vector3(x, y, z);
            return position;
        }
    }

}

