using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BicycleRider
{
    public enum ObstacleType
    {
        Moto,
        Car,
        Rail
    }
    public class Obstacle : MonoBehaviour
    {
        [SerializeField]
        float speed = -10;
        [SerializeField]
        ObstacleType obstacleType;
        private int direction;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void MoveBack()
        {
            direction = -1;
            StartCoroutine(CR_MoveBack());
        }
        public void MoveToFront()
        {
            direction = 1;
            StartCoroutine(CR_MoveToFront());
        }
        IEnumerator CR_MoveBack()
        {
            while (GameManager.Instance.GameState == GameState.Playing)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
        IEnumerator CR_MoveToFront()
        {
            while (GameManager.Instance.GameState == GameState.Playing)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }

        public void SetupPositionOnLane(float widthLane)
        {
            switch (obstacleType)
            {
                case ObstacleType.Car:
                    {
                        transform.position = new Vector3(direction * widthLane / 8, transform.position.y, transform.position.z); // Nam sat giua duong
                        break;
                    }
                case ObstacleType.Moto:
                    {
                        transform.position = new Vector3(direction * widthLane / 8 * 3, transform.position.y, transform.position.z); // Nam sat ngoai le
                        break;
                    }
            }
        }

        public void SetUpRotationOnLane()
        {
            if (direction > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (direction < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
    }
}