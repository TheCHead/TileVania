using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingWater : MonoBehaviour
{
    [SerializeField] float risingRate = 0.2f;


    // Update is called once per frame
    void Update()
    {
        float yMove = risingRate * Time.deltaTime;
        transform.Translate(new Vector2(0f, yMove));
    }
}
