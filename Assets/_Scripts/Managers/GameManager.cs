using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Gamified.Managers
{
    public class GameManager : GenericSingleton<GameManager>
    {
        public static event Action<GameState> OnBeforeStateChanged;
        public static event Action<GameState> OnAfterStateChanged;

        public GameState State { get; private set; }

        private AsyncOperation _asyncOperation;

        // Kick the game off with the first state
        private void Start() => ChangeState(GameState.PreStarting);

        public void ChangeState(GameState newState)
        {
            OnBeforeStateChanged?.Invoke(newState);

            State = newState;
            switch (newState)
            {
                case GameState.PreStarting:
                    HandlePreStarting();
                    break;
                case GameState.Starting:
                    HandleStarting();
                    break;
                case GameState.InGame:
                    HandleInGame();
                    break;
                case GameState.Waiting:
                    HandleInWaiting();
                    break;
                case GameState.Win:
                    HandleWinning();
                    break;
                case GameState.Lose:
                    HandleLosing();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }

            OnAfterStateChanged?.Invoke(newState);

            Debug.Log($"New state: {newState}");
        }

        private void HandlePreStarting()
        {
            // Do some start setup, could be environment, cinematics etc
            // Eventually call ChangeState again with your next state

            //ChangeState(GameState.Starting);
        }

        private void HandleStarting()
        {
            Loading(SceneNames.AVATAR);
            //ChangeState(GameState.InGame);
        }

        private void HandleInGame()
        {
            Loading(SceneNames.IN_GAME);// Core gameplay and/or in-game            
        }

        private void HandleInWaiting()
        {

            ChangeState(GameState.Waiting);
        }

        private void HandleWinning()
        {
            ChangeState(GameState.Win);
        }

        private void HandleLosing()
        {
            ChangeState(GameState.Lose);
        }

        #region Scene Management
        public void Loading(string levelName) => StartCoroutine(LoadingDetails(levelName)); //this should be called to start the Coroutine of the SceneManagement
        public void UnLoading(string levelName) => StartCoroutine(UnLoadingDetails(levelName));//this should be called to unload scene.
        IEnumerator LoadingDetails(string levelName)
        {
            yield return null;

            //Begin to load the Scene you specify
            LoadLevel(levelName);
            //Don't let the Scene activate until you allow it to
            _asyncOperation.allowSceneActivation = false;
            Debug.Log("Pro :" + _asyncOperation.progress);
            //When the load is still in progress, output the Text and progress bar
            while (!_asyncOperation.isDone)
            {
                //Output the current progress
                //loadText.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";

                // Check if the load has finished
                if (_asyncOperation.progress >= 0.9f)
                {
                    //Change the Text to show the Scene is ready
                    //loadText.text = "Press the space bar to continue";
                    //loadText.text = "Loaded all of the resources";
                    //Wait to you press the space key to activate the Scene
                    //if (Input.GetKeyDown(KeyCode.Space))
                    //Activate the Scene
                    _asyncOperation.allowSceneActivation = true;
                    //ChangeState(GameStates.GAMEPLAY);

                }

                yield return null;
            }
        }

        IEnumerator UnLoadingDetails(string levelName)
        {
            yield return null;

            //Begin to Unload the Scene you specify
            UnLoadLevel(levelName);
            //Don't let the Scene activate until you allow it to
            _asyncOperation.allowSceneActivation = false;
            Debug.Log("Pro :" + _asyncOperation.progress);
            //When the Unload is still in progress, output the Text and progress bar
            while (!_asyncOperation.isDone)
            {
                //Output the current progress
                //UnloadText.text = "UnLoading progress: " + (asyncOperation.progress * 100) + "%";

                // Check if the load has finished
                if (_asyncOperation.progress >= 0.9f)
                {
                    //Change the Text to show the Scene is ready
                    //UnloadText.text = "UnLoaded all of the resources";
                    //Activate the UnloadScene
                    _asyncOperation.allowSceneActivation = true;
                    //ChangeState(GameStates.GAMEPLAY);

                }

                yield return null;
            }
        }

        public int GetActiveSceneIndex() => SceneManager.GetActiveScene().buildIndex; 
        public void LoadLevel(string levelName) => _asyncOperation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
        public void LoadLevel(int levelIndex) => _asyncOperation = SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Single);
        public void UnLoadLevel(string levelName) => _asyncOperation = SceneManager.UnloadSceneAsync(levelName, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        public void UnLoadLevel(int levelIndex) => _asyncOperation = SceneManager.UnloadSceneAsync(levelIndex, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);

        #endregion
    }
}


