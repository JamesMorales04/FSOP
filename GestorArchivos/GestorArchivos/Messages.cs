using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorArchivos
{
    /*
         * File sistem port: 8082 
         * GUI port: 8081
         * Kernel port: 8080
         * 
         */
    class Messages
    {
        private Comunication comunication;
        private GestorArchivos core;
        private Files file = new Files();
        

        public Messages(GestorArchivos core, Comunication comunication)
        {
            this.comunication = comunication;
            this.core = core;

        }


        public void Actions(string msg)
        {
            string info = "";
            string[] inmsg;
            string[] msgClean = msg.Replace("<EOF>", "").Replace("{", "").Replace("}", "").Split(',');

            string[] action = msgClean[0].Split(':');
            string[] dst = msgClean[2].Split(':');


            if (dst[1] == "GestorArc")
            {
                switch (action[1])
                {
                    case "create":
                        Console.WriteLine("hola");
                        inmsg = msgClean[3].Split('\"');
                        info = file.createFolder(inmsg[1]);
                        comunication.sendMessage(info, 8080);
                        break;
                    case "delete":
                        inmsg = msgClean[3].Split('\"');
                        info = file.deleteFolder(inmsg[1]);
                        comunication.sendMessage(info, 8080);
                        break;
                    case "send":
                        inmsg = msgClean[3].Split('\"');
                        inmsg = inmsg[1].Split('>');

                        file.createFile(inmsg[1]);
                        break;
                    case "stop":
                        inmsg = msgClean[3].Split('\"');
                        file.createFolder(inmsg[1]);
                        core.stopGestorArc();
                        break;
                }
            }
            else
            {
                file.createFile(msg.Replace("<EOF>", "").Replace("{", "").Replace("}", ""));
            }

        }

        public string Response()
        {
            var seed = Environment.TickCount;
            var random = new Random(seed);

            var value = random.Next(0, 2);
            string msg = "";
   
            switch (value)
            {
                case 0:
                    msg = "{codterm:0,msg:\"OK\"}<EOF>";
                    break;
                case 1:
                    msg = "{codterm:1,msg:\"0\"}<EOF>";
                    break;
                case 2:
                    msg = "{codterm:2,msg:\"Err\"}<EOF>";
                    break;
            }
            return msg;
        }

    }
}