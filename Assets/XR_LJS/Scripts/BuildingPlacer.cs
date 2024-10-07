using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
// using Newtonsoft.Json;  // Json �Ľ��� ���� �ʿ� (Newtonsoft.Json ��Ű�� �ʿ�)

public class BuildingPlacer : MonoBehaviour
{
    public static BuildingPlacer instance;  // �̱��� ����
    public LayerMask groundLayerMask;  // �ٴ� ���̾� ����ũ
                                       // public GridManager gridManager;  // �׸��� �Ŵ��� (�׸��� ���� ����)

    protected GameObject _toBuild;  // ���� ��ġ ���� ������Ʈ
    protected Camera _mainCamera;  // ���� ī�޶�
    protected Ray _ray;  // ����
    protected RaycastHit _hit;  // ����ĳ��Ʈ ��Ʈ ���
                                //  protected ObjectData _objectData;  // JSON���� ���� ������Ʈ ������

    private void Awake()
    {
        instance = this;  // �̱��� ����
        _mainCamera = Camera.main;  // ���� ī�޶� ����
    }

    private void Update()
    {
        // if (_objectData != null) // JSON���� ���� ������Ʈ�� ���� ���� ����
        {
            // ���콺 ��Ŭ������ ��ġ ���
            if (Input.GetMouseButtonDown(1))
            {
                CancelBuilding();
                return;
            }

            // ���콺�� UI ���� ������ ��ġ ������ �����
            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (_toBuild.activeSelf) _toBuild.SetActive(false);
                return;
            }
            else if (!_toBuild.activeSelf) _toBuild.SetActive(true);

            // ���콺 ��ġ���� ���� �߻�
           //  _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
           // if (Physics.Raycast(_ray, out _hit, 1000f, groundLayerMask))
           // {
           // ���� ��Ʈ ������ �׸��� ���� ����
           //Vector3 snappedPosition = gridManager.GetSnappedPosition(_hit.point, _objectData.size);
           //  _toBuild.transform.position = snappedPosition;

                // �׸��忡 ������Ʈ�� ������ ����������, ������ �ʷϻ����� ����
                // if (gridManager.IsOccupied(snappedPosition, _objectData.size))
                //  {
                //     SetPlacementMode(PlacementMode.Invalid);
                // }
                //    else
                //    {
                //        SetPlacementMode(PlacementMode.Valid);
                //    }

                //    // ���콺 ��Ŭ������ ��ġ Ȯ��
                //    if (Input.GetMouseButtonDown(0))
                //    {
                //        if (_toBuild.GetComponent<BuildingManager>().hasValidPlacement)
                //        {
                //            PlaceObject();
                //        }
                //    }
                //}
            else if (_toBuild.activeSelf) _toBuild.SetActive(false);
        }
    }

    // �����κ��� ���� JSON ������ �Ľ� �� ������Ʈ ���� �غ�
    //public void SetObjectDataFromJson(string jsonData)
    //{
    //    _objectData = JsonConvert.DeserializeObject<ObjectData>(jsonData);
    //    PrepareBuilding();
    //}

    protected virtual void PrepareBuilding()
    {
        if (_toBuild) Destroy(_toBuild);

        // �����κ��� ���� ������Ʈ �������� �ν��Ͻ�ȭ
        //  _toBuild = Instantiate(Resources.Load<GameObject>(_objectData.prefabPath));
        _toBuild.SetActive(false);

        BuildingManager m = _toBuild.GetComponent<BuildingManager>();
        m.isFixed = false;
        m.SetPlacementMode(PlacementMode.Valid);
    }

    private void CancelBuilding()
    {
        Destroy(_toBuild);
        _toBuild = null;
        //  _objectData = null;
    }

    private void PlaceObject()
    {
        BuildingManager m = _toBuild.GetComponent<BuildingManager>();
        m.SetPlacementMode(PlacementMode.Fixed);

        // �׸��忡 ������Ʈ ����
        //   gridManager.OccupyGrid(_toBuild.transform.position, _objectData.size);

        _toBuild = null;
        //  _objectData = null;
    }

    private void SetPlacementMode(PlacementMode mode)
    {
        BuildingManager m = _toBuild.GetComponent<BuildingManager>();
        m.SetPlacementMode(mode);
    }
}
