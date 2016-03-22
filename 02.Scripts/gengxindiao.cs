using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class gengxindiao : MonoBehaviour {
	private string roip="ssixjssixj.f3322.net";
	//	private string roip="192.168.1.114";
	private int lport=7005;
	public GameObject guan_li;//连接网络失败
	public AsyncOperation asOperReturn;         //异步场景加载的返回
	
	public Text m_hintText;                     //提示文字
	public Text m_progess;                      //进度值
	public Slider m_sliderProgress;             //进度条
	private int a=0;
	private uint _nowprocess;                   //显示的进度条值
	private bool _isTrans = false;              //是否进入过渡
	private int i=1;
	private float donglushijian=2.5f;//换子弹时间
	private float linjieshijian=300.5f;//连接时间退出
	void Start()
	{
		m_hintText.text = "正在检测网络......";
		_nowprocess = 0;
		StartCoroutine(ChangeScene());
	}
	
	void Update()
	{
	
		if(i==0 && a==0){

			switch (Network.peerType) {
			case NetworkPeerType .Disconnected:
				 
				Startconnect ();
				break;
			case NetworkPeerType .Client:
				Clientto ();
				break;
			case NetworkPeerType.Connecting:
				Debug.Log ("连接中");

				break;
			}

		}else if (i == 1 && a==0) {

//		Debug.Log(asOperReturn.progress);
			if (asOperReturn == null)
				return;
		
			uint toProcess;
		
			if (asOperReturn.progress < 0.9f)
				toProcess = (uint)(asOperReturn.progress * 100);
			else
				toProcess = 100;
		
			if (_nowprocess < toProcess)
				_nowprocess++;
		
			m_sliderProgress.value = _nowprocess * 0.01f;
			m_progess.text = _nowprocess.ToString () + "%";
		
			if (_nowprocess >= 0 && _nowprocess <= 30) {
				m_hintText.text = "正在检测网络......";


			} else if (_nowprocess <= 60) {
				m_hintText.text = "数据读取中......";
			} else if (_nowprocess <= 100) {
				m_hintText.text = "创建场景中......";
			}
			
		
			if (_nowprocess == 100)
				asOperReturn.allowSceneActivation = true;
		}
	}
	IEnumerator ChangeScene()
	{
		asOperReturn = Application.LoadLevelAsync("001");
		asOperReturn.allowSceneActivation = false;
		yield return asOperReturn;
	}

	public void dui_shu(){

		Application.Quit ();//退出游戏

	}
	public void ju_xu(){
		a = 0;
		guan_li.gameObject.SetActive (false);
	}
	void Startconnect(){
		linjieshijian-= Time .deltaTime;		//连接服务器
		if (linjieshijian <= 0) {
			guan_li.gameObject.SetActive (true);
			a=1;
		}
		
		donglushijian -= Time .deltaTime;		//连接服务器
		if (donglushijian <= 0) {

			NetworkConnectionError error = Network .Connect (roip, lport, "unitynetwork");//unitynetwork随便设的密码
			donglushijian=10.5f;
				Debug.Log (error);
		}
	}
	void Clientto(){
	//	wupinlan.wangluo_i = true;
		i = 1;	
	}

}
