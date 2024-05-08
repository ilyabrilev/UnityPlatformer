using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel1 : MonoBehaviour
{
    private void Awake()
    {
        MusicManager.instance.PlayMusic("Dragon Castle", 2, 2);
    }
}
