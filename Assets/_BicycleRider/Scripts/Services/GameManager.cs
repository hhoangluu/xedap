using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

namespace BicycleRider
{
    public enum GameState
    {
        Prepare,
        Playing,
        Paused,
        PreGameOver,
        GameOver
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public static event System.Action<GameState, GameState> GameStateChanged;

        private static bool isRestart;


        public GameState GameState
        {
            get
            {
                return _gameState;
            }
            private set
            {
                if (value != _gameState)
                {
                    GameState oldState = _gameState;
                    _gameState = value;

                    if (GameStateChanged != null)
                        GameStateChanged(_gameState, oldState);
                }
            }
        }

        public static int GameCount
        {
            get { return _gameCount; }
            private set { _gameCount = value; }
        }

        private static int _gameCount = 0;

        [Header("Set the target frame rate for this game")]
        [Tooltip("Use 60 for games requiring smooth quick motion, set -1 to use platform default frame rate")]
        public int targetFrameRate = 30;

        [Header("Current game state")]
        [SerializeField]
        private GameState _gameState = GameState.Prepare;

        // List of public variable for gameplay tweaking
        [Header("Gameplay Config")]

        [SerializeField]
        private Vector3 startPlayerPosition;



        [Range(0f, 1f)]
        public float coinFrequency = 0.1f;
        [Range(0f, 1f)]
        public float magnetFrequency = 0.1f;
        [Range(0f, 1f)]
        public float starFrequency = 0.2f;

        // List of public variables referencing other objects
        [Header("Object References")]
        public PlayerController playerController;

        void OnEnable()
        {
            PlayerController.PlayerDied += PlayerController_PlayerDied;
            PlayerController.PlayerWin += PlayerController_PlayerWin;

        }

      

        void OnDisable()
        {
            PlayerController.PlayerWin -= PlayerController_PlayerWin;
            PlayerController.PlayerDied -= PlayerController_PlayerDied;
        }

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

        // Use this for initialization
        void Start()
        {
            // Initial setup
            Application.targetFrameRate = targetFrameRate;
          //  ScoreManager.Instance.Reset();

            PrepareGame();
        }

        // Update is called once per frame
        void Update()
        {

        }

        // Listens to the event when player dies and call GameOver
        void PlayerController_PlayerDied()
        {
            GameOver();
        }
        private void PlayerController_PlayerWin()
        {
            GameOver();
        }
        // Make initial setup and preparations before the game can be played
        public void PrepareGame()
        {
            HidePlayer();
            GameState = GameState.Prepare;

            // Automatically start the game if this is a restart.
            if (isRestart)
            {
                isRestart = false;
               // StartGame();
            }
        }

        // A new game official starts
        public void StartGame()
        {
            StartCoroutine(DelayStartGame());
        }


        IEnumerator DelayStartGame()
        {

            yield return new WaitForEndOfFrame();
            ShowPlayer();
            GameState = GameState.Playing;
            //if (SoundManager.Instance.background != null)
            //{
            //    SoundManager.Instance.PlayMusic(SoundManager.Instance.background);
            //}
        }

        // Called when the player died
        public void GameOver()
        {
            GameState = GameState.GameOver;
            GameCount++;

        }
        public void GameWin()
        {
            GameState = GameState.GameOver;
            GameCount++;

        }
        // Start a new game
        public void RestartGame(float delay = 0)
        {
            isRestart = true;
            StartCoroutine(CRRestartGame(delay));
        }

        IEnumerator CRRestartGame(float delay = 0)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void HidePlayer()
        {

          //  if (playerController != null)
             //   playerController.gameObject.SetActive(false);
        }

        public void ShowPlayer()
        {
            if (playerController != null)
                playerController.gameObject.SetActive(true);
        }

        void CreateNewCharacter(int curChar)
        {
            if (playerController != null)
            {
                DestroyImmediate(playerController.gameObject);
                playerController = null;
            }
            StartCoroutine(CR_DelayCreateNewCharacter(curChar));

        }

        IEnumerator CR_DelayCreateNewCharacter(int curChar)
        {
            yield return new WaitForEndOfFrame();
            GameObject player = Instantiate(CharacterManager.Instance.characters[curChar]);
            player.transform.position = startPlayerPosition;
            playerController = player.GetComponent<PlayerController>();
        }

        public void PauseGame()
        {
            GameState = GameState.Paused;
        }

        public void DestroyEnemys()
        {

        }


    }
}