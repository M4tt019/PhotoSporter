using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;

namespace PhotoSorter
{
    public class ClsFolderManager
    {
        #region Attributi
        //List<string> CartelleCreate;
        string _sourceFolder;
        string _destinationFolder;


        #endregion
        #region Costrutture
        public ClsFolderManager()
        {

        }
        #endregion
        #region Proprietà
        public string SourceFolder
        {
            get => _sourceFolder;
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                    throw new Exception("Il percorso della cartella di origine non può essere vuoto.");
                if (!System.IO.Directory.Exists(value))
                    throw new Exception("La cartella di origine specificata non esiste.");
                _sourceFolder = value;
            }
        }
        public string DestinationFolder {
            get => _destinationFolder;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Il percorso della cartella di destinazione non può essere vuoto.");
                if (!System.IO.Directory.Exists(value))
                    throw new Exception("La cartella di destinazione specificata non esiste.");
                _destinationFolder = value;
            }
            }

        #endregion
        #region Metodi
        internal void CreateVersionedFolders(string destinationFolder, List<string> cartelleCreate)
        {
            string completo = destinationFolder + " Completo";
                System.IO.Directory.Move(destinationFolder, completo);

            foreach (var cartella in cartelleCreate)
            {
                if (System.IO.Directory.Exists(cartella))
                {
                    string nuovaCartella = cartella + " Selezionato";
                    CopyFolder(cartella, nuovaCartella);
                }
            }
        }

        public static void CopyFolder(string origine, string copia)
        {
            if (!System.IO.Directory.Exists(copia))
                System.IO.Directory.CreateDirectory(copia);

            // Copia file
            foreach (string file in System.IO.Directory.GetFiles(origine))
            {
                string destFile = Path.Combine(copia, Path.GetFileName(file));
                File.Copy(file, destFile, true); // overwrite = true
            }
        }
    }
        #endregion 
}
