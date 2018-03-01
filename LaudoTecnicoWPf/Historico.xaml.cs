using LaudoTecnicoWPF.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LaudoTecnicoWPF
{
    /// <summary>
    /// Interaction logic for Historico.xaml
    /// </summary>
    public partial class Historico : Window
    {
        public SQLiteConnection conexao;
        public DataTable dt;
        public Historico()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            
            database dbModel = new database();

            dbModel.Construir();
            this.conexao = dbModel.conn;
            conexao.Open();


            string txtSqlQuery = "SELECT * FROM Laudos ";

          

            SQLiteCommand insertSQL = new SQLiteCommand(txtSqlQuery, conexao);

            insertSQL.ExecuteNonQuery();

            SQLiteDataAdapter dataadp = new SQLiteDataAdapter(insertSQL);

            this.dt = new DataTable("Laudos");
            dataadp.Fill(dt);

            griddados.ItemsSource = dt.DefaultView;
            dataadp.Update(dt);


        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Obtem o registro como um datarowview
            var selectedRow = griddados.SelectedItem as DataRowView;
            string NumeroOcorrencia = (string)selectedRow["NumeroOcorrencia"];
            string DescricaoOcorrencia = (string)selectedRow["DescricaoOcorrencia"];
            string ProjetoProblema = (string)selectedRow["ProjetoProblema"];
            string responsavel = (string)selectedRow["responsavel"];
            string DataDiagnostico = (string)selectedRow["DataDiagnostico"];
            string DataEntrega = (string)selectedRow["DataEntrega"];
            string DataImplantacao = (string)selectedRow["DataImplantacao"];
            string status = (string)selectedRow["status"];
            string AcaoPontual = (string)selectedRow["AcaoPontual"];
            string AcaoLegado = (string)selectedRow["AcaoLegado"];
            string Clientes = (string)selectedRow["Clientes"];
            string DescricaoProblema = (string)selectedRow["DescricaoProblema"];
            string LaudoTecnico = (string)selectedRow["LaudoTecnico"];
            string Solucao = (string)selectedRow["Solucao"];

            ((MainWindow)Application.Current.MainWindow).txtOcorrencia.Text = NumeroOcorrencia;
            ((MainWindow)Application.Current.MainWindow).txtDescricao.Text = DescricaoOcorrencia;
            ((MainWindow)Application.Current.MainWindow).txtProjeto.Text = ProjetoProblema;
            ((MainWindow)Application.Current.MainWindow).txtResponsavel.Text = responsavel;
            ((MainWindow)Application.Current.MainWindow).txtDtDiagnostico.Text = DataDiagnostico;
            ((MainWindow)Application.Current.MainWindow).txtDtEntrega.Text = DataEntrega;
            ((MainWindow)Application.Current.MainWindow).txtDtImplantacao.Text = DataImplantacao;
            ((MainWindow)Application.Current.MainWindow).txtCorretiva.Text = AcaoPontual;
            ((MainWindow)Application.Current.MainWindow).txtAcao.Text = AcaoLegado;
            ((MainWindow)Application.Current.MainWindow).txtClientes.Text = Clientes;
            ((MainWindow)Application.Current.MainWindow).txtProblema.Text = DescricaoProblema;
            ((MainWindow)Application.Current.MainWindow).txtLaudo.Text = LaudoTecnico;
            ((MainWindow)Application.Current.MainWindow).txtSolucao.Text = Solucao;


            this.Close();


        }

        private void click_deletar(object sender, RoutedEventArgs e)
        {

            //Obtem o registro como um datarowview
            var selectedRow = griddados.SelectedItem as DataRowView;
            var id = selectedRow["id_laudo"];

            string txtSqlQuery = "DELETE FROM Laudos WHERE Id_laudo ="+id;

            SQLiteCommand insertSQL = new SQLiteCommand(txtSqlQuery, this.conexao);

            insertSQL.ExecuteNonQuery();


            // Atualiza novamente o grid de datos
            txtSqlQuery = "SELECT * FROM Laudos ";
            insertSQL = new SQLiteCommand(txtSqlQuery, conexao);

            insertSQL.ExecuteNonQuery();

            SQLiteDataAdapter dataadp = new SQLiteDataAdapter(insertSQL);

            this.dt = new DataTable("Laudos");
            dataadp.Fill(dt);

            griddados.ItemsSource = dt.DefaultView;
            dataadp.Update(dt);


        }
      
    }
}
