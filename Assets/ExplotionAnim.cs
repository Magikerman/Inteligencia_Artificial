using System.Collections;
using System.Drawing;
using UnityEngine;

public class ExplotionAnim : MonoBehaviour
{
    [SerializeField] private float finalSize;
    [SerializeField] private float animSpeed;

    private Renderer expRenderer;
    [SerializeField] private Renderer inRenderer;
    private float alpha = 0.8f;

    private void Awake()
    {
        transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        expRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(finalSize, finalSize, finalSize), animSpeed * Time.deltaTime);
        
        if (transform.localScale.x > finalSize - 0.5f)alpha = Mathf.Lerp(alpha, 0, animSpeed * Time.deltaTime);
        
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();

        expRenderer.GetPropertyBlock(mpb, 0);
        mpb.SetFloat("_Alpha", alpha);
        expRenderer.SetPropertyBlock(mpb, 0);

        inRenderer.GetPropertyBlock(mpb, 0);
        mpb.SetFloat("_Alpha", alpha);
        inRenderer.SetPropertyBlock(mpb, 0);

        if (alpha < 0.001f) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            other.gameObject.GetComponent<PlayerController>().Die();
        }
    }
}
