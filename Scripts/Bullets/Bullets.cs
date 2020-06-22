using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    private Transform spawnTransform;
    private Transform targetTransform;
    private float bulletSpeed = 10;
    private void OnEnable()
    {
        OnBulletSpwan();
    }
    public void SetSpawnTransform(Transform spawnTrans)
    {
        spawnTransform = spawnTrans;
    }
    public void SetTargetTransform(Transform targetTrans)
    {
        targetTransform = targetTrans;
    }
    private void OnBulletSpwan()
    {
        if (spawnTransform != null)
        {
            transform.position = spawnTransform.position;
            transform.rotation = spawnTransform.rotation;
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, bulletSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position,targetTransform.position)<0.1f)
        {
            BulletSpawnPool.instance.BackToPool(gameObject.tag,this);
            Enemy target = targetTransform.GetComponent<Enemy>();
            target.OnTakenDamage(1);
        }
    }
}
