using UnityEngine;

namespace Utils
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T instance;

        //Returns the instance of this singleton.
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (instance == null)
                    {
                        return null;
                    }
                }

                return instance;
            }
        }

        protected void PassTroughScenes()
        {
            if (IsThisTheInstance())
                DontDestroyOnLoad(gameObject);
            else
                Destroy(this);
        }

        protected bool IsThisTheInstance()
        {
            if (Instance != this)
                return false;
            else
                return true;
        }
    }
}