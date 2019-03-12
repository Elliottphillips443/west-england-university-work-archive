using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PropertiesManager : GenericSingletonClass<PropertiesManager>{
    [SerializeField]
    private PropertiesList BinTowersList = new PropertiesList();
    [SerializeField]
    private PropertiesList RecycleTowerList = new PropertiesList();

    [SerializeField]
    private TowerProperties jsonString;
    [SerializeField]
    private TextAsset asset;
    [SerializeField]
    private string assetStr;

    public override void Awake()
    {
        base.Awake();
        //load the properties from the json files
        //bin properties
        asset = Resources.Load("TextData/BinTowerProperties") as TextAsset;
        BinTowersList = JsonUtility.FromJson<PropertiesList>(asset.text);

        asset = Resources.Load("TextData/RecyclingTowerProperties") as TextAsset;
        RecycleTowerList = JsonUtility.FromJson<PropertiesList>(asset.text);
        //assetStr = File.ReadAllText("Assets/Resources/TextData/BinTowerProperties.json");
        //jsonString = JsonUtility.FromJson<TowerProperties>(assetStr);
    }

    public TowerProperties GetProperties(int levelNum, TowerTypes towerType)
    {
        switch(towerType)
        {
            case TowerTypes.BIN:
                return BinTowersList.propertiesList[levelNum - 1];
            case TowerTypes.RECYCLING:
                return RecycleTowerList.propertiesList[levelNum - 1];
            default:
                break;
        }

        Debug.Log("couldn't get properties");
        return BinTowersList.propertiesList[0];
    }

    public int getMaxLevel(TowerTypes towerType)
    {
         switch(towerType)
        {
            case TowerTypes.BIN:
                return BinTowersList.propertiesList.Count;
            case TowerTypes.RECYCLING:
                return RecycleTowerList.propertiesList.Count;
            default:
                break;
        }
        return 1;
    }
}
