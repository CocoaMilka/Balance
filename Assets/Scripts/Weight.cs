using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weight : MonoBehaviour
{
    // Used for visual size and actual weight of object
    public int weight;

    // Stores the offset between the mouse and the object's position
    private Vector3 offset; 

    void Start()
    {
        // Generate a random weight between 1 and 10
        weight = Random.Range(1, 10);

        // Scale the GameObject based on the weight
        ScaleObject();
    }

    void ScaleObject()
    {
        // Adjust the scale of the GameObject based on the weight
        float scaleFactor = 0.1f * weight;

        transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }

    private void OnMouseDown()
    {
        // Calculate the offset between the mouse click position and the object's position
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        // Calculate the new position of the object based on the mouse position
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;

        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }
}
