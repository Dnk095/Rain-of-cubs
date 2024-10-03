using System;
using TMPro;
using UnityEngine;

public class Counter<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _quantitySpawnedObjects;
    [SerializeField] private TextMeshProUGUI _quatityActiveObjects;
    [SerializeField] private TextMeshProUGUI __quantityCreated;
    [SerializeField] private Spawner<T> _spawner;

    private Type _type;

    private void Awake()
    {
        _type = typeof(T);
    }

    private void OnEnable()
    {
        _spawner.CountObject += Draw;
    }

    private void OnDisable()
    {
        _spawner.CountObject -= Draw;
    }

    public void Draw(int created, int spawned, int active)
    {
        __quantityCreated.text = $"Количество созданных {_type} = {created}";
        _quantitySpawnedObjects.text = $"Количество заспавненых {_type} = {spawned}";
        _quatityActiveObjects.text = $"Количество активных {_type} = {active}";
    }
}
