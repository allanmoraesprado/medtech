using Infraestructure.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Infraestructure.StatusSistema
{
    public class MensagemUnidade
    {
        public RetornoCodigo Codigo;
        public RetornoStatus Status;
        public string Mensagem;

        [XmlIgnore]
        public object Retorno { get; set; }

        public MensagemUnidade() { }

        public MensagemUnidade(RetornoCodigo Codigo, RetornoStatus Status, object mensagem = null)
        {
            this.Codigo = Codigo;
            this.Status = Status;
            this.Mensagem = string.Format("{0} {1}", Codigo.ToDescription(), mensagem == null ? "" : mensagem.ToString());
        }

        public MensagemUnidade(RetornoCodigo Codigo, RetornoStatus Status, string Mensagem)
        {
            this.Codigo = Codigo;
            this.Status = Status;
            this.Mensagem = Mensagem;
        }

        public MensagemUnidade(RetornoCodigo Codigo, RetornoStatus Status, string Mensagem, object Retorno)
        {
            this.Codigo = Codigo;
            this.Status = Status;
            this.Mensagem = Mensagem;
            this.Retorno = Retorno;
        }
    }
}
