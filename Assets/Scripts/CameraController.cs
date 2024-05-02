using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector2 velocity = Vector2.zero;

    private void Update()
    {
        //transform.position = Vector2.SmoothDamp(transform.position, new Vector2(currentPosX, transform.position.y), ref velocity, speed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}
