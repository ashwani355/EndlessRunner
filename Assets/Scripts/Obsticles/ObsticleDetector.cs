using UnityEngine;

public class ObsticleDetector : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obsticles"))
        {
            ObsticlesManager.obsticleRespawn();
        }
    }
}
