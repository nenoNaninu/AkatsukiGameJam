using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neno.Scripts
{
    public class Exploder : MonoBehaviour
    {
        List<GameObject> enemyList = new List<GameObject>();
        List<GameObject> lineList = new List<GameObject>();


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
    }
}


