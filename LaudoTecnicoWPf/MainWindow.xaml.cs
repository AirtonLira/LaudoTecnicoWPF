using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;



using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using LaudoTecnicoWPF.Model;
using System.Data.SQLite;

namespace LaudoTecnicoWPF
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            txtDiretorio.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        }

        public string FileName
        {

            get { return txtDiretorio.Text; }

            set { txtDiretorio.Text = value; }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            database dbModel = new database();

            dbModel.Construir();
            SQLiteConnection conexao = dbModel.conn;
            conexao.Open();

            ImageCodecInfo myImageCodecInfo;
            Encoder myEncoder;
            EncoderParameters myEncoderParameters;
            EncoderParameter myEncoderParameter;

            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            path = path.Substring(6);
            path += @"\modelo.jpg";

            Bitmap image1 = new Bitmap(path);
            Graphics graphic = null;
            graphic = Graphics.FromImage(image1);
            myImageCodecInfo = GetEncoderInfo("image/jpeg");

            myEncoderParameters = new EncoderParameters(1);
            myEncoder = Encoder.Quality;

 
            string sChamado = txtOcorrencia.Text;
            string sDescricao = txtDescricao.Text;
            string sProjeto = string.Format(txtProjeto.Text, Environment.NewLine);

            string sResponsavel = txtResponsavel.Text;
            string sDataDiagnostico = txtDtDiagnostico.Text;
            string sDataEntrega = txtDtEntrega.Text;
            string sDataImplantacao = txtDtImplantacao.Text;
            string sStatus = txtStatus.Text;

            string sAcaoCorrentivaPontual = txtCorretiva.Text;
            string sAcaoCorrentivaLegado = txtAcao.Text;
            string sClienteImpacto = txtClientes.Text;

            string sDescricaoProblema = txtProblema.Text;
            string sLaudoTecnico = txtLaudo.Text;
            string sSolucao = txtSolucao.Text;

            FontFamily family = new FontFamily("Arial");

            Font myFont = new Font(family, 9);
            Font myDescricao = new Font(family, 25);


            graphic.DrawString(sChamado, myFont, Brushes.Black, 25, 130); // numero do chamado
            graphic.DrawString(sDescricao, myDescricao, Brushes.Black, 390, 20); // Descricao do chamado
            graphic.DrawString(sProjeto, myFont, Brushes.Black, 300, 130); // Projeto que causou o problema
            graphic.DrawString(sResponsavel, myFont, Brushes.Black, 560, 130); // Responsavel
            graphic.DrawString(sDataDiagnostico, myFont, Brushes.Black, 730, 130); // Data Diagnostico
            graphic.DrawString(sDataEntrega, myFont, Brushes.Black, 870, 130); // Data entrega
            graphic.DrawString(sDataImplantacao, myFont, Brushes.Black, 1000, 130); // Data Implantação
            graphic.DrawString(sStatus, myFont, Brushes.Black, 1117, 130); // Data Implantação

            graphic.DrawString(sAcaoCorrentivaPontual, myFont, Brushes.Black, 560, 250); // Ação Corretiva pontual
            graphic.DrawString(sAcaoCorrentivaLegado, myFont, Brushes.Black, 560, 320); // Ação corretiva legado
            graphic.DrawString(sClienteImpacto, myFont, Brushes.Black, 560, 370); // Clientes impactados

            graphic.DrawString(sDescricaoProblema, myFont, Brushes.Black, 25, 530); // Descrição do problema
            graphic.DrawString(sLaudoTecnico, myFont, Brushes.Black, 440, 520); // Laudo Tecnico 
            graphic.DrawString(sSolucao, myFont, Brushes.Black, 855, 520); // Solução 

            myEncoderParameter = new EncoderParameter(myEncoder, 75L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            string sResultModelo = sChamado.Replace(@"\", "-");
            sResultModelo += " " + sDescricao + ".jpg";

            string diretorio = System.IO.Path.Combine(txtDiretorio.Text, sResultModelo);

            image1.Save(diretorio, myImageCodecInfo, myEncoderParameters);

            System.Diagnostics.Process.Start(diretorio);

            string txtSqlQuery = "INSERT INTO Laudos (NumeroOcorrencia, DescricaoOcorrencia, " +
                "ProjetoProblema, responsavel, DataDiagnostico, DataEntrega, DataImplantacao, status,AcaoPontual,AcaoLegado," +
                "Clientes,DescricaoProblema,LaudoTecnico,Solucao) ";

            txtSqlQuery += string.Format(" VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')",
                    sChamado, sDescricao, sProjeto, sResponsavel, sDataDiagnostico, sDataEntrega, sDataImplantacao, sStatus, sAcaoCorrentivaPontual,
                    sAcaoCorrentivaLegado, sClienteImpacto, sDescricaoProblema, sLaudoTecnico, sSolucao);


            SQLiteCommand insertSQL = new SQLiteCommand(txtSqlQuery, conexao);
            insertSQL.ExecuteNonQuery();

            conexao.Close();

        }

        public event EventHandler FileNameChanged;

        private void BrowseTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            e.Handled = true;
            if (FileNameChanged != null)
                FileNameChanged(this, EventArgs.Empty);
        }


        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        // Função para tratamento da quebra de linha com o limite de caracteres - Projeto
        private void txtProjeto_KeyDown(object sender, KeyEventArgs e)
        {
            int CurrentLine = txtProjeto.GetLineIndexFromCharacterIndex(txtProjeto.Text.Length);
            int textLength = txtProjeto.GetLineLength(CurrentLine);
            if (textLength >= 23)
            {
                txtProjeto.Text += "\n";
                txtProjeto.SelectionStart = txtProjeto.Text.Length ;
            }
        }
        // Função para tratamento da quebra de linha com o limite de caracteres - Problema
        private void txtProblema_KeyDown(object sender, KeyEventArgs e)
        {
            int CurrentLine = txtProblema.GetLineIndexFromCharacterIndex(txtProblema.Text.Length);
            int textLength = txtProblema.GetLineLength(CurrentLine);
            if (textLength >= 58)
            {
                txtProblema.Text += "\n";
                txtProblema.SelectionStart = txtProblema.Text.Length;
            }
        }

        // Função para tratamento da quebra de linha com o limite de caracteres - Laudo tecnico
        private void txtLaudo_KeyDown(object sender, KeyEventArgs e)
        {
            int CurrentLine = txtLaudo.GetLineIndexFromCharacterIndex(txtLaudo.Text.Length);
            int textLength = txtLaudo.GetLineLength(CurrentLine);
            if (textLength >= 58)
            {
                txtLaudo.Text += "\n";
                txtLaudo.SelectionStart = txtLaudo.Text.Length;
            }
        }

        private void txtSolucao_KeyDown(object sender, KeyEventArgs e)
        {
            int CurrentLine = txtSolucao.GetLineIndexFromCharacterIndex(txtSolucao.Text.Length);
            int textLength = txtSolucao.GetLineLength(CurrentLine);
            if (textLength >= 58)
            {
                txtSolucao.Text += "\n";
                txtSolucao.SelectionStart = txtSolucao.Text.Length;
            }
        }

        private void Button_historicoClick(object sender, RoutedEventArgs e)
        {
            var newW = new Historico();
            newW.Show(); // works
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
                this.FileName = dialog.SelectedPath;
        }

        private void btnLimpar(object sender, RoutedEventArgs e)
        {
            txtOcorrencia.Text = "";
            txtDescricao.Text = "";
            txtProjeto.Text = "";
            txtResponsavel.Text = "";
            txtDtDiagnostico.Text = "";
            txtDtEntrega.Text = "";
            txtDtImplantacao.Text = "";
            txtCorretiva.Text = "";
            txtAcao.Text = "";
            txtClientes.Text = "";
            txtProblema.Text = "";
            txtLaudo.Text = "";
            txtSolucao.Text = "";
        }

        private void DockPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            var newW = new GerarCaminho();
            newW.Show(); // works
        }
    }
}
