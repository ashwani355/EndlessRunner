using System.Collections.Generic;
using UnityEngine;

public class ObsticlesManager : MonoBehaviour
{
    public GameObject obsticlePrefab;

    public Transform player;
    public Transform obsticleParent;

    public int maxObsticles = 20;

    [Tooltip("positions of cube on track")]
    public float[] xPositions;
    public float distanceToSpawnObsticles = 2f;

    private float tempDistance;
    private Queue<GameObject> allObsticles = new Queue<GameObject>();

    public delegate void ReSpawnObsticle();
    public static ReSpawnObsticle obsticleRespawn;
    private void OnEnable()
    {
        obsticleRespawn += ReArrangeObsticles;
    }

    private void Start()
    {
        tempDistance = distanceToSpawnObsticles;
        InitializeObsticles();
    }
    private void InitializeObsticles()
    {
        for (int i = 0; i <= maxObsticles; i++)
        {
            GameObject inst = Instantiate(obsticlePrefab);
            SetObsticlesPosition(inst);
        }
    }

    private void SetObsticlesPosition(GameObject inst)
    {
        int _randPos = Random.Range(0, xPositions.Length);
        inst.transform.position = new Vector3(xPositions[_randPos], TrackManager.instance.trackParent.position.y + 0.5f, tempDistance += distanceToSpawnObsticles);
        allObsticles.Enqueue(inst);
        inst.transform.SetParent(obsticleParent);
    }

    private void ReArrangeObsticles()
    {
        SetObsticlesPosition(allObsticles.Dequeue());
        ScoreManager.scoreEvent(1);
    }
    private void OnDisable()
    {
        obsticleRespawn -= ReArrangeObsticles;
    }

}
