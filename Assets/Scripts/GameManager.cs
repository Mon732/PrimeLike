using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameManager
{
    public Settings settings = new Settings();

    static GameManager _instance;
    GameState gameState = GameState.Game;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }

            return _instance;
        }
    }

    public enum GameState
    {
        MainMenu,
        Game,
        Paused
    };

    [System.Serializable]
    public class Settings
    {
        public Video video;
        public Audio audio;
        public Controls controls;

        [System.Serializable]
        public class Video
        {
            public bool fullScreen;
            public int width;
            public int height;
            public float fieldOfView;
            public QualitySettings qualitySettings;
        }

        [System.Serializable]
        public class Audio
        {
            public float musicVol;
            public float effectsVol;
        }

        [System.Serializable]
        public class Controls
        {
            public InputManager.InputBase[] inputs;
        }

        public Settings()
        {
            video = new Video();
            audio = new Audio();
            controls = new Controls();

            video.fullScreen = Screen.fullScreen;
            video.width = Screen.width;
            video.height = Screen.height;
            video.fieldOfView = 90;

            audio.musicVol = 1;
            audio.effectsVol = 1;

            controls.inputs = InputManager.Instance.GetInputs();

            //Debug.Log(video.fieldOfView);
        }
    }

    private GameManager()
    {

    }

    public void SetState(GameState state)
    {
        gameState = state;
    }
    public GameState GetState()
    {
        return gameState;
    }

    public void SaveSettings()
    {
        Debug.Log("Saving settings...");

        try
        {
            using (StreamWriter file = new StreamWriter(Application.dataPath + @"/settings.cfg"))
            {
                file.Write(JsonUtility.ToJson(settings, true));
            }

            Debug.Log("Settings saved!");
        }
        catch (System.UnauthorizedAccessException e)
        {
            Debug.LogError("Could not save settings: " + e.ToString());
        }
    }

    public void LoadSettings()
    {
        Debug.Log("Loading settings...");

        try
        {
            using (StreamReader file = new StreamReader(Application.dataPath + @"/settings.cfg"))
            {
                settings = (Settings)JsonUtility.FromJson(file.ReadToEnd(), typeof(Settings));
            }

            InputManager.Instance.SetControls(settings.controls.inputs);

            Debug.Log(settings.video.height);
            Debug.Log("Settings loaded!");
        }
        catch (FileNotFoundException e)
        {
            SaveSettings();
        }
    }
}

