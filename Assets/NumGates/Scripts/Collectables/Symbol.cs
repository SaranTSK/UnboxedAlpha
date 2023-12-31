using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public class Symbol : Collectable
    {
        protected override void Init()
        {
            base.Init();

        }

        protected override void Destroy()
        {
            base.Destroy();
        }

        protected override void Collected()
        {
            if(gameplayManager.IsBonus == false)
            {
                gameplayManager.OnSymbolCollected?.Invoke();
            }

            Destroy(gameObject);
        }
    }
}

