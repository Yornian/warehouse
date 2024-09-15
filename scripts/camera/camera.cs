using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // 玩家对象的Transform
    public Vector3 offset=new Vector3(0,-0.3f,0); // 相机与玩家之间的偏移量
    public float smoothSpeed = 0.125f; // 相机跟随的平滑速度
    public Vector2 minBounds;
    public Vector2 maxBounds;
    private float fixedZ; // 固定相机的Z轴
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

            // 限制相机位置在边界范围内
            float clampedX = Mathf.Clamp(smoothedPosition.x, minBounds.x, maxBounds.x);
            float clampedY = Mathf.Clamp(smoothedPosition.y, minBounds.y, maxBounds.y);
            transform.position = new Vector3(clampedX, clampedY, fixedZ);
        }
    }
    void LateUpdate()
    {
        
    }
}
