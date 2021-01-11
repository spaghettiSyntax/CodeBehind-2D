// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;

public class NinjaStarManager : MonoBehaviour
{
    // Singleton
    public static NinjaStarManager instance;

    [SerializeField] private GameObject ninja = null;
    [SerializeField] private AudioClip[] bounceSounds = null;
    [SerializeField] private float randomBounceFactor = 0.2f;

    private Vector2 ninjaToStarVector;
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
        ninjaToStarVector = transform.position - ninja.transform.position;
        audioSource = GetComponent<AudioSource>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasThrown)
        {
            LockStarToNinja();
            NinjaStarToss();
        }
    }

    private void NinjaStarToss()
    {
        if (Input.GetKeyDown(KeyCode.W)
            || Input.GetKeyDown(KeyCode.S))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(8, 15), Random.Range(8,15));
            hasThrown = true;
        }
    }

    private void LockStarToNinja()
    {
        Vector2 ninjaPos = new Vector2(ninja.transform.position.x,
                                       ninja.transform.position.y);
        transform.position = ninjaPos + ninjaToStarVector;
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

    public void ResetNinjaStar()
    {
        hasThrown = false;
    }
}
