using UnityEngine;

namespace _00._Work.Teams.PMC._01._Codes
{
    public class ClearUIManager : MonoBehaviour
    {
        [SerializeField] private Enemy enemy;
        [SerializeField] private ClearUIController clearUI;

        void Start()
        {
            clearUI.SubscribeToEnemy(enemy);
        }
    }
}