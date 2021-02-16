using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using VivoPagamentoJudiciais.Services.Core;

namespace VivoPagamentoJudiciais.Services.UIElement.Adquira
{
    public class AdquiraFaturaInternaPage
    {
        public IWebElement fieldTomador => Driver.driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div[3]/div[1]/div[1]/div[2]/div/div/div[3]/div/div/div[2]/input"));
        public IWebElement fieldSiglaAreaGestora => Driver.driver.FindElement(By.Id("pid-managementBoard"));
        public IWebElement fieldNumeroFaturaInterna => Driver.driver.FindElement(By.Id("pid-invoiceNumber"));
        public IWebElement fieldDataVencimento => Driver.driver.FindElement(By.CssSelector("#pid-expirationDate > div:nth-child(1) > div:nth-child(1) > input:nth-child(1)"));
        public IWebElement fieldNumeroDocumentoSuporte => Driver.driver.FindElement(By.Id("pid-supportDocumentNumber"));
        public IWebElement fieldSequencial => Driver.driver.FindElement(By.Id("pid-sequentialNumber"));
        public IWebElement fieldFormaPagamento => Driver.driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div[3]/div[1]/div[4]/div[2]/div/div/div[3]/div[1]/div[1]/div[2]/input"));
        public IWebElement fieldTipoDocumentoSAP => Driver.driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div[3]/div[1]/div[2]/div[2]/div/div/div[3]/div[4]/div[1]/div[2]/input"));
        public IWebElement fieldAreaReclamacao => Driver.driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div[3]/div[1]/div[2]/div[2]/div/div/div[3]/div[4]/div[2]/div[2]/input"));
        public IWebElement fieldTipoNotaFiscalAnaliseTributaria => Driver.driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div[3]/div[1]/div[2]/div[2]/div/div/div[3]/div[5]/div[1]/div[2]/input"));
        public IWebElement fieldNomeFavorecido => Driver.driver.FindElement(By.Id("pid-beneficiaryName"));
        public IWebElement fieldCNPJCPFFavorecido => Driver.driver.FindElement(By.Id("pid-beneficiaryTaxNumber"));
        public IWebElement fieldCodigoSAPFornecedor => Driver.driver.FindElement(By.Id("pid-supplierSapCode"));
        public IWebElement fieldEmpresa => Driver.driver.FindElement(By.Id("pid-company"));
        public IWebElement fieldComarca => Driver.driver.FindElement(By.Id("pid-state"));
        public IWebElement fieldCodigoOcorrencia => Driver.driver.FindElement(By.Id("pid-occurrenceCode"));
        public IWebElement fieldFormaOcorrencia => Driver.driver.FindElement(By.Id("gwt-uid-183"));
        public IWebElement fieldTipoPagamento => Driver.driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div[3]/div[1]/div[4]/div[2]/div/div/div[3]/div[1]/div[2]/div[3]/input"));
        public IWebElement fieldDescricaoPagamento => Driver.driver.FindElement(By.Id("pid-paymentDescription"));
                                                                                                
        public IWebElement btnCriarFatura => Driver.driver.FindElement(By.Id("TID_CONTENTPANEL_observer_ObserverInternalInvoicesListPanel_BUTTON_CREATE_INVOICE"));
    }
}
