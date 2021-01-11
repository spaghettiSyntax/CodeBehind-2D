// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;

public class NinjaManager : MonoBehaviour
{
    public static NinjaManager instance;

    public bool isPushingLeft = false;
    public bool isPushingRight = false;

    [SerializeField] private CannonManager mainCannon = null;

    private Animator animator;
    private Vector2 cannonToNinjaVector;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        cannonToNinjaVector = transform.position - mainCannon.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)
            || Input.GetKeyDown(KeyCode.A))
        {
            animator.SetBool("isPushing", true);
            isPushingLeft = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow)
            || Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("isPushing", false);
            isPushingLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)
            || Input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool("isPushing", true);
            isPushingRight = true;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow)
            || Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("isPushing", false);
            isPushingRight = false;
        }

        LockNinjaToMainCannon();
    }

    private void LockNinjaToMainCannon()
    {
        Vector2 mainCannonPos = new Vector2(mainCannon.transform.position.x,
                                            mainCannon.transform.position.y);
        transform.position = mainCannonPos + cannonToNinjaVector;
    }
}
