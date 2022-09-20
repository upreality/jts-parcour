using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartListener : MonoBehaviour
{
    [SerializeField] private UnityEvent onStart;
    void Start()
    {
        if(onStart!=null)
            onStart.Invoke();
    }
}
