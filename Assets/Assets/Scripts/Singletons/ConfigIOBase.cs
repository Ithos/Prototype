using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class ConfigIOBase
{

    protected Dictionary<string, string> _configurationValues = new Dictionary<string, string>();

    virtual protected Dictionary<string, string> ReadConfigFile(string path)
    {
        Dictionary<string, string> ret = new Dictionary<string, string>();

        try
        {
            string[] file = System.IO.File.ReadAllLines(path);

            foreach (string line in file)
            {
                line.Trim();
                char[] tmp = line.ToCharArray();

                if (tmp[0] == '\n')
                    continue;

                if (tmp[0] == '#')
                {
                    Debug.Log("Configure echo-> " + line);
                }
                else
                {
                    string[] split = line.Split('=');
                    foreach (string part in split)
                    {
                        part.Trim();
                    }

                    ret.Add(split[0], split[1]);
                }

            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error while reading configuration file " + path);
            Debug.LogError("Error info: " + ex.Message);
        }

        return ret;
    }

    virtual protected void WriteConfigFile(string path, Dictionary<string, string> data)
    {
        try
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter(path);

            foreach(KeyValuePair<string, string> current in data)
            {
                writer.WriteLine(current.Key + "=" + current.Value);
            }

        }
        catch(Exception e)
        {
            Debug.LogError("Error while writing configuration file " + path);
            Debug.LogError("Error info: " + e.Message);
        }


    }

    virtual protected void CreateConfigFile(string path)
    {
        try
        {
            System.IO.File.Create(path);
        }
        catch(Exception e)
        {
            Debug.LogError("Error while creating configuration file " + path);
            Debug.LogError("Error info: " + e.Message);
        }
    }

    virtual public bool checkProperty(string name)
    {
        return _configurationValues.ContainsKey(name);
    }

    virtual public string getValue(string name)
    {
        if (checkProperty(name))
        {
            return _configurationValues[name];
        }

        return string.Empty;
    }

    virtual public T value<T>(string name)
    {
        return (T)Convert.ChangeType(getValue(name), typeof(T));
    }

}
