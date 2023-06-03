using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trolley : MonoBehaviour
{
    [SerializeField] private Transform[] _handles;
    [SerializeField] private Transform[] _movePoints;
    public Transform[] Handles=>_handles;
    public Transform[] MovePoints=>_movePoints;
    public Transform Tr { get; private set; }
    private void Start()
    {
        Tr = GetComponent<Transform>();
    }

}
