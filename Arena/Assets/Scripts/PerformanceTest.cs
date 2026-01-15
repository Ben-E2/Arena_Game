using UnityEngine;

public class PerformanceTest : MonoBehaviour
{
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = -1;
    }
}
