﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRUD_Basico.Classes;

namespace CRUD_Basico
{
    public partial class FormPrincipal : Form
    {
        private List<Aluno> _alunos;
        public FormPrincipal()
        {
            InitializeComponent();
            _alunos = new Aluno().ObterAlunos();
        }

        private void ConfiguraDgvAluno()
        {
            DgvAlunos.Columns.Add("Id", "Código");
            DgvAlunos.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DgvAlunos.Columns["Id"].Visible = false;

            DgvAlunos.Columns.Add("Nome", "Nome do Aluno");
            DgvAlunos.Columns["Nome"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DgvAlunos.Columns.Add("DtNasc", "Data de Nascimento");
            DgvAlunos.Columns["DtNasc"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void CarregaDgvAluno()
        {
            DgvAlunos.Rows.Clear();
            foreach (Aluno aluno in _alunos)
            {
                DgvAlunos.Rows.Add(aluno.Id, aluno.Nome, aluno.DtNascimento.ToString("dd/MM/yyyy");
            }

        }

        private void BtnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                Aluno novoAluno = new Aluno(TxbNome.Text, DtpDtNascimento.Value, CkbAtivo.Checked);

                novoAluno.Cadastrar();
                MessageBox.Show($"Aluno cadastrado com sucesso:\n {novoAluno.Nome}\nId inserido pelo banco: {novoAluno.Id}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                //List<Aluno> alunos = new Aluno().ObterAlunos();
                //DgvAlunos.DataSource = alunos;

                ConfiguraDgvAluno();
                CarregaDgvAluno();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        private void DgvAlunos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            Aluno alunoselecionado = _alunos.Find(a => a.Id == (int)DgvAlunos.Rows[e.RowIndex].Cells["Id"].Value);

            TxbNome.Text = alunoselecionado.Nome;
            DtpDtNascimento.Value = alunoselecionado.DtNascimento;
            CkbAtivo.Checked = alunoselecionado.Ativo;

            ConfiguraBotoesECampos(3);
            
        }

        private void ConfiguraBotoesECampos(int estilo)
        {
            if (estilo == 1)
            {
                TsbNovo.Enabled = true;
                TsbSalvar.Enabled = false;
                TsbEditar.Enabled = false;
                TsbExcluir.Enabled = false;

                TxbNome.Clear();
                TxbNome.Enabled = true;
                CkbAtivo.Enabled = true;
                CkbAtivo.Checked = true;
                DtpDtNascimento.Enabled = true;
            }
            else if (estilo == 2)
            {
                TsbNovo.Enabled = true;
                TsbSalvar.Enabled = true;
                TsbEditar.Enabled = false;
                TsbExcluir.Enabled = false;
            }
            else if (estilo == 3)
            {
                TsbNovo.Enabled = true;
                TsbSalvar.Enabled = false;
                TsbEditar.Enabled = true;
                TsbExcluir.Enabled = false;

                TxbNome.Enabled = false;
                DtpDtNascimento.Enabled = false;
                CkbAtivo.Enabled = false;
            }
            else if (estilo == 4)
            {
                TsbNovo.Enabled = true;
                TsbSalvar.Enabled = true;
                TsbEditar.Enabled = false;
                TsbExcluir.Enabled = true;

                TxbNome.Enabled = true;
                DtpDtNascimento.Enabled = true;
                CkbAtivo.Enabled = true;
            }
        }

        private void TsbNovo_Click(object sender, EventArgs e)
        {
            ConfiguraBotoesECampos(1);
        }

        private void TxbNome_KeyDown(object sender, KeyEventArgs e)
        {
            if (TxbNome.Text.Trim().Length >= 3)
            {
                ConfiguraBotoesECampos(2);
            }
        }

        private void TsbEditar_Click(object sender, EventArgs e)
        {
            ConfiguraBotoesECampos(4);
        }
    }
}
