using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class qiangkou : MonoBehaviour {

	public AudioClip qiangshengyin;////枪声声音
	public Rigidbody zidan;
	public float zidansudu=40f;




	public void kaiqiang(){
//		GetComponent<AudioSource> ().PlayOneShot (qiangshengyin);//播放声音		开枪
		Rigidbody projectile_bl = Instantiate (zidan, transform.position, transform.rotation) as Rigidbody;
		projectile_bl.velocity = transform.TransformDirection (new Vector3 (0, zidansudu, 0));
	}



}

