using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BicycleRider
{
    public class UIManager : MonoBehaviour
    {
        public GameObject playButton;
        public GameObject restartButton;

        // Start is called before the first frame update

        private void OnEnable()
        {
            GameManager.GameStateChanged += GameManager_GameStateChanged;
        }

       
        private void OnDisable()
        {
            GameManager.GameStateChanged -= GameManager_GameStateChanged;
        }
        private void GameManager_GameStateChanged(GameState newState, GameState oldState)
        {
            if (newState == GameState.GameOver)
            {
                restartButton.SetActive(true);
            }

        }

        void Start()
        {
            playButton.GetComponent<Button>().onClick.AddListener(() => StartButtonClick());
            restartButton.GetComponent<Button>().onClick.AddListener(() => RestartButtonClick());

        }

        // Update is called once per frame
        void Update()
        {

        }
        void StartButtonClick()
        {
            GameManager.Instance.StartGame();
            playButton.SetActive(false);
        }

        void RestartButtonClick()
        {
            GameManager.Instance.RestartGame();
            restartButton.SetActive(false);
        }

   

    }
}