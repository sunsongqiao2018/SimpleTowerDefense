using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class GameobjectPool <T>: MonoBehaviour where T : Component
{
    //NOTE:Design issue can use the nested class below 

    //public abstract class Pool
    //{
    //    public string tag;
    //    public T objectUnit;
    //    public int size;
    //}
    public string tag;
    [SerializeField]
    public T objectUnit;
    public int size;
    //public List<Pool> Pools;
    private Dictionary<string, Queue<T>> gameObjectPools = new Dictionary<string, Queue<T>>();
    #region SingletonDefine
    public static GameobjectPool<T> instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //On Start we generate the pools from inspectors, cant properly work for now

        //foreach (Pool pool in Pools)
        //{
            Queue<T> genericPool = new Queue<T>();
            for (int i = 0; i <size; i++)
            {
                T obj = Instantiate(objectUnit);
                obj.transform.parent = transform; 
                obj.gameObject.SetActive(false);
                genericPool.Enqueue(obj);
            }
            gameObjectPools.Add(tag, genericPool);
        //}
    }
    public virtual T Get(string tag,bool setActive=true)
    {
        if (!gameObjectPools.ContainsKey(tag))
        {
            Debug.Log("Provided Tag does not exist in the pool dictionary");
            return null;
        }
        else if (gameObjectPools[tag].Count == 0)
        {
            Debug.Log("Pools objects were all in use, consider enlarge the pool size or optimize the use of pool objects");
            return null;
        }
        {
                T obj = gameObjectPools[tag].Dequeue();
            if (setActive)
            {

                obj.gameObject.SetActive(true);
            }
                return obj;

        }

    }
    public virtual IEnumerator Get(string tag,int amount,float intermittent)
    {
        if (!gameObjectPools.ContainsKey(tag))
        {
            Debug.Log("Provided Tag does not exist in the pool dictionary");
            yield return null;
        }
        else
        {
            for (int i = 0; i < amount; i++)
            {
                if (gameObjectPools[tag].Count<amount-(i+1))
                {
                    Debug.Log("Not enough gameobjects in the pool, wait for restock");
                   yield return null;
                }
               T obj = gameObjectPools[tag].Dequeue();
                obj.gameObject.SetActive(true);
                yield return new WaitForSeconds(intermittent);
            }

        }

    }
    public void BackToPool(string tag,T obj)
    {
        gameObjectPools[tag].Enqueue(obj);
        obj.gameObject.SetActive(false);
    }
}
