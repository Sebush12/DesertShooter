using UnityEngine;
using System.Collections;
using StarterAssets;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{	
	// Place holders to allow connecting to other objects
	public Transform spawnPoint;
	public GameObject player;

	// Flags that control the state of the game
	private float elapsedTime = 0;
	private bool isRunning = false;
	private bool isFinished = false;
	public int scoreTreasure = 0;
	public int enemyScore = 0;

	// So that we can access the player's controller from this script
	private CharacterController pcController;
	private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;


	// Use this for initialization
	void Start ()
	{
		thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
		//Tell Unity to allow character controllers to have their position set directly. This will enable our respawn to work
		Physics.autoSyncTransforms = true;

		// Finds the First Person Controller script on the Player
		pcController = player.GetComponent<CharacterController> ();
	
		// Disables controls at the start.
		pcController.enabled = false;
	}


	//This resets to game back to the way it started
	private void StartGame()
	{
		elapsedTime = 0;
		isRunning = true;
		isFinished = false;

		// Move the player to the spawn point, and allow it to move.
		PositionPlayer();
		pcController.enabled = true;
	}


	// Update is called once per frame
	void Update ()
	{
		// Add time to the clock if the game is running
		if (isRunning)
		{
			elapsedTime = elapsedTime + Time.deltaTime;
			if(scoreTreasure >= 7 || player.GetComponent<Player>().currentHealth <= 0)
			{
				FinishedGame();
			}
		}
	}


	//Runs when the player needs to be positioned back at the spawn point
	public void PositionPlayer()
	{
		player.transform.position = spawnPoint.position;
		player.transform.rotation = spawnPoint.rotation;
	}


	// Runs when the player enters the finish zone
	public void FinishedGame()
	{
		isRunning = false;
		isFinished = true;
		pcController.enabled = false;
		player.SetActive(false);
		
	}
	
	
	//This section creates the Graphical User Interface (GUI)
	void OnGUI() {
		
		if(!isRunning)
		{
			string message;

			if(isFinished)
			{
				message = "Play Again?!";
			}
			else
			{
				message = "Welcome to Sebastians Game Land! Press Enter or Click to play!";
			}

			//Define a new rectangle for the UI on the screen
			Rect startButton = new Rect(Screen.width/2 - 240, Screen.height/2, 500, 30);

			//if (GUI.Button(startButton, message) || starterAssetsInputs.jump)
			//{
				//start the game if the user clicks to play
				StartGame ();
			//}
		}
		
		// If the player finished the game, show the final time
		if(isFinished)
		{
			GUI.Box(new Rect(Screen.width / 2 - 65, 185, 130, 40), "Your Time Was");
			GUI.Label(new Rect(Screen.width / 2 - 10, 200, 20, 30), ((int)elapsedTime).ToString());
		}
		else if(isRunning)
		{ 
			// If the game is running, show the current time
			GUI.Box(new Rect(Screen.width / 2 - 65, Screen.height - 115, 130, 40), "Your Time Is");
			GUI.Label(new Rect(Screen.width / 2 - 10, Screen.height - 100, 20, 30), ((int)elapsedTime).ToString());
		}
	}
}
