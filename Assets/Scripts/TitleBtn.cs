using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleBtn : MonoBehaviour
{
    public void _onClickStart()
    {
        SceneManager.LoadScene(1);
    }
    public void Update()
    {
        if (Input.anyKeyDown)
        {
            _onClickStart();
        }
    }
}
