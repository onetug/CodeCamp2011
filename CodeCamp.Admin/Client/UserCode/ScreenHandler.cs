using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Client;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;

namespace CodeCamp.Admin
{
    public static class ScreenHandler
    {

        private static IScreenObject currentParentScreen;
        private static IEntityObject currentParentEntity;

        public static void SetCurrentParentScreen(IScreenObject scr)
        {
            currentParentScreen = scr;
        }

        public static void RefreshCurrentParent()
        {
            currentParentScreen.Details.Dispatcher.BeginInvoke(new Action(refresh));

        }

        private static void refresh()
        {
            try
            {
                currentParentScreen.Refresh();

            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }

        public static void SetCurrentParentEntity(IEntityObject entity)
        {
            currentParentEntity = entity;
        }

        public static IEntityObject GetCurrentParentEntity()
        {
            return currentParentEntity;
        }

        //public static void RefreshParentEntity()
        //{
        //    currentParentScreen.Details.Dispatcher.BeginInvoke(new Action(refreshSelectedItem));
        //}

        //private static void refreshSelectedItem()
        //{
        //    try
        //    {
        //        //currentParentScreen.Details.Members..currentParentEntity.Refresh();

        //    }
        //    catch (Exception ex)
        //    {
        //        var message = ex.Message;
        //    }
        //}
    }
}
