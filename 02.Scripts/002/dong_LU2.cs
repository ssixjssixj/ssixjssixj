using UnityEngine;
using System.Collections;

public class dong_LU2 : MonoBehaviour {

	public RPG_Camera Camera;
	
	void OnJoinedRoom()
	{
		CreatePlayerObject();
	}
	
	void CreatePlayerObject()
	{
		Vector3 position = new Vector3( 19.0f, 0.02f, 9.0f );//生角色位置

		GameObject newPlayerObject = PhotonNetwork.Instantiate( "04.Prefabs/Robot Kyle RPG", position, Quaternion.identity, 0 );//生角色

		Camera.Target = newPlayerObject.transform;//把照相机给自已控制的那个角色

	}
}
