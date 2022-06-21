using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ServicesLocator
{
    private static readonly Dictionary<Type, object> listServices = new Dictionary<Type, object>();  // Création du dictionnaire pour la liste des services

    public static void RegisterService<T>(T service)
    {
        listServices[typeof(T)] = service;
    }

    public static T GetService<T>()
    {
        return (T)listServices[typeof(T)];
    }

}
