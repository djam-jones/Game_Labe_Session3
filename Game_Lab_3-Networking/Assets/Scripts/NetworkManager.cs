using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour 
{
	//Technical Stuff
	private const string TypeName 		= "Ninjas";
	private const string GameName 		= "NinjasRoom";
	private const string LocalHostIP	= "127.0.0.1";
	private const string IP				= "172.17.56.31";
	
	//List of Hosts and servers
	private HostData[] _hostList;

	//Player Stuff
	public GameObject _player;
	private Vector2 _randomPos = Vector2.zero;
	private float _randomFactor;

	void Start()
	{
		//_randomFactor = Random.Range(_randomPos(-6f, -5f), _randomPos(6f, 5f));
	}

	private void StartServer()
	{
		MasterServer.ipAddress = LocalHostIP;

		Network.InitializeServer(3, 62100, !Network.HavePublicAddress());
		MasterServer.RegisterHost(TypeName, GameName);
	}

	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}
	
	private void RefreshHostList()
	{
		MasterServer.RequestHostList(TypeName);
	}

	private void SpawnAPlayer()
	{
		Network.Instantiate(_player, new Vector2(0, 0), Quaternion.identity, 0);
	}

	void OnServerInitialized()
	{
		print("Server Initialized.");
		SpawnAPlayer();
	}

	void OnConnectedToServer()
	{
		print("Server Joined.");
		SpawnAPlayer();
	}

	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if(msEvent == MasterServerEvent.HostListReceived)
		{
			_hostList = MasterServer.PollHostList();
		}
	}

	void OnGUI()
	{
		if(!Network.isClient && !Network.isServer)
		{
			if(GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
			{
				StartServer();
			}

			if(GUI.Button(new Rect(100, 250, 250, 100), "Refresh Servers"))
			{
				RefreshHostList();
			}

			if (_hostList != null)
			{
				for (int i = 0; i < _hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), _hostList[i].gameName))
						JoinServer(_hostList[i]);
				}
			}
		}
	}
}