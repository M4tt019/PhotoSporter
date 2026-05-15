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
using System.Windows.Forms;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;


namespace PhotoSorter
{
    public class PhotoProgress
    {
        public int Value { get; set; }
        public int Maximum { get; set; }
    }

    public class ClsPhotoOrganizer
    {
        #region attributi
        bool _cancellare;        //indica se i file devono essere cancellati e meno
        string _formatoData;    //indica quali formato di data si utilizza
         List<string> _cartelleCreate = new List<string>(); //elenco delle cartelle create
        List<string> _errori = new List<string>();
        #endregion
        #region costrutture
        public ClsPhotoOrganizer()
        {
            Cancellare = false;
            FormatoData = "yyyy-MM-dd";
        }
        #endregion
        #region proprietà
        public bool Cancellare { get => _cancellare; set => _cancellare = value; }
        public string FormatoData { get => _formatoData; set => _formatoData = value; }
        public  List<string> CartelleCreate { get => _cartelleCreate; set => _cartelleCreate = value; }
        public List<string> Errori { get => _errori; set => _errori = value; }
        #endregion

        #region metodi
        public void OrganizePhotos(string sourceFolder, string destinationFolder, int startDay, IProgress<PhotoProgress> progress)
        {
            CartelleCreate = CartelleCreate.Distinct().OrderBy(folder => folder).ToList();
            List<String> CartelleCreateApp = new List<string>(CartelleCreate);
            List<string> cartelleGiornoCreate = new List<string>();
            int valore = 0;
            progress?.Report(new PhotoProgress { Maximum = CartelleCreateApp.Count, Value = valore });

            foreach (var file in CartelleCreateApp)
            {
                try
                {
                    if (System.IO.Directory.Exists(file))
                    {
                        string cartellaGiorno = Path.Combine(destinationFolder, "Giorno " + startDay);
                        ClsFolderManager.CopyFolder(file, cartellaGiorno, Errori);
                        System.IO.Directory.Delete(file, true);
                        CartelleCreate.Remove(file);
                        cartelleGiornoCreate.Add(cartellaGiorno);
                        startDay++;
                    }
                }
                catch (Exception ex)
                {
                    Errori.Add($"Errore con la cartella {file}: {ex.Message}");
                }
                finally
                {
                    valore++;
                    progress?.Report(new PhotoProgress { Maximum = CartelleCreateApp.Count, Value = valore });
                }

            }

            CartelleCreate = cartelleGiornoCreate;
        }
        public void OrganizePhotosByDate(string sourceFolder, string destinationFolder, string dateFormat, IProgress<PhotoProgress> progress)
        {
            CartelleCreate.Clear();
            Errori.Clear();
            var imageFiles = GetImmagines(sourceFolder);
            int totalFiles = imageFiles.Count;
            int valore = 0;
            progress?.Report(new PhotoProgress { Maximum = totalFiles, Value = valore });
            foreach (var file in imageFiles)
            {
                try
                {
                    //Estrai la data di scatto
                    var DataScatto = PrendiDataScatto(file) ?? File.GetCreationTime(file);

                    //Crea la sottocartella basata sulla data(formato scelto)
                    string dateFolder = Path.Combine(destinationFolder, DataScatto.ToString(dateFormat));
                    System.IO.Directory.CreateDirectory(dateFolder);
                    string destinationPath;
                    copiaOordina(dateFolder, file, out destinationPath);
                    CartelleCreate.Add(dateFolder);
                }catch (Exception ex)
                {
                    Errori.Add($"Errore con l'immagine {file}: {ex.Message}");
                }
                finally
                {
                    valore++;
                    progress?.Report(new PhotoProgress { Maximum = totalFiles, Value = valore });
                }
               

            }
        }
        public void copiaOordina(string nameFolder, string file, out string destinationPath)
        {
            // Sposta o copia file il file
            destinationPath = GetDestinationPathUnivoco(nameFolder, file);
            if (string.IsNullOrWhiteSpace(destinationPath))
            {
                if (Cancellare && File.Exists(file))
                    File.Delete(file);
                return;
            }

            if (Cancellare)
                File.Move(file, destinationPath);
            else
                File.Copy(file, destinationPath);

        }

        private string GetDestinationPathUnivoco(string folder, string sourcePath)
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


        public static List<string> GetImmagines(string sourceFolder)
        {
            List<string> immagini = new List<string>();
            Queue<string> cartelle = new Queue<string>();
            cartelle.Enqueue(sourceFolder);

            while (cartelle.Count > 0)
            {
                string cartellaCorrente = cartelle.Dequeue();

                try
                {
                    foreach (string file in System.IO.Directory.GetFiles(cartellaCorrente))
                    {
                        if (file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                            file.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                            file.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                        {
                            immagini.Add(file);
                        }
                    }
                }
                catch
                {
                }

                try
                {
                    foreach (string directory in System.IO.Directory.GetDirectories(cartellaCorrente))
                        cartelle.Enqueue(directory);
                }
                catch
                {
                }
            }

            return immagini;
        }

        static DateTime? PrendiDataScatto(string path)
        {
            try
            {
                var directories = ImageMetadataReader.ReadMetadata(path);
                var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                return subIfdDirectory?.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);
            }
            catch
            {
                return null; // Restituisci null se non è possibile leggere i metadati
            }
        }
        #endregion
    }
}

