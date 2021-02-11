using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using VivoPagamentoJudiciais.Services.Core;

namespace VivoPagamentoJudiciais.Services.UIElement.VivoPagto
{
    public class VivoPagtoFaturaInternaPage
    {

        public IWebElement fieldNomeModelo => Driver.driver.FindElement(By.Id("cbModelos_chosen"));
        public IWebElement fieldAreaReclamacao => Driver.driver.FindElement(By.Id("FiltroAreaReclamacao"));
        public IWebElement fieldTipoPagamento => Driver.driver.FindElement(By.Id("FiltroMeioPagamento"));
        public IWebElement fieldFornecedor => Driver.driver.FindElement(By.Id("FiltroFornecedor"));
        public IWebElement fieldEmpresa => Driver.driver.FindElement(By.Id("FiltroEmpresa"));
        public IWebElement fieldAreaGestora => Driver.driver.FindElement(By.Id("SiglaAreaGestora"));
        public IWebElement fieldSequencial => Driver.driver.FindElement(By.Id("Sequencial"));
        public IWebElement fieldTipoDocumento => Driver.driver.FindElement(By.Id("IdTipoDocumento"));

        public IWebElement fieldAreaReclamacao1 => Driver.driver.FindElement(By.Id("IdAreaReclamacao"));
        public IWebElement fieldNomeEmissor => Driver.driver.FindElement(By.Id("NomeEmissor"));
        public IWebElement fieldTipoDocumentoFavorecido => Driver.driver.FindElement(By.Name("tipoDocumentoFavorecido"));
        public IWebElement fieldCpfCnpjFavorecido => Driver.driver.FindElement(By.Id("CpfCnpjFavorecido"));

        public IWebElement fieldNomeFavorecido => Driver.driver.FindElement(By.Id("NomeFavorecido"));
        public IWebElement fieldCodigoSapFornecedor => Driver.driver.FindElement(By.Id("CodigoSapFornecedor"));
        public IWebElement fieldIdMeioPagamento => Driver.driver.FindElement(By.Id("IdMeioPagamento"));
        public IWebElement fieldMesReferencia => Driver.driver.FindElement(By.Id("MesReferencia"));
        public IWebElement fieldDataVencimento => Driver.driver.FindElement(By.Id("DataVencimento"));
        public IWebElement fieldValor => Driver.driver.FindElement(By.Name("Valor"));

        public IWebElement formUploadFile => Driver.driver.FindElement(By.Id("dropzoneForm"));
        
        public VivoPagtoFaturaInternaPage()
        {

        }
    }
}
