const {Telegraf} = require('telegraf');

const BOT_TOKEN = ""
const bot = new Telegraf(BOT_TOKEN)

bot.start(async (ctx) => ctx.reply("Ciao!"));

bot.command('menu',async (ctx) => ctx.reply("ok"))



bot.launch();