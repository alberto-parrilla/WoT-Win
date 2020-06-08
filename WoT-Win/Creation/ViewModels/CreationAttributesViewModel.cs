﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using ClientDLL.Client;
using KernelDLL.Game.Enums;
using WoT_Win.Common.ViewModels;
using WoT_Win.Creation.Services;

namespace WoT_Win.Creation.ViewModels
{
    public class CreationAttributesViewModel : BaseCreationViewModel
    {
        public CreationAttributesViewModel(IMainClient client, CreationManager creationManager) : base(client, creationManager)
        {
            Attributes = new List<CreationAttributeViewModel>()
            {
                new CreationAttributeViewModel(EnumAttribute.Str, 10, this, creationManager),
                new CreationAttributeViewModel(EnumAttribute.Dex, 10, this, creationManager),
                new CreationAttributeViewModel(EnumAttribute.Con, 10, this, creationManager),
                new CreationAttributeViewModel(EnumAttribute.Int, 10, this, creationManager),
                new CreationAttributeViewModel(EnumAttribute.Wis, 10, this, creationManager),
                new CreationAttributeViewModel(EnumAttribute.Cha, 10, this, creationManager),
            };
            Points = 6;
            IsVisible = true;
        }

        protected override string GetHeader()
        {
            var currentCulture = CultureInfo.CurrentUICulture.Name;
            var dic = $"CreationAttributesView.{currentCulture}.xaml";
            var uri = $"/WoT-Win;component/Resources/Languages/{dic}";
            ResourceDictionary res = (ResourceDictionary)Application.LoadComponent(new Uri(uri, UriKind.Relative));
            return (string)res["Header"];
        }

        public List<CreationAttributeViewModel> Attributes { get; private set; }
        private int _points;

        public int Points
        {
            get { return _points; }
            set
            {
                _points = value;
                OnPropertyChanged();
                Validate();
            }
        }

        public override void Validate()
        {
            if (Points > 0)
            {
                IsValid = false;
                Error = LanguageManager.GetResourceValue("CreationAttributesView", "ErrorPoints");
                
            }
            else
            {
                IsValid = true;
                Error = "";
            }
            RaiseEvents();
        }

        protected override string Validate(string propertyName)
        {          
            return "";
        }

        public override string Error { get; set; }

        private void RaiseEvents()
        {
            OnPropertyChanged("IsValid");
            OnPropertyChanged("Error");
        }

        public override void RefreshUI()
        {
            OnPropertyChanged("Header");

            foreach (var attribute in Attributes)
            {
                attribute.RefreshUI();
            }

            Validate();
        }

        public override void OnLoaded()
        {
           
        }
    }
}
