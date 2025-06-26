using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour
{
    public void playBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void exitBtn()
    {
        Application.Quit();
    }
}
