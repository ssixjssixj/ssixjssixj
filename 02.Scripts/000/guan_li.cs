using UnityEngine;
using System.Collections;

public class guan_li : MonoBehaviour {

	public AudioClip zidanwan;////退出游戏声音

	// Use this for initialization
	//	void Start () {
	//	
	//	}
	//	
	//	// Update is called once per frame
	//	void Update () {
	//	
	//	}
	public void dui_shu(){
		this.GetComponent<AudioSource> ().PlayOneShot (zidanwan);//播放声音
		Application.Quit ();//退出游戏
		print ("退出游戏");
	
	}

}
