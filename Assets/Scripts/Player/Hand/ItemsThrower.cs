using UnityEngine;

public class ItemsThrower : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] float _power;
    [SerializeField] TrajectoryRenderer _trajectoryRenderer;
    [SerializeField] private Transform _playerTransform;

    private Vector3 speed;
    private void Start()
    {
        _camera = Camera.main;
    }
    public void Throw(Item item)
    {
        var itemRB = item.GetComponent<Rigidbody2D>();
        itemRB.AddForce(speed, ForceMode2D.Impulse);
        _trajectoryRenderer.ToogleLineRenderer(false);
    }

    public void CalculateTrajectory()
    {
        _trajectoryRenderer.ToogleLineRenderer(true);

        float enter;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        new Plane(-Vector3.forward, transform.position).Raycast(ray, out enter);
        Vector3 mouseInWorld = ray.GetPoint(enter);
        _playerTransform.localScale = new Vector3(Mathf.Sign(mouseInWorld.x),1,1);
        speed = (mouseInWorld - transform.position) * _power;

        _trajectoryRenderer.ShowTrajectory(transform.position, speed);
    }

    public void ToogleLineRenderer(bool isEnabled)
    {
        _trajectoryRenderer.ToogleLineRenderer(isEnabled);
    }
}
