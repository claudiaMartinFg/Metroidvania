using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventos : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    public void FinishAttack()
    {
        playerController.isAttacking = false;
    }

}
