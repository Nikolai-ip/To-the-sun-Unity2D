using UnityEngine;

public class ItemsThrower : MonoBehaviour
{
    [SerializeField] private float _power;
    [SerializeField] private float _rotateForce;
    [SerializeField] private TrajectoryRenderer _trajectoryRenderer;
    [SerializeField] private Transform _playerTransform;
    private Camera _camera;
    private PickUpItem _pickUpItem;
    private Vector3 speed;

    private void Start()
    {
        _camera = Camera.main;
        _pickUpItem = GetComponent<PickUpItem>();
    }

    public void Throw(Item item)
    {
        var itemRB = item.GetComponent<Rigidbody2D>();
        item.GetComponent<FallingSound>().InFlight = true;
        itemRB.AddForce(speed, ForceMode2D.Impulse);
        _trajectoryRenderer.ToogleLineRenderer(false);
    }

    public void CalculateTrajectory()
    {
        _trajectoryRenderer.ToogleLineRenderer(true);

        var mouseInWorld = _camera.ScreenToWorldPoint(Input.mousePosition);
        _playerTransform.localScale = new Vector3(Mathf.Sign(mouseInWorld.x - _playerTransform.position.x), 1, 1);
        speed = (mouseInWorld - transform.position) * _power;

        _trajectoryRenderer.ShowTrajectory(_pickUpItem.CurrentItem.transform.position, speed);
    }

    public void ToogleLineRenderer(bool isEnabled)
    {
        _trajectoryRenderer.ToogleLineRenderer(isEnabled);
    }
}