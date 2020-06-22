using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameControl : MonoBehaviour
{
    public GameObject[] towerArray;
    public int[] towerPriceArray = new int[3] { 10, 15, 30 };

    private GameObject towerToBePlaced;
    private bool inPlacing;
    [SerializeField]
    private Button towerButton;

    private void Start()
    {
        inPlacing = false;
    }
    public void OnPurchasingTower(int type)
    {
        if (PlayerManager._instance.GetGold() >= towerPriceArray[type])
        {
            towerButton.enabled = false;
            inPlacing = true;
            var v3 = Input.mousePosition;
            v3.z = 12;
            v3 = Camera.main.ScreenToWorldPoint(v3);
            towerToBePlaced = Instantiate(towerArray[type], v3, Quaternion.identity);
            towerToBePlaced.transform.GetComponent<Tower>().enabled = false;
        }
        else
        {
            Debug.Log("insufficient funds");
        }
    }
    private void Update()
    {
        if (inPlacing && towerToBePlaced != null)
        {
            var v3 = Input.mousePosition;
            v3.z = 12;
            v3 = Camera.main.ScreenToWorldPoint(v3);
            towerToBePlaced.transform.position = new Vector3(v3.x, 0.75f, v3.z);

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(towerToBePlaced.transform.position, new Vector3(towerToBePlaced.transform.position.x, -1, towerToBePlaced.transform.position.z), out RaycastHit hit))
                {
                    if (hit.transform.tag == "towerBase")
                    {
                        towerToBePlaced.transform.position = new Vector3(hit.transform.position.x, 0.5f, hit.transform.position.z);
                        towerToBePlaced.transform.GetComponent<Tower>().enabled = true;

                        PlayerManager._instance.SpendGold(10);
                        towerToBePlaced = null;
                        //Check if vacant and place the tower 
                    }
                    else
                    {
                        Destroy(towerToBePlaced);
                    }
                }
                else
                {
                    Destroy(towerToBePlaced);
                }
                towerButton.enabled = true;
            }
        }
    }
}
