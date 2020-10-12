﻿using System.Collections.Generic;
using UnityEngine;

public class BulletPoolsContainer : MonoBehaviour
{
    public static BulletPoolsContainer Instance
    {
        get;
        private set;
    }

    [Tooltip("ДОЛЖНЫ ИДТИ В ТОМ ЖЕ ПОРЯДКЕ, ЧТО И ТИПЫ ДВИЖЕНИЙ В ПЕРЕЧИСЛЕНИИ")]
    [SerializeField] private GameObject[] _bulletsPrefabs = null;
    private List<BulletPool> _pools = new List<BulletPool>();
    private Transform _transform;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        _transform = transform;

        for (int i = 0; i < _bulletsPrefabs.Length; ++i)
        {
            _pools.Add(new BulletPool(_bulletsPrefabs[i], _transform));
        }
    }

    public GameObject GetObjectFromPool(BulletTypes type)
    {
        return _pools[(int)type].GetObjectFromPool();
    }

    public void ReturnObjectToPool(BulletTypes type, GameObject go)
    {
        _pools[(int)type].ReturnObjectToPool(go);
    }
}

public class BulletPool
{
    private GameObject _prefab = null;
    private Transform _transform = null;
    private Queue<GameObject> _pool = new Queue<GameObject>();

    public BulletPool(GameObject prefab, Transform transform)
    {
        _prefab = prefab;
        _transform = transform;
    }

    private void AddObjectToPool(int amount)
    {
        GameObject go;
        for (int i = 0; i < amount; ++i)
        {
            go = GameObject.Instantiate(_prefab);
            go.transform.parent = _transform;
            go.SetActive(false);
            _pool.Enqueue(go);
        }
    }

    public GameObject GetObjectFromPool()
    {
        if (_pool.Count > 0)
        {
            GameObject go;
            go = _pool.Dequeue();
            go.SetActive(true);
            go.transform.parent = null;
            return go;
        }
        else
        {
            AddObjectToPool(10);
            return GetObjectFromPool();
        }
    }

    public void ReturnObjectToPool(GameObject go)
    {
        go.transform.parent = _transform;
        go.SetActive(false);
        _pool.Enqueue(go);
    }
}

public enum BulletTypes
{
    StraightMove,
    PlayerBullet,
}