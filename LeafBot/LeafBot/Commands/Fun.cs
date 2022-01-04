using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

using Newtonsoft.Json;

using LeafBot.Data.Models;

namespace LeafBot.Commands
{
  class Fun : BaseCommandModule
  {
    [Command("kanye")]
    [Description("I wonder what amazing things he's said recently..")]
    [Aliases("knowledge", "smarts", "mind")]
    public async Task Kanye(CommandContext ctx)
    {
      using (var webClient = new HttpClient())
      {
        // Query UrbanDictionary api
        string response = await webClient.GetStringAsync("http://api.kanye.rest/");

        // Convert response to object
        var data = JsonConvert.DeserializeObject<KayneApiResponse>(response);

        if (data != null)
        {
          // Build an embed to display the results
          var embed = new DiscordEmbedBuilder
          {
            Title = $"'{Formatter.Italic(data.Quote)}'",

            // I think it looks better without the thumbnail for now
            //Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
            //{
            //  Url = @"https://upload.wikimedia.org/wikipedia/commons/thumb/1/11/Kanye_West_at_the_2009_Tribeca_Film_Festival.jpg/800px-Kanye_West_at_the_2009_Tribeca_Film_Festival.jpg"
            //}
          };

          //embed.ImageUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/1/11/Kanye_West_at_the_2009_Tribeca_Film_Festival.jpg/800px-Kanye_West_at_the_2009_Tribeca_Film_Festival.jpg";
          embed.Footer = new DiscordEmbedBuilder.EmbedFooter
          {
            Text = "Kanye West"
          };

          await ctx.Channel.SendMessageAsync(embed: embed);
        }
        else
        {
          await ctx.Channel.SendMessageAsync($"Kanye seems to be asleep at the moment...");
        }
      }
    }
  }
}
