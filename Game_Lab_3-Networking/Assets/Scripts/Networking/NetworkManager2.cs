using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Net;

public class NetworkManager2 : MonoBehaviour {

	//Technical Stuff
	private string port 	= "25565";
	private IPAddress[] ip;
	//Lists of Hosts & Servers
	private HostData[] _hostList;

	//Semi-Technical Stuff
	private string _typeName = "Name";
	private string _gameName = "Game";

	//UI Stuff
	public Text ipText;
	public Text portText;

	//Server Menu
	public GameObject roomPanelUI;

	void Start()
	{
		ipText.text			= "IP: " + GetIP();
		portText.text 		= "Port: " 	+ port;
	}

	public void HostServer()
	{
		MasterServer.ipAddress = GetIP();

		Network.InitializeServer(3, int.Parse(port), !Network.HavePublicAddress());
		MasterServer.RegisterHost(_typeName, _gameName);
	}

	public void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}

	public void RefreshHostList()
	{
		MasterServer.RequestHostList(_typeName);

		if(_hostList != null)
		{
			for( int i = 0; i < _hostList.Length; i++ )
			{
				/*TODO:
				 * Instantiate a Menu Prefab for each Server you find.
				 */
			}
		}
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
