﻿using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Transactions;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace FarmCord
{
    public class Program
    {
        private DiscordSocketClient _client;
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()

        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info
            });
            _client.Log += Log;
            await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("Discord Token"));
            await _client.StartAsync();
            await Task.Delay(-1);
        }
        private Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }
        public class CommandHandler
        {
            private readonly DiscordSocketClient _client;
            private readonly CommandService _commands;

            public CommandHandler(DiscordSocketClient client, CommandService commands)
            {
                _commands = commands;
                _client = client;
            }
            public async Task InstallCommandsAsync()
            {
                _client.MessageReceived += HandleCommandAsync;
                await _commands.AddModuleAsync(assembly: Assembly.GetEntryAssembly(), services: null);
            }
        }
    }
}