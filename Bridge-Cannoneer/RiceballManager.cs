// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;

public class RiceballManager : MonoBehaviour
{
    // Singleton
    public static RiceballManager instance;

    [SerializeField] private GameObject sensei = null;
    [SerializeField] private AudioClip[] bounceSounds = null;
    [SerializeField] private float randomBounceFactor = 0.2f;

    private Vector2 riceballToSenseiVector;
    private bool hasThrown = false;
    // Cached Component References
    private AudioSource audioSource;
    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        riceballToSenseiVector = transform.position - sensei.transform.position;
        audioSource = GetComponent<AudioSource>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasThrown)
        {
            LockRiceballToSensei();
            RiceBallToss();
        }
    }

    private void RiceBallToss()
    {
        if (Input.GetKeyDown(KeyCode.E)
            || Input.GetKeyDown(KeyCode.S))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-8, -15), Random.Range(8, 15));
            hasThrown = true;
        }
    }

    private void LockRiceballToSensei()
    {
        Vector2 senseiPos = new Vector2(sensei.transform.position.x,
                                        sensei.transform.position.y);
        transform.position = senseiPos + riceballToSenseiVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector3(Random.Range(0f, randomBounceFactor),
                                            Random.Range(0f, randomBounceFactor));

        if (hasThrown)
        {
            AudioClip audioClip = bounceSounds[0];
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void ResetRiceball()
    {
        hasThrown = false;
    }
}
