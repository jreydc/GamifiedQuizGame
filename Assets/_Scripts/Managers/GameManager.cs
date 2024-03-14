using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }

    // Kick the game off with the first state
    void Start() => ChangeState(GameState.Starting);

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

        ChangeState(GameState.Starting);
    }

    private void HandleStarting()
    {

        ChangeState(GameState.InGame);
    }

    private void HandleInGame()
    {
        // Core gameplay and/or in-game
        //ChangeState(GameState.InGame);
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
}

[Serializable]
public enum GameState
{
    PreStarting = 0, //Loading Assets, Game Resources, etc....
    Starting = 1, //On Main Menu, Loading to the Gameplay
    InGame = 2, //On Gameplay
    Waiting = 3, // On Transition between Progresses and/or Scenes
    Win = 4,
    Lose = 5,
}

