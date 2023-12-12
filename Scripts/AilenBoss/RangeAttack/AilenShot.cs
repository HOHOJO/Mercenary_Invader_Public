using UnityEngine;

public class AlienShot : MonoBehaviour
{
    [SerializeField] private Transform shotPosition;
    [SerializeField] private string bulletPoolTag = "BulletPool";
    [SerializeField] private float circleShotInterval = 2.0f;
    [SerializeField] private int maxCircleShots = 10;

    [SerializeField] private float spinDuration = 10.0f;
    [SerializeField] private float spinShotInitialTurnSpeed = 1.0f;
    [SerializeField] private float spinShotMaxTurnSpeed = 50.0f;
    [SerializeField] private float spinShotInterval = 0.2f;
    [SerializeField] private float restDuration = 5.0f;

    [SerializeField] private AudioClip bulletSound;
    private float timeSinceLastShot = 0f;
    private float restTimer = 0f;
    private float currentTurnSpeed;
    private int shotsFired = 0;
    private float spawnTimer;

    private ObjectPool objectPool;

    private enum ShotState
    {
        CircleShot,
        SpinShot,
        Rest
    }

    private ShotState currentState = ShotState.CircleShot;

    private void Start()
    {
        currentTurnSpeed = spinShotInitialTurnSpeed;

        objectPool = FindObjectOfType<ObjectPool>();
    }

    private void Update()
    {
        switch (currentState)
        {
            case ShotState.CircleShot:
                UpdateCircleShotState(circleShotInterval, CircleShoot);
                break;

            case ShotState.SpinShot:
                UpdateSpinShotState();
                break;

            case ShotState.Rest:
                UpdateRestState();
                break;
        }
    }

    private void UpdateCircleShotState(float interval, System.Action<Vector3> shootAction)
    {
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= interval && shotsFired < maxCircleShots)
        {
            shootAction(shotPosition.position);
            SoundManager.Instance.SFXPlay("BulletShot", bulletSound);
            timeSinceLastShot = 0f;
        }

        if (shotsFired >= maxCircleShots)
        {
            currentState = ShotState.Rest;
        }
    }

    private void UpdateSpinShotState()
    {
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= spinDuration)
        {
            currentState = ShotState.CircleShot;
            timeSinceLastShot = 0f;
            shotsFired = 0;

            currentTurnSpeed = Mathf.Min(currentTurnSpeed + 2.0f, spinShotMaxTurnSpeed);
        }
        else
        {
            SpinShoot();
        }
    }

    private void UpdateRestState()
    {
        restTimer += Time.deltaTime;

        if (restTimer >= restDuration)
        {
            currentState = ShotState.SpinShot;
            restTimer = 0f;
        }
    }

    private void SpinShoot()
    {
        transform.Rotate(Vector3.up * (currentTurnSpeed * 100 * Time.deltaTime));

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spinShotInterval)
        {
            ActivateBullet();
            spawnTimer = 0f;
        }
    }

    private void CircleShoot(Vector3 position)
    {
        for (int i = 0; i < 360; i += 20)
        {
            ActivateBullet(position, Quaternion.Euler(0, i, 0));
        }

        shotsFired++;
    }

    private void ActivateBullet()
    {
        GameObject bullet = objectPool.SpawnFromPool(bulletPoolTag);
        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
        }
        SoundManager.Instance.SFXPlay("BulletShot", bulletSound);
    }

    private void ActivateBullet(Vector3 position, Quaternion rotation)
    {
        GameObject bullet = objectPool.SpawnFromPool(bulletPoolTag);
        if (bullet != null)
        {
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
        }
    }
}
