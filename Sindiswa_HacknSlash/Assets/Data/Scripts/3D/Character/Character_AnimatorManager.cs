using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace OWL
{
    public class Character_AnimatorManager : MonoBehaviour
    {
        Character_Manager charManager;

        protected virtual void Awake()
        {
            charManager = GetComponent<Character_Manager>();
        }
       public void UpdateAnimator_MovementParameters(float horizontalValue, float verticalValue)
        {
            charManager.animator.SetFloat("Horizontal", horizontalValue);
            charManager.animator.SetFloat("Vertical", verticalValue);
        }
    }
}

