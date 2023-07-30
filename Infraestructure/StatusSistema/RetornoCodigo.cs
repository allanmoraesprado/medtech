using System.ComponentModel;

namespace Infraestructure.StatusSistema
{
    public enum RetornoCodigo
    {
        [Description("NÃO DEFINIDO.")]
        NAO_DEFINIDO = 0,
        [Description("Este usuário não existe.")]
        USUARIO_INDEFINIDO = 1,
        [Description("Senha Incorreta.")]
        SENHA_INCORRETA = 2,
        [Description("Usuário já cadastrado.")]
        USUARIO_JA_EXISTE = 3,
        [Description("Falha no cadastro.")]
        ERRO_CADASTRO = 4,
        [Description("Cadastro efetuado com sucesso!")]
        SUCESSO_CADASTRO = 5,
        [Description("Dados atualizados com sucesso!")]
        DADOS_ATUALIZADOS = 6,
        [Description("CPF já cadastrado no sistema!")]
        CPF_EXISTENTE = 7,
        [Description("Ficha deletada com sucesso!")]
        FICHA_DELETADA = 8,
        [Description("Ficha atualizada com sucesso!")]
        FICHA_ATUALIZADA = 9,
        [Description("Acesso negado!")]
        ACESSO_NEGADO = 10
    }
}
