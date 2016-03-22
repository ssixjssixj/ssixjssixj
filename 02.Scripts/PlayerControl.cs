using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;

public class PlayerControl : MonoBehaviour {
	public float speed=5.0f;
	public float rotSpeed=120.0f;
	
	private Transform tr;
	private PhotonView pv;
	public Material[] _material;//给不同角色，不同颜色
	
	private Vector3 currPos;
	private Quaternion currRot;
	//只有自己角色发射子弹 
	//public Transform firePos;
	public GameObject qiangkou;

	//同步CALLBACK函数
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			print ("1111");
			//自身的角色数据 给 别的网络用户 发送
			stream.SendNext(tr.position);
			stream.SendNext(tr.rotation);
		}
		else
		{
			print ("2222");
			//别的角色数据，这里接收
			currPos = (Vector3)stream.ReceiveNext();
			currRot = (Quaternion)stream.ReceiveNext();
		}
	}
	
	void Start () 
	{
		tr=GetComponent<Transform>();
		pv = GetComponent<PhotonView>();
		
		if (pv.isMine)
		{
			//只有自身的角色，才给设像头的控制权连接
			Camera.main.GetComponent<SmoothFollow>().target = tr;
			this.GetComponent<Renderer>().material = _material[0];
		}
		else
		{
			this.GetComponent<Renderer>().material = _material[1];
		}
		//同步CALLBACK函数发生的话，必须本页面代码连接上去
		pv.ObservedComponents[0] = this;
		print (pv.ObservedComponents[0]);

	}
	
	void Update () 
	{
		if (pv.isMine)
		{

			//只有自身的角色才能用键盘控制
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");
			
			tr.Translate(Vector3.forward * v * Time.deltaTime * speed);
			tr.Rotate(Vector3.up * h * Time.deltaTime * rotSpeed);
			
			if (Input.GetButtonDown("Fire1"))
			{
	//			print ("4444");
				Fire();
			}
		}
		else
		{
		//	print ("5555");
			//连接网络的别的用户的话，用时时传送的变量来移动
			tr.position = Vector3.Lerp(tr.position, currPos, Time.deltaTime * 10.0f);//接收别的角色移动
			tr.rotation = Quaternion.Lerp(tr.rotation, currRot, Time.deltaTime * 10.0f);//接收别的角色移动
		}
	}   

	void Fire(){
	
	
		StartCoroutine(this.CreateBullet());
		pv.RPC("FireRPC",PhotonTargets.Others);
	}
	IEnumerator CreateBullet()
	{

		qiangkou.GetComponent<qiangkou>().kaiqiang();
//		Instantiate(bullet, firePos.position, firePos.rotation);
		yield return null;
	}
	[PunRPC]
	void FireRPC()
	{

		StartCoroutine(this.CreateBullet());
	}
	}
	