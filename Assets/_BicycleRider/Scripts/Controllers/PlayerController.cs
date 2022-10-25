using UnityEngine;
using System.Collections;

namespace BicycleRider
{
    public class PlayerController : MonoBehaviour
    {
        public static event System.Action PlayerDied;
        public static event System.Action PlayerWin;
        public static event System.Action onPlayerTriggerMapBounds;


        public LayerMask groundLayer;

        private Rigidbody2D playerRigidbody;

        [SerializeField]
        private float maxSpeedY = 20f;
        [SerializeField]
        private float acceleration = 15.0f;

        private int direction = 0;
        [SerializeField]
        private float speedX = 5;
        [SerializeField]
        private float speedY = -5;

        private bool grounded;
        void OnEnable()
        {
            GameManager.GameStateChanged += OnGameStateChanged;
        }

        void OnDisable()
        {
            GameManager.GameStateChanged -= OnGameStateChanged;
        }

        void Start()
        {
            playerRigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {

            if (GameManager.Instance.GameState == GameState.Playing)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (Input.mousePosition.x < Screen.width / 2)
                    {
                        MoveLeft();
                    }
                    else if (Input.mousePosition.x > Screen.width / 2)
                    {
                        MoveRight();
                    }
                }
                if (Input.GetMouseButtonUp(0)) direction = 0;
                transform.position = (Vector2) transform.position +  new Vector2(speedX * direction * Time.deltaTime, speedY * Time.deltaTime);
            }
        }



        void FixedUpdate()
        {
            if (GameManager.Instance.GameState == GameState.Playing)
            {
            }
        }

        // Listens to changes in game state
        void OnGameStateChanged(GameState newState, GameState oldState)
        {
            if (newState == GameState.Playing)
            {

            }
        }

        // Calls this when the player dies and game over
        IEnumerator Die()
        {
            if (SoundManager.Instance.background != null)
            {
                SoundManager.Instance.StopMusic();
            }

            SoundManager.Instance.PlaySound(SoundManager.Instance.gameOver);
            // Delay end game
            yield return new WaitForSeconds(2f);
            // Fire event
            if (PlayerDied != null)
                PlayerDied();
        }



        private void SpeedUp()
        {
        }

        //private IEnumerator CR_SpeedUp()
        //{
        //    while (holding)
        //    {
        //        velocity += acceleration * Time.deltaTime;
        //        transform.position += (velocity*Time.deltaTime + 0.5f* acceleration * Time.deltaTime * Time.deltaTime) * foward.normalized;
        //        if (velocity >= maxSpeed)
        //        {
        //            velocity = maxSpeed;
        //        }
        //        yield return new WaitForEndOfFrame();
        //    }
        //}

        //private void SpeedDown()
        //{
        //    StartCoroutine(CR_SpeedDown());
        //}

        //private IEnumerator CR_SpeedDown()
        //{
        //    while (!holding)
        //    {
        //        velocity -= acceleration * Time.deltaTime;
        //        if (velocity < 0)
        //        {
        //            velocity = 0;
        //        }
        //        else
        //        {
        //            transform.position += (velocity * Time.deltaTime + 0.5f * acceleration * Time.deltaTime * Time.deltaTime) * foward.normalized;
        //        }
        //        yield return new WaitForEndOfFrame();
        //    }
        //}

        private void MoveLeft()
        {
            if (direction > -1)
                direction--;
        }

        private void MoveRight()
        {
            if (direction < 1)
                direction++;
        } 

  


        private IEnumerator CR_MoveLeft(float percent)
        {
            //Debug.Log("Move Left");
            //float ratio = 0.1f;
            //Vector3 startPosition = transform.position;
         //   Vector3 targetPosition = new Vector3(startPositionPlayerMouseDown.x + MapManager.Instance.WidthRoad * percent,transform.position.y, transform.position.z);
            //float timeToReachTarget = turnSpeed / Mathf.Abs(startPosition.x - targetPosition.x);
            //float startTime = Time.time;
            //while (holding)
            //{

            //    float percentMoving = (Time.time - startTime) / timeToReachTarget;
            //    transform.position = Vector3.Lerp(startPosition, targetPosition, percentMoving);
            //    Debug.Log("Percent " + percentMoving + timeToReachTarget);
            //    Debug.Log("Start Position " + startPosition + "targetPosition " + targetPosition);

                yield return new WaitForEndOfFrame();
            //}
        }

        // Xu li va cham


		private void OnTriggerEnter2D(Collider2D collision)
		{
            Debug.LogError(collision.gameObject.tag);
            if (collision.gameObject.CompareTag("Map"))
            {
                onPlayerTriggerMapBounds?.Invoke();
            }
        }
		

        private void StartSpamTouch()
        {

        }
      

        private void CheckGrounded()
        {
          
        }


    }
}