using UnityEngine;
using UnityEditor;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using System;

namespace Geekbrains.Editor
{
    public class MyWindow : EditorWindow
    {
        string _nameObject = "game_";
        string _nameObjectDel = "Wall";
        private static readonly Dictionary<string, int> _nameDictionary = new Dictionary<string, int>();

        [MenuItem("Мое меню/Задание домашки")]
        public static void ShowWindow()
        {
            // Отобразить существующий экземпляр окна. Если его нет, создаем
            EditorWindow.GetWindow(typeof(MyWindow),false,"Домашка урока");
        }
        void OnGUI()
        {
            // Здесь методы отрисовки схожи с методами в пользовательском интерфейсе, который вы разрабатывали на курсе “Unity3D. Уровень 1”
            GUILayout.Space(20);
            GUILayout.Label("Переименовывание", EditorStyles.boldLabel);
          
            _nameObject = EditorGUILayout.TextField("Имена оъектов с", _nameObject);

            GUILayout.Space(10);

            if (GUILayout.Button("Переименовать уникальными именами"))
            {
                NewMenuOption(_nameObject);
            }

            GUILayout.Space(30);
            GUILayout.Label("Удаление оъектов", EditorStyles.boldLabel);
            _nameObjectDel = EditorGUILayout.TextField("Содержащие в имени: ", _nameObjectDel);

            GUILayout.Space(15);

            if (GUILayout.Button("Удалить обьекты на сцене"))
            {
                DeletAllObj(_nameObjectDel);
            }
        }

        private void DeletAllObj(string str)
        {
            if (Application.isPlaying)
            {
                Debug.Log("Удаляю все объекты...");
                EditorGUILayout.HelpBox("Вы удалили обьекты", MessageType.Warning);
                var sceneObj = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];                                  // Находим все объекты на сцене
                if (sceneObj != null)
                {
                    foreach (var obj in sceneObj)
                    {
                        if (obj.name.IndexOf(_nameObjectDel) != -1)
                            Destroy(obj);
                    }
                }
            }
            else
            {
                Debug.Log("Удаление объектов только при включенной игре...");
                EditorGUILayout.HelpBox("Удаление объектов только при включенной игре", MessageType.Warning);
            }
        }

        private static void NewMenuOption(string str)
        {
            Debug.Log("Переименовываю объекты...");
            EditorGUILayout.HelpBox("Вы переименовали обьекты", MessageType.Warning);
            var sceneObj = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];                                  // Находим все объекты на сцене
            if (sceneObj != null)
            {
                foreach (var obj in sceneObj)
                {
                    DataCollection(obj);
                }
            }
            foreach (var i in _nameDictionary)
            {
                for (var j = 0; j < i.Value; j++)
                {
                    var gameObj = GameObject.Find(i.Key);
                    if (gameObj)
                    {
                        gameObj.name += str+j;
                    }
                }
            }
            _nameDictionary.Clear();           // Очищаем память
        }
        /// <summary>
        /// Собирает информацию об объекте (имя и индекс)
        /// </summary>
        /// <param name="sceneObj">Объект на сцене</param>
        private static void DataCollection(GameObject sceneObj)
        {
            string[] tempName = sceneObj.name.Split('(');
            tempName[0] = tempName[0].Trim();  // Убираем пробелы
            if (!_nameDictionary.ContainsKey(tempName[0]))
            {
                _nameDictionary.Add(tempName[0], 0);
            }
            else
            {
                _nameDictionary[tempName[0]]++;
            }
            sceneObj.name = tempName[0];
        }
    }
}
