/*
 * 서버와 통신하는 소켓 라이브러리 흉내
*/

using System.Threading;

public class SocketLib
{
    //서버 작업 처리 동안 대기할 쓰레드
    private Thread thread;
    
    public SocketLib()
    {
        thread = new Thread(new ThreadStart(Run));
    }

    //서버에 요청
    public void Request()
    {
        thread.Start();
    }

    //서버 작업 진행
    void Run()
    {
        //서버 작업 시작
        string result;

        Thread.Sleep(3000);

        //서버 작업 종료
        result = "This is result data.";

        Response(result);
    }

    //응답 받음
    public void Response(string result)
    {
        //결과 데이타를 큐에 넣기
        Postbox.GetInstance.PushData(result);
    }
}
