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
    public class ClsPhotoOrganizer
    {
        #region attributi
        bool _cancellare;        //indica se i file devono essere cancellati e meno
        string _formatoData;    //indica quali formato di data si utilizza
         List<string> _cartelleCreate = new List<string>(); //elenco delle cartelle create
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
        #endregion

        #region metodi
        public void OrganizePhotos(string sourceFolder, string destinationFolder, int startDay, ProgressBar progressBar)
        {
            CartelleCreate = CartelleCreate.OrderBy(folder => folder).ToList();
            List<String> CartelleCreateApp = new List<string>(CartelleCreate);
            progressBar.Maximum = CartelleCreateApp.Count;
            progressBar.Value = 0;

            foreach (var file in CartelleCreateApp)
            {
                try
                {
                    if (System.IO.Directory.Exists(file))
                    {
                        ClsFolderManager.CopyFolder(file, destinationFolder + @"\Giorno " + startDay);
                        System.IO.Directory.Delete(file, true);
                        CartelleCreate.Remove(file);
                        startDay++;

                        progressBar.Value++;
                    }
                }catch
                {
                    MessageBox.Show($"errore con l'immagine {file}.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        public void OrganizePhotosByDate(string sourceFolder, string destinationFolder, string dateFormat, ProgressBar progressBar)
        {
            var imageFiles = GetImmagines(sourceFolder);
            int totalFiles = imageFiles.Count;
            progressBar.Maximum = totalFiles;
            progressBar.Value = 0;
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
                    //Aggiorna la progress bar
                    progressBar.Value++;
                }catch
                {
                    MessageBox.Show($"errore con l'immagine {file}.","Errore",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
               

            }
        }
        public void copiaOordina(string nameFolder, string file, out string destinationPath)
        {
            // Sposta o copia file il file
            destinationPath = Path.Combine(nameFolder, Path.GetFileName(file));
            if (!File.Exists(destinationPath))
            {
                if (Cancellare)
                    File.Move(file, destinationPath);
                else
                    File.Copy(file, destinationPath);
            }

        }


        public static List<string> GetImmagines(string sourceFolder)
        {
            return System.IO.Directory.GetFiles(sourceFolder, "*.*", SearchOption.AllDirectories)
                                      .Where(f => f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                                  f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                                                  f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                                      .ToList();
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

