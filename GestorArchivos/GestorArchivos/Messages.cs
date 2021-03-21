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
            string[] msgClean = msg.Replace("<EOF>", "").Replace("{", "").Replace("}", "").Split(',');

            for (int i = 0; i < msgClean.Length; i++)
            {
                Console.WriteLine(msgClean[i]);
            }

            string[] action = msgClean[0].Split(':');
            string[] dst = msgClean[2].Split(':');


            if (dst[1] == "GestorArc")
            {
                if (action[1] == "create")
                {
                    Console.WriteLine("hola");
                    string[] inmsg = msgClean[3].Split('\"');
                    info = file.createFolder(inmsg[1]);
                    comunication.sendMessage(info, 8082);

                }
                else if (action[1] == "delete")
                {
                    string[] inmsg = msgClean[3].Split('\"');
                    info = file.deleteFolder(inmsg[1]);
                    comunication.sendMessage(info, 8082);
                }
                else if (action[1] == "send")
                {
                    string[] inmsg = msgClean[3].Split('\"');

                    inmsg = inmsg[1].Split('>');

                    info = file.createFile(inmsg[1]);
                    comunication.sendMessage(info, 8082);
                }
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
                    msg = "{codterm:0, msg:”OK”}";
                    break;
                case 1:
                    msg = "{codterm:1, msg:”0”}";
                    break;
                case 2:
                    msg = "{codterm:2, msg:”Err”}";
                    break;
            }
            return msg;
        }

        /* private void longMessage(string[] msgClean, string rawMsg)
         {
             switch (msgClean[2].Split(':')[1])
             {
                 case "GUI":
                     comunication.sendMessage(rawMsg, 8081);
                     break;
                 case "GestorArc":
                     comunication.sendMessage(rawMsg, 8082);
                     break;
                 case "kernel":
                     //comunication.sendMessage(rawMsg, 8082);
                     core.stopKernel();
                     break;
                 case "APP":
                     comunication.sendMessage(rawMsg, 8083);
                     break;
                 default:
                     break;
             }
         }*/

    }
}