using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    Camera cam;
    float angle;
    [Tooltip("Flip object when rotate over 90 degree")]
    [SerializeField] bool flip;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        if (Mathf.Approximately(Time.timeScale, 0f)) return;

        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseDirection = (mousePosition - transform.position).normalized;

        angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;

        if (flip) Flip();

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.localRotation = rotation;
    }

    void Flip()
    {
        float scaleX = 1;
        if (angle > 90 || angle < -90) {
            scaleX = -1;
            angle = -180 + angle;
        }
        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }
}
