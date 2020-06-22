using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject pathPointPack;
    public float waveWaitTime;
    public int totalWave;

    private List<Transform> _pathPoints;
    private float _elapsedTime;
    private int _currentWave;

    private void Awake()
    {
        LoadLevelPath();
        AssignPathToUnit();
    }
    // Start is called before the first frame update
    void Start()
    {
        SetWave(0);
       // EnemySpawnPool.instance.Get("ufo", 1);
    }

    private void SetWave(int waveNum)
    {
        _currentWave = waveNum;
        _elapsedTime = 0;
    }
    //load the path transforms
    void LoadLevelPath()
    {
        //assign and reset the list to use;
        _pathPoints = new List<Transform>();
        if (pathPointPack !=null)
        {
            Transform[] pointsArray = pathPointPack.GetComponentsInChildren<Transform>();
            //remove the first one which is from the parent;
            for (int i = 1; i < pointsArray.Length; i++)
            {
                _pathPoints.Add(pointsArray[i]);
            }
        }
    }
    void AssignPathToUnit()
    {
        Enemy.pathPoints = _pathPoints.ToArray();
    }
    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_currentWave == totalWave)
        {
            Debug.Log("Wave completed!");
            return;
        }
        UIcontrol._instance.UpdateCountDownNum((int)Mathf.Round(waveWaitTime - _elapsedTime));
        if (_elapsedTime >= waveWaitTime)
        {
            StartCoroutine( EnemySpawnPool.instance.Get("ufo", _currentWave+1,0.5f));
            _currentWave++;
            SetWave(_currentWave);
        }
    }
}
