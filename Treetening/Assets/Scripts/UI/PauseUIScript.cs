using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUIScript : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] InteractScript interactScript;

    public void exitBtn()
    {
        SceneManager.LoadScene(0);
    }

    public void resumeBtn()
    {
        interactScript.handlePause();
    }
}
