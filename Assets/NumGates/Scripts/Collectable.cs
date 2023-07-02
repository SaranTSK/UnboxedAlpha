using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public class Collectable : MonoBehaviour
    {
        public Action OnCollected;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private BoxCollider boxCollider;

        [SerializeField] protected int value;

        protected GameManager gameManager;
        protected GameplayManager gameplayManager;

        protected virtual void Start()
        {
            Init();
        }

        protected virtual void OnDestroy()
        {
            Destroy();
        }

        protected virtual void Init()
        {
            gameManager = GameManager.Instance;
            gameplayManager = gameManager.GameplayManager;

            OnCollected += Collected;
        }

        protected virtual void Destroy()
        {
            OnCollected -= Collected;
        }

        protected virtual void Collected()
        {

        }
    }
}

