/*
 * Singleton.cs
 * 설명 :
 * Unity의 MonoBehaviour을 상속하는 객체의 싱글톤 일경우 사용 할 수 있다.
 * 
 * 사용법 :
 * class A : SingletonGameObject<A> { ... }
 * 
 */

 /** 출처 : 이 재 희 **/  /** 원작자 : 이 원 혁 **/

using UnityEngine;

public class SingletonGameObject<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(T)) as T;

                if (instance == null)
                {
                    Debug.LogError(typeof(T).Name + " Instance was not created");
                    return null;
                }

                (instance as SingletonGameObject<T>).Init();
            }

            return instance;
        }
    }

    protected virtual void Init()
    {

    }
}