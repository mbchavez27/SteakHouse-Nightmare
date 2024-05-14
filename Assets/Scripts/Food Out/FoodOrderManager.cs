using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOrderManager : MonoBehaviour
{
    public GameObject[] foodorder = new GameObject[3];
    public GameObject[] orderlist = new GameObject[4];
    public Transform[] orderpos = new Transform[4];
    public int orderNum = 0;
    public float orderDelay,orderrate = 0f;

    void Start()
    {
        orderDelay = Random.Range(1, 8);
    }


    void Update()
    {
        // Spawn
        orderrate += 1 * Time.deltaTime;
        if (orderrate >= orderDelay)
        {
            if (orderlist[orderNum] == null) // if empty
            {
                int foodnumber = Random.Range(0, 3);
                orderDelay = Random.Range(6, 14);
                orderrate = 0;
                Ordering(foodnumber);
                orderNum += 1;

                if (orderNum > 3) //if full reset
                {
                    orderNum = 0;
                }
            }
            if(orderlist[orderNum] != null) //not empty
            {
                orderDelay = Random.Range(1, 8);
                orderrate = 0;
                orderNum += 1;

                if (orderNum > 3) //if full reset
                {
                    orderNum = 0;
                }
            }
        }
    }

    void Ordering(int foodnumber)
    {
        orderlist[orderNum] = Instantiate(foodorder[foodnumber], orderpos[orderNum].position, orderpos[orderNum].rotation);
        orderlist[orderNum].transform.SetParent(this.transform);
    }
}
