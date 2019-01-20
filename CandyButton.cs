using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is the object reference to the buttons/candies that are on screen
 */

public class CandyButton
{
    //Variables
    private Vector2Int position;    //Where is the button x, y
    private Rect buttonSize;    //How big is the button in terms of rectangles
    private int size;    //How big is the button width and height
    private CandyTypes candyType;    //What kind of candy are we?
    
    //Properties
    //Return the candies position in the grid
    public Vector2Int Position{
        get{ return this.position; }
    }
    
    //REturn the candies rectangular shape
    public Rect ButtonSize{
        get{ return this.buttonSize; }
    }

    //Return our candies type
    public CandyTypes CandyType{
        get{ return this.candyType; }
    }
    
    //Constructor
    //Create our candies dimensions and construction
    public CandyButton(Vector2Int position, int size){
        this.position = position;
        this.size = size;

        candyType = (position.x + position.y) % 3 == 1 ? CandyTypes.None : CandyTypes.Strawberry;
        
        this.buttonSize = new Rect(position.x * size, position.y * size, size, size);
    }
    
    //Functions
    //Turn the candy to a matched typed
    //This shows that the candy has been matched with another
    public void Matched(){
        this.candyType = CandyTypes.Matched;
    }
}
