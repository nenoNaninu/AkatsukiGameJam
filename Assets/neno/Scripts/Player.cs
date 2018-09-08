using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neno.Scripts
{
    public class Player : MonoBehaviour
    {
        List<GameObject> enemyList = new List<GameObject>();
        List<GameObject> lineList = new List<GameObject>();
        public float distance = 100f;

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
                    DrawEnemyCombineLine();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                ExplodeEnemy();
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
        }

        void DrawEnemyCombineLine()
        {
            if (enemyList != null)
            {
                for (int i = 1; i < enemyList.Count; i++)
                {
                    GameObject line = new GameObject("Line");
                    LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
                    lineList.Add(line);
                    Vector3 startVec = enemyList[i - 1].transform.position;
                    Vector3 endVec = enemyList[i].transform.position;
                    lineRenderer.positionCount = 2;
                    lineRenderer.startWidth = 0.01f;
                    lineRenderer.endWidth = 0.01f;
                    lineRenderer.startColor = Color.blue;
                    lineRenderer.endColor = Color.cyan;
                    lineRenderer.SetPosition(0, startVec);
                    lineRenderer.SetPosition(1, endVec);
                }
            }
        }
    }
}


