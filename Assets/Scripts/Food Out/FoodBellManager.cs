using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBellManager : MonoBehaviour
{
    public GameObject orderStation,foodlist;

    void Start()
    {
        orderStation = GameObject.FindGameObjectWithTag("OrderStation");
        foodlist = GameObject.FindGameObjectWithTag("FoodList");
    }

    void Update()
    {
        
    }

    public void Bell()
    {
        //Bell
        if (GameObject.FindGameObjectWithTag("OrderSteakEgg") != null)
        {
            if (orderStation.GetComponent<FoodOutManager>().whatsteak == whatSteak.steakegg)
            {
                StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().AfterOrder());
                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<scoreManager>().gamescore += 10;
                StartCoroutine(GameObject.FindGameObjectWithTag("GameSystem").GetComponent<scoreManager>().ShowScore());
                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<timeLimitManager>().time_ += 10;
                StartCoroutine(GameObject.FindGameObjectWithTag("GameSystem").GetComponent<timeLimitManager>().ShowPlusTime());
                Destroy(GameObject.FindGameObjectWithTag("OrderSteakEgg"));
                GameObject.FindGameObjectWithTag("OrderStation").GetComponent<FoodOutManager>().RemovePlate();
            }
        }
        if (GameObject.FindGameObjectWithTag("OrderSteakBread") != null)
        {
            if (orderStation.GetComponent<FoodOutManager>().whatsteak == whatSteak.steakbread)
            {
                StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().AfterOrder());
                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<scoreManager>().gamescore += 10;
                StartCoroutine(GameObject.FindGameObjectWithTag("GameSystem").GetComponent<scoreManager>().ShowScore());
                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<timeLimitManager>().time_ += 10;
                StartCoroutine(GameObject.FindGameObjectWithTag("GameSystem").GetComponent<timeLimitManager>().ShowPlusTime());
                Destroy(GameObject.FindGameObjectWithTag("OrderSteakBread"));
                GameObject.FindGameObjectWithTag("OrderStation").GetComponent<FoodOutManager>().RemovePlate();
            }
        }
        if (GameObject.FindGameObjectWithTag("OrderSteakPotato") != null)
        {
            if (orderStation.GetComponent<FoodOutManager>().whatsteak == whatSteak.steakpotato)
            {
                StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().AfterOrder());
                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<scoreManager>().gamescore += 10;
                StartCoroutine(GameObject.FindGameObjectWithTag("GameSystem").GetComponent<scoreManager>().ShowScore());
                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<timeLimitManager>().time_ += 10;
                StartCoroutine(GameObject.FindGameObjectWithTag("GameSystem").GetComponent<timeLimitManager>().ShowPlusTime());
                Destroy(GameObject.FindGameObjectWithTag("OrderSteakPotato"));
                GameObject.FindGameObjectWithTag("OrderStation").GetComponent<FoodOutManager>().RemovePlate();
            }
        }
    }
}
