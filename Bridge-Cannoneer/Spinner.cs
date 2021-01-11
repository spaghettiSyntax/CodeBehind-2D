// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] private float spinRotationSpeed = 360f;

    private void Update()
    {
        transform.Rotate(0, 0, spinRotationSpeed * Time.deltaTime);
    }
}
