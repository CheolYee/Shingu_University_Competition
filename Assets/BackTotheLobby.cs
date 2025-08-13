using _00._Work.Teams.PMC._01._Codes.UI;
using _00.Work.Scripts.UI;
using UnityEngine;

public class BackTotheLobby : MonoBehaviour
{
    public void BackBtnClicked()
    {
        FadeManager.Instance.FadeToScene(0);
    }
}
