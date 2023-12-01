using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceUIManager : MonoBehaviour
{
    [SerializeField] private ResourceTemplateScript template;
    [SerializeField] private TileMapGenerator tileMapGenerator;
    private Dictionary<Resource, ResourceTemplateScript> dic;
    private void Start(){
        dic = new Dictionary<Resource, ResourceTemplateScript>();
        Resource[] res = tileMapGenerator.GlobalResources;
        for (int i = 0; i < res.Length; i++){
            ResourceTemplateScript newTemplate = Instantiate(template, transform);
            newTemplate.gameObject.SetActive(true);
            newTemplate.SetImage(res[i].UISprite);
            newTemplate.SetNumber(0);
            dic.Add(res[i],newTemplate);
        }
        TileMapGenerator.g_Storage.OnStorageChanged += OnStorageChanged;
    }

    private void OnStorageChanged(object s, Storage.OnStorageChangedEventArgs e){
        dic[e.resource].SetNumber(e.count);
    }

}
