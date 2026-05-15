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
    public partial class FrmHome : Form
    {
        public FrmHome()
        {
            InitializeComponent();
            CaricaConfigurazioneUtente();
            this.FormClosing += FrmHome_FormClosing;
            AggiornaEtichettaSoglia();
        }
        #region Variabili globali

        ClsPhotoOrganizer PhotoOrganizer = new ClsPhotoOrganizer();
        ClsFolderManager FolderManager = new ClsFolderManager();
        int trackSensibilita = 5;
        string cartellaDestinazioneBase = string.Empty;
        #endregion
        #region gestione impostazioni ordinamento

        private void rdData_CheckedChanged_1(object sender, EventArgs e)
        {
            pnTypeDate.Enabled = true;
            pnDays.Enabled = false;
        }
        private void rdGiorno_CheckedChanged_1(object sender, EventArgs e)
        {
            pnDays.Enabled = true;
            rbDataAm.Checked = false;
            rdDataEur.Checked = false;
            pnTypeDate.Enabled = false;
        }

        #endregion

        #region Ordinamento
        private async void btSmorta_Click(object sender, EventArgs e)
        {
            Cursor cursorePrecedente = Cursor.Current;
            try
            {
                string cartellaDestinazione = GetCartellaDestinazioneFinale();
                PreparaCartellaDestinazione(cartellaDestinazione);
                FolderManager.DestinationFolder = cartellaDestinazione;
                PhotoOrganizer.FormatoData = GetDateFormat();  // Ottieni il formato della data
                SalvaConfigurazioneUtente();

                pgSpostamentoFile.Style = ProgressBarStyle.Continuous;
                // Trova unità rimovibili
                var removableDrive = DriveInfo.GetDrives().FirstOrDefault(drive => drive.DriveType == DriveType.Removable && drive.IsReady);
                if (removableDrive == null)
                    throw new Exception("Nessuna unità rimovibile rilevata. Assicurati di aver inserito una chiavetta USB o un disco esterno.");

                // Conferma con l'utente
                FolderManager.SourceFolder = removableDrive.RootDirectory.FullName; // Imposta la cartella di origine come la radice dell'unità rimovibile
                ValidaPercorsiOrigineDestinazione(FolderManager.SourceFolder, FolderManager.DestinationFolder);

                DialogResult result = MessageBox.Show(
                    $"Origine foto: {FolderManager.SourceFolder}\nDestinazione: {FolderManager.DestinationFolder}\n\nVuoi continuare?",
                    "Conferma origine e destinazione",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.No) return;

                if (PhotoOrganizer.FormatoData == string.Empty && !rdGiorno.Checked)
                    throw new Exception("Errore: nessun formato di ordinamento selezionato");

                string sourceFolder = FolderManager.SourceFolder;
                string destinationFolder = FolderManager.DestinationFolder;
                string formatoData = PhotoOrganizer.FormatoData;
                bool ordinaPerGiorno = rdGiorno.Checked;
                int startDay = (int)nudGiorni.Value;
                bool cancellaOrigine = ckCancellaCartella.Checked;
                bool creaCartelleDoppie = ckCartelleDoppie.Checked;
                bool cancellaDoppioni = cbCancellaDoppioni.Checked;
                int soglia = trackSensibilita;
                string cartellaDaAprire = destinationFolder;
                string outputPulizia = string.Empty;

                btSmorta.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                pgSpostamentoFile.Style = ProgressBarStyle.Continuous;

                var progress = new Progress<PhotoProgress>(p =>
                {
                    pgSpostamentoFile.Maximum = Math.Max(1, p.Maximum);
                    pgSpostamentoFile.Value = Math.Min(pgSpostamentoFile.Maximum, Math.Max(0, p.Value));
                });

                List<string> errori = await Task.Run(() =>
                {
                    PhotoOrganizer.Cancellare = cancellaOrigine;

                    if (ordinaPerGiorno)
                    {
                        PhotoOrganizer.OrganizePhotosByDate(sourceFolder, destinationFolder, "yyyy-MM-dd", progress);
                        PhotoOrganizer.OrganizePhotos(sourceFolder, destinationFolder, startDay, progress);
                    }
                    else
                    {
                        PhotoOrganizer.OrganizePhotosByDate(sourceFolder, destinationFolder, formatoData, progress);
                    }

                    string cartellaDaFiltrare = destinationFolder;
                    List<string> cartelleDaFiltrare = new List<string>();
                    if (creaCartelleDoppie)
                    {
                        cartelleDaFiltrare = FolderManager.CreateVersionedFolders(destinationFolder, PhotoOrganizer.CartelleCreate);
                        cartellaDaAprire = destinationFolder;
                    }
                    else
                    {
                        cartelleDaFiltrare.Add(cartellaDaFiltrare);
                    }

                    if (cancellaDoppioni)
                    {
                        try
                        {
                            List<string> outputPulizie = new List<string>();
                            foreach (string cartella in cartelleDaFiltrare)
                            {
                                string outputCartella = EseguiPuliziaDoppioni(cartella, soglia);
                                if (!string.IsNullOrWhiteSpace(outputCartella))
                                    outputPulizie.Add($"{Path.GetFileName(cartella)}:\n{outputCartella}");
                            }

                            outputPulizia = string.Join("\n", outputPulizie);
                        }
                        catch (Exception ex)
                        {
                            PhotoOrganizer.Errori.Add($"Errore durante la pulizia doppioni: {ex.Message}");
                        }
                    }

                    return new List<string>(PhotoOrganizer.Errori);
                });

                if (!string.IsNullOrWhiteSpace(outputPulizia))
                    MessageBox.Show(outputPulizia, "Pulizia doppioni", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (errori.Count > 0)
                    MessageBox.Show($"Operazione completata con {errori.Count} errori.\n\n{string.Join("\n", errori.Take(5))}", "Avvisi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                DialogResult dr = MessageBox.Show("Operazione completata, vuoi aprire la cartella?", "Apri Cartella", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                    Process.Start("explorer.exe", cartellaDaAprire);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore: {ex.Message}", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btSmorta.Enabled = true;
                pgSpostamentoFile.Style = ProgressBarStyle.Continuous;
                pgSpostamentoFile.Value = 0;
                Cursor.Current = cursorePrecedente;
            }


        }
        private string GetDateFormat()
        {
            if (rdDataEur.Checked) return "dd-MM-yyyy";  // Formato europeo
            if (rbDataAm.Checked) return "MM-dd-yyyy";  // Formato americano
           // MessageBox.Show("Errore: formato data non selezionato", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return string.Empty;
        }

        private void PreparaCartellaDestinazione(string cartellaDestinazione)
        {
            if (string.IsNullOrWhiteSpace(cartellaDestinazione))
                throw new Exception("Il percorso della cartella di destinazione non puo' essere vuoto.");

            if (!System.IO.Directory.Exists(cartellaDestinazione))
                System.IO.Directory.CreateDirectory(cartellaDestinazione);
        }

        private string GetCartellaDestinazioneFinale()
        {
            if (ckNewDirectory.Checked)
            {
                if (string.IsNullOrWhiteSpace(cartellaDestinazioneBase))
                    throw new Exception("Seleziona prima una cartella di destinazione.");
                if (string.IsNullOrWhiteSpace(tbNewDirectory.Text))
                    throw new Exception("Inserisci il nome della nuova cartella.");

                return Path.Combine(cartellaDestinazioneBase, tbNewDirectory.Text.Trim());
            }

            return tbIndirizzo.Text.Trim();
        }

        private void ValidaPercorsiOrigineDestinazione(string origine, string destinazione)
        {
            string origineCompleta = Path.GetFullPath(origine).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar;
            string destinazioneCompleta = Path.GetFullPath(destinazione).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar;

            if (string.Equals(origineCompleta, destinazioneCompleta, StringComparison.OrdinalIgnoreCase))
                throw new Exception("La cartella di origine e quella di destinazione non possono essere la stessa.");

            if (destinazioneCompleta.StartsWith(origineCompleta, StringComparison.OrdinalIgnoreCase))
                throw new Exception("La destinazione non puo' stare dentro la chiavetta/unità sorgente. Scegli una cartella sul PC o su un'altra unità.");

            if (origineCompleta.StartsWith(destinazioneCompleta, StringComparison.OrdinalIgnoreCase))
                throw new Exception("La sorgente non puo' stare dentro la destinazione. Scegli due cartelle separate.");
        }
        #endregion
        #region gestione cartelle
        private void btIndirizzoCartella_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Seleziona una cartella esistente";
            fbd.SelectedPath = @"C:\";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                cartellaDestinazioneBase = fbd.SelectedPath;
                FolderManager.DestinationFolder = cartellaDestinazioneBase;  // Ottieni il percorso della cartella selezionata
                tbIndirizzo.Text = cartellaDestinazioneBase;
                ckNewDirectory.Enabled = true;
                btSmorta.Enabled = true;
            }
        }


        private void ckNewDirectory_CheckedChanged(object sender, EventArgs e)
        {
            if (ckNewDirectory.Checked)
            {
                pnNewDirectory.Visible = true;
                AggiornaPercorsoNuovaCartella();
            }
            else
            {
                pnNewDirectory.Visible = false;
                tbNewDirectory.Text = null;
                tbIndirizzo.Text = cartellaDestinazioneBase;
            }
        }

        private void tbNewDirectory_TextChanged(object sender, EventArgs e)
        {
            if (ckNewDirectory.Checked)
                AggiornaPercorsoNuovaCartella();
        }

        private void AggiornaPercorsoNuovaCartella()
        {
            if (string.IsNullOrWhiteSpace(cartellaDestinazioneBase))
                return;

            if (string.IsNullOrWhiteSpace(tbNewDirectory.Text))
                tbIndirizzo.Text = cartellaDestinazioneBase + @"\";
            else
                tbIndirizzo.Text = Path.Combine(cartellaDestinazioneBase, tbNewDirectory.Text);
        }
        #endregion
        #region cancellaCartella
        private void ckCancellaCartella_CheckedChanged(object sender, EventArgs e)
        {
            PhotoOrganizer.Cancellare= ckCancellaCartella.Checked;
        }
        #endregion

        private void tbIndirizzo_TextChanged(object sender, EventArgs e)
        {
            ckNewDirectory.Enabled = true;
        }
        private void cbCancellaDoppioni_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCancellaDoppioni.Checked)
                pnCancellaDoppioni.Enabled = true;
            else
                pnCancellaDoppioni.Enabled = false;
        }

        private void trbLivelloSoglia_Scroll(object sender, EventArgs e)
        {
            ApplicaLivelloSoglia();
            AggiornaEtichettaSoglia();
        }

        private void ApplicaLivelloSoglia()
        {
            switch (trbLivelloSoglia.Value)
            {
                case 1:
                    trackSensibilita = 5;
                    break;
                case 2:
                    trackSensibilita = 12;
                    break;
                case 3:
                    trackSensibilita = 17;
                    break;
                default:
                    trackSensibilita = 5;
                    trbLivelloSoglia.Value = 1;
                    break;
            }
        }

        private void AggiornaEtichettaSoglia()
        {
            label8.Text = $"Livello tolleranza doppioni:";
        }

        private void CaricaConfigurazioneUtente()
        {
            var impostazioni = Photosporter.Properties.Settings.Default;

            if (!string.IsNullOrWhiteSpace(impostazioni.CartellaDestinazione) && System.IO.Directory.Exists(impostazioni.CartellaDestinazione))
            {
                cartellaDestinazioneBase = impostazioni.CartellaDestinazione;
                if (impostazioni.CreaNuovaCartella && !string.IsNullOrWhiteSpace(impostazioni.NomeNuovaCartella))
                    cartellaDestinazioneBase = RiparaCartellaBaseSalvata(cartellaDestinazioneBase, impostazioni.NomeNuovaCartella);

                FolderManager.DestinationFolder = cartellaDestinazioneBase;
                tbIndirizzo.Text = cartellaDestinazioneBase;
                ckNewDirectory.Enabled = true;
                btSmorta.Enabled = true;
            }

            tbNewDirectory.Text = impostazioni.NomeNuovaCartella;
            ckNewDirectory.Checked = impostazioni.CreaNuovaCartella && !string.IsNullOrWhiteSpace(cartellaDestinazioneBase);

            rdData.Checked = impostazioni.OrdinaPerData;
            rdGiorno.Checked = impostazioni.OrdinaPerGiorno;
            rdDataEur.Checked = impostazioni.DataEuropea;
            rbDataAm.Checked = impostazioni.DataAmericana;
            nudGiorni.Value = Math.Max(nudGiorni.Minimum, Math.Min(nudGiorni.Maximum, impostazioni.GiornoIniziale));

            ckCancellaCartella.Checked = impostazioni.CancellaOrigine;
            ckCartelleDoppie.Checked = impostazioni.CartelleDoppie;
            cbCancellaDoppioni.Checked = impostazioni.CancellaDoppioni;
            pnCancellaDoppioni.Enabled = cbCancellaDoppioni.Checked;

            trbLivelloSoglia.Value = Math.Max(trbLivelloSoglia.Minimum, Math.Min(trbLivelloSoglia.Maximum, impostazioni.LivelloSoglia));
            ApplicaLivelloSoglia();
        }

        private string RiparaCartellaBaseSalvata(string cartellaSalvata, string nomeNuovaCartella)
        {
            string nomeUltimaCartella = Path.GetFileName(cartellaSalvata.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
            if (!string.Equals(nomeUltimaCartella, nomeNuovaCartella, StringComparison.OrdinalIgnoreCase))
                return cartellaSalvata;

            DirectoryInfo parent = System.IO.Directory.GetParent(cartellaSalvata);
            return parent == null ? cartellaSalvata : parent.FullName;
        }

        private void SalvaConfigurazioneUtente()
        {
            var impostazioni = Photosporter.Properties.Settings.Default;

            impostazioni.CartellaDestinazione = cartellaDestinazioneBase ?? string.Empty;
            impostazioni.CreaNuovaCartella = ckNewDirectory.Checked;
            impostazioni.NomeNuovaCartella = tbNewDirectory.Text.Trim();
            impostazioni.OrdinaPerData = rdData.Checked;
            impostazioni.OrdinaPerGiorno = rdGiorno.Checked;
            impostazioni.DataEuropea = rdDataEur.Checked;
            impostazioni.DataAmericana = rbDataAm.Checked;
            impostazioni.GiornoIniziale = (int)nudGiorni.Value;
            impostazioni.CancellaOrigine = ckCancellaCartella.Checked;
            impostazioni.CartelleDoppie = ckCartelleDoppie.Checked;
            impostazioni.CancellaDoppioni = cbCancellaDoppioni.Checked;
            impostazioni.LivelloSoglia = trbLivelloSoglia.Value;
            impostazioni.Save();
        }

        private void FrmHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            SalvaConfigurazioneUtente();
        }

        private string EseguiPuliziaDoppioni(string cartella, int soglia)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string exePath = Path.Combine(baseDirectory, "FiltraFoto.exe");
            string scriptPath = Path.Combine(baseDirectory, "FiltraFoto.py");

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.WorkingDirectory = baseDirectory;

            string argomentiPulizia = $"--input {ProteggiArgomento(cartella)} --soglia {soglia} --cancella";
            if (File.Exists(exePath))
            {
                startInfo.FileName = exePath;
                startInfo.Arguments = argomentiPulizia;
            }
            else if (File.Exists(scriptPath))
            {
                startInfo.FileName = "python";
                startInfo.Arguments = $"{ProteggiArgomento(scriptPath)} {argomentiPulizia}";
            }
            else
            {
                throw new FileNotFoundException("Non ho trovato FiltraFoto.exe o FiltraFoto.py nella cartella dell'applicazione.");
            }

            using (Process processo = Process.Start(startInfo))
            {
                string output = processo.StandardOutput.ReadToEnd();
                string error = processo.StandardError.ReadToEnd();
                processo.WaitForExit();

                if (processo.ExitCode != 0)
                    throw new Exception(string.IsNullOrWhiteSpace(error) ? output : error);

                return output;
            }
        }

        private string ProteggiArgomento(string valore)
        {
            return "\"" + valore.Replace("\"", "\\\"") + "\"";
        }
    }
}
