using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 firstPoint;
    float sensitivity = 2.5f;

    [SerializeField] private LocationLogic ll;

    void Update()
    {
        TouchRotation();
    }
    void TouchRotation()
    {
        if (Input.touchCount > 0 && ll.canSwipe)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                firstPoint = Input.GetTouch(0).position;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector3 secondPoint = Input.GetTouch(0).position;

                float x = FilterGyroValues(secondPoint.x - firstPoint.x);
                RotateRightLeft(x * sensitivity);

                float y = FilterGyroValues(secondPoint.y - firstPoint.y);
                RotateUpDown(y * -sensitivity);

                firstPoint = secondPoint;
            }
        }
    }
    float FilterGyroValues(float axis)
    {
        float thresshold = 0.5f;
        if (axis < -thresshold || axis > thresshold)
        {
            return axis;
        }
        else
        {
            return 0;
        }
    }

    public void RotateUpDown(float axis)
    {
        transform.RotateAround(transform.position, transform.right, axis * Time.deltaTime);
    }

    //rotate the camera rigt and left (y rotation)
    public void RotateRightLeft(float axis)
    {
        transform.RotateAround(transform.position, Vector3.up, axis * Time.deltaTime);
    }
}