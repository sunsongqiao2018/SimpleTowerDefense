using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int _playerScore;
    private readonly int _playerHealth =5;
    [HideInInspector]
    public int playerHealthRemaining;
    private int _playerGold;

    public static PlayerManager _instance;

    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        _playerScore = 0;
        _playerGold = 20;
        playerHealthRemaining = _playerHealth;
        UIcontrol._instance.UpdatePlayerHeath(playerHealthRemaining);
        InvokeRepeating("AddGold",0,3);
    }
    private void AddGold()
    {
        _playerGold++;
        UIcontrol._instance.UpdatePlayerGod(_playerGold);
    }
    public int GetGold()
    {
        return _playerGold;
    }
    public void AddScore(int amount)
    {
        _playerScore += amount;
    }
    public void SpendGold(int amount)
    {
        _playerGold -= amount;
        UIcontrol._instance.UpdatePlayerGod(_playerGold);
    }
    public void OnTakeDamage(int amount)
    {
        playerHealthRemaining -= amount;
        UIcontrol._instance.UpdatePlayerHeath(playerHealthRemaining);
        if (playerHealthRemaining < 0) playerHealthRemaining = 0;
    }
    private void Update()
    {
        if (playerHealthRemaining == 0)
        {
            Time.timeScale = 0;
            Debug.Log("Game Over");
        }
    }
}
