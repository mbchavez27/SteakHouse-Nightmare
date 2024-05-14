using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingFoodManager : MonoBehaviour
{
    public Sprite[] cookingSprite = new Sprite[3];
    public float cooking_time, cooked_time, burnt_time;
    public GameObject food;

    void Update()
    {
        cooking_time += .1f * Time.deltaTime;//cooking time
        if(cooking_time > cooked_time)
        {
            food.gameObject.layer = 11; //cooked
            this.GetComponent<SpriteRenderer>().sprite = cookingSprite[1];
        }
        if(cooking_time > burnt_time) {
            food.gameObject.layer = 12; //burnt
            this.GetComponent<SpriteRenderer>().sprite = cookingSprite[2];
        }

    }
}
