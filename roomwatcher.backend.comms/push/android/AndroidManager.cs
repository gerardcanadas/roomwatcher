using PushSharp;
using PushSharp.Android;
using PushSharp.Core;
using System;

namespace roomwatcher.backend.comms.push.android
{
    public class AndroidManager
    {
        private const string GCM_API_KEY = "AIzaSyAvAiTAme51hfbjvR9TNT3WWojkkUTHdoE";
        private const string GCM_DEVICE_ID = "d5FlLu4qu1M:APA91bGZWjPGHx1ejMYhQQJrXQU_xscQi_k_3uzxvxkZ43437PZzc5it5Dd3Wp-klvpDsUC6-fJsZ364Z1gLERdnsaDvBMvGun5JOYIESgUgVFGfZJgwiiLFGPmOXn7kEXb-KtfX7GUv";

        public void SendPush(string message, string deviceId = GCM_DEVICE_ID)
        {
            PushBroker push = new PushBroker();
            //Wire up the events for all the services that the broker registers
            push.OnNotificationSent += NotificationSent;
            push.OnChannelException += ChannelException;
            push.OnServiceException += ServiceException;
            push.OnNotificationFailed += NotificationFailed;
            push.OnDeviceSubscriptionExpired += DeviceSubscriptionExpired;
            push.OnDeviceSubscriptionChanged += DeviceSubscriptionChanged;
            push.OnChannelCreated += ChannelCreated;
            push.OnChannelDestroyed += ChannelDestroyed;

            //Registering the GCM Service and sending an Android Notification
            push.RegisterGcmService(new GcmPushChannelSettings(GCM_API_KEY));

            //Fluent construction of an Android GCM Notification
            //IMPORTANT: For Android you MUST use your own RegistrationId here that gets generated within your Android app itself!
            push.QueueNotification(new GcmNotification().ForDeviceRegistrationId(deviceId)
                .WithJson("{'message' : '" + message + "'}"));

        }

        static void DeviceSubscriptionChanged(object sender, string oldSubscriptionId, string newSubscriptionId, INotification notification)
        {
            //Currently this event will only ever happen for Android GCM
            Console.WriteLine("Device Registration Changed:  Old-> " + oldSubscriptionId + "  New-> " + newSubscriptionId + " -> " + notification);
        }

        static void NotificationSent(object sender, INotification notification)
        {
            Console.WriteLine("Sent: " + sender + " -> " + notification);
        }

        static void NotificationFailed(object sender, INotification notification, Exception notificationFailureException)
        {
            Console.WriteLine("Failure: " + sender + " -> " + notificationFailureException.Message + " -> " + notification);
        }

        static void ChannelException(object sender, IPushChannel channel, Exception exception)
        {
            Console.WriteLine("Channel Exception: " + sender + " -> " + exception);
        }

        static void ServiceException(object sender, Exception exception)
        {
            Console.WriteLine("Service Exception: " + sender + " -> " + exception);
        }

        static void DeviceSubscriptionExpired(object sender, string expiredDeviceSubscriptionId, DateTime timestamp, INotification notification)
        {
            Console.WriteLine("Device Subscription Expired: " + sender + " -> " + expiredDeviceSubscriptionId);
        }

        static void ChannelDestroyed(object sender)
        {
            Console.WriteLine("Channel Destroyed for: " + sender);
        }

        static void ChannelCreated(object sender, IPushChannel pushChannel)
        {
            Console.WriteLine("Channel Created for: " + sender);
        }
    }
}
