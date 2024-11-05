using UnityEngine;

public class Teleport : MonoBehaviour, IInteractable
{
    [SerializeField] StageManager stageManager;
    [SerializeField] Transform imageF;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        imageF.LookAt(player.position);
    }

    public void Interact()
    {
        if (stageManager.StageNum == stageManager.LastStage)
            UIManager.Instance.ShowUI("Boss Stage Canvas");
        else
            UIManager.Instance.ShowUI("Next Stage Canvas");
    }
}