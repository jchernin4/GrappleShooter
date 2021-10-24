using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
    public Transform player1;

    void Update() {
        transform.position = player1.position;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if (plane.Raycast(ray, out distance)) {
            Vector3 target = ray.GetPoint(distance);

            Vector3 direction = target - transform.position;
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }

        /* Vector3 mousePos = Input.mousePosition;
         mousePos.z = Camera.main.nearClipPlane;
         Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
         worldPosition.y = 0.5f;

        transform.LookAt (mousePos);
       */
    }
}