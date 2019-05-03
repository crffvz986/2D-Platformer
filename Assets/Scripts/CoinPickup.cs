using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinSFX;
    [SerializeField] int coinValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameSession>().AddToScore(coinValue);
        AudioSource.PlayClipAtPoint(coinSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
