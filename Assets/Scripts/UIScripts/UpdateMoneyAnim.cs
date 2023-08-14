using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMoneyAnim : MonoBehaviour
{
    [SerializeField] private Animation anim;
    void Start()
    {
        EventBus.UpdateMoneyEvent.AddListener(PlayAnimation);
    }
    private void PlayAnimation()
    {
        anim.Play();
    }
}
