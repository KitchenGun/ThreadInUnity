/*
 * 클라이언트
 * 서버에 데이타를 요청하고 응답받아 UI에 표시
*/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour 
{
    //Inspector
    public Text textUI;         //데이타를 표시할 UI
    //Concrete classes
    private SocketLib socket;   //서버와 통신하는 소켓라이브러리
    private Postbox postbox;    //메세지 큐를 관리하는 우편함

	void Awake () 
    {
        socket = new SocketLib();
        postbox = Postbox.GetInstance;
        
        //큐 탐색 시작
        StartCoroutine(CheckQueue());
        //데이타 요청 시작
        RequestData();
	}

    //서버에 데이타 요청
    private void RequestData()
    {
        socket.Request();
    }

    //서버에서 응답 받음
    public void ResponseData(string data)
    {
        textUI.text = data;
    }

    //큐를 주기적으로 탐색
    private IEnumerator CheckQueue()
    {
        //1초 주기로 탐색
        WaitForSeconds waitSec = new WaitForSeconds(1);

        while (true)
        {
            //우편함에서 데이타 꺼내기
            string data = postbox.GetData();

            //우편함에 데이타가 있는 경우
            if (!data.Equals(string.Empty))
            {
                //데이타로 UI 갱신
                ResponseData(data);
                yield break;
            }

            yield return waitSec;
        }
    }
}
