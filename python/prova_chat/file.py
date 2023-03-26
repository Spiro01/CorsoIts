from tkinter import filedialog as fd
import ntpath
class filez:
    def open():
        path = fd.askopenfilename()
             
        
        return str(path)

    def save():
        path = fd.asksaveasfilename()
        return str(path)

    def name(path):
        
        head, tail = ntpath.split(path)
        return tail or ntpath.basename(head)
