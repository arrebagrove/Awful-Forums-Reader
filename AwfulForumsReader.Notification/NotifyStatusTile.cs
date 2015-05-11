﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Windows.Data.Xml.Dom;
using Windows.Networking.Connectivity;
using Windows.UI.Notifications;
using AwfulForumsLibrary.Entity;

namespace AwfulForumsReader.Notification
{
    public class NotifyStatusTile
    {
        public static bool IsInternet()
        {
#if DEBUG
            return true;
#else
                        ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null &&
                            connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
#endif
        }

        public static void CreateBookmarkLiveTile(ForumThreadEntity forumThread)
        {
            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text05);
            XmlNodeList tileAttributes = tileXml.GetElementsByTagName("text");
            tileAttributes[0].AppendChild(tileXml.CreateTextNode(forumThread.Name));
            tileAttributes[1].AppendChild(tileXml.CreateTextNode(string.Format("Killed By: {0}", forumThread.KilledBy)));
            tileAttributes[2].AppendChild(
                tileXml.CreateTextNode(string.Format("Unread Posts: {0}", forumThread.RepliesSinceLastOpened)));
            //var imageElement = tileXml.GetElementsByTagName("image");
            //imageElement[0].Attributes[1].NodeValue = "http://fi.somethingawful.com/forums/posticons/lf-arecountry.gif" ;
            var tileNotification = new TileNotification(tileXml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }

        public static void CreateToastNotification(ForumThreadEntity forumThread)
        {
            string replyText = forumThread.RepliesSinceLastOpened > 1 ? " has {0} replies." : " has {0} reply.";
            XmlDocument notificationXml =
                ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText02);
            XmlNodeList toastElements = notificationXml.GetElementsByTagName("text");
            toastElements[0].AppendChild(
                notificationXml.CreateTextNode(string.Format("\"{0}\"", forumThread.Name)));
            toastElements[1].AppendChild(
                notificationXml.CreateTextNode(string.Format(replyText, forumThread.RepliesSinceLastOpened)));
            XmlNodeList imageElement = notificationXml.GetElementsByTagName("image");
            string imageName = string.Empty;
            if (string.IsNullOrEmpty(imageName))
            {
                imageName = forumThread.ImageIconLocation;
            }
            imageElement[0].Attributes[1].NodeValue = imageName;
            IXmlNode toastNode = notificationXml.SelectSingleNode("/toast");
            string test = "{" + string.Format("type:'toast', 'threadId':{0}", forumThread.ThreadId) + "}";
            var xmlElement = (XmlElement)toastNode;
            if (xmlElement != null) xmlElement.SetAttribute("launch", test);
            var toastNotification = new ToastNotification(notificationXml);
            var nameProperty = toastNotification.GetType().GetRuntimeProperties().FirstOrDefault(x => x.Name == "Tag");
            if (nameProperty != null)
            {
                nameProperty.SetValue(toastNotification, forumThread.ThreadId.ToString());
            }
            ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
        }

        public static void CreateToastNotification(string header, string text)
        {
            XmlDocument notificationXml =
    ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText02);
            XmlNodeList toastElements = notificationXml.GetElementsByTagName("text");
            toastElements[0].AppendChild(
                notificationXml.CreateTextNode(header));
            toastElements[1].AppendChild(
                 notificationXml.CreateTextNode(text));
            XmlNodeList imageElement = notificationXml.GetElementsByTagName("image");
            string imageName = string.Empty;
            if (string.IsNullOrEmpty(imageName))
            {
                imageName = @"Assets/Logo.scale-100.png";
            }
            imageElement[0].Attributes[1].NodeValue = imageName;
            IXmlNode toastNode = notificationXml.SelectSingleNode("/toast");
            string test = "{" + string.Format("type:'toast'") + "}";
            var xmlElement = (XmlElement)toastNode;
            if (xmlElement != null) xmlElement.SetAttribute("launch", test);
            var toastNotification = new ToastNotification(notificationXml)
            {
                ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(30)
            };
            ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
        }
    }
}
