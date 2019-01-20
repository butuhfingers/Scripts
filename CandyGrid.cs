using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyGrid : MonoBehaviour{
	//Variables
	private int gridSizeX = 5;	//Grid size on the x axis
	private int gridSizeY = 5;	//Grid size on the y axis
	private CandyButton[,] candyButtons;	//The multi dimensional array of candies

	//Properties
	public int GridSizeX {
		get { return gridSizeX; }
	}
	public int GridSizeY {
		get { return gridSizeY; }
	}


	public void Awake(){
		candyButtons = new CandyButton[this.gridSizeX, this.gridSizeY];
		
		//Initialize all of the buttons
		for (var x = 0; x < this.gridSizeX; x++){
			for (var y = 0;y < this.gridSizeY;y++){
				candyButtons[x,y] = new CandyButton(new Vector2Int(x, y), 125);
			}
		}
	}
	
	//Functions
	public void OnGUI(){
		//Foreach candy in the candy buttons, we need to create a grid element
		foreach (var candy in candyButtons){
			//Check if the button was pushed and if their are adjacent candies
			if (GUI.Button(candy.ButtonSize, candy.CandyType.ToString()) && CheckAdjacentCandies(candy)){
				for (var candyPos = candy.Position.y - 1; candyPos <= candy.Position.y + 1; candyPos += 2){
					candyButtons[candy.Position.x, candyPos].Matched();
				}
			}	
		}
	}

	private bool CheckAdjacentCandies(CandyButton candy){
		try{
			CandyButton previousCandy = candyButtons[candy.Position.x, 0];
			
			//Check if their are candies in the same vertical column
			for (int candyPos = 0; candyPos < gridSizeY;candyPos++){
				CandyButton currentCandy = this.candyButtons[candy.Position.x, candyPos];
				
				//Check if there is nothing, if so continue onto the next part of the loop
				if (currentCandy.CandyType == CandyTypes.None){
					continue;
				}
				
				//Check if we are equal to our previous candy type or were none
				if (currentCandy.CandyType != previousCandy.CandyType && previousCandy.CandyType != CandyTypes.None){
					return false;
				}
				//Check if our point is equal to the previous candy we found and is between
				//Two of the same candies
				else if(currentCandy.CandyType == previousCandy.CandyType &&
				        candy.Position.y >= previousCandy.Position.y &&
				        candy.Position.y <= currentCandy.Position.y){
					return true;
				}

				//Set the current candies type
				previousCandy = currentCandy;
			}
		}//Something went horribly wrong, abort
		catch (IndexOutOfRangeException outOfRangeException){
			Debug.Log("We threw and index out of range exception");
			//We are on an edge and we can't do anything
			return false;
		}
		
		//We didn't match anything
		//Always return false if a match can not be found
		return false;
	}
}
