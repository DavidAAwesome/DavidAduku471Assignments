using System;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField] private GameManager2 manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerStateManager>() != null)
        {
            manager.EndGame();
        }
    }
}
