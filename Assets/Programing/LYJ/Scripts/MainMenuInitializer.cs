using UnityEngine;

public class MainMenuInitializer : MonoBehaviour
{
    //메인메뉴 UI는 처음씬에만 존재해야함
    //UI 자체를 싱글톤으로 해둬서 이 스크립트가 없으면 메인 캔버스가 계속 등장하게됨
    //Main Menu Manager
    private void Start()
    {
        if (!UIManager.Instance.IsUIActive("Main Canvas"))
        {
            UIManager.Instance.ShowUI("Main Canvas");
        }
    }
}
