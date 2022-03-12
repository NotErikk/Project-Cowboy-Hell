using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtPlayer : Node
{
    GameObject player;
    GameObject weaponToAim;

    public AimAtPlayer(GameObject player, GameObject weaponToAim)
    {
        this.player = player;
        this.weaponToAim = weaponToAim;
    }

    public override NodeState Evaluate()
    {
        Vector3 diff = (player.transform.position - weaponToAim.transform.position).normalized;
        float z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        weaponToAim.transform.rotation = Quaternion.Euler(0, 0, z);

        if (diff.x >= 0) weaponToAim.transform.localScale = new Vector3(1, 1, 1);
        else weaponToAim.transform.localScale = new Vector3(-1, -1, 1);
        return NodeState.Success;
    }
}
