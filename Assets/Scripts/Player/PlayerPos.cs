using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{
    private GameMaster gm;
    private PlayerLife player;

    private void Start()
    {
        player = GetComponent<PlayerLife>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPointPos;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!player.IsDie)
                gm.deathsValue++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
