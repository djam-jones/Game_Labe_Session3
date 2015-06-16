using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HostGame : MonoBehaviour {

	private int _maximumPlayers;
	private int _minimumPlayers;
	private int _amountOfPlayers;

	public Text players;
	public Text roomName;
	public Text serverName;

	public GameObject gameStatus;
	public GameObject lobbyScreen;

	private GameObject _networkManager;
	private NetworkManager2 _networkManagerScript;

	void Awake()
	{
		_networkManager = GameObject.Find("Network Manager");
		_networkManagerScript = _networkManager.GetComponent<NetworkManager2>();
	}

	void Start()
	{
		//Minimum of the total amount of players is 2
		_minimumPlayers = 2;
		//Maximum of the total amount of players is 12
		_maximumPlayers = 12;

		_amountOfPlayers = 6;
	}

	public void HostAGame()
	{
		gameStatus.SetActive(false);
		gameObject.SetActive(true);
	}

	public void CancelHosting()
	{
		gameObject.SetActive(false);
		gameStatus.SetActive(true);
	}

	public void CreateGame()
	{
		//Function for creating the game should be here ((string)roomName.toString(), (int)_amountOfPlayers)
		print("Server Created with Name: " + roomName.text.ToString() + "\n" + "And a Maximum amount of players of:  " + _amountOfPlayers.ToString());

		if(!Network.isServer && !Network.isClient)
		{
			//Initialize the Server
			_networkManagerScript.HostServer(_amountOfPlayers, roomName.ToString());
		}

		//Disables this UI menu to create a game
		gameObject.SetActive(false);

		//Enables Lobby UI
//		lobbyScreen.SetActive(true);

		//Sets the UI to the Name of the Server
		serverName.text = "Server: " + roomName.text.ToString();

		/* TODO:
		 * Make a lobby with the list of players currently in the lobby and a start button to place all the players on the field
		 * Network.Instantiate(A random level according to the maximum amount of players chosen);
		 * Network.Instantiate(All the players that joined the lobby);
		 */
	}

	public void StartGame()
	{
		_networkManagerScript.SpawnGameMapAndPlayersServer();
	}

	public void Players(int amount)
	{
		_amountOfPlayers += amount;
		if(_amountOfPlayers < _minimumPlayers)
		{
			_amountOfPlayers = _minimumPlayers;
			return;
		}
		else if (_amountOfPlayers > _maximumPlayers)
		{
			_amountOfPlayers = _maximumPlayers;
			return;
		}
		else
		{
			players.text = "" + _amountOfPlayers + "/" + _maximumPlayers;
			return;
		}
	}
}
