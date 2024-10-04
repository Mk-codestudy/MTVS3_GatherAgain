using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    public Grid Grid;
    public GameObject PObject;
    Vector3Int lastCellPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���콺 ������ ��ġ�� ���� �׸��� �� ��ǥ ���
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        Vector3Int cellPosition = Grid.WorldToCell(mouseWorldPosition);

        // ������Ʈ�� ���ο� ���� ���� ���� ����
        if (cellPosition != lastCellPosition)
        {
            lastCellPosition = cellPosition;

            // �׸��� �� ��ǥ�� ���� ��ǥ�� ��ȯ�Ͽ� 3D ������Ʈ ��ġ ������Ʈ
            Vector3 snappedPosition = Grid.CellToWorld(cellPosition);
            PObject.transform.position = snappedPosition;

            // ��ġ �������� ���ο� ���� ���� ����
            if (IsPlacementValid(cellPosition))
            {
                PObject.GetComponent<Renderer>().material.color = Color.green; // ��ġ ����: �ʷϻ�
            }
            else
            {
                PObject.GetComponent<Renderer>().material.color = Color.red; // ��ġ �Ұ�: ������
            }
        }

        // 90�� ������ ȸ��
        if (Input.GetKeyDown(KeyCode.R))
        {
            PObject.transform.Rotate(0, 90, 0);
        }

        // ��ġ Ȯ�� (���콺 Ŭ��)
        if (Input.GetMouseButtonDown(0) && IsPlacementValid(cellPosition))
        {
            PlaceObject(cellPosition);
        }
    }

    // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
    Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    // ��ġ �������� Ȯ�� (�浹 �˻�)
    bool IsPlacementValid(Vector3Int cellPosition)
    {
        Collider[] colliders = Physics.OverlapBox(Grid.CellToWorld(cellPosition), PObject.transform.localScale / 2);
        return colliders.Length == 0; // �浹�� ������ ��ġ ����
    }

    // ������Ʈ ��ġ Ȯ��
    void PlaceObject(Vector3Int cellPosition)
    {
        GameObject placedObject = Instantiate(PObject);
        placedObject.transform.position = Grid.CellToWorld(cellPosition);
        placedObject.GetComponent<Renderer>().material.color = Color.white; // ��ġ�� ������Ʈ�� ������� ǥ��
    }
}

