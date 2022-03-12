using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour
{
    [SerializeField]
    private LayerMask Player;

    [SerializeField]
    private GameObject DestroyParticle;

    public float damage;

    private void Start()
    {
        StartCoroutine(KillSelfOverTime());
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
            DestroySelf();
    }

    private IEnumerator KillSelfOverTime()
    {
        yield return new WaitForSeconds(3);
        DestroySelf();
    }

    private void DestroySelf()
    {
        Instantiate(DestroyParticle, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        Destroy(gameObject);
    }
}
