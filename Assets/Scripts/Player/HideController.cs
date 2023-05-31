using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideController : MonoBehaviour
{
    [SerializeField] private Transform _leftHideBorder;
    [SerializeField] private Transform _rightHideBorder;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _shelterMask;
    public bool IsHidden { get; private set; }
    private void Update()
    {
        IsHidden = Physics2D.OverlapCircle(_leftHideBorder.position, _radius, _shelterMask)
            && Physics2D.OverlapCircle(_rightHideBorder.position, _radius, _shelterMask);
    }
    private void OnDrawGizmos()
    {
        if (_leftHideBorder && _rightHideBorder)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_leftHideBorder.position, _radius);
            Gizmos.DrawWireSphere(_rightHideBorder.position, _radius);
        }
    }
}
