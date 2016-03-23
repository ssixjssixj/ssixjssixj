using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private PhotonView pv;
	public Transform[] SpawnPoint;//怪物数组
	public float createTime = 3.0f;//生怪时间
	private int i;
	void Start ()
	{
		pv = PhotonView.Get(this);
		SpawnPoint = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();	
	}
	
	void Update ()
	{
		if (PhotonNetwork.connected && PhotonNetwork.isMasterClient )
		{
			if (Time.time>createTime)
			{
			MakeEnemy();
				createTime = Time.time + 3.0f;
			}
		}	
	}
	void MakeEnemy()
	{
		if(i<=3){
			i++;
		StartCoroutine( this.CreateEnemy() );
		}
	}
	IEnumerator CreateEnemy()
	{
		int idx = Random.Range(1,SpawnPoint.Length);
		PhotonNetwork.InstantiateSceneObject("04.Prefabs/Enemy", SpawnPoint[idx].position, Quaternion.identity, 0, null);
		yield return null;            
	}

}
