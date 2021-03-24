using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorArchivos
{
    class Files
    {
        public string createFile(string dato)
        {
            string path = @"C:\parcial2so\Log.txt";
            string msg;
            string data = DateTime.Now.ToLongDateString() + DateTime.Now.ToString("hh:mm:ss");
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                fs.Seek(0, SeekOrigin.End);


                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(data + " - " + dato);
                sw.Close();
                fs.Close();

                msg = "";
            }
            catch (Exception)
            {
                
                msg = "{cmd:send,src:GestorArc,dst:GUI,msg:\"Error->Error al crear el carpeta\"}";
            }
            return msg;

        }

        public string createFolder(string name)
        {
            string path = @"C:\parcial2so\" + name;
            string msg;

            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    msg = "{cmd:send,src:GestorArc,dst:GUI, msg:\"OK\"}";
                    createFile("La carpeta " + name+ " fue creada correctamente.");

                }
                else
                {
                    createFile("La carpeta " + name + " ya existe.");
                    msg = "{cmd:send,src:GestorArc,dst:GUI,msg:\"Error->La carpeta ya existe\"}";
                }
                

            }
            catch (Exception)
            {
                createFile("Error al crear la carpeta " + name );
                msg = "{cmd:send,src:GestorArc,dst:GUI,msg:\"Error->Error al crear el carpeta\"}";
            }
            return msg;
        }

        public string deleteFolder(string name)
        {
            string path = @"C:\parcial2so\" + name;
            string msg;

            try
            {
                Directory.Delete(path, true);
                msg = "{cmd:send,src:GestorArc,dst:GUI,msg:\"OK\"}";
                createFile("La carpeta " + name + " fue eliminada correctamente.");
            }
            catch (Exception)
            {
                createFile("Error al borrar la carpeta " + name + ".");
                msg = "{cmd:send,src:GestorArc,dst:GUI,msg:\"Error->Error al crear el carpeta\"}";

            }
            return msg;
        }
    }
}
