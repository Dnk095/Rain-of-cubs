using System;
using TMPro;
using UnityEngine;

public class Counter<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _quantitySpawnedObjects;
    [SerializeField] private TextMeshProUGUI _quatityActiveObjects;
    [SerializeField] private TextMeshProUGUI _quantityCreatedObject;
    [SerializeField] private Spawner<T> _spawner;

    private Type _type;

    private void Awake()
    {
        _type = typeof(T);
    }

    private void OnEnable()
    {
        _spawner.ChangedQuantityCreatedObject += DrawCreatedQuantity;
        _spawner.ChangedQuantityActiveObject += DrawActiveQuantity;
        _spawner.ChangedQuantitySpawnedObject += DrawSpawnedQuantity;
    }

    private void OnDisable()
    {
        _spawner.ChangedQuantityCreatedObject -= DrawCreatedQuantity;
        _spawner.ChangedQuantityActiveObject -= DrawActiveQuantity;
        _spawner.ChangedQuantitySpawnedObject -= DrawSpawnedQuantity;
    }

    private void DrawCreatedQuantity(int quantity)
    {
        _quantityCreatedObject.text = $"Количество созданных {_type} = {quantity}";
    }

    private void DrawSpawnedQuantity(int quantity)
    {
        _quantitySpawnedObjects.text = $"Количество заспавненых {_type} = {quantity}";
    }

    private void DrawActiveQuantity(int quantity)
    {
        _quatityActiveObjects.text = $"Количество активных {_type} = {quantity}";
    }
}
