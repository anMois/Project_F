using UnityEngine;

public class MainMenuInitializer : MonoBehaviour
{
    private void Start()
    {
        if (!UIManager.Instance.IsUIActive("Main Canvas"))
        {
            UIManager.Instance.ShowUI("Main Canvas");
        }
    }
}
