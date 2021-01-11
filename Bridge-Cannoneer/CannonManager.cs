// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    // Singleton
    public static CannonManager instance;

    // Config Parameters
    [SerializeField] private float screenWidthInUnits = 16f;
    [SerializeField] public GameObject cannonTop = null;

    public bool cannonStraightUp = false;
    public bool cannonRight = false;
    public bool cannonAngledRight = false;
    public bool cannonLeft = false;
    public bool cannonAngledLeft = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mousePosInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        Vector3 rotationVector = cannonTop.transform.rotation.eulerAngles;
        if (mousePosInUnits > cannonTop.transform.position.x + 2)
        {
            // Face cannon angled right
            cannonStraightUp = false;
            cannonAngledRight = true;
            cannonAngledLeft = false;

            rotationVector.z = -45;
            cannonTop.transform.rotation = Quaternion.Euler(rotationVector);
        }
        else if (mousePosInUnits < cannonTop.transform.position.x - 2)
        {
            // Face cannon angled left
            cannonStraightUp = false;
            cannonAngledRight = false;
            cannonAngledLeft = true;

            rotationVector.z = 45;
            cannonTop.transform.rotation = Quaternion.Euler(rotationVector);
        }
        else
        {
            // Face cannon straight up
            cannonStraightUp = true;
            cannonAngledRight = false;
            cannonAngledLeft = false;

            rotationVector.z = 0;
            cannonTop.transform.rotation = Quaternion.Euler(rotationVector);
        }

        if (NinjaManager.instance.isPushingLeft)
        {
            Vector2 cannonPos = new Vector2(transform.position.x - 0.1f, transform.position.y);
            cannonPos.x = Mathf.Clamp(cannonPos.x, 1.5f, 19.8f);
            transform.position = cannonPos;
        }

        if (NinjaManager.instance.isPushingRight)
        {
            Vector2 cannonPos = new Vector2(transform.position.x + 0.1f, transform.position.y);
            cannonPos.x = Mathf.Clamp(cannonPos.x, 1.5f, 19.8f);
            transform.position = cannonPos;
        }
    }

    public float GetXPos()
    {
        if (FindObjectOfType<GameSession>().IsAutoPlayEnabled())
        {
            return FindObjectOfType<CannonBallManager>().transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }        
    }
}
