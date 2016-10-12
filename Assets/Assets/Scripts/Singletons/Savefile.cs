using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Savefile : ConfigIOBase
{

    static private Savefile _instance = null;

    const string SAVEFILES_PATH = @"./savefiles/";
    const string EXTENSION = ".svf";

    private string _fileName = "savefile";
    private string _path = "";

    private Savefile()
    {
        _instance = this;
        _path = SAVEFILES_PATH + _fileName + EXTENSION;
        _configurationValues = null;
    }

    static public Savefile getInstance()
    {
        if(_instance == null)
        {
            new Savefile();
        }

        return _instance;
    }

    public void unloadSavefile()
    {
        _configurationValues = null;
    }

    public void loadSavefile()
    {
        _configurationValues = ReadConfigFile(_path);
    }

    public void createSavefile()
    {
        CreateConfigFile(_path);
        _configurationValues = new Dictionary<string, string>();
    }

    public void addData(string key, string value)
    {
        if(_configurationValues != null)
        {
            if(_configurationValues.ContainsKey(key))
            {
                _configurationValues[key] = value;
            }
            else
            {
                _configurationValues.Add(key, value);
            }
        }
        else
        {
            Debug.LogError("No savefile loaded.");
        }
    }

    public void setNewSavefile(string filename)
    {
        unloadSavefile();
        _fileName = filename;
        _path = SAVEFILES_PATH + _fileName + EXTENSION;
    }
	
    public void writeSavefile()
    {
        if(_configurationValues != null)
        {
            WriteConfigFile(_path, _configurationValues);
        }
        else
        {
            Debug.LogError("No savefile loaded.");
        }
    }

    public Dictionary<string, string> getSavedData()
    {
        if(_configurationValues == null)
        {
            Debug.LogError("No savefile loaded.");
        }

        return _configurationValues;
    }

    static public bool checkSavefilesDir()
    {
        string[] dirs = Directory.GetFiles(SAVEFILES_PATH, "*" + EXTENSION);

        if (dirs.Length > 0)
            return true;

        return false;
    }

    public void deleteSavefile()
    {
        if(_configurationValues != null)
        {
            Directory.Delete(_path);
            _configurationValues = null;
        }
        else
        {
            Debug.LogError("No savefile loaded.");
        }
    }
}
