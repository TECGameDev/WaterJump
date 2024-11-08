using UnityEngine;

public class RisingWater : MonoBehaviour
{
    public float speed;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed, Space.World);
    }
}
