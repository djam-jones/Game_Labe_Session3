/*using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HostGame : MonoBehaviour {

	private string _ip;
	private int _port;

	private float _maximumPlayers;
	private float _minimumPlayers;
	private float _amountOfPlayers;

	public Text players;
	public Text roomName;

	public GameObject gameStatus;

	void Start()
	{
		//minimum of the total amout of players is 2
		_minimumPlayers = 2;
		_maximumPlayers = 6;
		_amountOfPlayers = _maximumPlayers;
	}

	public void hostAGame(string hostIp, int hostPort)
	{
		gameStatus.SetActive(false);

		_ip = hostIp;
		_port = hostPort;
	}

	public void cancelHosting()
	{
		gameObject.SetActive(false);
		gameStatus.SetActive(true);
	}

	public void createGame()
	{
		//function for creating the game should be here ((string)roomName.toString(), (int)_amountOfPlayers)
		Debug.Log(roomName.text.ToString() + " --- " + _amountOfPlayers.ToString());
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
*/