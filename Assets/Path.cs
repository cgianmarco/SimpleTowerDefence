using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public static Path Instance;

    private void Awake()
    {
        Instance = this;
    }
}
