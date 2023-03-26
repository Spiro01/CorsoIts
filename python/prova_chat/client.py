import socket, threading
from key import encrypt
from file import filez
from os import path
import re


user = input('Inserisci il tuo nome: ')

#password = input('Inserisci la password: ')
key = b'BhFBTJrJwsV5M-bG9yg598kNsbnjEuFsUgIuCtrY0lg='

def handle_messages(connection: socket.socket):
    '''
        Receive messages sent by the server and display them to user
    '''

    while True:
        try:
            
            
            lenght = int(encrypt.dec(key,connection.recv(1024)))  
            msg = connection.recv(lenght)     
            msg = encrypt.dec(key,msg)
        
            msg = msg.decode('utf-8')
            # If there is no message, there is a chance that connection has closed
            # so the connection will be closed and an error will be displayed.
            # If not, it will try to decode message in order to show to user.
            if '!file!' in msg:
                
                
                
                msg = msg.replace('!file!','')
                name = re.search('!(.*)!', msg).group(1)
                msg = msg.replace('!'+name+'!','')
                msg.encode('utf-8')
                msg = msg.lstrip()
                with open(name,'w') as file:
    
                        file.write(msg)
                        file.close()

                print('successfully downloaded.')
            
            if msg:
                print(msg)
            else:
                connection.close()
                break

        except Exception as e:
            print(f'Error handling message from server: {e}')
            connection.close()
            break

def client() -> None:
    '''
        Main process that start client connection to the server 
        and handle it's input messages
    '''

    SERVER_ADDRESS = ''
    SERVER_PORT = 12000

    try:
        # Instantiate socket and start connection with server
        socket_instance = socket.socket()
        socket_instance.connect((SERVER_ADDRESS, SERVER_PORT))
        # Create a thread in order to handle messages sent by server
        threading.Thread(target=handle_messages, args=[socket_instance]).start()
        
        print('Connected to chat!')
        
        # Read user's input until it quit from chat and close connection
        while True:
            msg = input()
            
            if msg == '<f>':
                data = filez.open()
                name = filez.name(data)
                
                print('Sending',data)
                if data != '':
                    file = open(data,'r')
                    data = file.read()
                    
                    
                    data = str(data)
                    data = '!file! ' + '!'+name+'! ' + data
                    
                    
                    msg = encrypt.enc(key,data)
                        
                    socket_instance.send(encrypt.enc(key,str((len(msg.decode('utf-8'))))))
                    socket_instance.send(msg)
                        
                        
                        

                    
                    
               
                #data = socket_instance.recv(1024).decode('utf-8')
                
            elif msg == '<q>':
                
                break

            else:
                msg = '<'+user+'> '+ msg
            
                msg = encrypt.enc(key,msg)
            
                socket_instance.send(encrypt.enc(key,str((len(msg.decode('utf-8'))))))
            # Parse message to utf-8
                socket_instance.send(msg)

        # Close connection with the server
        socket_instance.close()

    except Exception as e:
        print(f'Error connecting to server socket {e}')
        socket_instance.close()


if __name__ == "__main__":
    client()