﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip CoinPickupSFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 15)
        {
            AudioSource.PlayClipAtPoint(CoinPickupSFX, Camera.main.transform.position);
            FindObjectOfType<GameSession>().AddCoins();
            Destroy(gameObject);
        }   
    }
}
