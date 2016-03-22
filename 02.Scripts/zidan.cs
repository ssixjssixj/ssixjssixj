using UnityEngine;
using System.Collections;

public class zidan : MonoBehaviour {
	
	//public GameObject explosion;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	//	Destroy(gameObject,1.1f);
		
	}

	void OnCollisionEnter(Collision other){
	//	Instantiate (explosion,transform.position,transform.rotation);
		
		Destroy(gameObject);
		if (other.gameObject.tag == "Enemy") {
			EnemyControl eec=other.gameObject.GetComponent<EnemyControl>();
			eec.SetPosition ();
			print (other);
			print ("hhhh");
		}
	
	
	}
}
