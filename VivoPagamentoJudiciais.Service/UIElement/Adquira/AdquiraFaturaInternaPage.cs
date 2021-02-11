using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using VivoPagamentoJudiciais.Service.Core;

namespace VivoPagamentoJudiciais.Service.UIElement.Adquira
{
    public class AdquiraFaturaInternaPage
    {
        public IWebElement fieldTomador => Driver.driver.FindElement(By.Id("gwt-uid-36"));
        public IWebElement fieldSiglaAreaGestora => Driver.driver.FindElement(By.Id("pid-managementBoard"));
        public IWebElement fieldNumeroFaturaInterna => Driver.driver.FindElement(By.Id("pid-invoiceNumber"));
        public IWebElement fieldDataVencimento => Driver.driver.FindElement(By.CssSelector("#pid-expirationDate > div:nth-child(1) > div:nth-child(1) > input:nth-child(1)"));
        public IWebElement fieldNumeroDocumentoSuporte => Driver.driver.FindElement(By.Id("pid-supportDocumentNumber"));
        public IWebElement fieldSequencial => Driver.driver.FindElement(By.Id("pid-sequentialNumber"));
        public IWebElement fieldTipoDocumentoSAP => Driver.driver.FindElement(By.Id("gwt-uid-50"));
        public IWebElement fieldAreaReclamacao => Driver.driver.FindElement(By.Id("gwt-uid-165"));
        public IWebElement fieldTipoNotaFiscalAnaliseTributaria => Driver.driver.FindElement(By.Id("gwt-uid-167"));
        public IWebElement fieldNomeFavorecido => Driver.driver.FindElement(By.Id("pid-beneficiaryName"));
        public IWebElement fieldCNPJCPFFavorecido => Driver.driver.FindElement(By.Id("pid-beneficiaryTaxNumber"));
        public IWebElement fieldCodigoSAPFornecedor => Driver.driver.FindElement(By.Id("pid-supplierSapCode"));
        public IWebElement fieldEmpresa => Driver.driver.FindElement(By.Id("pid-company"));
        public IWebElement fieldComarca => Driver.driver.FindElement(By.Id("pid-state"));
        public IWebElement fieldCodigoOcorrencia => Driver.driver.FindElement(By.Id("pid-occurrenceCode"));
        public IWebElement fieldFormaOcorrencia => Driver.driver.FindElement(By.Id("gwt-uid-183"));
        public IWebElement fieldFormaPagamento => Driver.driver.FindElement(By.Id("gwt-uid-70"));
        public IWebElement fieldTipoPagamento => Driver.driver.FindElement(By.Id("gwt-uid-185"));
        public IWebElement fieldDescricaoPagamento => Driver.driver.FindElement(By.Id("pid-paymentDescription"));

        public IWebElement btnCriarFatura => Driver.driver.FindElement(By.Id("TID_CONTENTPANEL_observer_ObserverInternalInvoicesListPanel_BUTTON_CREATE_INVOICE"));
    }
}
