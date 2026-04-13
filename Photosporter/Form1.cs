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
        }
        #region Variabili globali

        ClsPhotoOrganizer PhotoOrganizer = new ClsPhotoOrganizer();
        ClsFolderManager FolderManager = new ClsFolderManager();

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
        private void btSmorta_Click(object sender, EventArgs e)
        {
            try
            {
                FolderManager.DestinationFolder = tbIndirizzo.Text.Trim();
                PhotoOrganizer.FormatoData = GetDateFormat();  // Ottieni il formato della data

                pgSpostamentoFile.Style = ProgressBarStyle.Continuous;
                // Trova unità rimovibili
                var removableDrive = DriveInfo.GetDrives().FirstOrDefault(drive => drive.DriveType == DriveType.Removable && drive.IsReady);
                if (removableDrive == null)
                    throw new Exception("Nessuna unità rimovibile rilevata. Assicurati di aver inserito una chiavetta USB o un disco esterno.");

                // Conferma con l'utente
                DialogResult result = MessageBox.Show($"Unità rilevata: {removableDrive.Name}. Vuoi continuare?", "Conferma", MessageBoxButtons.YesNo);
                if (result == DialogResult.No) return;

                FolderManager.SourceFolder = removableDrive.RootDirectory.FullName; // Imposta la cartella di origine come la radice dell'unità rimovibile
                if (PhotoOrganizer.FormatoData!=string.Empty)
                {
                    PhotoOrganizer.OrganizePhotosByDate(FolderManager.SourceFolder, FolderManager.DestinationFolder, PhotoOrganizer.FormatoData, pgSpostamentoFile);
                }
                else if (rdGiorno.Checked)
                {
                    PhotoOrganizer.FormatoData = "yyyy-MM-dd";
                    int startDay = (int)nudGiorni.Value;
                    PhotoOrganizer.OrganizePhotos(FolderManager.SourceFolder, FolderManager.DestinationFolder, startDay, pgSpostamentoFile);
                }
                else
                    throw new Exception("Errore: nessun formato di ordinamento selezionato");

                if (ckCartelleDoppie.Checked)
                {
                    FolderManager.CreateVersionedFolders(FolderManager.DestinationFolder,PhotoOrganizer.CartelleCreate);
                }

                DialogResult dr = MessageBox.Show("Operazione completata, vuoi aprire la cartella?", "Apri Cartella", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                    Process.Start("explorer.exe", FolderManager.DestinationFolder);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore: {ex.Message}", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private string GetDateFormat()
        {
            if (rdDataEur.Checked) return "dd-MM-yyyy";  // Formato europeo
            if (rbDataAm.Checked) return "MM-dd-yyyy";  // Formato americano
           // MessageBox.Show("Errore: formato data non selezionato", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return string.Empty;
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
                FolderManager.DestinationFolder = fbd.SelectedPath;  // Ottieni il percorso della cartella selezionata
                tbIndirizzo.Text = FolderManager.DestinationFolder;
                ckNewDirectory.Enabled = true;
                btSmorta.Enabled = true;
            }
        }


        private void ckNewDirectory_CheckedChanged(object sender, EventArgs e)
        {
            if (ckNewDirectory.Checked)
            {
                pnNewDirectory.Visible = true;
                tbIndirizzo.Text += @"\";
            }
            else
            {
                pnNewDirectory.Visible = false;
                tbNewDirectory.Text = null;
                tbIndirizzo.Text = FolderManager.DestinationFolder;
            }
        }

        private void tbNewDirectory_TextChanged(object sender, EventArgs e)
        {
            tbIndirizzo.Text = FolderManager.DestinationFolder + @"\"+ tbNewDirectory.Text;
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
    }
}
