using System;
using System.Threading.Tasks;

using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

using LeafBot.Data;

namespace LeafBot.Commands
{
  class Counters : BaseCommandModule
  {
    private Random rng = new Random();

    [Command("snake")]
    [Description("I have been instructed to look out for snakes... Dam server is full of them... ")]
    [Aliases("snakery", "snek")]
    public async Task SerpentCount(CommandContext ctx)
    {
      // Increment stat
      Stats.SnakeCount++;

      // I mean this save is a bit redundant because the save function is on a timer,
      // but hopes for the moment while leafbot sometimes gets closed before the backup occurs
      await Stats.Save(ctx.Client);

      await ctx.Channel.SendMessageAsync($"Someone has committed an act of snakery {Stats.SnakeCount} times {DiscordEmoji.FromName(ctx.Client, ":snake:")}");
    }

    [Command("buns")]
    [Description("LeafBot promptly informs you of how many buns have been digitally liberated")]
    [Aliases("buncount", "count")]
    public async Task ServedBuns(CommandContext ctx)
    {
      DiscordEmoji emoji = DiscordEmoji.FromName(ctx.Client, ":rabbit:");

      await ctx.Channel.SendMessageAsync($"LeafBot has {Strings.DELIVERED_STRINGS[rng.Next(Strings.DELIVERED_STRINGS.Length)]} {Stats.BunniesServed} bunnies {emoji}");
    }
  }
}
