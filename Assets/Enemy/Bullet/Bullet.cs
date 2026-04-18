using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject explotion;
    [SerializeField] private GameObject guidePrefab;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float distanceWarning;

    private GameObject guide;

    private void Awake()
    {
        guide = Instantiate(guidePrefab, new Vector3(0, 99999999, 0), Quaternion.identity);
        guide.transform.rotation = Quaternion.Euler(-90f, 0, 0);
        guide.SetActive(false);
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, 100f, ground);

        if (hit.collider != null)
        {
            guide.SetActive(true);
            guide.transform.position = hit.point + Vector3.up/10;
            float scale = (1.1f - Mathf.Clamp01(hit.distance/distanceWarning)) * 13f;
            guide.transform.localScale = new Vector3(scale, scale, scale);
        }
        else guide.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(explotion, transform.position, Quaternion.identity);
        Destroy(guide);
        Destroy(gameObject);
    }
}

public class ThrowData
{
    public float angle;
    public float deltaY;
    public float deltaXZ;

    public Vector3 throwVelocity;

    public ThrowData(float angle, float deltaY, float deltaXZ, Vector3 throwVelocity)
    {
        this.angle = angle;
        this.deltaY = deltaY;
        this.deltaXZ = deltaXZ;
        this.throwVelocity = throwVelocity;
    }
}
