using UnityEngine;
using System.Collections;

public class BulletControl : MonoBehaviour {

	public float speed = 10.0f;
	public float fireRange = 300.0f;
	public float damage = 10.0f;
	
	private Transform tr;
	private Vector3 spawnPoint;
	
	void Start ()
	{
		tr = this.GetComponent<Transform>();
		spawnPoint = tr.position;	
	}
	
	void Update ()
	{
		tr.Translate(Vector3.forward*Time.deltaTime*speed);
		if ((spawnPoint - tr.position).sqrMagnitude > fireRange)
		{
			StartCoroutine(this.DestroyBullet());
		}	
	}

	IEnumerator DestroyBullet()
	{
	//	Destroy(this.gameObject);
		yield return null;
	}

	void OnCollisionEnter(Collision other){		//检测碰撞
		//	Instantiate (explosion,transform.position,transform.rotation);
		
		//	Destroy(gameObject);
		if (other.gameObject.tag == "Enemy") {
			EnemyControl eec=other.gameObject.GetComponent<EnemyControl>();
			eec.SetPosition ();
		}
		
		
	}
}
