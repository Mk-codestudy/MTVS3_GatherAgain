using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputMGR : MonoBehaviour
{
    [SerializeField]
    Camera sceneCamera;

    Vector3 lastPosition;

    [SerializeField]
    LayerMask placementLayermask;

    public event Action OnClicked, OnExit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { //���ű��� ����ǥ�� �����ϰ� ȣ���ϴ� ����� 
            // �۾� �̺�Ʈ�� �߻��ϰ� ���𰡰� ���ŵǰ� �ִ� ���
            // �Ϻ� �޼��尡 �Ҵ�Ǿ� �ش��ư�� Ŭ���Ǿ��� � ���� �߻��ؾ� �Ѵٴ� ���� �� �޼��忡 �˸�
            OnClicked?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnExit?.Invoke();
        }
    }

    public bool isPointerOverUI()
        // ���ٽ� �̺�Ʈ �ý��� c#���� �ҷ��ͼ� true / false ��ȯ 
        => EventSystem.current.IsPointerOverGameObject();


    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit,100, placementLayermask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }
}
