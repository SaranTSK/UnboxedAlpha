using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public GameplayManager GameplayManager { get; private set; }
        public SpawnerManager SpawnerManager { get; private set; }
        public UIManager UIManager { get; private set; }
        public PlayerManager PlayerManager { get; private set; }
        public AudioManager AudioManager { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;

            GameplayManager = GetComponentInChildren<GameplayManager>();
            SpawnerManager = GetComponentInChildren<SpawnerManager>();
            PlayerManager = GetComponentInChildren<PlayerManager>();
            UIManager = GetComponentInChildren<UIManager>();
            AudioManager = GetComponentInChildren<AudioManager>();
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            CreateSave();

            PlayerManager.Initialize();
            GameplayManager.Initialize();
            SpawnerManager.Initialize();
            UIManager.Initialize();
            AudioManager.Initialize();

            AudioManager.PlayMusic(AudioMusic.HomeMusic);
        }

        private void Terminate()
        {
            PlayerManager.Terminate();
            GameplayManager.Terminate();
            SpawnerManager.Terminate();
            UIManager.Terminate();
            AudioManager.Terminate();
        }

        private void CreateSave()
        {
            if (!PlayerPrefs.HasKey(PlayerPrefsKey.Highscore))
            {
                PlayerPrefs.SetInt(PlayerPrefsKey.Highscore, 0);
            }

            if (!PlayerPrefs.HasKey(PlayerPrefsKey.Crypto))
            {
                PlayerPrefs.SetInt(PlayerPrefsKey.Crypto, 0);
            }

            if (!PlayerPrefs.HasKey(PlayerPrefsKey.Upgrades))
            {
                PlayerPrefs.SetString(PlayerPrefsKey.Upgrades, "0/0/0/0/0/0/0/0");
            }
        }
    }
}


