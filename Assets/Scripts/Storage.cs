using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage
{
    private Storage g_Storage = TileMapGenerator.g_Storage;
    public EventHandler<OnStorageChangedEventArgs> OnStorageChanged;
    public struct OnStorageChangedEventArgs{
        public int count;
        public Resource resource;
    }

    static OnStorageChangedEventArgs tempArgs;
    public Dictionary<Resource, int> storage;
    private readonly Resource producing;
    int capacity;
    public Storage(Resource[] resources, int capacity){
        storage = new Dictionary<Resource, int>();
        for (int i = 0; i < resources.Length; i++){
            storage.Add(resources[i], 0);
        }
        this.capacity = capacity;
    }
    public Storage(Resource consuming, Resource producing, int capacity){
        storage = new Dictionary<Resource, int>(){
            {consuming, 0},
            {producing, 0}
        };
        this.capacity = capacity;
        this.producing = producing;
    }
    public Storage(Resource[] consuming, Resource producing, int capacity){
        storage = new Dictionary<Resource, int>();
        for (int i = 0; i < consuming.Length; i++){
            storage.Add(consuming[i], 0);
        }
        storage.Add(producing, 0);
        this.capacity = capacity;
        this.producing = producing;
    }
    public void AddToStorage(Resource keyResource, int resCount){
        if(!storage.ContainsKey(keyResource)) return;  

        storage[keyResource] += resCount;
        
        if(storage[keyResource] > capacity) {
            storage[keyResource] = capacity;
            if(g_Storage == this){
                    Debug.Log("GlobalStorage is out of space!");
                    return;
                }
            RemoveFromStorage(keyResource, capacity);
            g_Storage.AddToStorage(keyResource, capacity);
        }
        tempArgs.count = storage[keyResource];
        tempArgs.resource = keyResource;
        OnStorageChanged?.Invoke(this,tempArgs);
    }
    public void RemoveFromStorage(Resource keyResource, int resCount){
        if(resCount > storage[keyResource]){
            if(g_Storage == this) return;
            // g_Storage.RemoveFromStorage(keyResource, capacity);
            AddToStorage(keyResource, capacity - storage[keyResource]);
        }
        storage[keyResource] -= resCount;
        tempArgs.count = storage[keyResource];
        tempArgs.resource = keyResource;
        OnStorageChanged?.Invoke(this,tempArgs);
    }
    public bool CheckIfEnoughtResource(Resource consuming, int required){
        if(storage[consuming] >= required) return true;
        return false;
    }

    public int GetNumberInStorage(Resource resource){
        return storage[resource];
    }
    public Dictionary<Resource, int> GetStorageDic(){
        return storage;
    }
}
