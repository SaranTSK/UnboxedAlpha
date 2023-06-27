using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public static class SpawnType
    {
        public static readonly int PureSoul = 0;
        public static readonly int ColorSoul = 1;
        public static readonly int MadSoul = 2;
        public static readonly int Crypto = 3;
        public static readonly int Diamond = 4;
        public static readonly int Clock = 5;
        public static readonly int Symbol = 6;
        public static readonly int Shield = 7;
    }

    public class SpawnerManager : MonoBehaviour
    {
        public Action OnUpdateWaveTimer;

        [Header("Spawinng Initial")]
        [SerializeField] private float frameOffset;
        [SerializeField] private GameObject[] spawnPrefs;

        [Header("Spawning Timer")]
        [SerializeField] private float waveTimer;

        [Header("Spawning Conditions")]
        [SerializeField] private int minSpawning;
        [SerializeField] private int maxSpawning;
        [SerializeField] private int spawningLevel;         // Condition to upgrade spawner
        [SerializeField] private float baseSpawningSpeed;
        [SerializeField] private float bonusSpawningSpeed;
        [SerializeField] private float spawningSpeed;       // Speed between each wave of spawning
        [SerializeField] private float spawningDelaySpeed;  // Speed between each spawning

        private const float TIMER_MULTIPLIER = 10f; // covert millisec to second

        [Header("Spawning Rate")]
        [SerializeField] private float baseColorSoulRate;
        [SerializeField] private float baseCrpytoRate;
        [SerializeField] private float baseDiamondlRate;
        [SerializeField] private float baseClockRate;
        [SerializeField] private float baseSymbolRate;
        [SerializeField] private float baseShieldRate;
        [SerializeField] private float baseItemRate;

        private float currentColorSoulRate;
        private float currentCrpytoRate;
        private float currentDiamondlRate;
        private float currentClockRate;
        private float currentSymbolRate;
        private float currentShieldRate;
        private float currentItemRate;

        [Header("Manager")]
        private GameManager gameManager;
        private UIManager uiManager;
        private GameplayManager gameplayManager;

        public void Initialize()
        {
            InitManager();
            InitVariable();

            EnableAction();
        }

        public void Terminate()
        {
            DisableAction();
        }

        private void InitVariable()
        {
            spawningSpeed = baseSpawningSpeed * TIMER_MULTIPLIER;
            waveTimer = baseSpawningSpeed * TIMER_MULTIPLIER;
        }

        private void InitManager()
        {
            gameManager = GameManager.Instance;

            uiManager = gameManager.UIManager;
            gameplayManager = gameManager.GameplayManager;
        }

        private void EnableAction()
        {
            OnUpdateWaveTimer += UpdateWaveTimer;

            gameplayManager.OnStartBonusTimer += StartBonusTimer;
            gameplayManager.OnEndBonusTimer += EndBonusTimer;
        }

        private void DisableAction()
        {
            OnUpdateWaveTimer -= UpdateWaveTimer;

            gameplayManager.OnStartBonusTimer -= StartBonusTimer;
            gameplayManager.OnEndBonusTimer -= EndBonusTimer;
        }

        private void UpdateWaveTimer()
        {
            if(waveTimer - 1 > 0f)
            {
                waveTimer--;
            }
            else
            {
                RandomSpawning();
                waveTimer = spawningSpeed;
            }
        }

        private void RandomSpawnPosition(GameObject spawnedItem)
        {
            Camera camera = Camera.main;

            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            // Calculate frame size
            Vector2 leftFrameSize = UnboxedUtils.CalculateUISize(uiManager.UIGameplay.LeftFrame, frameOffset);
            Vector2 rightFrameSize = UnboxedUtils.CalculateUISize(uiManager.UIGameplay.RightFrame, frameOffset);
            Vector2 topFrameSize = UnboxedUtils.CalculateUISize(uiManager.UIGameplay.TopFrame, frameOffset);
            Vector2 bottomFrameSize = UnboxedUtils.CalculateUISize(uiManager.UIGameplay.BottomFrame, frameOffset);

            Vector2 contentTopFrameSize = UnboxedUtils.CalculateUISize(uiManager.UIGameplay.ContentTopFrame, frameOffset);
            Vector2 contentBottomFrameSize = UnboxedUtils.CalculateUISize(uiManager.UIGameplay.ContentBottomFrame, frameOffset);

            // Create frame offset
            float leftOffset = leftFrameSize.x;
            float rightOffset = rightFrameSize.x;
            float topOffset = topFrameSize.y + contentTopFrameSize.y;
            float bottomOffset = bottomFrameSize.y + contentBottomFrameSize.y;

            // Calculate spawned item size
            Vector2 spawnedSize = UnboxedUtils.CalculateSpriteSize(spawnedItem);

            // Create spawned item offset
            float itemWidthOffset = spawnedSize.x / 2f;
            float itemHeightOffset = spawnedSize.y / 2f;

            // Fix spawn position
            //int randomId = UnityEngine.Random.Range(0, 4);
            //Vector3[] randomSet = new Vector3[4];
            //randomSet[0] = new Vector3(leftOffset + itemWidthOffset, topOffset + itemHeightOffset, 0f);
            //randomSet[1] = new Vector3(screenWidth - rightOffset - itemWidthOffset, topOffset + itemHeightOffset, 0f);
            //randomSet[2] = new Vector3(leftOffset + itemWidthOffset, screenHeight - bottomOffset - itemHeightOffset, 0f);
            //randomSet[3] = new Vector3(screenWidth - rightOffset - itemWidthOffset, screenHeight - bottomOffset - itemHeightOffset, 0f);
            //Vector3 randomPosition = camera.ScreenToWorldPoint(randomSet[randomId]);

            // Random spawn position
            float randomWidth = UnityEngine.Random.Range(leftOffset + itemWidthOffset, screenWidth - rightOffset - itemWidthOffset);
            float randomHeight = UnityEngine.Random.Range(topOffset + itemHeightOffset, screenHeight - bottomOffset - itemHeightOffset);

            Vector3 randomPosition = camera.ScreenToWorldPoint(new Vector3(randomWidth, randomHeight, 0f));

            randomPosition.y = randomPosition.y * -1;
            randomPosition.z = 0f;

            spawnedItem.transform.position = randomPosition;
        }

        #region Timer Action
        private void StartBonusTimer()
        {
            spawningSpeed = bonusSpawningSpeed * TIMER_MULTIPLIER;
        }

        private void EndBonusTimer()
        {
            spawningSpeed = baseSpawningSpeed * TIMER_MULTIPLIER;
        }
        #endregion

        #region Random
        private void InitRate()
        {
            currentColorSoulRate = baseColorSoulRate;
            currentCrpytoRate = baseCrpytoRate;
            currentDiamondlRate = baseDiamondlRate;
            currentClockRate = baseClockRate;
            currentSymbolRate = baseSymbolRate;
            currentShieldRate = baseShieldRate;
            currentItemRate = baseItemRate;
        }

        private void RandomSpawning()
        {
            int itemId = UnityEngine.Random.Range(0, spawnPrefs.Length);
            GameObject spawnedItem = Instantiate(spawnPrefs[itemId]);
            //GameObject spawnedItem = Instantiate(spawnPrefs[SpawnType.Clock]);
            RandomSpawnPosition(spawnedItem);
        }

        private void SpawnSoul()
        {

        }

        private void SpawnItem()
        {

        }
        #endregion
    }
}


