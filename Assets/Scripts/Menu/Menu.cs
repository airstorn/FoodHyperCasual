using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameStates
{
   public class Menu : MonoBehaviour
   {

      [SerializeField] private GameObject _defaultPage;
      [SerializeField] private GameObject[] _menuObjects;
      [SerializeField] private EventSystem _uiSystem;
      private IMenuPagable[] _pages;


      public static Menu Instance;

      private void Awake()
      {
         Instance = this;
         Pull();
      } 

      private void Pull()
      {
         _pages = _menuObjects.Select(obj => obj.GetComponent<IMenuPagable>()).ToArray();

         for (int i = 0; i < _pages.Length; i++)
         {
            _pages[i].Hide();
         }
         
         SwitchPage(_defaultPage, this);
      }

      public void SwitchPage<T>(GameObject page, T args)
      {
         var pageElement = GetPage(page);
         foreach (var tempPage in _pages)
         {
            if (tempPage == pageElement)
            { 
               tempPage.Show(args);
            }
            else
            {
               tempPage.Hide();
            }
         }
      } 
      public void SwitchPage(GameObject page)
      {
         var pageElement = GetPage(page);
         foreach (var tempPage in _pages)
         {
            if (tempPage == pageElement)
            {
               tempPage.Show(this);
            }
            else
            {
               tempPage.Hide();
            }
         }
      }

      public void OpenPageOverlayed<T>(GameObject page, T args)
      {
         GetPage(page).Show(args);
      }

      private IMenuPagable GetPage(GameObject page)
      {
         return page.GetComponent<IMenuPagable>();
      }
   }
}
