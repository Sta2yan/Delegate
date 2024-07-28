using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool<T> where T: MonoBehaviour
{
    private readonly Stack<T> Stack = new Stack<T>();
    private readonly Create<T> OnCreate;
    private readonly Request<T> OnInit;
    private readonly Release<T> OnDeInit;
    private readonly int CountCreateNew;

    public ObjectPool(int objectsOnStart, int countCreateNew, Create<T> onCreate, Request<T> onInit, Release<T> onDeInt)
    {
        IEnumerable<T> objects = onCreate(objectsOnStart);

        foreach (T obj in objects) 
            Stack.Push(obj);

        OnCreate = onCreate;
        OnInit = onInit;
        OnDeInit = onDeInt;
        CountCreateNew = countCreateNew;
    }
    
    public T Request() => OnInit(Stack, out T obj) ? obj : CreateNew();
    
    public void Release(T obj) => OnDeInit(obj);

    private T CreateNew()
    {
        IEnumerable<T> objects = OnCreate(CountCreateNew);

        foreach (T obj in objects) 
            Stack.Push(obj);

        return objects.First();
    }
}

public delegate IEnumerable<TResult> Create<out TResult>(int count); 
public delegate bool Request<T>(IEnumerable<T> enumerable, out T obj);
public delegate void Release<in T>(T obj); 
