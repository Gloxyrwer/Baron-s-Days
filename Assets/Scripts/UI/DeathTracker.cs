using UnityEngine;
using TMPro;

public class DeathTracker : MonoBehaviour
{
    private GameMaster gm;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private int deaths = 0;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        deaths = gm.deathsValue;
        textMeshPro.text = $"Смерти: {deaths.ToString()}";
    }
}
