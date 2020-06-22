using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIcontrol : MonoBehaviour
{
    
    public static UIcontrol _instance;
    [SerializeField]
    private Text _countDownText, _playerGold,_playerHealth;
    private void Awake()
    {
        _instance = this;
    }

    public void UpdateCountDownNum(int countdown)
    {
        _countDownText.text = countdown.ToString();
    }
    public void UpdatePlayerGod(int amount)
    {
        _playerGold.text = amount.ToString();
    }
    public void UpdatePlayerHeath(int amount)
    {
        _playerHealth.text = amount.ToString();
    }
}
