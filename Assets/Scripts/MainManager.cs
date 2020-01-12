using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod()]
    private static void Init()
    {
        Application.targetFrameRate = 60;
    }
}
