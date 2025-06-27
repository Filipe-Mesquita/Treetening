using UnityEngine;

public class PlayerCatcherScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou no trigger é o PlayerCapsule
        if (other.CompareTag("Player"))
        {

            Debug.Log("Dentro do collider");
            Debug.Log($"{other.gameObject.name}");

            // Gets the Player's CharacterController
            CharacterController controller = other.GetComponent<CharacterController>();
            if (controller != null)
            {
                // Teleports the player to (0,0,0)
                controller.enabled = false; // Temporarily disable to avoid conflicts
                other.transform.position = Vector3.zero;
                controller.enabled = true; // Reactivates CharacterController
            }
        }
    }
}
