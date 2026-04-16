using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum state { idle, pursue, range, circle}

    [SerializeField] private state enemyState;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float speed;

    private Context context;
    private EnemyTree tree;
    private Rigidbody rb;

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
                break;
            case state.pursue:
                rotation = SteeringBehaviour.GetRotation(transform, context.player);
                break;
            case state.range:
                rotation = -SteeringBehaviour.GetRotation(transform, context.player);
                break;
        }
    }

    private void FixedUpdate()
    {
        rotation.y = transform.forward.y;
        if (rotation != Vector3.zero)
            transform.forward = Vector3.Lerp(transform.forward, rotation, rotSpeed * Time.fixedDeltaTime);

        Vector3 dir = transform.forward;
        dir.y = 0;
        if (enemyState != state.idle)
            rb.AddForce(dir * speed * Time.fixedDeltaTime, ForceMode.Impulse);
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
