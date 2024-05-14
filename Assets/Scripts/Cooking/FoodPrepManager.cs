using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodPlateState {
    empty,steak,bread,potato,egg,steakpotato,steakbread,steakegg
};

public class FoodPrepManager : MonoBehaviour
{
    public Sprite foodplate;
    public Sprite foodSteak;
    public Sprite[] foodSteakBread = new Sprite[2];
    public Sprite[] foodSteakPotato = new Sprite[2];
    public Sprite[] foodSteakEgg = new Sprite[2];
    public FoodPlateState foodplateState;
    public bool isfinished = false;

    void Update()
    {
        //No Food
        if (foodplateState == FoodPlateState.empty)
        {
            isfinished = false;
            this.GetComponent<SpriteRenderer>().sprite = foodplate;
        }
        //One Food
        if (foodplateState == FoodPlateState.steak)
        {
            this.GetComponent<SpriteRenderer>().sprite = foodSteak;
        }
        if (foodplateState == FoodPlateState.bread)
        {
            this.GetComponent<SpriteRenderer>().sprite = foodSteakBread[0];
        }
        if (foodplateState == FoodPlateState.potato)
        {
            this.GetComponent<SpriteRenderer>().sprite = foodSteakPotato[0];
        }
        if (foodplateState == FoodPlateState.egg)
        {
            this.GetComponent<SpriteRenderer>().sprite = foodSteakEgg[0];
        }
        //Two Food
        if (foodplateState == FoodPlateState.steakbread)
        {
            isfinished = true;
            this.GetComponent<SpriteRenderer>().sprite = foodSteakBread[1];
            this.gameObject.tag = "SteakBread";
        }
        if (foodplateState == FoodPlateState.steakpotato)
        {
            isfinished = true;
            this.GetComponent<SpriteRenderer>().sprite = foodSteakPotato[1];
            this.gameObject.tag = "SteakPotato";
        }
        if (foodplateState == FoodPlateState.steakegg)
        {
            isfinished = true;
            this.GetComponent<SpriteRenderer>().sprite = foodSteakEgg[1];
            this.gameObject.tag = "SteakEgg";
        }
    } 
}
