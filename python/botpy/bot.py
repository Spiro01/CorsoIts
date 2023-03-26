import os
import requests
from telegram.ext import (
    Updater,
    CommandHandler,
    MessageHandler,
    Filters,
    ConversationHandler,
    CallbackContext,
)
import logging
from pytube import YouTube,Stream
from pytube import Search
from telegram import ReplyKeyboardMarkup, ReplyKeyboardRemove, Update,ForceReply
from moviepy.editor import VideoFileClip
svideo = 'x'



def convert_download():
    video = VideoFileClip(os.path.join("download","","download.mp4"))
    video.audio.write_audiofile(os.path.join("download","","download.mp3"))

def chatid(update):
    
    return update.effective_chat.id
def return_weather(citta):

    
    api_result = requests.get('https://api.openweathermap.org/data/2.5/forecast?q='+citta+',it&appid=4a62f9416c24545c1ae267c43a063248&units=metric')
    # Retrieve values from API
    data = api_result.json()
    temp = data['list'] [1]
    return str(temp)

def weather(update, context):

    #context.bot.send_message(chat_id=update.effective_chat.id, text='Inserisci la citta di cui vuoi sapere la temperatura')
    citta = update.message.text
    citta = citta.replace('/weather','')
    #context.bot.send_message(chat_id=update.effective_chat.id, text=citta)
    context.bot.send_message(chat_id=update.effective_chat.id, text=return_weather(citta))


def cerca(nome):
    cer = Search(nome)
    cer=cer.results
    cer = str(cer)
    cer = cer.replace('pytube.__main__.YouTube object:','')
    cer = cer.replace ('videoId=','https://www.youtube.com/watch?v=')
    cer = cer.translate({ord(i): None for i in '<>[]'})
    svideo = cer.split(',', 10)
    
    return svideo






def read(update,com):
    a = update.message.text
    a = a.replace(com,'')
    return a


def prova (update,context):
    context.bot.send_message(chat_id=update.effective_chat.id,text =  'test')
    convert_download()
    context.bot.send_video(chatid(update), video=open('me', yt.title+'.mp3'),timeout = 1000)   



def search (update,context):
    reply_keyboard = [['1', '2', '3']]
    ric = read (update,'/search')
    lastres = cerca(ric)
    for x in range(0,3):
        context.bot.send_message(chat_id=update.effective_chat.id,text =  lastres[x])
    global ricerca
    ricerca = lastres
    update.message.reply_text('Quale video?',reply_markup=ReplyKeyboardMarkup(reply_keyboard, one_time_keyboard=True, input_field_placeholder='Quale video?'),)





    
   
def start (update,context):
    context.bot.send_message(chat_id=update.effective_chat.id,text =  "Per iniziare digita /search (nome_video)")
    
def get_select(update, context):
    
    sel = update.message.text
    global selezione
    selezione =  int(sel)-1

    answer = 'La tua selezione:' + ricerca[selezione]  
    update.message.reply_text(answer)
    update.message.reply_text('Per scaricarlo usa /download')


def download (update,context):
    global yt
    yt = YouTube(ricerca[selezione])
    #yt = YouTube('https://www.youtube.com/watch?v=oc8RmW0bk4g&ab_channel=AlfettaUnocinquenove')
    stream = yt.streams.filter(file_extension='mp4')
    stream = yt.streams.get_highest_resolution()
    context.bot.send_message(chat_id=update.effective_chat.id,text =  "Download iniziato...")
    stream.download('download','download.mp4',None,False,1000,2) #path,filename,fileprefix,skipifexists,timeout,max_retries
    nome  = f'download.mp4'
    convert_download()
    context.bot.send_message(chat_id=update.effective_chat.id,text =  "Conversione effettuata, invio in corso....")
    #context.bot.send_message(chat_id=update.effective_chat.id,text =  f'Il video {nome} è stato scaricato')
    context.bot.send_audio(chatid(update), audio=open('download/download.mp3', 'rb'),timeout = 1000)
    
    
   
          

def main():
    TOKEN = "539146555:AAG94adhArc-FcwI6toQo98XvOp_JwubZQk"
    updater = Updater(token=TOKEN, use_context=True)
    dp = updater.dispatcher 



    weather_handler = CommandHandler('weather', weather)

    prova_handler = CommandHandler('prova', prova)

    search_handler = CommandHandler('search', search)

    start_handler = CommandHandler('start',start)
    download_handler = CommandHandler('download', download)
    dp.add_handler(download_handler)
    dp.add_handler(weather_handler)
    dp.add_handler(start_handler)
    dp.add_handler(prova_handler)
    dp.add_handler(search_handler)

    #dp.add_handler(CommandHandler('select', select))
    dp.add_handler(MessageHandler(Filters.text, get_select))
    
    
    
    
    logging.basicConfig(format='%(asctime)s - %(name)s - %(levelname)s - %(message)s',level=logging.INFO)
    updater.start_polling()

    

if __name__ == '__main__':
    main()
    

