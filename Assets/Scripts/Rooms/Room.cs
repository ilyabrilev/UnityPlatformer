using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private Vector2[] initialPosition;
    
    private void Awake()
    {
        //Save initial position
        initialPosition = new Vector2[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                initialPosition[i] = enemies[i].transform.position;
            }
        }
    }

    public void ActivateRoom(bool _status)
    {
        //Activate/deactivate enemies
        //ToDo: I dont like this logick
        /*
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].SetActive(_status);
                enemies[i].transform.position = initialPosition[i];
            }
        }
        */
    }
}
