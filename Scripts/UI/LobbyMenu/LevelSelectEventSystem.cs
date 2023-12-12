using UnityEngine;

public class LevelSelectEventSystem : MonoBehaviour
{
    public delegate void SelectLevelEventHandler();
    public static event SelectLevelEventHandler OnSelectNextLevel;
    public static event SelectLevelEventHandler OnSelectPreviousLevel;

    public static void SelectNextLevel()
    {
        OnSelectNextLevel?.Invoke();
    }

    public static void SelectPreviousLevel()
    {
        OnSelectPreviousLevel?.Invoke();
    }
}
