using UnityEngine;
using UnityEngine.UI;

public class BattleSceneTimer : MonoBehaviour
{
    [Tooltip("Our battle scene timer in seconds.")]
    [SerializeField] private float levelTimer = 10f;

    private bool triggeredLevelFinished = false;

    void Update()
    {
        if (triggeredLevelFinished)
        {
            return;
        }

        GetComponent<Slider>().value = Time.timeSinceLevelLoad / levelTimer;

        bool timerFinished = (Time.timeSinceLevelLoad >= levelTimer);

        if (timerFinished)
        {
            FindObjectOfType<BattleSceneController>().LevelTimerFinished();
            triggeredLevelFinished = true;
        }
    }
}
