using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectsDatabaseSO : ScriptableObject
{
    public List<ObjectData> objectData;
    internal object objectsData;
}

[Serializable]
public class ObjectData // ���߿��� ������ �����ؾ��� �͵� // ��ü ������ ����
                        //https://youtu.be/i9W1kqUinIs?si=1JJWg8ybyXCi-vbo ���� �״��
{
    public int MyProperty {  get; set; }
    [field:SerializeField]
    public string Name{ get; private set; }
    [field:SerializeField]
    public int ID { get; private set; }
    [field:SerializeField]
    public Vector2Int Size { get; private set; } = Vector2Int.one;
    [field:SerializeField]
    public GameObject Prefab { get; private set; }
}
