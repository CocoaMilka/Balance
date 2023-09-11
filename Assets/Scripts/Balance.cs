using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Balance : MonoBehaviour
{

    int weight = 0;
    public TMP_Text weightDisplay;

    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collission detected");

        weight += collision.gameObject.GetComponent<Weight>().weight;
        Destroy(collision.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        weightDisplay.text = "Weight: " + weight;
    }
}
