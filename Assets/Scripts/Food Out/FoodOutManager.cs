using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum whatSteak
{
    none,steakbread,steakegg,steakpotato
};

public class FoodOutManager : MonoBehaviour
{
    public Transform foodPos;
    public GameObject[] finished_steaks = new GameObject[3];
    public GameObject finished_food;
    public whatSteak whatsteak;

    public void PlaceSteakBread()
    {
        finished_food = Instantiate(finished_steaks[0], foodPos.position, foodPos.rotation);
        finished_food.transform.parent = this.transform;
        whatsteak = whatSteak.steakbread;
    }

    public void PlaceSteakPotato()
    {
        finished_food = Instantiate(finished_steaks[2], foodPos.position, foodPos.rotation);
        finished_food.transform.parent = this.transform;
        whatsteak = whatSteak.steakpotato;
    }


    public void PlaceSteakEgg()
    {
        finished_food = Instantiate(finished_steaks[1], foodPos.position, foodPos.rotation);
        finished_food.transform.parent = this.transform;
        whatsteak = whatSteak.steakegg;
    }

    public void RemovePlate()
    {
        whatsteak = whatSteak.none;
        Destroy(finished_food);
    }
}
