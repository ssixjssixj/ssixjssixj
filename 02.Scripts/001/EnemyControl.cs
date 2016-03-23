using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {

	private NavMeshAgent nvAgent;
	private Transform tr;
	private Transform target;
	private  Transform pos2;//敌人
	public GameObject[] players;
	private  GameObject player;//寻找目标Others
	public GameObject qiangkou;

	private PhotonView pv;
	private float traceTime = 0.5f;//跟踪角色时间
	private float gongjishijian=1.0f;//攻击时间
	private Vector3 currPos;//接收别的窗口怪物信息
	private Quaternion currRot;//接收别的窗口怪物信息
	private int i=0;
	private int ii=0;
	void Start ()
	{
		pos2=this.transform;//防御塔位置
		pv = PhotonView.Get(this);
		pv.observed = this;
		
		nvAgent = GetComponent<NavMeshAgent>();
		tr = GetComponent<Transform>();
		
		players = GameObject.FindGameObjectsWithTag("Player");
		target = players[0].transform;
		
		currPos = tr.position;
		
		float dist = (target.position - tr.position).sqrMagnitude;//计算距离
		foreach (GameObject _player in players)
		{
			if ( (_player.transform.position-tr.position ).sqrMagnitude<dist)
			{
				target = _player.transform;
				break;
			}
		}

		nvAgent.destination = target.position;
	}
	
	void Update ()
	{
	
		if (PhotonNetwork.isMasterClient)
		{

	
			if (Time.time>traceTime)
			{
				this.GetComponent<Animation> ().CrossFade ("riflerun");//跑
				players = GameObject.FindGameObjectsWithTag("Player");
				if(players==null || target==null){ return;}
				float dist = (target.position - tr.position).sqrMagnitude;
				foreach (GameObject _player in players)
				{
					if ((_player.transform.position - tr.position).sqrMagnitude < dist)
					{
						target = _player.transform;
						break;
					}
				}

				nvAgent.CompleteOffMeshLink ();
				nvAgent.Resume ();
				nvAgent.SetDestination (target.position);//开始寻路

		//		nvAgent.destination = target.position;
				traceTime = Time.time + 0.4f;

			}
			if (nvAgent.remainingDistance < 10.0f && target!=null) {
				nvAgent.Stop (true);//停止寻路　　
				this.GetComponent<Animation> ().CrossFade ("rifleidle");//站
				pos2.LookAt (target.position);//转向寻路点
				kai_qiang();
			}

		}
		else
		{
			tr.position = Vector3.Lerp(tr.position, currPos, Time.deltaTime * 10.0f);//接收别的怪物移动
			tr.rotation = Quaternion.Lerp(tr.rotation, currRot, Time.deltaTime * 10.0f);//接收别的怪物移动
			this.GetComponent<Animation> ().CrossFade ("riflerun");//跑

		}
	}
	
	//同步CALLBACK函数
	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
		//	print ("hhhh111");
			//自身角色给别的网络用户发送我的数据
			stream.SendNext(tr.position);
			stream.SendNext(tr.rotation);


		}
		else
		{
			//		print ("hhhh2222");
			//接收其它角色数据
			currPos = (Vector3)stream.ReceiveNext();
			currRot = (Quaternion)stream.ReceiveNext();
		
		}
	}
	void kai_qiang(){
		gongjishijian -= Time.deltaTime;
		if (gongjishijian < 0) {

			StartCoroutine(this.kai_qiang1());
				pv.RPC("kaiqiangRPC",PhotonTargets.Others);//通知别的窗口开枪
			gongjishijian = 1.0f;
		
	}
	}
	IEnumerator kai_qiang1()	//开枪
	{	
		qiangkou.GetComponent<qiangkou> ().kaiqiang ();

		yield return null;
	}
	[PunRPC]
	void kaiqiangRPC()//接收到别的窗口通知自己开枪了
	{
		StartCoroutine(this.kai_qiang1());

	}

	public void SetPosition(){		//子弹脚本调用这里通知你死亡了------------------
		StartCoroutine(this.CreateBullet());
		pv.RPC("FireRPC",PhotonTargets.Others);//通知别的窗口自己死亡了

	}

	[PunRPC]
	void FireRPC()//接收到别的窗口通知自己死亡了
	{
	
		StartCoroutine(this.CreateBullet());
	}
	IEnumerator CreateBullet()	//死亡
	{	
		this.GetComponent<Animation> ().CrossFade ("death");//死
		Destroy(gameObject,1.3f);
		yield return null;
	}

}
