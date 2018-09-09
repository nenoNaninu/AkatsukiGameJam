using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neno.Scripts
{
    public class Exploder : MonoBehaviour
    {
        List<GameObject> enemyList = new List<GameObject>();
        List<GameObject> lineList = new List<GameObject>();

        void Update()
        {
            DrawEnemyCombineLine();
        }

        public void Explode(List<GameObject> enemyList, List<GameObject> lineList)
        {
            this.enemyList = enemyList;
            this.lineList = lineList;
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


