using System;
using System.Collections.Generic;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectsToSave;
    [SerializeField] private List<Checkpoint> _checkPoints;
    [SerializeField] private SliderSoundChanger _soundChanger;
    private GameController _gameController;
    private GameData _gameData;
    private List<ILoadable> _loadableObjects;
    private Storage _storage;

    private void Start()
    {
        _gameController = GetComponent<GameController>();
        _storage = new Storage();

        foreach (var objectToSave in _objectsToSave)
        {
            var loadableObject = objectToSave.GetComponent<ILoadable>();

            if (loadableObject == null) throw new ArgumentException("Loaded to data saver not ILoadable object");

            _loadableObjects.Add(loadableObject);
        }

        Load();

        if (_gameData.CheckpointsData.Count < _checkPoints.Count)
        {
            _gameData.CheckpointsData = new List<bool>();
            for (var i = 0; i < _checkPoints.Count; i++) _gameData.CheckpointsData.Add(_checkPoints[i].IsReached);
        }
    }

    public void Save()
    {
        UpdateGameData();
        _storage.Save(_gameData);
    }

    public void Load()
    {
        _gameData = (GameData)_storage.Load(new GameData());

        for (var i = 0; i < _gameData.CheckpointsData.Count; i++)
            _checkPoints[i].IsReached = _gameData.CheckpointsData[i];

        _gameController.RespawnPlayerActor();


        _soundChanger.SoundValue = PlayerPrefs.GetFloat("Volume");

        /*
         * Loading your gameData to objects
         */
    }

    public void UpdateGameData()
    {
        for (var i = 0; i < _gameData.CheckpointsData.Count; i++)
            _gameData.CheckpointsData[i] = _checkPoints[i].IsReached;
    }
}