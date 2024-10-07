using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlacementMode
{
    Fixed,  // ��ġ�� Ȯ���� ����
    Valid,  // ��ġ�� ��ȿ�� ����
    Invalid // ��ġ�� ��ȿ���� ���� ����
}

public class BuildingManager : MonoBehaviour
{
    public Material validPlacementMaterial;  // ��ȿ�� ��ġ �� ����� ����
    public Material invalidPlacementMaterial;  // ��ȿ���� ���� ��ġ �� ����� ����

    public MeshRenderer[] meshComponents;  // �ǹ��� �� MeshRenderer ������Ʈ��
    private Dictionary<MeshRenderer, List<Material>> initialMaterials;  // �� MeshRenderer�� ���� ����� �������� ������ ��ųʸ�

    [HideInInspector] public bool hasValidPlacement;  // ���� ��ġ�� ��ȿ���� ���θ� ����
    [HideInInspector] public bool isFixed;  // ��ġ�� �����Ǿ����� ���θ� ����

    private int _nObstacles;  // �浹�ϴ� ��ֹ��� ����


    private void Awake()
    {
        hasValidPlacement = true;  // ��ġ�� ó���� ��ȿ�ϴٰ� ����
        isFixed = true;  // ó���� ��ġ�� �����Ǿ��ٰ� ����
        _nObstacles = 0;  // ó���� ��ֹ� ������ 0���� ����

        _InitializeMaterials();  // �ʱ� ���� ����
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isFixed) return;  // ��ġ�� ������ ���¶�� �浹�� ����

        // �ٴ� ������Ʈ�� ����
        if (_IsGround(other.gameObject)) return;

        _nObstacles++;  // �浹�� ��ֹ� ���� ����
        SetPlacementMode(PlacementMode.Invalid);  // ��ֹ��� �����ϸ� ��ȿ���� ���� ��ġ�� ����
    }

    private void OnTriggerExit(Collider other)
    {
        if (isFixed) return;  // ��ġ�� ������ ���¶�� �浹�� ����

        // �ٴ� ������Ʈ�� ����
        if (_IsGround(other.gameObject)) return;

        _nObstacles--;  // �浹�� ��ֹ� ���� ����
        if (_nObstacles == 0)  // ��ֹ��� ���� ��� ��ȿ�� ��ġ�� ����
            SetPlacementMode(PlacementMode.Valid);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _InitializeMaterials();  // �����Ϳ��� ��ũ��Ʈ�� ������ ������ �ʱ� ���� ������ �ٽ� ȣ��
    }
#endif

    public void SetPlacementMode(PlacementMode mode)
    {
        if (mode == PlacementMode.Fixed)
        {
            isFixed = true;  // ��ġ�� ������
            hasValidPlacement = true;  // ������ ��ġ�� �׻� ��ȿ
        }
        else if (mode == PlacementMode.Valid)
        {
            hasValidPlacement = true;  // ��ġ�� ��ȿ
        }
        else
        {
            hasValidPlacement = false;  // ��ġ�� ��ȿ���� ����
        }
        SetMaterial(mode);  // ��忡 �´� ���� ����
    }

    public void SetMaterial(PlacementMode mode)
    {
        if (mode == PlacementMode.Fixed)
        {
            // ��ġ�� ������ ��� ���� ������ ����
            foreach (MeshRenderer r in meshComponents)
                r.sharedMaterials = initialMaterials[r].ToArray();
        }
        else
        {
            // ��ȿ/��ȿ���� ���� ��ġ�� �´� ���� ����
            Material matToApply = mode == PlacementMode.Valid
                ? validPlacementMaterial : invalidPlacementMaterial;

            Material[] m;
            int nMaterials;
            foreach (MeshRenderer r in meshComponents)
            {
                nMaterials = initialMaterials[r].Count;  // ���� ����� ������ ���� Ȯ��
                m = new Material[nMaterials];
                for (int i = 0; i < nMaterials; i++)
                    m[i] = matToApply;  // ��� ������ �ش� ��忡 �´� ������ ����
                r.sharedMaterials = m;
            }
        }
    }

    private void _InitializeMaterials()
    {
        if (initialMaterials == null)
            initialMaterials = new Dictionary<MeshRenderer, List<Material>>();  // �ʱ�ȭ�� ��ųʸ� ����
        if (initialMaterials.Count > 0)
        {
            foreach (var l in initialMaterials) l.Value.Clear();  // ���� ���� ����Ʈ �ʱ�ȭ
            initialMaterials.Clear();  // ��ųʸ� ����
        }

        foreach (MeshRenderer r in meshComponents)
        {
            initialMaterials[r] = new List<Material>(r.sharedMaterials);  // �� MeshRenderer�� �ʱ� ������ ����
        }
    }

    private bool _IsGround(GameObject o)
    {
        // ������Ʈ�� �ٴ����� Ȯ���ϴ� �Լ�
        return ((1 << o.layer) & BuildingPlacer.instance.groundLayerMask.value) != 0;
    }
}
