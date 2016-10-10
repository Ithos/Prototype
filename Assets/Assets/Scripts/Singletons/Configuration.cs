using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Configuration: ConfigIOBase {

    static private Configuration _instance = null;
    const string CONFIGURE_PATH = @"./configure/configuration.conf";

    private static string _defaultPath = CONFIGURE_PATH;
    private string _path = "";

    static private Dictionary<string, Configuration> _fileMap = new Dictionary<string, Configuration>();

    private Configuration(string path)
    {
        _path = path;

        _configurationValues = ReadConfigFile(_path);
    }

    public static bool setDefaultFile(string path)
    {
        if (string.IsNullOrEmpty(path))
            return false;

        _defaultPath = path;

        return true;
    }

    static public Configuration getConfiguration(string spath = null)
    {
        string path = _defaultPath;

        if (spath != null)
            path = spath;

        if(!_fileMap.ContainsKey(path))
        {
            Configuration cf = new Configuration(path);
            _fileMap.Add(path, cf);
        }

        Configuration ret = _fileMap[path];

        return ret;

    }

    public static void removeConfiguration(string path)
    {
        if(path != null)
        {
            if(_fileMap.ContainsKey(path))
            {
                _fileMap.Remove(path);
            }
        }
        else
        {
            _fileMap.Clear();
        }
    }

    public static string getDefaultValue(string name)
    {
        return getConfiguration().getValue(name);
    }

    public static T getDefaultValue<T>(string name)
    {
        return (T)Convert.ChangeType(getDefaultValue(name), typeof(T));
    }

    override public void WriteConfigFile(string path, Dictionary<string, string> data)
    {
        throw new Exception("Programatically writing configuration files it's not allowed");
    }

    override public void CreateConfigFile(string path)
    {
        throw new Exception("Programatically creating configuration files it's not allowed");
    }
}
