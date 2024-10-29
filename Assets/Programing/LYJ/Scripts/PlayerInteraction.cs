using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float interactionDistance;
    [SerializeField] Image interactionUI;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        interactionUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        //플레이어가 상호작용 가능한 거리 내에 있을 때
        if (distance <= interactionDistance)
        {
            interactionUI.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                Interact();
            }
        }
        else
        {
            interactionUI.gameObject.SetActive(false);
        }
    }

    private void Interact()
    {
        Collider[] hitColliders = Physics.OverlapSphere(player.position, interactionDistance);
        foreach (var hitCollider in hitColliders)
        {
            IInteractable interactable = hitCollider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
                break;
            }
        }
    }
}
