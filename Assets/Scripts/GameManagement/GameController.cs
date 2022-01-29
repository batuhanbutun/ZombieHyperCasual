using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject healthBoost;
    [SerializeField] private GameObject titleMenu;
    [SerializeField] private EnemyStats _enemyStats;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameStats _gameStats;
    [SerializeField] private Text gameOverText;
    [SerializeField] GameObject zombiePrefab;
    [SerializeField] PlayerStats _playerStats;

    public int enemyCount;
    private float spawnPosX = 10f;
    private float spawnPosZ = 10f;
    private int waveNumber;
    

    void Start()
    {
        _gameStats.isGameOver = true;
    }

    
    void Update()
    {
        GameOver();
        if (_gameStats.isGameOver == false)
        {
            enemyCount = FindObjectsOfType<EnemyController>().Length;
            if (enemyCount == 0)
            {
                waveNumber++;
                SpawnEnemyWave(waveNumber);
                SpawnBoost();
            }
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float PosX = Random.Range(-spawnPosX, spawnPosX);
        float PosZ = Random.Range(-spawnPosZ, spawnPosZ);
        Vector3 randomPos = new Vector3(PosX, 0, PosZ);

        return randomPos;
    }

   private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(zombiePrefab, GenerateSpawnPosition(), zombiePrefab.transform.rotation);
        }
    }

   private void GameOver()
    {
        if (_playerStats.Health <= 0)
        {
            _gameStats.isGameOver = true;
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        _gameStats.isGameOver = false;
        switch (difficulty)
        {
            case 1:
                _enemyStats.Health = 60f;
                _enemyStats.AttackPower = 10f;
                break;
            case 2:
                _enemyStats.Health = 100f;
                _enemyStats.AttackPower = 20f;
                break;
            case 3:
                _enemyStats.Health = 150f;
                _enemyStats.AttackPower = 30f;
                break;
        }
        titleMenu.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void SpawnBoost()
    {
        Instantiate(healthBoost, GenerateSpawnPosition(), healthBoost.transform.rotation);
    }
}
