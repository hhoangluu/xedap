using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BicycleRider
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance { get; private set; }
        
        [SerializeField]
        private float widthRoad;
        [SerializeField]
        private int distanceSpawnOstacle;
        [SerializeField]
        private float distanceSpawnObstacleFromPlayer;
        public List<GameObject> obstaclesAvailable;
        public List<Obstacle> obstacles;

      

        [SerializeField]
        GameObject coinLine;
        [SerializeField]
        float minZPosSpawn;
        [SerializeField]
        float maxZPosSpawn;
        public int distanceCount = 1;
        public float WidthRoad { get => widthRoad; set => widthRoad = value; }

        [SerializeField]
        private float distanceSpawnMap;
        private List<PointBonus> listPointBonus = new List<PointBonus>();

        public List<MapPiece> mapPieces = new List<MapPiece>();
        public List<GameObject> mapPiecesPrefabs = new List<GameObject>();

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                DestroyImmediate(Instance.gameObject);
                Instance = this;
            }
            //  CreateNewCharacter(CharacterManager.Instance.CurrentCharacterIndex);
        }

        void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        private void OnEnable()
        {
            GameManager.GameStateChanged += GameManager_GameStateChanged;
			PlayerController.onPlayerTriggerMapBounds += PlayerController_onPlayerTriggerMapBounds;
        }

		private void PlayerController_onPlayerTriggerMapBounds()
		{
            SpawnMap();
		}

		private void OnDisable()
        {
            GameManager.GameStateChanged -= GameManager_GameStateChanged;
            PlayerController.onPlayerTriggerMapBounds -= PlayerController_onPlayerTriggerMapBounds;

        }
        private void GameManager_GameStateChanged(GameState newState, GameState oldState)
        {
            if (newState == GameState.Playing && oldState == GameState.Prepare)
                SpawnMap();
            //    LoadMapLevel();
            StartCoroutine(SpawnObstacleInterval());
        }


        // Update is called once per frame
        void Update()
        {

        }

        private void Start()
        {

        }
        public void DestroyObstacle(Obstacle obstacle)
        {
            obstacles.Remove(obstacle);
            Destroy(obstacle.gameObject);
        }

        List<Obstacle> remainObs = new List<Obstacle>();
        void LoadMapLevel()
        {
            int numberObstacle = 5;
            
            Vector3 firstObstacleDistance = new Vector3(0, 0, distanceSpawnOstacle);
            int curIndex  = InstanceObstacle(firstObstacleDistance,-1);

            for (int i = 1; i < numberObstacle; i++)
            {
                curIndex = InstanceObstacle(obstacles[obstacles.Count - 1].transform.position, curIndex);
            }
        }


        public int InstanceObstacle(Vector3 curObstaclePosition,int curObstacleIndex)
        {
            int randIndex = Random.Range(0, obstaclesAvailable.Count);
            while (randIndex == curObstacleIndex)
            {
                randIndex = Random.Range(0, obstaclesAvailable.Count);
            }
            float positionXObstacle = Random.Range(0f, 1f) > 0.5 ? widthRoad / 4 : -widthRoad / 4;

            Vector3 positionObstacle = new Vector3(positionXObstacle, 0, curObstaclePosition.z + distanceSpawnOstacle);

            GameObject obstacle = Instantiate(obstaclesAvailable[randIndex].gameObject, positionObstacle, Quaternion.identity);
            obstacles.Add(obstacle.GetComponent<Obstacle>());

            //float positionXObstacle = Random.Range(0f, 1f) > 0.5 ? widthRoad / 4 : -widthRoad / 4;
            //Vector3 positionObstacle = new Vector3(positionXObstacle, 0, curObstaclePosition.z + distanceSpawnObstacleFromPlayer);

            //GameObject obstacle = Instantiate(obstaclesAvailable[randIndex].gameObject, positionObstacle, Quaternion.identity);

            //obstacles.Add(obstacle.GetComponent<Obstacle>());
            if (positionXObstacle < 0)
            {
                obstacles[obstacles.Count - 1].MoveBack();
            }
            else
            {
                obstacles[obstacles.Count - 1].MoveToFront();
            }
            obstacles[obstacles.Count - 1].SetupPositionOnLane(widthRoad);
            obstacles[obstacles.Count - 1].SetUpRotationOnLane();
            return randIndex;
        }


        IEnumerator SpawnObstacleInterval()
        {
            while (GameManager.Instance.GameState == GameState.Playing)
            {
                yield return new WaitForEndOfFrame();
                if (GameManager.Instance.playerController.transform.position.z > distanceCount)
                {
                    distanceCount += distanceSpawnOstacle;
                  //  InstanceObstacle(obstacles.Count >0 ? obstacles[obstacles.Count -1].transform.position : Vector3.zero);
                }
            }
        }

        void SpawnMap()
        {
            int randomIndex = Random.Range(0, mapPiecesPrefabs.Count);
            if (mapPieces.Count == 0)
            {
                var map = Instantiate(mapPiecesPrefabs[randomIndex].gameObject, Vector3.zero, Quaternion.identity);
                mapPieces.Add(map.GetComponent<MapPiece>());
            }
            else
            {
                Vector3 positionNewPiece = (Vector2)mapPieces[mapPieces.Count - 1].transform.position + new Vector2(0, distanceSpawnMap);
                var map = Instantiate(mapPiecesPrefabs[randomIndex].gameObject, positionNewPiece, Quaternion.identity);
                mapPieces.Add(map.GetComponent<MapPiece>());
            }

        }
    }
}