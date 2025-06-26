using UnityEngine;

public class TestUIScript : MonoBehaviour
{
    void Update()
    {
        var ui = FindObjectOfType<PlayerStatsUI>();

        if (Input.GetKeyDown(KeyCode.G))
            ui.AddGold(10);

        if (Input.GetKeyDown(KeyCode.H))
            ui.AddGold(-10);

        if (Input.GetKeyDown(KeyCode.UpArrow))
            ui.AddMoralitate(10);

        if (Input.GetKeyDown(KeyCode.DownArrow))
            ui.SubtractMoralitate(10);
    }
}
