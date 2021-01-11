// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;
using Random = UnityEngine.Random;

public class CannonBallManager : MonoBehaviour
{
    // Singleton
    public static CannonBallManager instance;

    // Config Parameters
    [SerializeField] private CannonManager mainCannon = null;
    [SerializeField] private AudioClip[] bounceSounds = null;
    [SerializeField] private float randomBounceFactor = 0.2f;

    // States
    private bool hasLaunched = false;
    private float launchX;
    private float launchY;
    private Vector2 cannonToBallVector;
    
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
        audioSource = GetComponent<AudioSource>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        cannonToBallVector = transform.position - mainCannon.transform.position;        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasLaunched)
        {
            LockCannonBallToMainCannon();
            LaunchCannonBall();
        }
    }

    private void LaunchCannonBall()
    {
        if (Input.GetMouseButton(0)
            || Input.GetKeyDown(KeyCode.Space))
        {
            if (CannonManager.instance.cannonAngledLeft)
            {
                launchX = -10;
                launchY = 10;
            }
            else if (CannonManager.instance.cannonStraightUp)
            {
                launchX = 0;
                launchY = 15;
            }
            else if (CannonManager.instance.cannonAngledRight)
            {
                launchX = 10;
                launchY = 10;
            }

            GetComponent<Rigidbody2D>().velocity = new Vector2(launchX, launchY);

            // Play cannon shot sound
            audioSource.PlayOneShot(bounceSounds[1]);

            hasLaunched = true;
        }
    }

    private void LockCannonBallToMainCannon()
    {
        Vector2 mainCannonPos = new Vector2(mainCannon.transform.position.x,
                                            mainCannon.transform.position.y);
        transform.position = mainCannonPos + cannonToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector3(Random.Range(0f, randomBounceFactor), 
                                            Random.Range(0f, randomBounceFactor));

        if (hasLaunched)
        {
            AudioClip audioClip = bounceSounds[0];
            audioSource.PlayOneShot(audioClip);
        }        
    }

    public void ResetCannonBall()
    {
        hasLaunched = false;
    }
}
