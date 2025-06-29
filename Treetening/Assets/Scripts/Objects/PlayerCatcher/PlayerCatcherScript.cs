using UnityEngine;

public class PlayerCatcherScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Checks if the object that entered the trigger is the Player
        if (other.CompareTag("Player"))
        {

            Debug.Log("Inside the collider");
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
