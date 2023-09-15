using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public bool isGameover = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGameover = true;
        Destroy(collision.gameObject);

        Debug.Log("Plushie has reached the end of the belt");
    }
}
