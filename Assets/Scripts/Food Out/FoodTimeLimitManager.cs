using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FoodTimeLimitManager : MonoBehaviour
{
    public float timelimit;

    void Start()
    {
        timelimit = Random.Range(4, 13);
    }

    void Update()
    {
        timelimit -= .1f * Time.deltaTime;
        this.GetComponent<Image>().color = new Color(1, timelimit, timelimit);

        if(timelimit <= 0)
        {
           GameObject.FindGameObjectWithTag("GameSystem").GetComponent<timeLimitManager>().ExpiredFoodOrder();
           GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().StartExpiredFoodOrder();
           Destroy(this.gameObject);
        }
    }
}
