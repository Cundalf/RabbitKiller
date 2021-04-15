using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{   
    Rigidbody _rb;

    [SerializeField]
    private float velocity;
    [SerializeField]
    private int danio;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
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
        // Se ignora totalmente al player
        if (other.gameObject.CompareTag("Player")) return;

        // Control de daño
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().Die();
            Destroy(gameObject);

        } else if (other.gameObject.CompareTag("BustMap")) 
        {
            other.gameObject.GetComponent<Bust>().applyBust();
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<EasterBunny>().healtControl(danio);
            Destroy(gameObject);
        }
        
    }
}
