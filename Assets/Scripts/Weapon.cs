using UnityEngine;

public class Weapon : MonoBehaviour
{
    private const int BulletsOnStart = 3;
    private const int BulletCreateCount = 3;

    [SerializeField] private Transform _bulletsContainer;
    [SerializeField] private Transform _shotRoot;
    [SerializeField] private Bullet _bullet;
    
    private ObjectPool<Bullet> _bullets;

    private void Awake()
    {
        _bullets = new ObjectPool<Bullet>(BulletsOnStart, BulletCreateCount, CreateBullet, Init, DeInt);
    }

    public void Shoot()
    {
        _bullets.Request();
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        _bullets.Release(bullet);
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(_bullet, _bulletsContainer);
        bullet.gameObject.SetActive(false);
        return bullet;
    }
    
    private void Init(ref Bullet obj)
    {
        obj.gameObject.SetActive(true);
        obj.ResetSettings();
        obj.OnDestroy += OnDestroyBullet;
        obj.transform.position = _shotRoot.position;
    }
    
    private void DeInt(Bullet obj)
    {
        obj.OnDestroy -= OnDestroyBullet;
        obj.gameObject.SetActive(false);
    }
}
