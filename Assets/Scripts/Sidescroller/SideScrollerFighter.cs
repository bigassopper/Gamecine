﻿using UnityEngine;
namespace Sidescroller.Fighting
{
    public enum FighterState
    {
        Idle,
        Attacking,
        Blocking,
        Damaged,
        Dead,
        Dying,
        Dodging,
    }
    public class SideScrollerFighter : MonoBehaviour
    {
        Animator animator;
        public FighterState currentState = FighterState.Idle;

        //setando a velocidade de ataque
        [SerializeField] float timeBetweenAttacks = .5f;
        float timeSinceLastAttack = Mathf.Infinity;

        [SerializeField] float knockbackTime;
        float timeSinceKnockbackTime = Mathf.Infinity;

        [SerializeField] float attackRange = .3f;
        [SerializeField] float attackDamage = 1f;

        public Transform attackPos;

        private Vector3 startPosition;


        private void Start()
        {
            animator = GetComponent<Animator>();

            startPosition = transform.position;
        }

        public void ResetFighterPosition()
        {
            transform.position = startPosition;
        }

        public void ResetAnimator()
        {
            animator.SetFloat("Walk", 0f);
            animator.SetTrigger("Reset");

        }

        public void AttackBasic()
        {
            if (timeSinceLastAttack < timeBetweenAttacks) { return; }

            currentState = FighterState.Attacking;
            animator.SetTrigger("Attack");
            timeSinceLastAttack = 0;
        }

        public void Block()
        {
            currentState = FighterState.Blocking;
            animator.SetTrigger("Block");
        }

        public void Damaged()
        {
            currentState = FighterState.Damaged;
            animator.SetTrigger("Damage");
        }

        public void Death()
        {
            // Dying State ==> Death Animation
            currentState = FighterState.Dying;
            animator.SetTrigger("Death");

        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            timeSinceKnockbackTime += Time.deltaTime;
        }

        private void AttackBasicHit()
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange);//todo mudar pra box quando tiver as sprites certas
            foreach (Collider2D collider in enemiesToDamage)
            {
                if (collider == this.gameObject.GetComponent<Collider2D>()) continue;
                if (collider.GetComponent<Health>() != null)
                {
                    if (collider.GetComponent<SideScrollerFighter>() != null)
                    {
                        if (collider.GetComponent<SideScrollerFighter>().currentState == FighterState.Blocking)
                        {
                            this.gameObject.GetComponent<Health>().TakeDamage(1f);
                            continue;
                        }

                    }

                    collider.GetComponent<Health>().TakeDamage(attackDamage);
                }
            }
        }


        public void ChangeState(FighterState newState) //usado em eventos nos finais das animações pra retornar ao estado de idle;
        {
            currentState = newState;
        }

        private void OnDrawGizmos()
        {
            //Gizmos.DrawWireSphere(attackPos.position, attackRange);
        }
    }
}