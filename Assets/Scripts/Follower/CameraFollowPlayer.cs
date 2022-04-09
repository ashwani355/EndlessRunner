using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    private void LateUpdate()
    {
        transform.position = new Vector3(0, player.transform.position.y, player.transform.position.z) + offset;
    }
}
