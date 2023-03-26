private void Server_DataReceived(object? sender, Message e)
        {
            this.Dispatcher.Invoke(() =>
            {
                
                int numtogen;

                s = e.MessageString;
                e.Reply("ok");
                s = s.Replace("\u0013", "");
                //textbox.Text = textbox.Text + s;
                if(Int32.TryParse(s,out numtogen))
                {
                    list.Clear();
                    for (int i = 0; i < numtogen; i++)
                    {
                        list.Add(ran.Next(0, 100));
                        

                    }
                    Refreshbox();
                }else if(s == "ordina")
                {
                    
                    double[] ordinato = MergeSort.inPlaceMergeS(list.ToArray());
                    list.Clear();
                    list = ordinato.ToList();
                    Refreshbox();


                }

            });
            
        }