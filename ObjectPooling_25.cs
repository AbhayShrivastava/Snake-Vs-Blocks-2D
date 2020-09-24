using UnityEngine;
using System.Collections.Generic;

public class ObjectPooling_25 {
		
		
		private List<GameObject> objectPooled;
		private GameObject newObjectCreated;
		private GameObject parent;
		
		public ObjectPooling_25(GameObject go, int maxPool)
		{
			objectPooled = new List<GameObject>(maxPool);
			parent = new GameObject(go.name + " Pool");
        parent.tag = "TAG_14";
			newObjectCreated = go;
			for (int i = 0; i < maxPool; i++) {
				GameObject newGo = GameObject.Instantiate(go) as GameObject;
            newGo.GetComponent<AutoDestroy_25>().Bars();
            newGo.transform.parent = parent.transform;
				objectPooled.Add(newGo);
				newGo.SetActive(false);
				
			}
		}
		
		public GameObject GetObject()
		{
			for (int i = 0; i < objectPooled.Count; i++) {

            if (!objectPooled[i].activeSelf)
            {
                if (objectPooled.Contains(objectPooled[i]))
                {
                    objectPooled[i].SetActive(true);

                    return objectPooled[i];
                }
            }
			}

			GameObject newGo = GameObject.Instantiate(newObjectCreated) as GameObject;
			newGo.transform.parent = parent.transform;
        newGo.GetComponent<AutoDestroy_25>().Bars();
			objectPooled.Add(newGo);
			
			return newGo;
		}
		
	}