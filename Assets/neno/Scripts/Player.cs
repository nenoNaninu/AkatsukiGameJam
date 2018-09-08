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
        [SerializeField] private float distance = 100f;
        private LineRenderer enemy2CoursorLine;

        private bool explosionFlag = false;

        void Start()
        {
            GameObject tmp = new GameObject();
            enemy2CoursorLine = tmp.AddComponent<LineRenderer>();
            enemy2CoursorLine.material = new Material(Shader.Find("Sprites/Default"));
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
                if (Physics.Raycast(ray, out hit, distance, 1 << 8))
                {
                    enemyList.Add(hit.collider.gameObject);
                    CreateEnemyCombineLine();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                //RemoveEnemyMoment();
                explosionFlag = true;
                StartCoroutine(ExplodeEnemyCombine());
            }

            DrawEnemyCombineLine();
            DrawEnemy2Coursor();

        }

        void DrawEnemy2Coursor()
        {
            if (explosionFlag)
            {
                return;
            }

            if (enemyList != null)
            {
                if (enemyList.Count >= 1)
                {
                    this.enemy2CoursorLine.gameObject.SetActive(true);
                    Vector3 enemyPos = this.enemyList[enemyList.Count - 1].transform.position;
                    Vector3 playerPos = gameObject.transform.position;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    float player2EnemyDistance = (enemyPos - playerPos).magnitude;
                    enemy2CoursorLine.positionCount = 2;
                    Debug.Log(player2EnemyDistance);
                    enemy2CoursorLine.positionCount = 2;
                    enemy2CoursorLine.startWidth = 0.1f;
                    enemy2CoursorLine.endWidth = 0.1f;
                    enemy2CoursorLine.startColor = Color.blue;
                    enemy2CoursorLine.endColor = Color.cyan;
                    enemy2CoursorLine.SetPosition(0, enemyPos);
                    enemy2CoursorLine.SetPosition(1, ray.direction * player2EnemyDistance + ray.origin);
                }
            }
        }

        void RemoveEnemyMoment()
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
            enemy2CoursorLine.gameObject.SetActive(false);

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
                    if (enemyList.Count <= i + 1)
                    {
                        return;
                    }
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
                    lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

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

        IEnumerator ExplodeEnemyCombine()
        {

            if (enemyList == null)
            {
                yield break;
            }

            if (enemyList.Count > 0)
            {
                enemy2CoursorLine.gameObject.SetActive(false);
            }

            for (int enemyIndex = enemyList.Count - 1; 0 <= enemyIndex; enemyIndex--)
            {
                IEnemy enemy = enemyList[enemyIndex].GetComponent<IEnemy>();
                enemyList.Remove(enemyList[enemyIndex]);
                enemy.Explode();

                //ラインを徐々に消す処理
                //yield return new WaitForSeconds(0.5f);
                if (1 <= lineList.Count)
                {
                    GameObject lineObj = lineList[lineList.Count - 1];
                    lineList.Remove(lineObj);

                    float ignittionTime = 0.5f;
                    Vector3[] linePosition = new Vector3[2];
                    LineRenderer ignittionLine = lineObj.GetComponent<LineRenderer>();
                    
                    ignittionLine.GetPositions(linePosition);

                    Vector3 startPoint = linePosition[0];
                    Vector3 endPoint = linePosition[1];
                    float timeStamp = Time.time;
                        
                    while (ignittionTime >=0)
                    {
                        ignittionTime -= Time.deltaTime;
                        Vector3 endPos = Vector3.Lerp(endPoint, startPoint,(Time.time - timeStamp) / 0.5f);
                        ignittionLine.SetPosition(1,endPos);
                        yield return null;
                    }
                    Destroy(lineObj);

                    yield return null;
                }
            }
            explosionFlag = false;
        }

    }
}


