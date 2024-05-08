using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel1 : MonoBehaviour
{
    private void Start()
    {
        MusicManager.instance.PlayMusic("Dragon Castle", 2, 2);
    }
}
