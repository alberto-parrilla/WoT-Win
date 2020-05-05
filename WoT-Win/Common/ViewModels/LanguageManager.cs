using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using KernelDLL.Common;
using Microsoft.Win32;

namespace WoT_Win.Common.ViewModels
{
    internal static class LanguageManager
    {
        private static List<string> _registerViews = new List<string>()
        {
            "LoginView",
            "RegisterView",
            "UserView",
            "InitView",
            "LoadView",
            "CreationView",
            "CreationDataView",
            "CreationAttributesView",
            "CreationSkillsView",
            "CreationWeavesView",
            "MainGui",
            "BottomGuiView",
            "ChatView",
            "PlayerInventoryView",
            "ItemSlotView"
        };

        /// <summary>  
        /// Get application name from an element  
        /// </summary>  
        /// <param name="element"></param>  
        /// <returns></returns>  
        private static string getAppName(FrameworkElement element)
        {
            var elType = element.GetType().ToString();
            var elNames = elType.Split('.');
            return elNames[0];
        }

        /// <summary>  
        /// Generate a name from an element base on its class name  
        /// </summary>  
        /// <param name="element"></param>  
        /// <returns></returns>  
        private static string getElementName(FrameworkElement element)
        {
            var elType = element.GetType().ToString();
            var elNames = elType.Split('.');
            var elName = "";
            if (elNames.Length >= 2) elName = elNames[elNames.Length - 1];
            return elName;
        }

        /// <summary>  
        /// Get current culture info name base on previously saved setting if any,  
        /// otherwise get from OS language  
        /// </summary>  
        /// <param name="element"></param>  
        /// <returns></returns>  
        public static string GetCurrentCultureName(FrameworkElement element)
        {
            RegistryKey curLocInfo = Registry.CurrentUser.OpenSubKey("GsmLib" + @"\" + getAppName(element), false);  
    

                var cultureName = CultureInfo.CurrentUICulture.Name;
            if (curLocInfo != null)
            {
                cultureName = curLocInfo.GetValue(getElementName(element) + ".localization", "en-US").ToString();
            }
            return cultureName;
        }

        /// <summary>  
        /// Set language based on previously save language setting,  
        /// otherwise set to OS lanaguage  
        /// </summary>  
        /// <param name="element"></param>  
        public static void SetDefaultLanguage(FrameworkElement element)
        {
            SetLanguageResourceDictionary(element, GetLocXAMLFilePath(getElementName(element), GetCurrentCultureName(element)));
        }

        /// <summary>  
        /// Dynamically load a Localization ResourceDictionary from a file  
        /// </summary>  
        public static void SwitchLanguage(FrameworkElement element, string inFiveCharLang)
        {
            if (!_registerViews.Any(r => r.Contains(getElementName(element)))) return;

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(inFiveCharLang);
            SetLanguageResourceDictionary(element, GetLocXAMLFilePath(getElementName(element), inFiveCharLang));
            // Save new culture info to registry  
            RegistryKey UserPrefs = Registry.CurrentUser.OpenSubKey("GsmLib" + @"\" + getAppName(element), true);  
                        if (UserPrefs == null)
            {
                // Value does not already exist so create it  
                RegistryKey newKey = Registry.CurrentUser.CreateSubKey("GsmLib");
                UserPrefs = newKey.CreateSubKey(getAppName(element));
            }
            UserPrefs.SetValue(getElementName(element) + ".localization", inFiveCharLang);

            var viewModel = element.DataContext as BaseViewModel;
            if (viewModel != null)
            {
                viewModel.RefreshUI();
            }

            foreach (var userControl in FindVisualChildren<UserControl>(element))
            {
                // do something with tb here
                LanguageManager.SwitchLanguage(userControl, Util.CurrentCulture);
            }

            foreach (var tabItem in FindVisualChildren<TabItem>(element))
            {
                // do something with tabItem here
                viewModel = tabItem.DataContext as BaseViewModel;
                if (viewModel != null)
                {
                    viewModel.RefreshUI();
                }
            }
        }

        /// <summary>  
        /// Returns the path to the ResourceDictionary file based on the language character string.  
        /// </summary>  
        /// <param name="inFiveCharLang"></param>  
        /// <returns></returns>  
        public static string GetLocXAMLFilePath(string element, string inFiveCharLang)
        {
            string locXamlFile = element + "." + inFiveCharLang + ".xaml";
            string directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return Path.Combine(directory, "Resources/Languages", locXamlFile);
        }

        /// <summary>  
        /// Sets or replaces the ResourceDictionary by dynamically loading  
        /// a Localization ResourceDictionary from the file path passed in.  
        /// </summary>  
        /// <param name="inFile"></param>  
        private static void SetLanguageResourceDictionary(FrameworkElement element, String inFile)
        {
            if (File.Exists(inFile))
            {
                // Read in ResourceDictionary File  
                var languageDictionary = new ResourceDictionary();
                languageDictionary.Source = new Uri(inFile);
                // Remove any previous Localization dictionaries loaded  
                int langDictId = -1;
                for (int i = 0; i < element.Resources.MergedDictionaries.Count; i++)
                {
                    var md = element.Resources.MergedDictionaries[i];
                    // Make sure your Localization ResourceDictionarys have the ResourceDictionaryName  
                    // key and that it is set to a value starting with "Loc-".  
                    if (md.Contains("ResourceDictionaryName"))
                    {
                        if (md["ResourceDictionaryName"].ToString().StartsWith("Loc-"))
                        {
                            langDictId = i;
                            break;
                        }
                    }
                }
                if (langDictId == -1)
                {
                    // Add in newly loaded Resource Dictionary  
                    element.Resources.MergedDictionaries.Add(languageDictionary);
                }
                else
                {
                    // Replace the current langage dictionary with the new one  
                    element.Resources.MergedDictionaries[langDictId] = languageDictionary;
                }
            }
            else
            {
                MessageBox.Show("'" + inFile + "' not found.");
            }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static string GetResourceValue(string dictionary, string resource)
        {
            //var currentCulture = CultureInfo.CurrentUICulture.Name;
            var currentCulture = Util.CurrentCulture;
            var dic = $"{dictionary}.{currentCulture}.xaml";
            var uri = $"/WoT-Win;component/Resources/Languages/{dic}";
            ResourceDictionary res = (ResourceDictionary)Application.LoadComponent(new Uri(uri, UriKind.Relative));
            return (string)res[resource];
        }
    }
}
