using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private GameObject currentTarget;
    [SerializeField]
    private Transform CannonAnchor;
    private int fireRadius ;
    private float fireRate =0.5f;
    private bool canFire;
    private float coldDownInTime;

    // Start is called before the first frame update
    void Start()
    {
        canFire = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="ufo"&& !HasTarget())
        {
            currentTarget = other.gameObject;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (HasTarget() && other.gameObject ==currentTarget)
        {
            currentTarget = null;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (HasTarget())
        {
            currentTarget = other.gameObject;
        }
    }
    private void FollowTarget(GameObject target)
    {
        Vector3 targetAndTowerAngle = target.transform.position- transform.position ;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetAndTowerAngle, Vector3.up),0.3f);
    }
    private void Update()
    {
        if (HasTarget())
        {
            if (canFire)
            {
                Debug.Log($"Fire to{currentTarget}");
                Fire();
            }
            else
            {
                coldDownInTime += Time.deltaTime;
                if (coldDownInTime >= fireRate)
                    canFire = true;
            }
        }
 
    }


    private void Fire()
    {
        canFire = false;
        coldDownInTime = 0;
        Bullets bullet = BulletSpawnPool.instance.Get("bullets",false);
        bullet.SetSpawnTransform(CannonAnchor);
        bullet.SetTargetTransform(currentTarget.transform);
        bullet.gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
        if(HasTarget())
        FollowTarget(currentTarget);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fireRadius);
    }
    private bool HasTarget()
    {
        return currentTarget != null&&currentTarget.gameObject.activeInHierarchy;
    }
}
