using Microsoft.Exchange.WebServices.Data;
using System;
using System.Threading;
using VivoPagamentoJudiciais.Model.Entities;

namespace VivoPagamentoJudiciais.Utilities.EmailConnection
{
    public class EWSConnection
    {
        public static bool receivedEmail = false;
        private static GeracaoArquivo _parameters;
        private static int maxTimeForStreaming;
        private static int timeExecutionStreaming;

        static ManualResetEvent resetEvent;
        public async static void SetStreamingNotifications(ExchangeService service, ManualResetEvent rEvent, GeracaoArquivo parameters)
        {
            resetEvent = rEvent;
            _parameters = parameters;

            StreamingSubscription subscription;

            subscription = await service.SubscribeToStreamingNotifications(
                new FolderId[] { WellKnownFolderName.Inbox },
                EventType.NewMail);

            StreamingSubscriptionConnection connection = new StreamingSubscriptionConnection(service, parameters.GeracaoArquivoEmail.TempoStreaming);
            connection.AddSubscription(subscription);
            connection.OnNotificationEvent += OnEvent;
            connection.OnDisconnect += OnDisconnect;
            connection.Open();

            bool status = connection.IsOpen;
        }

        public static void OnEvent(object sender, NotificationEventArgs args)
        {
            StreamingSubscription subscription = args.Subscription;

            foreach (NotificationEvent notification in args.Events)
            {

                switch (notification.EventType)
                {
                    case EventType.NewMail:
                        break;
                }

                if (notification is ItemEvent)
                {
                    ItemEvent itemEvent = (ItemEvent)notification;
                    EmailMessage message = EmailMessage.Bind(args.Subscription.Service, itemEvent.ItemId.UniqueId).Result;
                    StreamingSubscriptionConnection connection = (StreamingSubscriptionConnection)sender;

                    if (_parameters.GeracaoArquivoEmail.EnviadoPor != null)
                    {
                        if (message.Sender.Address.Contains(_parameters.GeracaoArquivoEmail.EnviadoPor))
                        {
                            receivedEmail = true;
                            connection.Close();
                            resetEvent.Set();
                        }

                        return;
                    }

                    receivedEmail = true;
                    connection.Close();
                    resetEvent.Set();
                }
                else
                {
                    FolderEvent folderEvent = (FolderEvent)notification;
                }
            }
        }

        public static void OnDisconnect(object sender, SubscriptionErrorEventArgs args)
        {
            StreamingSubscriptionConnection connection = (StreamingSubscriptionConnection)sender;

            if (receivedEmail)
            {
                return;
            }

            connection.Open();
        }

        public static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            bool result = false;

            Uri redirectionUri = new Uri(redirectionUrl);
            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }
            return result;
        }
    }
}
