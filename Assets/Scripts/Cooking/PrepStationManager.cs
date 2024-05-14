using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepStationManager : MonoBehaviour
{
    public Transform platePos;
    public GameObject plate,plates,checkmark;

    void Update()
    {
        if (plates != null)
        {
            if (plates.GetComponent<FoodPrepManager>().isfinished)
            {
                checkmark.SetActive(true);
            }
            else
            {
                checkmark.SetActive(false);
            }
        }
        else if(plates == null)
        {
            checkmark.SetActive(false);
        }
    }

    public void PlacePlate()
    {
        if (plates == null)
        {
            plates = Instantiate(plate, platePos.position, platePos.rotation);
            plates.transform.parent = this.transform;
        }
    }

    public void RemovePlate()
    {
        Destroy(plates);
    }
}
