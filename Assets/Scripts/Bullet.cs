using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    public event Action<Bullet> OnDestroy;

    private void Update()
    {
        transform.Translate(transform.forward * _speed * Time.deltaTime);
    }

    public void ResetSettings()
    {
        print(name + " reset!");
        StartCoroutine(DestroyLogic());
    }

    private IEnumerator DestroyLogic()
    {
        yield return new WaitForSeconds(3f);
        OnDestroy?.Invoke(this);
    }
}
