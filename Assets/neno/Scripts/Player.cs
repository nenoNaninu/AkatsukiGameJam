using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Neno.Scripts
{
    public class Player : MonoBehaviour
    {
        List<GameObject> enemyList = new List<GameObject>();
        List<GameObject> lineList = new List<GameObject>();
        [SerializeField]private float distance = 100f;
        private LineRenderer enemy2coursorLine;


        void Start()
        {
            GameObject tmp = new GameObject();
            enemy2coursorLine = tmp.AddComponent<LineRenderer>();
            tmp.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // クリックしたスクリーン座標をrayに変換
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // Rayの当たったオブジェクトの情報を格納する
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit, distance,1 << 8))
                {
                    enemyList.Add(hit.collider.gameObject);
                    CreateEnemyCombineLine();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                ExplodeEnemy();
            }

            DrawEnemyCombineLine();
            DrawEnemy2Coursor();

        }

        void DrawEnemy2Coursor()
        {
            if (enemyList != null)
            {
                if (enemyList.Count >= 1)
                {
                    this.enemy2coursorLine.gameObject.SetActive(true);
                    Vector3 enemyPos = this.enemyList[enemyList.Count - 1].transform.position;
                    Vector3 playerPos = gameObject.transform.position;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    float player2EnemyDistance = (enemyPos - playerPos).magnitude;
                    Debug.Log(player2EnemyDistance);
                    enemy2coursorLine.positionCount = 2;
                    enemy2coursorLine.startWidth = 0.1f;
                    enemy2coursorLine.endWidth = 0.1f;
                    enemy2coursorLine.startColor = Color.blue;
                    enemy2coursorLine.endColor = Color.cyan;
                    enemy2coursorLine.SetPosition(0, enemyPos);
                    enemy2coursorLine.SetPosition(1, ray.direction*player2EnemyDistance + ray.origin);
                }
            }
        }

        void ExplodeEnemy()
        {
            if (enemyList != null)
            {
                foreach (var enemy in enemyList)
                {
                    Destroy(enemy);
                }
            }

            if (lineList != null)
            {
                foreach (var lineObj in lineList)
                {
                    Destroy(lineObj);
                }
            }
            enemy2coursorLine.gameObject.SetActive(false);

            if (enemyList != null) enemyList.Clear();
            if (lineList != null) lineList.Clear();
            enemyList = new List<GameObject>();
            lineList = new List<GameObject>();
        }

        void DrawEnemyCombineLine()
        {
            if (lineList != null)
            {
                for (int i = 0; i < lineList.Count; i++)
                {
                    Vector3 startVec = enemyList[i].transform.position;
                    Vector3 endVec = enemyList[i + 1].transform.position;
                    LineRenderer lineRenderer = lineList[i].GetComponent<LineRenderer>();
                    lineRenderer.positionCount = 2;
                    lineRenderer.startWidth = 0.1f;
                    lineRenderer.endWidth = 0.1f;
                    lineRenderer.startColor = Color.blue;
                    lineRenderer.endColor = Color.cyan;
                    lineRenderer.SetPosition(0, startVec);
                    lineRenderer.SetPosition(1, endVec);
                }
            }
        }


        void CreateEnemyCombineLine()
        {
            if (enemyList != null)
            {
                if (1 < enemyList.Count)
                {
                    GameObject line = new GameObject("Line");
                    LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
                    lineList.Add(line);
                    Vector3 startVec = enemyList[enemyList.Count - 2].transform.position;
                    Vector3 endVec = enemyList[enemyList.Count - 1].transform.position;
                    lineRenderer.positionCount = 2;
                    lineRenderer.startWidth = 0.1f;
                    lineRenderer.endWidth = 0.1f;
                    lineRenderer.startColor = Color.blue;
                    lineRenderer.endColor = Color.cyan;
                    lineRenderer.SetPosition(0, startVec);
                    lineRenderer.SetPosition(1, endVec);
                }
            }
        }
    }
}


