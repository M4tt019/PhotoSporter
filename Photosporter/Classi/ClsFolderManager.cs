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
        internal List<string> CreateVersionedFolders(string destinationFolder, List<string> cartelleCreate)
        {
            if (!System.IO.Directory.Exists(destinationFolder))
                throw new Exception($"La cartella '{destinationFolder}' non esiste. Impossibile creare le cartelle doppie.");

            List<string> cartelleSelezionate = new List<string>();
            string destinationCompleta = Path.GetFullPath(destinationFolder).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            List<string> cartelleDaDuplicare = cartelleCreate
                .Where(cartella => !string.IsNullOrWhiteSpace(cartella) && System.IO.Directory.Exists(cartella))
                .Select(cartella => Path.GetFullPath(cartella))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Where(cartella =>
                {
                    string parent = Path.GetDirectoryName(cartella);
                    string nome = Path.GetFileName(cartella);
                    return string.Equals(parent, destinationCompleta, StringComparison.OrdinalIgnoreCase) &&
                           !nome.StartsWith("Selezionato ", StringComparison.OrdinalIgnoreCase);
                })
                .ToList();

            foreach (string cartella in cartelleDaDuplicare)
            {
                string nomeCartella = Path.GetFileName(cartella);
                string selezionato = Path.Combine(destinationFolder, "Selezionato " + nomeCartella);

                try
                {
                    CopyFolder(cartella, selezionato);
                    cartelleSelezionate.Add(selezionato);
                }
                catch (Exception ex)
                {
                    throw new Exception($"La copia completa e' stata creata, ma non riesco a creare la copia filtrata '{selezionato}': {ex.Message}", ex);
                }
            }

            return cartelleSelezionate;
        }

        public static void CopyFolder(string origine, string copia, List<string> errori = null)
        {
            if (!System.IO.Directory.Exists(copia))
                System.IO.Directory.CreateDirectory(copia);

            foreach (string file in System.IO.Directory.GetFiles(origine))
            {
                try
                {
                    string destFile = GetDestinationPathUnivoco(copia, file);
                    if (!string.IsNullOrWhiteSpace(destFile))
                        File.Copy(file, destFile);
                }
                catch (Exception ex)
                {
                    errori?.Add($"Errore durante la copia di {file}: {ex.Message}");
                }
            }

            foreach (string directory in System.IO.Directory.GetDirectories(origine))
            {
                try
                {
                    string destDirectory = Path.Combine(copia, Path.GetFileName(directory));
                    CopyFolder(directory, destDirectory, errori);
                }
                catch (Exception ex)
                {
                    errori?.Add($"Errore durante la copia della cartella {directory}: {ex.Message}");
                }
            }
        }

        private static string GetDestinationPathUnivoco(string folder, string sourcePath)
        {
            string fileName = Path.GetFileName(sourcePath);
            string destinationPath = Path.Combine(folder, fileName);
            if (!File.Exists(destinationPath))
                return destinationPath;

            FileInfo sorgente = new FileInfo(sourcePath);
            FileInfo destinazione = new FileInfo(destinationPath);
            if (sorgente.Length == destinazione.Length)
                return string.Empty;

            string nomeSenzaEstensione = Path.GetFileNameWithoutExtension(fileName);
            string estensione = Path.GetExtension(fileName);
            int contatore = 1;

            do
            {
                destinationPath = Path.Combine(folder, $"{nomeSenzaEstensione} ({contatore}){estensione}");
                if (File.Exists(destinationPath) && sorgente.Length == new FileInfo(destinationPath).Length)
                    return string.Empty;

                contatore++;
            }
            while (File.Exists(destinationPath));

            return destinationPath;
        }
    }
        #endregion 
}
