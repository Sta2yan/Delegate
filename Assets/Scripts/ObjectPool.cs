using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T: MonoBehaviour
{
    private readonly Stack<T> _stack = new Stack<T>();
    private readonly Create<T> _onCreate;
    private readonly Request<T> _onInit;
    private readonly Release<T> _onDeInit;
    private readonly int _countCreateNew;

    public ObjectPool(int objectsOnStart, int countCreateNew, Create<T> onCreate, Request<T> onInit, Release<T> onDeInt)
    {
        if (objectsOnStart < 0)
            throw new IndexOutOfRangeException(nameof(objectsOnStart) + "can't be less 0");
        
        if (countCreateNew < 1)
            throw new IndexOutOfRangeException(nameof(objectsOnStart) + "can't be less 1");
        
        for (int i = 0; i < objectsOnStart; i++)
            _stack.Push(onCreate());

        _onCreate = onCreate;
        _onInit = onInit;
        _onDeInit = onDeInt;
        _countCreateNew = countCreateNew;
    }

    public T Request()
    {
        if (_stack.TryPop(out T obj) == false)
            obj = CreateNew();

        _onInit(ref obj);

        return obj;
    }

    public void Release(T obj)
    {
        _onDeInit(obj);

        _stack.Push(obj);
    }

    private T CreateNew()
    {
        for (int i = 0; i < _countCreateNew; i++)
            _stack.Push(_onCreate());

        return _stack.Pop();
    }
}

public delegate TResult Create<out TResult>(); 
public delegate void Request<T>(ref T obj);
public delegate void Release<in T>(T obj); 
