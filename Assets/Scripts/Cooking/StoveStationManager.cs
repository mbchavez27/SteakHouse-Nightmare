using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum whatFood
{
    None,Beef,Potato,Egg,Bread
}

public class StoveStationManager : MonoBehaviour
{
    public Transform foodPos;
    public GameObject[] foodtoCook = new GameObject[3];
    public GameObject food;
    public GameObject cookingfire,fire;
    public whatFood whatfood;
    public Vector3 offset;
    public GameObject cookingindicator;
    public float cookingprogress = 0;

    void Update()
    {
        if(food != null)
        {
            if (food.gameObject.layer != 12) //if not burnt
            {
                cookingindicator.SetActive(true);
                cookingprogress += .1f * Time.deltaTime;
                cookingindicator.GetComponent<SpriteRenderer>().color = new Color(cookingprogress, cookingprogress, cookingprogress);
                if (cookingprogress > 1)
                {
                    cookingprogress = 0;
                }
            }
        }
        else
        {
            cookingindicator.SetActive(false);
            cookingprogress = 0;
        }
    }


    public void CookBeef()
    {
        food = Instantiate(foodtoCook[0], foodPos.position, foodPos.rotation);
        food.transform.parent = this.transform;
        fire = Instantiate(cookingfire,foodPos.position + offset,foodPos.rotation);
        whatfood = whatFood.Beef;
    }
    public void CookPotato()
    {
        food = Instantiate(foodtoCook[1], foodPos.position, foodPos.rotation);
        food.transform.parent = this.transform;
        fire = Instantiate(cookingfire, foodPos.position + offset, foodPos.rotation);
        whatfood = whatFood.Potato;
    }
    public void CookEgg()
    {
        food = Instantiate(foodtoCook[2], foodPos.position, foodPos.rotation);
        food.transform.parent = this.transform;
        fire = Instantiate(cookingfire, foodPos.position + offset, foodPos.rotation);
        whatfood = whatFood.Egg;
    }
    public void CookBread()
    {
        food = Instantiate(foodtoCook[3], foodPos.position, foodPos.rotation);
        food.transform.parent = this.transform;
        fire = Instantiate(cookingfire, foodPos.position + offset, foodPos.rotation);
        whatfood = whatFood.Bread;
    }

    public void RemoveFood()
    {
        Destroy(food);
        Destroy(fire);
        whatfood = whatFood.None;
    }
}
