using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour, EnemySide.EnemyInterface
{
    //Could use a scriptableObject to store different types of Enemies;
    public float moveSpeed;
    public int lifePoint;
    private int lifeRemaining;
    private int takeDownScore;
    public static Transform[] pathPoints;
    private Queue<Transform> pathQue;
    private Vector3 nextMovePoint;
    private void OnEnable()
    {
        OnSpwan();
    }

    public void OnSpwan()
    {
        takeDownScore = 10;
        lifeRemaining = lifePoint;
        pathQue = new Queue<Transform>();
        //load all points to queue;
        foreach (Transform point in pathPoints)
        {
            pathQue.Enqueue(point);
        }
        transform.position = pathQue.Dequeue().position;
        UpdateMovePoint();
    }
    public void UpdateMovePoint()
    {

        nextMovePoint = pathQue.Dequeue().position;
    }
    public void OnMove()
    {

        transform.position = Vector3.MoveTowards(transform.position, nextMovePoint, moveSpeed * Time.deltaTime);
    }
    public void OnTakenDamage(int damageAmount)
    {
        lifeRemaining -= damageAmount;
    }

    public void OnShotDown()
    {
        EnemySpawnPool.instance.BackToPool(tag, this);
    }
    //Update gets called once per f
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, nextMovePoint);
        if (distance < 0.1f)
        {
            if (pathQue.Count != 0)
            {
                //Enemy not reached the final point;

                UpdateMovePoint();
            }
            else
            {
                OnShotDown();
                PlayerManager._instance.OnTakeDamage(1);
                //TODO will damage the player's base;
            }

        }
        else
        {
            OnMove();
        }
        if (lifeRemaining <= 0)
        {
            OnShotDown();
            PlayerManager._instance.AddScore(takeDownScore);
            //TODO also Add score to player;
        }
    }
}
