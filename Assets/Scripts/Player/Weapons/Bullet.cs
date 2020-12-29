using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float velocity;
    Rigidbody _rb;
    private int danio;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        this.danio = 1;
    }

    void Update()
    {
        _rb.velocity = transform.forward * velocity;
    }

    public void cargarDanio(int danio) 
    {
        this.danio = danio;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().Die();

        } else if (other.gameObject.CompareTag("BustMap")) 
        {
            other.gameObject.GetComponent<Bust>().appliBust();
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<EasterBunny>().healtControl(danio);
        }
        DestroyImmediate(gameObject);
    }
}
