using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    GameObject mouseIndicator, cellIndicator;
    [SerializeField]
    InputMGR inputMGR;
    [SerializeField]
    Grid grid;

    [SerializeField]
    ObjectsDatabaseSO database;
    int selectedobjectIndex = -1;

    [SerializeField]
    GameObject gridVisualization;

    private GridData floorData, furnitureData;

    private Renderer previewRenderer;

    private List<GameObject> placedGameObject = new();

    private void Start()
    {
        StopPlacemet();
        floorData = new();
        furnitureData = new();
        previewRenderer = cellIndicator.GetComponentInChildren<Renderer>();
    }

    public void startPlacement(int ID) //https://youtu.be/i9W1kqUinIs?si=3vk3xC-v2FYZCUAW 
    {
       // StopPlacemet();
        // ����Ÿ ���̽� ã�� ���ٽ����� �ε��� ã�Ƽ� ID�� ã�� ��ȯ
        selectedobjectIndex = database.objectData.FindIndex(data => data.ID == ID);
        if (selectedobjectIndex < 0)
        {
            Debug.LogError($"No ID Found {ID}");
            return;
        }
        gridVisualization.SetActive(true);
        cellIndicator.SetActive(true);
        inputMGR.OnClicked += PlaceStructure;
        inputMGR.OnExit += StopPlacemet;
    }

    // �׸��� ���� ���� �� ���콺 ��ư�� ������ �� ��ü�� �ν��Ͻ�ȭ �ϰ� ��ü�� ��ġ�� ��ġ�ϴ� �ڵ�
    private void PlaceStructure() 
    {
        if(inputMGR.isPointerOverUI())
        {
            return;
        }
        Vector3 mousePosition = inputMGR.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        bool placementValidity = CheckPlacementValidity(gridPosition, selectedobjectIndex);
        if (placementValidity == false)
            return;
        // ���� ������ ��ü �ε����� �����ϰ� ��Ʈ �������� �߰��� ���̹Ƿ� ���� �ε����� ��ü�Ͽ� ���� ��ü ������ ������ �������� ���
        GameObject newObject = Instantiate(database.objectData[selectedobjectIndex].Prefab);        
        newObject.transform.position = grid.CellToWorld(gridPosition);
        placedGameObject.Add(newObject);
        GridData selectedData = database.objectData[selectedobjectIndex].ID == 0 ?
            floorData :
            furnitureData;
        selectedData.AddObjectAt(gridPosition,
            database.objectData[selectedobjectIndex].Size,
            database.objectData[selectedobjectIndex].ID,
            placedGameObject.Count - 1);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedobjectIndex)
    {
        GridData selectedData = database.objectData[selectedobjectIndex].ID == 0 ? 
            floorData : 
            furnitureData;

        return selectedData.CanPlaceObjectAt(gridPosition, database.objectData[selectedobjectIndex].Size);
    }

    private void StopPlacemet()
    {
        selectedobjectIndex = -1;
        gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);
        inputMGR.OnClicked -= PlaceStructure;
        inputMGR.OnExit -= StopPlacemet;
    }

    private void Update()
    {
        if (selectedobjectIndex < 0)  
            return; 
        Vector3 mousePosition = inputMGR.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        bool placementValidity = CheckPlacementValidity(gridPosition, selectedobjectIndex);
        previewRenderer.material.color = placementValidity ? Color.yellow : Color.red;

        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);

    }
}
