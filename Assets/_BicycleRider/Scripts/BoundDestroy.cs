using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BicycleRider
{

    public class BoundDestroy : MonoBehaviour
    {
        private Transform playerTransform; 
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("Obstacle"))
            {
                MapManager.Instance.DestroyObstacle(col.gameObject.GetComponent<Obstacle>());
            }
        }
        Vector3 originalDistance;
        void FollowPlayer()
        {
            if (playerTransform != null)
            {
                transform.position = playerTransform.position + originalDistance;
            }

        }

        IEnumerator WaittingForPlayerTransform()
        {
            yield return new WaitWhile(() => GameManager.Instance.playerController.transform == null);
            playerTransform = GameManager.Instance.playerController.transform;
            originalDistance = transform.position - playerTransform.transform.position;

        }
    }
}