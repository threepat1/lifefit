using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InnovaFramework.iGUI
{
    public class iGUIUtility
    {
        private static List<iObject> dataStore = new List<iObject>();

        // Data Store
        public static void SaveObjectToStore(iObject mObject)
        {
            if(dataStore.Contains(mObject)) return;
            dataStore.Add(mObject);
        }
        public static void DeleteObjectFromStore(iObject mObject)
        {
            if(dataStore.Contains(mObject))
            {
                dataStore.Remove(mObject);
            }
        }
        public static iObject GetObjectByName(string name)
        {
            return dataStore.Find( o => o.name == name);
        }
        public static iObject GetObjectByTag(string tag)
        {
            return dataStore.Find( o => o.tag == tag);
        }

        public static Vector2 CalculateSizeFromObjects(params iObject[] mObjects)
        {
            Vector2 size = new Vector2();

            Vector2 minPos = new Vector2();
            Vector3 maxSize = new Vector3();
            for(int i = 0; i < mObjects.Length; i++)
            {
                if(maxSize.x < mObjects[i].right)
                {
                    maxSize.x = mObjects[i].right;
                }

                if(maxSize.y < mObjects[i].bottom)
                {
                    maxSize.y = mObjects[i].bottom;
                }

                if(i == 0)
                {
                    minPos = mObjects[i].position;
                }
                else
                {
                    if(minPos.x > mObjects[i].position.x)
                    {
                        minPos.x = mObjects[i].position.x;
                    }

                    if(minPos.y > mObjects[i].position.y)
                    {
                        minPos.y = mObjects[i].position.y;
                    }
                }
            }

            size.x = maxSize.x - minPos.x;
            size.y = maxSize.y - minPos.y;

            return size;
        }
    }
}