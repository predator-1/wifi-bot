using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Wifi_bot.Models;
using Wifi_Bot.Services;

namespace Wifi_bot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            if (activity?.ChannelId == "telegram")
            {
                var userLocation = activity.Entities?.Select(t => t.GetAs<TelegramGeoModel>())
                    .FirstOrDefault(r => r?.Geo?.Type == "GeoCoordinates");
                if (userLocation != null)
                {
                    var reply = context.MakeMessage();

                    WifiService wifiService = new WifiService();
                    var cards = new List<CardAction>();
                    foreach (var place in await wifiService.GetGoogleMaps(userLocation.Geo.Latitude, userLocation.Geo.Longitude))
                    {
                        cards.Add(new CardAction
                        {
                            Title = place.Title,
                            Type = ActionTypes.OpenUrl,
                            Value = place.Url
                        });
                    }

                    if (cards.Any())
                    {
                        reply.Attachments.Add(new HeroCard
                        {
                            Title = "Open your wifi places in 200 meters!",
                            Buttons = cards

                        }.ToAttachment());
                    }
                    else
                    {
                        reply.Text = "We can't find wifi places near you.";
                    }

                    await context.PostAsync(reply);

                    context.Wait(MessageReceivedAsync);

                    return;
                }
               
            }

            await context.PostAsync("Send me your location please.");

            context.Wait(MessageReceivedAsync);
        }
    }
}