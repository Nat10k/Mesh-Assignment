using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRay : MonoBehaviour
{
    public float pressure = 10f;
    Vector3 initRightClickPos = Vector2.zero;
    Camera cam;
    float camSpeed = 2f;

    private void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(mouseRay, out hit, Mathf.Infinity))
            {
                if (hit.collider.GetComponent<Jellyfier>())
                {
                    Jellyfier hitJellyfier = hit.collider.GetComponent<Jellyfier>();
                    hitJellyfier.ApplyPressureToPoint(hit.point, pressure);
                }
            }
        } 

        if (Input.GetMouseButtonDown(1))
        {
            initRightClickPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            Vector2 delta = Input.mousePosition - initRightClickPos;
            float currRotation = cam.transform.rotation.eulerAngles.x < 180 ? cam.transform.rotation.eulerAngles.x : 360 - cam.transform.rotation.eulerAngles.x;
            if (currRotation < 80)
            {
                cam.transform.RotateAround(Vector3.zero, cam.transform.right, delta.y * camSpeed * Time.deltaTime);
            }
            cam.transform.RotateAround(Vector3.zero, cam.transform.up, delta.x * camSpeed * Time.deltaTime);
            cam.transform.rotation = Quaternion.Euler(cam.transform.rotation.eulerAngles.x, cam.transform.rotation.eulerAngles.y,
                                        0f);
        }
    }
}
