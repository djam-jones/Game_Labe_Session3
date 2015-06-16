using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Net;

public class NetworkManager2 : MonoBehaviour {

	//TODO:
	/* CREATE QUICKCONNECT SYSTEM THAT USES ONLY THE IP ADDRESS AND PORT OF THE HOST
	 */

	//Quick Connect
	public Text quickIP;
	public Text quickPort;

	//Technical Stuff
	private string port 	= "25565";
	private IPAddress[] ip;
	//Lists of Hosts & Servers
	private HostData[] _hostList;

	//Semi-Technical Stuff
	private string _typeName = "Name";
	private string _gameName = "The_Six_Million_Dollar_Ninja_Strikes_Again";

	//UI Stuff
	public Text ipText;
	public Text portText;

	//Server Menu
	public GameObject roomPanelUI;
	public GameObject playerObject;
	public GameObject map;

	void Awake()
	{
		//
	}

	void Start()
	{
		ipText.text			= "IP: " + GetIP();
		portText.text 		= "Port: " 	+ port;
	}

	void OnGUI()
	{
		if(!Network.isServer && !Network.isClient){
			if(_hostList != null)
			{
				for (int i = 0; i < _hostList.Length; i++)
				{
					if (GUI.Button(new Rect(200, 75 + (85 * i), 300, 100), _hostList[i].gameType))
					{
						JoinServer(_hostList[i].ip.ToString());
						Debug.Log("Servers Found: " + i);
					}
				}
			}
		}
	}
	
	public void HostServer(int amountOfConnections, string typeName)
	{
		MasterServer.ipAddress = GetIP();
		MasterServer.port = 23466;

		Network.natFacilitatorIP = GetIP();
		Network.natFacilitatorPort = 50005;

		Network.InitializeServer(amountOfConnections, int.Parse(port), !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, _gameName, "NINJA'S STRIKE!");
	}

	public void JoinServer(string hostIP)
	{
		hostIP = quickIP.text.ToString();

		Network.Connect(hostIP, port);
		SpawnGameMapAndPlayers();
	}

	public void SpawnGameMapAndPlayers()
	{
		Network.Instantiate(playerObject, new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f)), Quaternion.identity, 0);
		Network.Instantiate(map, new Vector2(0, 0), Quaternion.identity, 0);
	}

	public void RefreshHostList()
	{
		MasterServer.RequestHostList(_typeName);

//		if(_hostList != null)
//		{
//			for( int i = 0; i < _hostList.Length; i++ )
//			{
//				/*TODO:
//				 * Instantiate a Menu Prefab for each Server you find.
//				 */
//
//				Debug.Log("Servers Found: ");
//			}
//		}
	}

	public void SelectServer()
	{
		print("Server Selected");

		/*TODO:
		 * Get Host Data from the Selected Server.
		 */
	}

	void OnServerInitialized()
	{
		print("Server Initialized.");
	}

	void OnConnectedToServer()
	{
		print("Server Joined.");
	}

	void OnMasterServerEvent(MasterServerEvent mse)
	{
		if(mse == MasterServerEvent.HostListReceived)
		{
			_hostList = MasterServer.PollHostList();
			print("host list");
		}
	}

	private string GetIP()
	{
		string strHostName = "";
		strHostName = Dns.GetHostName();
		IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
		
		ip = ipEntry.AddressList;
		
		//Debug.Log(ip[ip.Length - 1].ToString());
		return ip[ip.Length - 1].ToString();
	}

}
