using System.Collections;
using Game.Scripts.Controller.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouDiedSceneController : MonoBehaviour
{
    [SerializeField]
    private int RespawnTime;

    void Start()
    {
        InputHandler.Instance.DisableInput();
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(RespawnTime);

        PlayerController.Instance.Respawn();
        InputHandler.Instance.EnableInput();
        SceneManager.UnloadSceneAsync("YouDied");
    }
}
