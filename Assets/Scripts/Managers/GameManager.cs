using System;
using UnityEngine;
using static Utilities;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int placeCounter = 0;
    
    // private variables
    private bool isFinished = false;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < _GameReferenceHolder.enemies.Length; i++)
        {
            _GameReferenceHolder.enemies[i].SetColor(_GameReferenceHolder.enemyColors[i]);
        }
    }

    public void StartGame()
    {
        _GameReferenceHolder.playerController.StartGame();

        foreach (EnemyController enemy in _GameReferenceHolder.enemies)
        {
            enemy.StartGame();
        }
    }

    public void EndGame(bool win, int amount = 0)
    {
        if (isFinished) return;

        isFinished = true;
        
        if (win)
        {
            UIManager.Instance.OnPlayerCompletedLevel();
            StartCoroutine(UIManager.Instance.SpawnCoin(_GameReferenceHolder.playerGameObject.transform.position,
                amount));
        }
        else
        {
            UIManager.Instance.OnPlayerFailedLevel();
        }
    }
}
