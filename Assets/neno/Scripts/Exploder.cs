using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neno.Scripts
{
    public class Exploder : MonoBehaviour
    {
        List<GameObject> enemyList = new List<GameObject>();
        List<GameObject> lineList = new List<GameObject>();

        private EnemyType enemyType;

        void Update()
        {
            DrawEnemyCombineLine();
        }

        public void Explode(List<GameObject> enemyList, List<GameObject> lineList)
        {
            this.enemyList = enemyList;
            this.lineList = lineList;
            enemyType = enemyList[enemyList.Count - 1].GetComponent<IEnemy>().EnemyType;
            StartCoroutine(ExplodeEnemyCombine());
        }

        IEnumerator ExplodeEnemyCombine()
        {

            if (enemyList == null)
            {
                yield break;
            }

            for (int enemyIndex = enemyList.Count - 1; 0 <= enemyIndex; enemyIndex--)
            {
                IEnemy currentEnemy = enemyList[enemyIndex].GetComponent<IEnemy>();

                if (currentEnemy.EnemyType != this.enemyType)
                {
                    //違うやつがつなげられてた
                    Rigidbody mistakeEnemy = enemyList[enemyIndex].GetComponent<Rigidbody>();
                    Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                    mistakeEnemy.AddForce((player.transform.position - mistakeEnemy.transform.position).normalized * 50,ForceMode.Impulse);
                    foreach (var item in enemyList)
                    {
                        IEnemy enemy = item.GetComponent<IEnemy>();
                        enemy.Combined = false;
                        enemy.OnUnLink();
                    }

                    foreach (var line in lineList)
                    {
                        Destroy(line);
                    }

                    lineList.Clear();
                    enemyList.Clear();
                    yield break;
                }

                enemyList.Remove(enemyList[enemyIndex]);
                currentEnemy.Explode();

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

                    while (ignittionTime >= 0)
                    {
                        ignittionTime -= Time.deltaTime;
                        Vector3 endPos = Vector3.Lerp(endPoint, startPoint, (Time.time - timeStamp) / 0.5f);
                        ignittionLine.SetPosition(1, endPos);
                        yield return null;
                    }
                    Destroy(lineObj);

                    yield return null;
                }
            }
            Destroy(gameObject);
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
                    DrawLine(lineRenderer, startVec, endVec);
                }
            }
        }

        void DrawLine(LineRenderer lineRenderer, Vector3 start, Vector3 end)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.startColor = Color.blue;
            lineRenderer.endColor = Color.cyan;
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
        }
    }
}


