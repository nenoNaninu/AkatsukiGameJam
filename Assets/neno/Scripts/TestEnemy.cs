using System.Collections;
using System.Collections.Generic;
using Neno.Scripts;
using UnityEngine;

namespace Neno.Scripts
{
    public class TestEnemy : MonoBehaviour, IEnemy
    {
        private int id = 0;

        void IEnemy.OnLink()
        {

        }

        public bool Combined
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
                throw new System.NotImplementedException();
            }
        }

        public EnemyType EnemyType
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
                throw new System.NotImplementedException();
            }
        }

        void IEnemy.Explode()
        {
            Destroy(gameObject);
        }

        void Update()
        {
            gameObject.transform.position += new Vector3(0,0,0.01f);
        }
    }
}

