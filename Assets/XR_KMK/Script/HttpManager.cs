using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; //Http ��Ʈ��ũ ����� ���� ���ӽ����̽� �߰�
using System.Text;
using UnityEngine.UI;
using System; //Json, csv�� ���� ������ ���ڵ�(UTF-8) ����� ���� ���ӽ����̽� �߰�

public class HttpManager : MonoBehaviour
{
    public string url; //Public ������ �ּҸ� ���ϰ� ������� �� �ְ� �Ѵ�.

    [Header("��ư ����")]
    public Button btn_idle;
    public Button btn_image;
    public Button btn_PostJson;


    [Header("GET ����� ���� ����")]
    public RawImage img_response; //�̹����� �������� �� ���
    public Text text_response; //����� �ؽ�Ʈ

    void Start()
    {

    }

    public void GetIdle() //Get��� ��ư ���� �Լ�
    {
        btn_idle.interactable = false; //��ư ��Ȱ��ȭ
        StartCoroutine(GetIdleRequest(url));
    }

    //Get ��� �⺻�� �ڷ�ƾ
    IEnumerator GetIdleRequest(string url)
    {
        //http Get ��� �غ� �Ѵ�.
        UnityWebRequest request = UnityWebRequest.Get(url);

        //������ Get ��û�� �ϰ�, �����κ��� ������ �� ������ ����Ѵ�.
        yield return request.SendWebRequest(); //�̰� ������

        //���� �����κ��� �� ������ �����̶��... (���� �ڵ尡 200)
        //������� �����͸� ����Ѵ�
        if (request.result == UnityWebRequest.Result.Success)
        {
            string response = request.downloadHandler.text;
            print(response);
            text_response.text = response;
        }
        //�׷��� �ʴٸ�...(400, 404, etc...)
        else
        {
            //���� ������ ����Ѵ�
            print(request.error);
            text_response.text = request.error;
        }

        btn_idle.interactable = true; //��ư �ٽ� Ȱ��ȭ
    }


    public void GetImeage()
    {
        btn_image.interactable = false;
        StartCoroutine(GetImeageRequest(url));
    }
    //Get ��� �̹��� �޾ƿ���
    IEnumerator GetImeageRequest(string url)
    {
        // get(Texture) ����� �غ��Ѵ�.
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

        // ������ ��û�� �ϰ�, ������ ���� ������ ��ٸ���.
        yield return request.SendWebRequest();

        // ����, ������ �����̶��...
        if (request.result == UnityWebRequest.Result.Success)
        {
            // ���� �ؽ��� �����͸� Texture2D ������ �޾Ƴ��´�.
            Texture2D response = DownloadHandlerTexture.GetContent(request);

            // Texture2D �����͸� img_response�� texture ������ �־��.
            img_response.texture = response;

            // text_response�� ���� �ڵ� ��ȣ�� ����Ѵ�.
            text_response.text = "���� - " + request.responseCode.ToString();
        }
        // �׷��� �ʴٸ�...
        else
        {
            // ���� ������ text_response�� ����Ѵ�.
            print(request.error);
            text_response.text = request.error;
        }

        btn_image.interactable = true;
    }

    [System.Serializable]
    public struct RequestImage
    {
        public string img;
    }


    //Post ���
    //Json �����͸� Post�ϴ� �Լ�

    public void PostJson()
    {
        btn_PostJson.interactable = false;
        StartCoroutine(PostJsonRequest(url));
    }

    IEnumerator PostJsonRequest(string url)
    {
        JoinUserData userData = new JoinUserData(1, "asdf", "�������"); //�׽�Ʈ�Ҷ��� �� ���� �����ϸ� ��!!!!!!!!
        string userjsondata = JsonUtility.ToJson(userData, true); //Json���� ��ȯ!
        byte[] jsonBins = Encoding.UTF8.GetBytes(userjsondata); //����Ʈ ���·� �ٲ�� ������ �Ǵϱ� ���̽��� ����Ʈ�� ��ȯ!

        UnityWebRequest request = new UnityWebRequest(url, "POST"); //����Ʈ!

        request.SetRequestHeader("Content-Type", "application/json"); //�������� � ������ ���´��� ����� ���� �˷��ִ� ����. ������ �� ������ ���� ���� �غ� ��.
        request.uploadHandler = new UploadHandlerRaw(jsonBins); //����Ʈ�� ��ȯ�� Json���� ����!
        request.downloadHandler = new DownloadHandlerBuffer(); //����Ʈ ���·� ���� �Ŷ� Buffer��� �� �����

        //������ Post�� �����ϰ� ������ �� ������ ��ٸ���.
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success) //��ż���
        {
            //�ٿ�ε��ڵ鷯���� �ؽ�Ʈ ���� ���� �� UI�� ����ϱ�
            string response = request.downloadHandler.text;
            text_response.text = response;
            Debug.Log(response);
        }
        else //����
        {
            text_response.text = request.error;
            Debug.LogError(request.error);
        }
        btn_PostJson.interactable = true;
    }


}

    [System.Serializable]
    public struct JoinUserData
    {
        public int id;
        public string password;
        public string nickname;

        public JoinUserData(int id, string password, string nickname)
        {
            this.id = id;
            this.password = password;
            this.nickname = nickname;
        }
    }
