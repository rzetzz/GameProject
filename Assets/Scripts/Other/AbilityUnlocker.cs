using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUnlocker : MonoBehaviour
{
    [SerializeField] bool unlockDoubleJump,unlockDash,unlockWallJump,unlockChargeDash,unlockAttackBow;
    [SerializeField] DataSave data;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            

            if (unlockDash)
            {
                data.canDash = true;
            }
            if (unlockAttackBow)
            {
                data.canAttackBow = true;
            }
            if (unlockChargeDash)
            {
                data.canChargeDash = true;
            }
            if (unlockDoubleJump)
            {
                data.canDoubleJump = true;
            }
            if (unlockWallJump)
            {
                data.canWallJump = true;
            }
            this.gameObject.SetActive(false);
        }
        
    }
}
