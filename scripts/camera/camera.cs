using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // ��Ҷ����Transform
    public Vector3 offset=new Vector3(0,-0.3f,0); // ��������֮���ƫ����
    public float smoothSpeed = 0.125f; // ��������ƽ���ٶ�
    public Vector2 minBounds;
    public Vector2 maxBounds;
    private float fixedZ; // �̶������Z��
    private void Start()
    {
        fixedZ = transform.position.z;
        player = GameObject.Find("Player").transform;

    
    }
    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // �������λ���ڱ߽緶Χ��
            float clampedX = Mathf.Clamp(smoothedPosition.x, minBounds.x, maxBounds.x);
            float clampedY = Mathf.Clamp(smoothedPosition.y, minBounds.y, maxBounds.y);
            transform.position = new Vector3(clampedX, clampedY, fixedZ);
        }
    }
    void LateUpdate()
    {
        
    }
}
