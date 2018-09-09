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
        [SerializeField] private Transform oculusgoController;

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
#if UNITY_EDITOR

            if (Input.GetMouseButtonDown(0) )
            {
                // クリックしたスクリーン座標をrayに変換
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // Rayの当たったオブジェクトの情報を格納する
                CombineRequest(ray);
            }
            if (Input.GetMouseButtonDown(1))
            {
                ExplodeRequest();
            }

#endif
            if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
            {
                Ray ray = new Ray(oculusgoController.position,oculusgoController.forward);
                CombineRequest(ray);
            }

            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                ExplodeRequest();
            }

            DrawEnemyCombineLine();
            DrawEnemy2Coursor();

        }
        void CombineRequest(Ray ray)
        {
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, distance, 1 << 8))
            {
                IEnemy enemy = hit.collider.GetComponent<IEnemy>();
                if (enemy != null && !enemy.Combined)
                {
                    enemy.Combined = true;
                    enemyList.Add(hit.collider.gameObject);
                    CreateEnemyCombineLine();
                    enemy.OnLink();
                }
            }
        }

        void ExplodeRequest()
        {
            explosionFlag = true;
            GameObject expLinker = new GameObject();
            Exploder exploder = expLinker.AddComponent<Exploder>();
            exploder.Explode(this.enemyList, this.lineList);
            this.enemyList = new List<GameObject>();
            this.lineList = new List<GameObject>();
            this.enemy2CoursorLine.gameObject.SetActive(false);
            explosionFlag = false;
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
                    DrawLine(enemy2CoursorLine, enemyPos, ray.direction * player2EnemyDistance + ray.origin);
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
                    DrawLine(lineRenderer,startVec, endVec);
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


