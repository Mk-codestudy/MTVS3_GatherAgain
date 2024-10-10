using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //���� �Է�/����� ����
using System.Text;
using System.IO; //�ѱ� ���� ó���� �� �� �ְ� ǥ�� �Ծ��� UTF-8 ��¼�� ����


[System.Serializable] //����ȭ
public struct UserData //struct? :: class�� ������.<����ü> ������ ó���� �갡 �� ������.
{
    public string username;
    public int usertype; //1: ������, 2: �����, 3: �Խ�Ʈ
    public string petname;
    public int petage;
    public bool petgender;
    public bool islost;


    //�Ʒ��� ���� �ڵ�� Vector(��, ��, ��)�� ���� ��ȣ �ȿ� �ٷ� ���� ��ġ �ο�����
    public UserData(string username, int usertype, string petname, int petage, bool petgender, bool islost)
    {
        this.username = username;
        this.usertype = usertype;
        this.petname = petname;
        this.petage = petage;
        this.petgender = petgender;
        this.islost = islost;
    }
}

public class JsonParser : MonoBehaviour
{
    void Start()
    {
        #region Json������ ����� �����ϴ� �ڵ�
        //����ü �ν��Ͻ��� �����
        UserData userdata1 = new UserData("�������", 1, "����", 13, false, true);
        #region �������� �ο����
        //UserData userdata1 = new UserData();
        //userdata1.username = "�������";
        //userdata1.usertype = 1; 
        //userdata1.petname = "����";
        //userdata1.petage = 13;
        //userdata1.petgender = false;
        //userdata1.islost = true;
        //�̷� ������ �ο����� ���� ������ �ʹ� ����� Vector3(��, ��, ��)<< �־��ִ� ��ó�� �Լ�ȭ�ؾ߰���???? 
        #endregion

        //����ü �����͸� Json ���·� ��ȯ�Ѵ�. 
        string jsonUser1 = JsonUtility.ToJson(userdata1, true); //�ι�° bool ������ �̻ڰ� ��ġ�ϴ��� ������ ���� (��� ��������)
                                                                //print(jsonUser1);
        #endregion

    }

    //Json�� ���� �����ϱ�
    public void SaveJsonData(string json)
    {
        // 1. ���� ��Ʈ���� ���� ���·� ����.
        //FileStream fs = new FileStream(); //1.���� ��� ����


        // 2. ���� ��Ʈ���� Json �����͸� ����� �����Ѵ�.

        // 3. ��Ʈ���� �ݾ��ش�.

        // 
    }

}
