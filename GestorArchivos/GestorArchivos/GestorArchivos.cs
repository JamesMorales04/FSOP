
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace GestorArchivos
{ 
    public class GestorArchivos
    {
    
        public static int Main(String[] args)
        {

            Files file = new Files();

            GestorArchivos core = new GestorArchivos();
            Comunication comunicationSet = new Comunication();
            Messages messages = new Messages(core, comunicationSet);
            comunicationSet.setterMessages(messages);
            Thread listener = new Thread(() => comunicationSet.StartListening(8082));
            listener.Start();

            //messages.Actions("{cmd:create, src:GUI, dst:GestorArc, msg:\"coso\"}");
            return 0;
        }

        public void stopGestorArc()
        {
            System.Environment.Exit(1);
        }
    }
}