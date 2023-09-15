using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Balance : MonoBehaviour
{

    public int weight = 0;
    public TMP_Text weightDisplay;
    public GameManager manager;

    Animator anim;
    void Start()

    {
        manager = FindObjectOfType<GameManager>().GetComponent<GameManager>();

        anim = GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collission detected");

        weight += collision.gameObject.GetComponent<Weight>().weight;
        Destroy(collision.gameObject);

        manager.plushieCount++;
        anim.SetTrigger("Activate");
    }

    // Update is called once per frame
    void Update()
    {
        weightDisplay.text = "Weight: " + weight;
    }
}
