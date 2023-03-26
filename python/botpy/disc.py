import os
from pytube import YouTube,Stream
from pytube import Search
from moviepy.editor import VideoFileClip
import asyncio

import discord 


from discord.ext import commands




ffmpeg_options = {
    'options': '-vn'
}

def download (link):
    yt = YouTube(link)
    stream = yt.streams.filter(file_extension='mp4')
    stream = yt.streams.get_highest_resolution() 
    stream.download('download','download.mp4',None,False,1000,2) #path,filename,fileprefix,skipifexists,timeout,max_retries
    nome  = f'download.mp4'
    video = VideoFileClip(os.path.join("download","","download.mp4"))
    video.audio.write_audiofile(os.path.join("download","","download.mp3"))
    


def cerca(nome):
    cer = Search(nome)
    cer=cer.results
    cer = str(cer)
    cer = cer.replace('pytube.__main__.YouTube object:','')
    cer = cer.replace ('videoId=','https://www.youtube.com/watch?v=')
    cer = cer.translate({ord(i): None for i in '<>[]'})
    svideo = cer.split(',', 10)
    
    return svideo

 


class Music(commands.Cog):
    def __init__(self, bot):
        self.bot = bot

    @commands.command()
    async def join(self, ctx, *, channel: discord.VoiceChannel):
        """Joins a voice channel"""

        if ctx.voice_client is not None:
            return await ctx.voice_client.move_to(channel)

        await channel.connect()

    @commands.command()
    async def p(self,ctx,*,quary):
        c = cerca(str('non pago afito'))
        #for i in range (0,5):
          #  await ctx.send(str(i)+') '+c[i])
        download(c[0])
        ctx.voice_client.play(discord.FFmpegPCMAudio(executable="ffmpeg.exe", source="download/download.mp3"))

    @commands.command()
    async def s(self,ctx,*,quary):
   
        c = cerca(str(quary))
        for i in range (0,5):
            await ctx.send(str(i)+') '+c[i])
        msg = await commands.bot.wait_for("message")
        download(c[int(msg)])
        ctx.voice_client.play(discord.FFmpegPCMAudio(executable="ffmpeg.exe", source="download/download.mp3"))
    
    @commands.command()
    async def stop(self, ctx):
        """Stops and disconnects the bot from voice"""

        await ctx.voice_client.disconnect()

    @p.before_invoke
    @s.before_invoke
    
    async def ensure_voice(self, ctx):
        if ctx.voice_client is None:
            if ctx.author.voice:
                await ctx.author.voice.channel.connect()
            else:
                await ctx.send("You are not connected to a voice channel.")
                raise commands.CommandError("Author not connected to a voice channel.")
        elif ctx.voice_client.is_playing():
            ctx.voice_client.stop()

bot = commands.Bot(command_prefix=commands.when_mentioned_or(""),
                   description='Relatively simple music bot example')

@bot.event
async def on_ready():
    print(f'Logged in as {bot.user} (ID: {bot.user.id})')
    print('------')

bot.add_cog(Music(bot))
bot.run('NzU4MDg1MTcwNzQ5MjQzNTUy.X2pz1Q.RM56DihdmL9NrmSX42GVeCvmlpE')