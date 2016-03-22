using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PhotonInit : MonoBehaviour {
	public Text Text_dishi;//
	void Awake()
	{
		//Photon的网络，用版本分别连接
		PhotonNetwork.ConnectUsingSettings("MyTankWar 1.0");
	}
	//进入大厅的时候，呼出的CALLBACK函数
	void OnJoinedLobby()
	{
		Text_dishi.text = "进入大厅";
		Debug.Log("Joined Lobby");
		PhotonNetwork.JoinRandomRoom();
	}
	//进入随机房间失败时，呼出的CALLBACK函数
	void OnPhotonRandomJoinFailed()
	{
		Text_dishi.text = "进入随机房间失败";
		Debug.Log("No Room");
		PhotonNetwork.CreateRoom("MyRoom");
	}
	//生成房间完成的时候，呼出的CALLBACK函数
	void OnCreatedRoom()
	{
		Text_dishi.text = "生成房间完成";
		Debug.Log("Finish make a room");
	}
	
	//以进入房间的话，呼出的CALLBACK函数
	void OnJoinedRoom()
	{
		Text_dishi.text = "生成角色";
		Debug.Log("Joined room");
		//生成角色
		StartCoroutine(this.CreatePlayer());
	}
	//在网络里连接的 所有客户端里，生成角色
	IEnumerator CreatePlayer()
	{
		Text_dishi.text = "在网络里连接的 所有客户端里，生成角色";
	    //以下不是UNITY自带的Instantiate函数.使用方法也不一样.
		PhotonNetwork.Instantiate("04.Prefabs/MyPlayer",
	    new Vector3(0, 1, 0),
	    Quaternion.identity, 0);
	    yield return null;
	}

	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

}
