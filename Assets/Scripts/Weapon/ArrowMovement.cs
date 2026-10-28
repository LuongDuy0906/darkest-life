using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    private float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }
}
