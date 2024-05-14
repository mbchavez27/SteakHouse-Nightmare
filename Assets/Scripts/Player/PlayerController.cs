using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;


//Movement
public enum Direction
{
    up,down,left,right
}; 

//PickUp and Throw
public enum Station
{
   none,trashcan,foodprep,stove,plates,beef,bread,egg,potato,bell,order
};

public enum whatisHolding
{
    none,plate,beef,bread,potato,egg,cookedbeef,cookedbread,cookedpotato,cookedegg,burntfood,steakbread,steakpotato,steakegg
};

public class PlayerController : MonoBehaviour
{
    //Movement 
    Rigidbody2D rb2d;
    float playerSpeed;
    float[] playerspeeds = new float[2];
    public float[] whitechefspeeds = new float[2];
    public float[] blackchefspeeds = new float[2];
    public float[] redchefspeeds = new float[2];
    public Sprite[] playerSprites = new Sprite[4];
    public Direction playerDir;

    //PickUp
    public Transform ingredientpos;
    public GameObject[] ingredients = new GameObject[5];
    public GameObject[] cooked_food = new GameObject[5];
    public GameObject[] finished_steaks = new GameObject[3];
    public Station station;
    public whatisHolding whatisholding;
    //Place
    GameObject colstation,pickUp;
    //Player State
    public bool isArmed = false;
    public float[] orderStatedelay = new float[2];
    public Transform OrderStatepos;
    public GameObject afterOrderState,foodexpireState;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        //Player State
        ChangePlayerSpeedByColor();
        afterOrderState.SetActive(false);
        foodexpireState.SetActive(false);
    }

    void Update()
    {
        if (Time.timeScale == 1) // if not paused
        {
            Debug.Log(playerSpeed);
            //PC Controls
            Movement();
            PickUp();
            Throw();
            Place();
            OrderOut();

            //Mobile Controls
            MobileInput();
        }
    }

    //Player Movement
    void ChangePlayerSpeedByColor()
    {
        if (PlayerPrefs.GetString("Chef") == "Default")
        {
            playerspeeds[0] = whitechefspeeds[0];
            playerspeeds[1] = whitechefspeeds[1];
        }
        if (PlayerPrefs.GetString("Chef") == "Black")
        {
            playerspeeds[0] = blackchefspeeds[0];
            playerspeeds[1] = blackchefspeeds[1];
        }
        if (PlayerPrefs.GetString("Chef") == "Red")
        {
            playerspeeds[0] = redchefspeeds[0];
            playerspeeds[1] = redchefspeeds[1];
        }

        playerSpeed = playerspeeds[0];
    }

    void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        rb2d.velocity = new Vector2(horizontal * playerSpeed, vertical * playerSpeed);
        ChangePlayerSprite(horizontal,vertical);
    }


    void ChangePlayerSprite(float hor,float ver)
    {
        //Set Dir
        if(hor > 0)
        {
            playerDir = Direction.right;
        }
        if(hor < 0)
        {
            playerDir = Direction.left;
        }
        if(ver > 0)
        {
            playerDir = Direction.up;
        }
        if(ver < 0)
        {
            playerDir = Direction.down;
        }

        //Set Sprite 
        if(playerDir == Direction.down)
        {
            if (!isArmed)
            {
                this.GetComponent<SpriteRenderer>().sprite = playerSprites[0];
            }
            if (isArmed)
            {
                this.GetComponent<SpriteRenderer>().sprite = playerSprites[4];
            }
        }
        if (playerDir == Direction.up)
        {
            if (!isArmed)
            {
                this.GetComponent<SpriteRenderer>().sprite = playerSprites[1];
            }
            if (isArmed)
            {
                this.GetComponent<SpriteRenderer>().sprite = playerSprites[5];
            }
        }
        if (playerDir == Direction.left)
        {
            if (!isArmed)
            {
                this.GetComponent<SpriteRenderer>().sprite = playerSprites[2];
            }
            if (isArmed)
            {
                this.GetComponent<SpriteRenderer>().sprite = playerSprites[6];
            }
        }
        if (playerDir == Direction.right)
        {
            if (!isArmed)
            {
                this.GetComponent<SpriteRenderer>().sprite = playerSprites[3];
            }
            if (isArmed)
            {
                this.GetComponent<SpriteRenderer>().sprite = playerSprites[7];
            }
        }
    }

    //Player Mobile Movement
    public void MobileInput()
    {
        Vector2 hitorigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hits = Physics2D.Raycast(hitorigin, Vector2.zero);
        if (Input.GetMouseButton(0) || Input.touchCount == 1)
        {
            if (hits.collider != null)
            {
                if (hits.collider.tag == "arrowRight")
                {
                    rb2d.velocity = new Vector2(1 * playerSpeed, rb2d.velocity.x);
                    playerDir = Direction.right;
                }
                if (hits.collider.tag == "arrowLeft")
                {
                    rb2d.velocity = new Vector2(-1 * playerSpeed, rb2d.velocity.x);
                    playerDir = Direction.left;
                }
                if (hits.collider.tag == "arrowUp")
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.y, 1 * playerSpeed);
                    playerDir = Direction.up;
                }
                if (hits.collider.tag == "arrowDown")
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.y, -1 * playerSpeed);
                    playerDir = Direction.down;
                }
            }
        }
        if (Input.GetMouseButtonDown(0) || Input.touchCount == 1)
        {
            if(hits.collider != null)
            {
                if(hits.collider.tag == "MobileEnter")
                {
                    #region "Throw"
                    //Throw
                    if (station == Station.trashcan)
                    {
                        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayThrow();
                        isArmed = false;
                    }
                    #endregion
                    #region "Pick-Up"
                    if (!isArmed)
                    {
                        //For Plates
                        if (station == Station.plates)
                        {
                            GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                            isArmed = true;
                            pickUp = Instantiate(ingredients[0], ingredientpos.position, ingredientpos.rotation);
                            pickUp.transform.parent = this.transform;
                            pickUp.tag = "Ingredients1";
                            whatisholding = whatisHolding.plate;
                        }
                        //For Ingredients
                        if (station == Station.beef)
                        {
                            GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                            isArmed = true;
                            pickUp = Instantiate(ingredients[1], ingredientpos.position, ingredientpos.rotation);
                            pickUp.transform.parent = this.transform;
                            pickUp.tag = "Ingredients1";
                            whatisholding = whatisHolding.beef;
                        }
                        if (station == Station.bread)
                        {
                            GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                            isArmed = true;
                            pickUp = Instantiate(ingredients[2], ingredientpos.position, ingredientpos.rotation);
                            pickUp.transform.parent = this.transform;
                            pickUp.tag = "Ingredients1";
                            whatisholding = whatisHolding.bread;
                        }
                        if (station == Station.potato)
                        {
                            GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                            isArmed = true;
                            pickUp = Instantiate(ingredients[3], ingredientpos.position, ingredientpos.rotation);
                            pickUp.transform.parent = this.transform;
                            pickUp.tag = "Ingredients1";
                            whatisholding = whatisHolding.potato;
                        }
                        if (station == Station.egg)
                        {
                            GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                            isArmed = true;
                            pickUp = Instantiate(ingredients[4], ingredientpos.position, ingredientpos.rotation);
                            pickUp.transform.parent = this.transform;
                            pickUp.tag = "Ingredients1";
                            whatisholding = whatisHolding.egg;
                        }
                        //For Cooked Food
                        if (station == Station.stove)
                        {
                            if (colstation.GetComponent<StoveStationManager>().whatfood == whatFood.Beef)
                            {
                                if (colstation.GetComponent<StoveStationManager>().food.layer == 11)//Cooked 
                                {
                                    GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                                    colstation.GetComponent<StoveStationManager>().RemoveFood();
                                    isArmed = true;
                                    pickUp = Instantiate(cooked_food[0], ingredientpos.position, ingredientpos.rotation);
                                    pickUp.transform.parent = this.transform;
                                    pickUp.tag = "Ingredients1";
                                    pickUp.layer = 11;//cooked;
                                    whatisholding = whatisHolding.cookedbeef;
                                }
                                if (colstation.GetComponent<StoveStationManager>().food.layer == 12)//Burnt 
                                {
                                    GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                                    colstation.GetComponent<StoveStationManager>().RemoveFood();
                                    isArmed = true;
                                    pickUp = Instantiate(cooked_food[1], ingredientpos.position, ingredientpos.rotation);
                                    pickUp.transform.parent = this.transform;
                                    pickUp.tag = "Ingredients1";
                                    pickUp.layer = 12;//burnt;
                                    whatisholding = whatisHolding.burntfood;
                                }
                            }
                            if (colstation.GetComponent<StoveStationManager>().whatfood == whatFood.Potato)
                            {
                                if (colstation.GetComponent<StoveStationManager>().food.layer == 11)//Cooked 
                                {
                                    GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                                    colstation.GetComponent<StoveStationManager>().RemoveFood();
                                    isArmed = true;
                                    pickUp = Instantiate(cooked_food[2], ingredientpos.position, ingredientpos.rotation);
                                    pickUp.transform.parent = this.transform;
                                    pickUp.tag = "Ingredients1";
                                    pickUp.layer = 11;//cooked;
                                    whatisholding = whatisHolding.cookedpotato;
                                }
                                if (colstation.GetComponent<StoveStationManager>().food.layer == 12)//Burnt 
                                {
                                    GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                                    colstation.GetComponent<StoveStationManager>().RemoveFood();
                                    isArmed = true;
                                    pickUp = Instantiate(cooked_food[3], ingredientpos.position, ingredientpos.rotation);
                                    pickUp.transform.parent = this.transform;
                                    pickUp.tag = "Ingredients1";
                                    pickUp.layer = 12;//burnt;
                                    whatisholding = whatisHolding.burntfood;
                                }
                            }
                            if (colstation.GetComponent<StoveStationManager>().whatfood == whatFood.Egg)
                            {
                                if (colstation.GetComponent<StoveStationManager>().food.layer == 11)//Cooked 
                                {
                                    GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                                    colstation.GetComponent<StoveStationManager>().RemoveFood();
                                    isArmed = true;
                                    pickUp = Instantiate(cooked_food[4], ingredientpos.position, ingredientpos.rotation);
                                    pickUp.transform.parent = this.transform;
                                    pickUp.tag = "Ingredients1";
                                    pickUp.layer = 11;//cooked;
                                    whatisholding = whatisHolding.cookedegg;
                                }
                                if (colstation.GetComponent<StoveStationManager>().food.layer == 12)//Burnt 
                                {
                                    GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                                    colstation.GetComponent<StoveStationManager>().RemoveFood();
                                    isArmed = true;
                                    pickUp = Instantiate(cooked_food[5], ingredientpos.position, ingredientpos.rotation);
                                    pickUp.transform.parent = this.transform;
                                    pickUp.tag = "Ingredients1";
                                    pickUp.layer = 12;//burnt;
                                    whatisholding = whatisHolding.burntfood;
                                }
                            }
                            if (colstation.GetComponent<StoveStationManager>().whatfood == whatFood.Bread)
                            {
                                if (colstation.GetComponent<StoveStationManager>().food.layer == 11)//Cooked 
                                {
                                    GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                                    colstation.GetComponent<StoveStationManager>().RemoveFood();
                                    isArmed = true;
                                    pickUp = Instantiate(cooked_food[6], ingredientpos.position, ingredientpos.rotation);
                                    pickUp.transform.parent = this.transform;
                                    pickUp.tag = "Ingredients1";
                                    pickUp.layer = 11;//cooked;
                                    whatisholding = whatisHolding.cookedbread;
                                }
                                if (colstation.GetComponent<StoveStationManager>().food.layer == 12)//Burnt 
                                {
                                    GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                                    colstation.GetComponent<StoveStationManager>().RemoveFood();
                                    isArmed = true;
                                    pickUp = Instantiate(cooked_food[7], ingredientpos.position, ingredientpos.rotation);
                                    pickUp.transform.parent = this.transform;
                                    pickUp.tag = "Ingredients1";
                                    pickUp.layer = 12;//burnt;
                                    whatisholding = whatisHolding.burntfood;
                                }
                            }
                        }
                        //Preperation Station for getting the Food
                        if (station == Station.foodprep)
                        {
                            if (colstation.GetComponent<PrepStationManager>().plates != null)
                            {
                                if (colstation.GetComponent<PrepStationManager>().plates.tag == "SteakBread")
                                {
                                    GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                                    colstation.GetComponent<PrepStationManager>().RemovePlate();
                                    isArmed = true;
                                    pickUp = Instantiate(finished_steaks[0], ingredientpos.position, ingredientpos.rotation);
                                    pickUp.transform.parent = this.transform;
                                    pickUp.tag = "Ingredients1";
                                    whatisholding = whatisHolding.steakbread;
                                }
                                if (colstation.GetComponent<PrepStationManager>().plates.tag == "SteakEgg")
                                {
                                    GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                                    colstation.GetComponent<PrepStationManager>().RemovePlate();
                                    isArmed = true;
                                    pickUp = Instantiate(finished_steaks[1], ingredientpos.position, ingredientpos.rotation);
                                    pickUp.transform.parent = this.transform;
                                    pickUp.tag = "Ingredients1";
                                    whatisholding = whatisHolding.steakegg;
                                }
                                if (colstation.GetComponent<PrepStationManager>().plates.tag == "SteakPotato")
                                {
                                    GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                                    colstation.GetComponent<PrepStationManager>().RemovePlate();
                                    isArmed = true;
                                    pickUp = Instantiate(finished_steaks[2], ingredientpos.position, ingredientpos.rotation);
                                    pickUp.transform.parent = this.transform;
                                    pickUp.tag = "Ingredients1";
                                    whatisholding = whatisHolding.steakpotato;
                                }
                            }
                        }
                        //For Getting from Order Out
                        if (station == Station.order)
                        {
                            if (colstation.GetComponent<FoodOutManager>().finished_food != null)
                            {
                                if (colstation.GetComponent<FoodOutManager>().finished_food.tag == "SteakBread")
                                {
                                    GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                                    colstation.GetComponent<FoodOutManager>().RemovePlate();
                                    isArmed = true;
                                    pickUp = Instantiate(finished_steaks[0], ingredientpos.position, ingredientpos.rotation);
                                    pickUp.transform.parent = this.transform;
                                    pickUp.tag = "Ingredients1";
                                    whatisholding = whatisHolding.steakbread;
                                }
                                if (colstation.GetComponent<FoodOutManager>().finished_food.tag == "SteakEgg")
                                {
                                    GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                                    colstation.GetComponent<FoodOutManager>().RemovePlate();
                                    isArmed = true;
                                    pickUp = Instantiate(finished_steaks[1], ingredientpos.position, ingredientpos.rotation);
                                    pickUp.transform.parent = this.transform;
                                    pickUp.tag = "Ingredients1";
                                    whatisholding = whatisHolding.steakegg;
                                }
                                if (colstation.GetComponent<FoodOutManager>().finished_food.tag == "SteakPotato")
                                {
                                    GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                                    colstation.GetComponent<FoodOutManager>().RemovePlate();
                                    isArmed = true;
                                    pickUp = Instantiate(finished_steaks[2], ingredientpos.position, ingredientpos.rotation);
                                    pickUp.transform.parent = this.transform;
                                    pickUp.tag = "Ingredients1";
                                    whatisholding = whatisHolding.steakpotato;
                                }
                            }
                        }
                    }
                    #endregion
                    #region "Place"
                    #region "Preperation"
                    //Preperation
                    if (station == Station.foodprep)
                    {
                        //Preparing Plates
                        if (whatisholding == whatisHolding.plate)
                        {
                            if (colstation.GetComponent<PrepStationManager>().plates == null)//if empty
                            {
                                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                colstation.GetComponent<PrepStationManager>().PlacePlate();
                                isArmed = false;
                            }
                        }

                        //Returning Finished Foods
                        if (whatisholding == whatisHolding.steakbread)
                        {
                            if (colstation.GetComponent<PrepStationManager>().plates == null)//if empty
                            {
                                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                colstation.GetComponent<PrepStationManager>().PlacePlate();
                                colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.steakbread;
                                isArmed = false;
                            }
                        }
                        if (whatisholding == whatisHolding.steakegg)
                        {
                            if (colstation.GetComponent<PrepStationManager>().plates == null)//if empty
                            {
                                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                colstation.GetComponent<PrepStationManager>().PlacePlate();
                                colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.steakegg;
                                isArmed = false;
                            }
                        }
                        if (whatisholding == whatisHolding.steakpotato)
                        {
                            if (colstation.GetComponent<PrepStationManager>().plates == null)//if empty
                            {
                                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                colstation.GetComponent<PrepStationManager>().PlacePlate();
                                colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.steakpotato;
                                isArmed = false;
                            }
                        }

                        //Preperaing food
                        if (colstation.GetComponent<PrepStationManager>().plates != null) // if there is a plate
                        {
                            if (!colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().isfinished) //if plate is not yet finished
                            {
                                //Empty
                                if (colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState == FoodPlateState.empty)
                                {
                                    if (whatisholding == whatisHolding.cookedbeef)
                                    {
                                        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                        colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.steak;
                                        isArmed = false;
                                    }
                                    if (whatisholding == whatisHolding.cookedbread)
                                    {
                                        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                        colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.bread;
                                        isArmed = false;
                                    }
                                    if (whatisholding == whatisHolding.cookedegg)
                                    {
                                        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                        colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.egg;
                                        isArmed = false;
                                    }
                                    if (whatisholding == whatisHolding.cookedpotato)
                                    {
                                        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                        colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.potato;
                                        isArmed = false;
                                    }
                                }
                                //If plate with steak
                                if (colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState == FoodPlateState.steak)
                                {
                                    if (whatisholding == whatisHolding.cookedbread)
                                    {
                                        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                        colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.steakbread;
                                        isArmed = false;
                                    }
                                    if (whatisholding == whatisHolding.cookedegg)
                                    {
                                        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                        colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.steakegg;
                                        isArmed = false;
                                    }
                                    if (whatisholding == whatisHolding.cookedpotato)
                                    {
                                        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                        colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.steakpotato;
                                        isArmed = false;
                                    }
                                }
                                //If plate with potato
                                if (colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState == FoodPlateState.potato)
                                {
                                    if (whatisholding == whatisHolding.cookedbeef)
                                    {
                                        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                        colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.steakpotato;
                                        isArmed = false;
                                    }
                                }
                                //If plate with egg
                                if (colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState == FoodPlateState.egg)
                                {
                                    if (whatisholding == whatisHolding.cookedbeef)
                                    {
                                        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                        colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.steakegg;
                                        isArmed = false;
                                    }
                                }
                                //If plate with bread
                                if (colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState == FoodPlateState.bread)
                                {
                                    if (whatisholding == whatisHolding.cookedbeef)
                                    {
                                        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                        colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.steakbread;
                                        isArmed = false;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    #region "Stove"
                    //Stove
                    if (station == Station.stove)
                    {
                        if (colstation.GetComponent<StoveStationManager>().food == null)//if empty
                        {
                            if (whatisholding == whatisHolding.beef)
                            {
                                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                colstation.GetComponent<StoveStationManager>().CookBeef();
                                isArmed = false;
                            }
                            if (whatisholding == whatisHolding.potato)
                            {
                                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                colstation.GetComponent<StoveStationManager>().CookPotato();
                                isArmed = false;
                            }
                            if (whatisholding == whatisHolding.egg)
                            {
                                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                colstation.GetComponent<StoveStationManager>().CookEgg();
                                isArmed = false;
                            }
                            if (whatisholding == whatisHolding.bread)
                            {
                                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                colstation.GetComponent<StoveStationManager>().CookBread();
                                isArmed = false;
                            }
                        }
                    }
                    #endregion
                    #region "Order Out"
                    //Order Out Station
                    if (station == Station.order)
                    {
                        if (colstation.GetComponent<FoodOutManager>().finished_food == null)
                        {
                            if (whatisholding == whatisHolding.steakbread)
                            {
                                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                colstation.GetComponent<FoodOutManager>().PlaceSteakBread();
                                isArmed = false;
                            }
                            if (whatisholding == whatisHolding.steakegg)
                            {
                                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                colstation.GetComponent<FoodOutManager>().PlaceSteakEgg();
                                isArmed = false;
                            }
                            if (whatisholding == whatisHolding.steakpotato)
                            {
                                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                colstation.GetComponent<FoodOutManager>().PlaceSteakPotato();
                                isArmed = false;
                            }
                        }
                    }
                    #endregion
                    #endregion
                    #region "Order Out"
                    if (station == Station.bell)
                    {
                        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayBell();
                        colstation.GetComponent<FoodBellManager>().Bell();
                    }
                    #endregion
                }
            }
        }
    }

    //Intraction with Objects
    void Throw()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(station == Station.trashcan)
            {
                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayThrow();
                isArmed = false;
            }
        }
        //if disarmed
        if (!isArmed)
        {
            Destroy(GameObject.FindGameObjectWithTag("Ingredients1"));
            whatisholding = whatisHolding.none;
        }
    }

    void PickUp()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isArmed)
            {
                //For Plates
                if (station == Station.plates)
                {
                    GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                    isArmed = true;
                    pickUp = Instantiate(ingredients[0], ingredientpos.position, ingredientpos.rotation);
                    pickUp.transform.parent = this.transform;
                    pickUp.tag = "Ingredients1";
                    whatisholding = whatisHolding.plate;
                }
                //For Ingredients
                if (station == Station.beef)
                {
                    GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                    isArmed = true;
                    pickUp = Instantiate(ingredients[1], ingredientpos.position, ingredientpos.rotation);
                    pickUp.transform.parent = this.transform;
                    pickUp.tag = "Ingredients1";
                    whatisholding = whatisHolding.beef;
                }
                if (station == Station.bread)
                {
                    GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                    isArmed = true;
                    pickUp = Instantiate(ingredients[2], ingredientpos.position, ingredientpos.rotation);
                    pickUp.transform.parent = this.transform;
                    pickUp.tag = "Ingredients1";
                    whatisholding = whatisHolding.bread;
                }
                if (station == Station.potato)
                {
                    GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                    isArmed = true;
                    pickUp = Instantiate(ingredients[3], ingredientpos.position, ingredientpos.rotation);
                    pickUp.transform.parent = this.transform;
                    pickUp.tag = "Ingredients1";
                    whatisholding = whatisHolding.potato;
                }
                if (station == Station.egg)
                {
                    GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                    isArmed = true;
                    pickUp = Instantiate(ingredients[4], ingredientpos.position, ingredientpos.rotation);
                    pickUp.transform.parent = this.transform;
                    pickUp.tag = "Ingredients1";
                    whatisholding = whatisHolding.egg;
                }
                //For Cooked Food
                if(station == Station.stove)
                {
                    if(colstation.GetComponent<StoveStationManager>().whatfood == whatFood.Beef)
                    {                       
                        if (colstation.GetComponent<StoveStationManager>().food.layer == 11)//Cooked 
                        {
                            GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                            colstation.GetComponent<StoveStationManager>().RemoveFood();
                            isArmed = true;
                            pickUp = Instantiate(cooked_food[0], ingredientpos.position, ingredientpos.rotation);
                            pickUp.transform.parent = this.transform;
                            pickUp.tag = "Ingredients1";
                            pickUp.layer = 11;//cooked;
                            whatisholding = whatisHolding.cookedbeef;
                        }
                        if (colstation.GetComponent<StoveStationManager>().food.layer == 12)//Burnt 
                        {
                            GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                            colstation.GetComponent<StoveStationManager>().RemoveFood();
                            isArmed = true;
                            pickUp = Instantiate(cooked_food[1], ingredientpos.position, ingredientpos.rotation);
                            pickUp.transform.parent = this.transform;
                            pickUp.tag = "Ingredients1";
                            pickUp.layer = 12;//burnt;
                            whatisholding = whatisHolding.burntfood;
                        }
                    }
                    if (colstation.GetComponent<StoveStationManager>().whatfood == whatFood.Potato)
                    {
                        if (colstation.GetComponent<StoveStationManager>().food.layer == 11)//Cooked 
                        {
                            GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                            colstation.GetComponent<StoveStationManager>().RemoveFood();
                            isArmed = true;
                            pickUp = Instantiate(cooked_food[2], ingredientpos.position, ingredientpos.rotation);
                            pickUp.transform.parent = this.transform;
                            pickUp.tag = "Ingredients1";
                            pickUp.layer = 11;//cooked;
                            whatisholding = whatisHolding.cookedpotato;
                        }
                        if (colstation.GetComponent<StoveStationManager>().food.layer == 12)//Burnt 
                        {
                            GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                            colstation.GetComponent<StoveStationManager>().RemoveFood();
                            isArmed = true;
                            pickUp = Instantiate(cooked_food[3], ingredientpos.position, ingredientpos.rotation);
                            pickUp.transform.parent = this.transform;
                            pickUp.tag = "Ingredients1";
                            pickUp.layer = 12;//burnt;
                            whatisholding = whatisHolding.burntfood;
                        }
                    }
                    if (colstation.GetComponent<StoveStationManager>().whatfood == whatFood.Egg)
                    {
                        if (colstation.GetComponent<StoveStationManager>().food.layer == 11)//Cooked 
                        {
                            GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                            colstation.GetComponent<StoveStationManager>().RemoveFood();
                            isArmed = true;
                            pickUp = Instantiate(cooked_food[4], ingredientpos.position, ingredientpos.rotation);
                            pickUp.transform.parent = this.transform;
                            pickUp.tag = "Ingredients1";
                            pickUp.layer = 11;//cooked;
                            whatisholding = whatisHolding.cookedegg;
                        }
                        if (colstation.GetComponent<StoveStationManager>().food.layer == 12)//Burnt 
                        {
                            GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                            colstation.GetComponent<StoveStationManager>().RemoveFood();
                            isArmed = true;
                            pickUp = Instantiate(cooked_food[5], ingredientpos.position, ingredientpos.rotation);
                            pickUp.transform.parent = this.transform;
                            pickUp.tag = "Ingredients1";
                            pickUp.layer = 12;//burnt;
                            whatisholding = whatisHolding.burntfood;
                        }
                    }
                    if (colstation.GetComponent<StoveStationManager>().whatfood == whatFood.Bread)
                    {
                        if (colstation.GetComponent<StoveStationManager>().food.layer == 11)//Cooked 
                        {
                            GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                            colstation.GetComponent<StoveStationManager>().RemoveFood();
                            isArmed = true;
                            pickUp = Instantiate(cooked_food[6], ingredientpos.position, ingredientpos.rotation);
                            pickUp.transform.parent = this.transform;
                            pickUp.tag = "Ingredients1";
                            pickUp.layer = 11;//cooked;
                            whatisholding = whatisHolding.cookedbread;
                        }
                        if (colstation.GetComponent<StoveStationManager>().food.layer == 12)//Burnt 
                        {
                            GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                            colstation.GetComponent<StoveStationManager>().RemoveFood();
                            isArmed = true;
                            pickUp = Instantiate(cooked_food[7], ingredientpos.position, ingredientpos.rotation);
                            pickUp.transform.parent = this.transform;
                            pickUp.tag = "Ingredients1";
                            pickUp.layer = 12;//burnt;
                            whatisholding = whatisHolding.burntfood;
                        }
                    }
                }
                //Preperation Station for getting the Food
                if(station == Station.foodprep)
                {
                    if(colstation.GetComponent<PrepStationManager>().plates != null)
                    {
                        if(colstation.GetComponent<PrepStationManager>().plates.tag == "SteakBread")
                        {
                            GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                            colstation.GetComponent<PrepStationManager>().RemovePlate();
                            isArmed = true;
                            pickUp = Instantiate(finished_steaks[0], ingredientpos.position, ingredientpos.rotation);
                            pickUp.transform.parent = this.transform;
                            pickUp.tag = "Ingredients1";
                            whatisholding = whatisHolding.steakbread;
                        }
                        if (colstation.GetComponent<PrepStationManager>().plates.tag == "SteakEgg")
                        {
                            GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                            colstation.GetComponent<PrepStationManager>().RemovePlate();
                            isArmed = true;
                            pickUp = Instantiate(finished_steaks[1], ingredientpos.position, ingredientpos.rotation);
                            pickUp.transform.parent = this.transform;
                            pickUp.tag = "Ingredients1";
                            whatisholding = whatisHolding.steakegg;
                        }
                        if (colstation.GetComponent<PrepStationManager>().plates.tag == "SteakPotato")
                        {
                            GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                            colstation.GetComponent<PrepStationManager>().RemovePlate();
                            isArmed = true;
                            pickUp = Instantiate(finished_steaks[2], ingredientpos.position, ingredientpos.rotation);
                            pickUp.transform.parent = this.transform;
                            pickUp.tag = "Ingredients1";
                            whatisholding = whatisHolding.steakpotato;
                        }
                    }
                }
                //For Getting from Order Out
                if (station == Station.order)
                {
                    if (colstation.GetComponent<FoodOutManager>().finished_food != null)
                    {
                        if (colstation.GetComponent<FoodOutManager>().finished_food.tag == "SteakBread")
                        {
                            GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                            colstation.GetComponent<FoodOutManager>().RemovePlate();
                            isArmed = true;
                            pickUp = Instantiate(finished_steaks[0], ingredientpos.position, ingredientpos.rotation);
                            pickUp.transform.parent = this.transform;
                            pickUp.tag = "Ingredients1";
                            whatisholding = whatisHolding.steakbread;
                        }
                        if (colstation.GetComponent<FoodOutManager>().finished_food.tag == "SteakEgg")
                        {
                            GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                            colstation.GetComponent<FoodOutManager>().RemovePlate();
                            isArmed = true;
                            pickUp = Instantiate(finished_steaks[1], ingredientpos.position, ingredientpos.rotation);
                            pickUp.transform.parent = this.transform;
                            pickUp.tag = "Ingredients1";
                            whatisholding = whatisHolding.steakegg;
                        }
                        if (colstation.GetComponent<FoodOutManager>().finished_food.tag == "SteakPotato")
                        {
                            GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPickUp();
                            colstation.GetComponent<FoodOutManager>().RemovePlate();
                            isArmed = true;
                            pickUp = Instantiate(finished_steaks[2], ingredientpos.position, ingredientpos.rotation);
                            pickUp.transform.parent = this.transform;
                            pickUp.tag = "Ingredients1";
                            whatisholding = whatisHolding.steakpotato;
                        }
                    }
                }
            }
        }
    }

    void Place()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Preperation
            if (station == Station.foodprep)
            {
                //Preparing Plates
                if (whatisholding == whatisHolding.plate)
                {
                    if (colstation.GetComponent<PrepStationManager>().plates == null)//if empty
                    {
                        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                        colstation.GetComponent<PrepStationManager>().PlacePlate();
                        isArmed = false;
                    }
                }

                //Returning Finished Foods
                if(whatisholding == whatisHolding.steakbread)
                {
                    if (colstation.GetComponent<PrepStationManager>().plates == null)//if empty
                    {
                        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                        colstation.GetComponent<PrepStationManager>().PlacePlate();
                        colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.steakbread;
                        isArmed = false;
                    }
                }
                if (whatisholding == whatisHolding.steakegg)
                {
                    if (colstation.GetComponent<PrepStationManager>().plates == null)//if empty
                    {
                        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                        colstation.GetComponent<PrepStationManager>().PlacePlate();
                        colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.steakegg;
                        isArmed = false;
                    }
                }
                if (whatisholding == whatisHolding.steakpotato)
                {
                    if (colstation.GetComponent<PrepStationManager>().plates == null)//if empty
                    {
                        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                        colstation.GetComponent<PrepStationManager>().PlacePlate();
                        colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.steakpotato;
                        isArmed = false;
                    }
                }

                //Preperaing food
                if (colstation.GetComponent<PrepStationManager>().plates != null) // if there is a plate
                {
                    if (!colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().isfinished) //if plate is not yet finished
                    {                     
                        //Empty
                        if (colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState == FoodPlateState.empty)
                        {
                            if (whatisholding == whatisHolding.cookedbeef)
                            {
                                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.steak;
                                isArmed = false;
                            }
                            if (whatisholding == whatisHolding.cookedbread)
                            {
                                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.bread;
                                isArmed = false;
                            }
                            if (whatisholding == whatisHolding.cookedegg)
                            {
                                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.egg;
                                isArmed = false;
                            }
                            if (whatisholding == whatisHolding.cookedpotato)
                            {
                                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.potato;
                                isArmed = false;
                            }
                        }
                        //If plate with steak
                        if (colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState == FoodPlateState.steak)
                        {
                            if (whatisholding == whatisHolding.cookedbread)
                            {
                                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.steakbread;
                                isArmed = false;
                            }
                            if (whatisholding == whatisHolding.cookedegg)
                            {
                                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.steakegg;
                                isArmed = false;
                            }
                            if (whatisholding == whatisHolding.cookedpotato)
                            {
                                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.steakpotato;
                                isArmed = false;
                            }
                        }
                        //If plate with potato
                        if (colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState == FoodPlateState.potato)
                        {
                            if (whatisholding == whatisHolding.cookedbeef)
                            {
                                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.steakpotato;
                                isArmed = false;
                            }
                        }
                        //If plate with egg
                        if (colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState == FoodPlateState.egg)
                        {
                            if (whatisholding == whatisHolding.cookedbeef)
                            {
                                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.steakegg;
                                isArmed = false;
                            }
                        }
                        //If plate with bread
                        if (colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState == FoodPlateState.bread)
                        {
                            if (whatisholding == whatisHolding.cookedbeef)
                            {
                                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                                colstation.GetComponent<PrepStationManager>().plates.GetComponent<FoodPrepManager>().foodplateState = FoodPlateState.steakbread;
                                isArmed = false;
                            }
                        }
                    }
                }
            }

            //Stove
            if (station == Station.stove)
            {
                if (colstation.GetComponent<StoveStationManager>().food == null)//if empty
                {
                    if (whatisholding == whatisHolding.beef)
                    {
                        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                        colstation.GetComponent<StoveStationManager>().CookBeef();
                        isArmed = false;
                    }
                    if (whatisholding == whatisHolding.potato)
                    {
                        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                        colstation.GetComponent<StoveStationManager>().CookPotato();
                        isArmed = false;
                    }
                    if (whatisholding == whatisHolding.egg)
                    {
                        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                        colstation.GetComponent<StoveStationManager>().CookEgg();
                        isArmed = false;
                    }
                    if (whatisholding == whatisHolding.bread)
                    {
                        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                        colstation.GetComponent<StoveStationManager>().CookBread();
                        isArmed = false;
                    }
                }
            }

            //Order Out Station
            if(station == Station.order)
            {
                if (colstation.GetComponent<FoodOutManager>().finished_food == null)
                {
                    if (whatisholding == whatisHolding.steakbread)
                    {
                        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                        colstation.GetComponent<FoodOutManager>().PlaceSteakBread();
                        isArmed = false;
                    }
                    if (whatisholding == whatisHolding.steakegg)
                    {
                        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                        colstation.GetComponent<FoodOutManager>().PlaceSteakEgg();
                        isArmed = false;
                    }
                    if (whatisholding == whatisHolding.steakpotato)
                    {
                        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayPlace();
                        colstation.GetComponent<FoodOutManager>().PlaceSteakPotato();
                        isArmed = false;
                    }
                }
            }
        }
    }

    //Order out Food
    void OrderOut()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (station == Station.bell)
            {
                GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayBell();
                colstation.GetComponent<FoodBellManager>().Bell();
            }
        }
    }

    //After Order or Food Expire
    public IEnumerator AfterOrder()
    {
        afterOrderState.SetActive(true);
        playerSpeed = playerspeeds[1];
        yield return new WaitForSeconds(orderStatedelay[0]);
        ChangePlayerSpeedByColor();
        afterOrderState.SetActive(false);
    }

    public void StartExpiredFoodOrder()
    {
        StartCoroutine(ExpiredFoodOrder());
    }

    public IEnumerator ExpiredFoodOrder()
    {
        GameObject.FindGameObjectWithTag("GameSystem").GetComponent<MusicManager>().PlayExpired();
        foodexpireState.SetActive(true);
        this.GetComponent<SpriteRenderer>().color = Color.gray;
        yield return new WaitForSeconds(orderStatedelay[1]);
        this.GetComponent<SpriteRenderer>().color = Color.white;
        foodexpireState.SetActive(false);
    }

    void OnCollisionStay2D(Collision2D col)
       {
            //Stations
            if (col.gameObject.tag == "TrashCan")
            {
                col.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                station = Station.trashcan;
            }
            if (col.gameObject.tag == "Preperation")
            {
                col.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                station = Station.foodprep;
                colstation = col.gameObject;
            }
            if (col.gameObject.tag == "Stove")
            {
                col.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                station = Station.stove;
                colstation = col.gameObject;
            }
            if (col.gameObject.tag == "Bell")
            {
                col.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                station = Station.bell;
                colstation = col.gameObject;
            }
            if (col.gameObject.tag == "OrderStation")
            {
                col.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                station = Station.order;
                colstation = col.gameObject;
            }
            //Plates
            if (col.gameObject.tag == "Plate")
            {
                col.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                station = Station.plates;
            }
            //Ingredients to get
            if (col.gameObject.tag == "Potato")
            {
                col.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                station = Station.potato;
            }
            if (col.gameObject.tag == "Bread")
            {
                col.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                station = Station.bread;
            }
            if (col.gameObject.tag == "Steak")
            {
                col.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                station = Station.beef;
            }
            if (col.gameObject.tag == "Egg")
            {
                col.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                station = Station.egg;
            }
        }

    void OnCollisionExit2D(Collision2D col)
        {
            col.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            station = Station.none;
            colstation = null;
        }
}
