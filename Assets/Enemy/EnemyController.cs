using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum state { idle, pursue, range, circle}

    [SerializeField] private state enemyState;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float speed;

    private bool inRange;

    private Context context;
    private EnemyTree tree;
    private Rigidbody rb;

    [SerializeField] private GameObject cannon;

    private Vector3 rotation = new Vector3(1,0,0);

    void Awake()
    {
        context = GetComponent<Context>();
        tree = GetComponent<EnemyTree>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        tree.Evaluate(this, context);

        switch(enemyState)
        {
            case state.idle:
                rotation = Vector3.zero;
                inRange = false;
                break;
            case state.pursue:
                rotation = SteeringBehaviour.GetRotation(transform, context.player);
                inRange = true;
                break;
            case state.range:
                rotation = -SteeringBehaviour.GetRotation(transform, context.player);
                inRange = true;
                break;
        }
    }

    private void FixedUpdate()
    {
        rotation.y = transform.forward.y;
        Vector3 dir = transform.forward;
        dir.y = 0;

        if (enemyState != state.idle)
        {
            transform.forward = Vector3.Lerp(transform.forward, rotation, rotSpeed * Time.fixedDeltaTime);
            rb.AddForce(dir * speed * Time.fixedDeltaTime, ForceMode.Impulse);

            if (enemyState == state.pursue) cannon.transform.forward = rotation;
            else cannon.transform.forward = -rotation;
        }
    }

    public void ChangeToIdle()
    {
        enemyState = state.idle;
    }

    public void ChangeToPursue()
    {
        enemyState = state.pursue;
    }

    public void ChangeToRange()
    {
        enemyState = state.range;
    }

    public void ChangeToCircle()
    {
        enemyState = state.circle;
    }
}
