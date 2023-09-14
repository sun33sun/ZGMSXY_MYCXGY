using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using QFramework;

namespace ZGMSXY_MYCXGY
{
    public partial class GameLibrary : ViewController
    {
        public List<GameObject> learnObjects;

        public Dictionary<CraftRes.CraftType, GameObject> learnObjectInstanceDic =
            new Dictionary<CraftRes.CraftType, GameObject>();
        
        public Dictionary<int, GameObject> interactiveObjectInstanceDic = new Dictionary<int, GameObject>();
        
        public GameObject GetLearnGameObject(CraftRes.CraftType craftType)
        {
            string strCraft = craftType.ToString();
            GameObject target = learnObjects.First(obj => obj.name == strCraft);
            GameObject targetInstance = Instantiate(target, Vector3.zero, Quaternion.identity, ModelParent.transform);
            targetInstance.transform.position = Vector3.zero;
            learnObjectInstanceDic.Add(craftType, targetInstance);
            return targetInstance;
        }

        public void DestroyLearnGameObject(CraftRes.CraftType craftType)
        {
            if (learnObjectInstanceDic.ContainsKey(craftType))
            {
                GameObject target = learnObjectInstanceDic[craftType];
                learnObjectInstanceDic.Remove(craftType);
                Destroy(target);
            }
        }
    }
}