using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CRUD_agenda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MySqlConnection sqlCon = null;
        private string strCon = @"server=127.0.0.1; port=3306; userid=root; password=; database=bdagenda; SslMode = none";
        private string strSql = string.Empty;

        private void Form1_Load(object sender, EventArgs e)
        {
            //TELA

            listaGrid();

            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnAlterar.Enabled = false;
            btnLimpar.Enabled = false;
            btnExcluir.Enabled = false;
            btnBuscar.Enabled = true;
            btnAtualizar.Enabled = true;

            txtId.Enabled = false;
            txtNome.Enabled = true;
            mskTelefone.Enabled = false;
            txtEmail.Enabled = false;
            mskData.Enabled = false;
            txtEndereco.Enabled = false;
            rxtNota.Enabled = false;

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            // botão sair 

            Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // botão de buscar

            strSql = "select * from pessoas where nome=@Nome";
            sqlCon = new MySqlConnection(strCon);
            MySqlCommand comando = new MySqlCommand(strSql, sqlCon);

            comando.Parameters.AddWithValue("@Nome", txtNome.Text);

            try
            {
                if (txtNome.Text == string.Empty)
                {
                    throw new Exception("Você precisa digitar um Nome!");
                }
                sqlCon.Open();

                MySqlDataReader dr = comando.ExecuteReader();

                if (dr.HasRows == false)
                {
                    throw new Exception("Nome não cadastrado!");
                }
                dr.Read();

                txtId.Text = Convert.ToString(dr["id"]);
                txtNome.Text = Convert.ToString(dr["nome"]);
                mskTelefone.Text = Convert.ToString(dr["telefone"]);
                txtEmail.Text = Convert.ToString(dr["email"]);
                txtEndereco.Text = Convert.ToString(dr["Endereco"]);
                rxtNota.Text = Convert.ToString(dr["nota"]);
                mskData.Text = Convert.ToString(dr["data"]);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                sqlCon.Close();
            }

            btnNovo.Enabled = false;
            btnSalvar.Enabled = false;
            btnAlterar.Enabled = true;
            btnLimpar.Enabled = true;
            btnExcluir.Enabled = true;

            txtId.Enabled = false;
            txtNome.Enabled = true;
            mskTelefone.Enabled = true;
            txtEmail.Enabled = true;
            mskData.Enabled = true;
            txtEndereco.Enabled = true;
            rxtNota.Enabled = true;


            txtNome.Focus();

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            // botão novo

            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            btnAlterar.Enabled = false;
            btnLimpar.Enabled = true;
            btnExcluir.Enabled = false;

            txtId.Enabled = true;
            txtNome.Enabled = true;
            mskTelefone.Enabled = true;
            txtEmail.Enabled = true;
            mskData.Enabled = true;
            txtEndereco.Enabled = true;
            rxtNota.Enabled = true;

        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            // botão limpar

            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnAlterar.Enabled = false;
            btnLimpar.Enabled = false;
            btnExcluir.Enabled = false;
            btnBuscar.Enabled = true;

            txtId.Enabled = false;
            txtNome.Enabled = true;
            mskTelefone.Enabled = false;
            txtEmail.Enabled = false;
            mskData.Enabled = false;
            txtEndereco.Enabled = false;
            rxtNota.Enabled = false;

            txtId.Text = "";
            txtNome.Text = "";
            txtEndereco.Text = "";
            mskTelefone.Text = "";
            txtEmail.Text = "";
            mskData.Text = "";
            rxtNota.Text = "";

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            // botão alterar editar

            strSql = "update pessoas set id=@Id, nome=@Nome, telefone=@Telefone, email=@Email, data=@Data, endereco=@Endereco, nota=@Nota where id=@IdBuscar";
            sqlCon = new MySqlConnection(strCon);
            MySqlCommand comando = new MySqlCommand(strSql, sqlCon);

            comando.Parameters.AddWithValue("@IdBuscar", txtId.Text);

            comando.Parameters.AddWithValue("@Id", txtId.Text);
            comando.Parameters.AddWithValue("@Nome", txtNome.Text);
            comando.Parameters.AddWithValue("@Telefone", mskTelefone.Text);
            comando.Parameters.AddWithValue("@Email", txtEmail.Text);
            comando.Parameters.AddWithValue("@Data", mskData.Text);
            comando.Parameters.AddWithValue("@Endereco", txtEndereco.Text);
            comando.Parameters.AddWithValue("@Nota", rxtNota.Text);

            try
            {
                sqlCon.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Contato atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }

            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnAlterar.Enabled = false;
            btnLimpar.Enabled = false;
            btnExcluir.Enabled = false;
            btnBuscar.Enabled = true;
            btnAtualizar.Enabled = true;

            txtId.Enabled = false;
            txtNome.Enabled = true;
            mskTelefone.Enabled = false;
            txtEmail.Enabled = false;
            mskData.Enabled = false;
            txtEndereco.Enabled = false;
            rxtNota.Enabled = false;

            txtId.Text = "";
            txtNome.Text = "";
            txtEndereco.Text = "";
            mskTelefone.Text = "";
            txtEmail.Text = "";
            mskData.Text = "";
            rxtNota.Text = "";

        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            // botão atualizar
            listaGrid();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // botão salvar

            strSql = "Insert into pessoas (id, nome, telefone, email, data, endereco, nota) values (@Id, @Nome, @Telefone, @Email, @Data, @Endereco, @Nota)";
            sqlCon = new MySqlConnection(strCon);
            MySqlCommand comando = new MySqlCommand(strSql, sqlCon);


            comando.Parameters.AddWithValue("@Id", txtId.Text);
            comando.Parameters.AddWithValue("@Nome", txtNome.Text);
            comando.Parameters.AddWithValue("@Telefone", mskTelefone.Text);
            comando.Parameters.AddWithValue("@Email", txtEmail.Text);
            comando.Parameters.AddWithValue("@Data", mskData.Text);
            comando.Parameters.AddWithValue("@Endereco", txtEndereco.Text);
            comando.Parameters.AddWithValue("@Nota", rxtNota.Text);

            try
            {
                sqlCon.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Cadastro realizado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            // botão excluir

            if (MessageBox.Show("Deseja realmente excluir este contato?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                MessageBox.Show("Operação cancelada!");
            }
            else
            {
                strSql = "delete from pessoas where id=@Id";
                sqlCon = new MySqlConnection(strCon);
                MySqlCommand comando = new MySqlCommand(strSql, sqlCon);

                comando.Parameters.AddWithValue("@Id", txtId.Text);

                try
                {
                    sqlCon.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Contato deletado com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlCon.Close();
                }

                btnNovo.Enabled = true;
                btnSalvar.Enabled = false;
                btnAlterar.Enabled = false;
                btnLimpar.Enabled = false;
                btnExcluir.Enabled = false;
                btnBuscar.Enabled = true;
                btnAtualizar.Enabled = true;

                txtId.Enabled = false;
                txtNome.Enabled = true;
                mskTelefone.Enabled = false;
                txtEmail.Enabled = false;
                mskData.Enabled = false;
                txtEndereco.Enabled = false;
                rxtNota.Enabled = false;

                txtId.Text = "";
                txtNome.Text = "";
                txtEndereco.Text = "";
                mskTelefone.Text = "";
                txtEmail.Text = "";
                mskData.Text = "";
                rxtNota.Text = "";
            }
        }

        
        public void listaGrid ()
        {
            //data grid

            strSql = "select * from pessoas";

            sqlCon = new MySqlConnection(strCon);
            MySqlCommand comando = new MySqlCommand(strSql, sqlCon);
            

            try
            {
       
                MySqlDataAdapter objAdp = new MySqlDataAdapter(comando);

                DataTable dtLista = new DataTable();
                
                objAdp.Fill(dtLista);

                dataGrid.DataSource = dtLista;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

    }
}
