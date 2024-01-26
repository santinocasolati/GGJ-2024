using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpinchoLook : MonoBehaviour
{
    public Transform headBone;
    public float rotationSpeed = 5f;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Plane plane = new Plane(Vector3.forward, transform.position);

        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 targetPoint = ray.GetPoint(distance);
            Quaternion desiredRot = Quaternion.Euler(CalculateResult(targetPoint));

            headBone.localRotation = Quaternion.Lerp(headBone.localRotation, desiredRot, Time.deltaTime * rotationSpeed);
        }
    }

    Vector3 CalculateResult(Vector3 input)
    {
        float xResult = 10 * input.x + 1.5f * input.z;
        float yResult = 0;
        float zResult = input.y * 5;

        xResult = Mathf.Clamp(xResult, -80f, 80f);
        zResult = Mathf.Clamp(zResult, -60f, 60f);

        return new Vector3(xResult, yResult, zResult);
    }
}
