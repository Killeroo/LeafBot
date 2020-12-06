﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

using LeafBot.Data;

namespace LeafBot.Commands
{
  public class Games : BaseCommandModule
  {
    private Random random = new Random();

    [Command("roll")]
    [Description("Instructs LeafBot to roll a lettuce shaped dice")]
    [Aliases("rtd", "dice", "rollthedice")]
    public async Task RollTheDice(CommandContext ctx)
    {
      DiscordEmoji emoji = DiscordEmoji.FromName(ctx.Client, ":game_die:");

      await ctx.RespondAsync($"{ctx.Member.Mention} rolls {random.Next(0, 101)} {emoji}");
    }

    [Command("flip")]
    [Description("Instructs LeafBot to flip a cabbage leaf")]
    [Aliases("coin", "coinflip")]
    public async Task FlipCoin(CommandContext ctx)
    {
      DiscordEmoji emoji = DiscordEmoji.FromName(ctx.Client, ":leaves:");

      string outcome = "";
      if (random.Next(0, 2) == 1)
      {
        outcome = "Heads";
      } 
      else
      {
        outcome = "Tails";
      }

      await ctx.RespondAsync($"{ctx.Member.Mention} flips a cabbage leaf {emoji} ... {outcome}! ");
    }

    [Command("role")]
    [Description("LeafBot will decide what role you should queue as, indecisive fool...")]
    [Aliases("icantdecide", "pickrole", "halp", "queue")]
    public async Task PickRole(CommandContext ctx)
    {
      DiscordEmoji damageEmoji = DiscordEmoji.FromName(ctx.Client, ":crossed_swords:");
      DiscordEmoji tankEmoji = DiscordEmoji.FromName(ctx.Client, ":shield:");
      DiscordEmoji healerEmoji = DiscordEmoji.FromName(ctx.Client, ":medical_symbol:");

      // setup the choices (the keys are the _key_ to this)
      // (I hate myself)
      Dictionary<int, string> roles = new Dictionary<int, string>
      {
        {1, $"Tank {tankEmoji}"},
        {2, $"Damage {damageEmoji}"},
        {3, $"Healer {healerEmoji}" }
      };

      // Choose random number of roles to pick, randomly order roles, pick random number of items and order based on key
      int numberOfRoles = random.Next(1, 4);
      var picks = roles.OrderBy(x => random.Next()).Take(numberOfRoles).OrderBy(x => x.Key).ToArray();

      // Go through each picked role and HARDCODE the commas and 'and's
      // I hate this so much, but it's late and I need to sleep
      string result = "";
      int count = 0;
      foreach (var pick in picks)
      {
        count++;
        result += pick.Value;
        if (picks.Length == 2 && count == 1)
        {
          result += " and ";
        }
        else if (picks.Length > 2 && count == 2)
        {
          result += " and ";
        }
        else if (picks.Length > 2 && count == 1)
        {
          result += ", ";
        }
      }
      await ctx.RespondAsync($"{ctx.Member.Mention}, you will queue for {result}. {Strings.COMMANDING_STRINGS[random.Next(Strings.COMMANDING_STRINGS.Length)]}!");
    }
  }
}
