using UnityEngine;
using TMPro;


namespace SurvivorGame
{
    public class DamageText : MonoBehaviour
    {
        [Header("Elements")]
        [SerializeField] private Animator animator;
        [SerializeField] private TMP_Text damageText;

        [NaughtyAttributes.Button]
        public void PlayAnimation(int damage)
        {
            damageText.text = damage.ToString();
            animator.Play("Animate");
        }
    }
}
