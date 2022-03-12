using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStateManager : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] AssetGenerator assetGenerator;
    [SerializeField] TeleporterScript myTeleporter;
    [SerializeField] GameObject myMiniMapIcon;
    int enemiesRemaining;

    public void Start()
    {
        GetenemiesInRoom();
        UnlockRoom();
        myMiniMapIcon.SetActive(false);
    }
    [ContextMenu("Unlock")]
    void UnlockRoom()
    {
            if (door != null) door.SetActive(false);       
    }

    [ContextMenu("Lock")]
    public void LockRoom()
    {
        door.SetActive(true);
            
    }

    public void EnterRoom()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        if (door != null) LockRoom();
        myMiniMapIcon.SetActive(true);
    }


    public void RoomIsNowComplete()
    {
        if (door != null) UnlockRoom();
        myTeleporter.ActivateTeleporter();
    }

    void GetenemiesInRoom() => enemiesRemaining = assetGenerator.ReturnEnemyTotal();


    public void EnemyDied()
    {
        enemiesRemaining--;
        if (enemiesRemaining <= 0) RoomIsNowComplete();
    }
}
