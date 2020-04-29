using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Persistencia;
using Persistencia.DTO;

namespace SistemaApoioAlocacaoRH
{
    public partial class CadastrarUsuario : System.Web.UI.Page
    {
        private Fachada fachada;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                LimparDados();
                LoadCombos();
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                CadastrarNovoUsuario();
            }

        }

        private void LoadCombos()
        {
            fachada = new Fachada();
            try
            {
                List<DropDownItem> listaTipoUsuario = fachada.LoadTipoUsuario();
                listaTipoUsuario.Insert(0, new DropDownItem(null, ""));
                ddlTipo.DataSource = listaTipoUsuario;
                ddlTipo.DataBind();
            }
            catch (Exception ex)
            {
                Master.OpenErrorModal(ex.Message); 
            }
        }

        private void CadastrarNovoUsuario()
        {
            // se voltar false dispara a popup de warning avisando que já existe esse username.
            fachada = new Fachada();
            try
            {
                string usuNome = txtNomeUsuario.Text;
                string nome = txtNome.Text;
                string sobrenome = txtSobrenome.Text;
                string email = txtEmail.Text;
                string senha = txtSenha.Text;
                int tipo = Convert.ToInt32(ddlTipo.SelectedItem.Value);

                bool cadastrado = fachada.CadastrarUsuario(usuNome, nome, sobrenome, tipo, email, senha);
                
                if (cadastrado)
                {
                    // abre a popup de sucesso. (verdinha)
                    Master.OpenSuccesModal("Usuário cadastrado com Sucesso!");
                    LimparDados();
                }
                else
                {
                    Master.OpenWarningModal("O nome de usuário selecionado não está disponível.");
                }
            }
            catch (Exception ex)
            {
                Master.OpenErrorModal(ex.Message);
            }
        }

        private void LimparDados()
        {
            txtNomeUsuario.Text = String.Empty;
            txtNome.Text = String.Empty;
            txtSobrenome.Text = String.Empty;
            txtEmail.Text = String.Empty;
            ddlTipo.Text = String.Empty;
            txtSenha.Text = String.Empty;
        }

    }
}