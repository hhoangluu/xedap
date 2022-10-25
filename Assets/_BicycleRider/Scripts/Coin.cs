using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BicycleRider {
    public class Coin : MonoBehaviour
    {
        [SerializeField]
        private float rotationSpeed;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            SelfRotate();
        }

        private void SelfRotate()
        {
            transform.eulerAngles += new Vector3(0, rotationSpeed * Time.deltaTime, 0);
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                CoinManager.Instance.AddCoins(1);
                Destroy(gameObject);
            }
        }
    }
}
