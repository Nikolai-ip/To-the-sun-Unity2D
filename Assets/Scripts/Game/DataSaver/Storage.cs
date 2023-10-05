using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

internal class Storage
{
    private readonly string _filePath;
    private BinaryFormatter _formatter;

    public Storage()
    {
        var directory = Application.persistentDataPath + "/saves";

        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

        _filePath = directory + "/GameSave.save";

        InitBinaryFormatter();
    }

    private void InitBinaryFormatter()
    {
        _formatter = new BinaryFormatter();
        var selector = new SurrogateSelector();

        /*
         * Adding you surrogates:
         * selector.AddSurrogate(typeof(), ...);
         */

        _formatter.SurrogateSelector = selector;
    }

    public object Load(object saveDataByDefault)
    {
        if (!File.Exists(_filePath))
            if (saveDataByDefault != null)
            {
                Save(saveDataByDefault);
                return saveDataByDefault;
            }

        var file = File.Open(_filePath, FileMode.Open);
        var savedData = _formatter.Deserialize(file);
        file.Close();
        return savedData;
    }

    public void Save(object saveData)
    {
        var file = File.Create(_filePath);
        _formatter.Serialize(file, saveData);
        file.Close();
    }
}