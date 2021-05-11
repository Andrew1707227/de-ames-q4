using UnityEngine;

public class camara_compadre : MonoBehaviour
{
    public Transform Target;
    public Vector3 offset;
    public float SpeedSmooth;
    private void FixedUpdate()
    {
        Follow();
    }
    void Follow()
    {
        Vector3 TargetPosition = Target.position + offset;
        Vector3 SmoothPosition = Vector3.Lerp(transform.position, TargetPosition, SpeedSmooth * Time.fixedDeltaTime);
        transform.position = SmoothPosition;
    }
}
