
using AutoItX3Lib;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using VivoPagamentoJudiciais.Service.UIElement;
using VivoPagamentoJudiciais.Service.UIElement.Adquira;

namespace VivoPagamentoJudiciais.Service.Core
{
    public static class ActionsPage
    {
        public static void InitializeDriver()
        {
            if (Driver.driver == null)
            {
                Driver.driver = new FirefoxDriver();
                Driver.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            }
        }
        public static void FillAdquiraLoginForm(string username, string password)
        {
            AdquiraPage objAdquira = new AdquiraPage();
            objAdquira.fieldUserName.SendKeys(username);
            objAdquira.fieldPassword.SendKeys(password);
            objAdquira.btnLogin.Click();

        }

        public static void FillAdquiraFaturaInternaForm()
        {
            AdquiraFaturaInternaPage objAdquiraFaturaInternaPage = new AdquiraFaturaInternaPage();
            
            Thread.Sleep(3000);
            if (objAdquiraFaturaInternaPage.fieldTomador.Displayed)
                objAdquiraFaturaInternaPage.fieldTomador.SendKeys(Config.AdquiraPage.FaturaInternaForm.Tomador);

            Thread.Sleep(1000);
            objAdquiraFaturaInternaPage.fieldSiglaAreaGestora.SendKeys(Config.AdquiraPage.FaturaInternaForm.SiglaAreaGestora);
            Thread.Sleep(1000);
            objAdquiraFaturaInternaPage.fieldNumeroFaturaInterna.SendKeys(Config.AdquiraPage.FaturaInternaForm.NumeroFaturaInterna);
            Thread.Sleep(1000);
            objAdquiraFaturaInternaPage.fieldDataVencimento.SendKeys(Config.AdquiraPage.FaturaInternaForm.DataVencimento);
            Thread.Sleep(1000);
            objAdquiraFaturaInternaPage.fieldNumeroDocumentoSuporte.SendKeys(Config.AdquiraPage.FaturaInternaForm.NumeroDocumentoSuporte);
            objAdquiraFaturaInternaPage.fieldSequencial.SendKeys(Config.AdquiraPage.FaturaInternaForm.NumeroSequencial);
            Thread.Sleep(1000);
            if (objAdquiraFaturaInternaPage.fieldTipoDocumentoSAP.Displayed)
                objAdquiraFaturaInternaPage.fieldTipoDocumentoSAP.SendKeys(Config.AdquiraPage.FaturaInternaForm.TipoDocumentoSAP);
            Thread.Sleep(1000);
            objAdquiraFaturaInternaPage.fieldAreaReclamacao.SendKeys(Config.AdquiraPage.FaturaInternaForm.AreaReclamacao);
            Thread.Sleep(1000);
            objAdquiraFaturaInternaPage.fieldTipoNotaFiscalAnaliseTributaria.SendKeys(Config.AdquiraPage.FaturaInternaForm.TipoNotaFiscalAnaliseTributaria);
            Thread.Sleep(1000);
            objAdquiraFaturaInternaPage.fieldNomeFavorecido.SendKeys(Config.AdquiraPage.FaturaInternaForm.NomeFavorecido);
            Thread.Sleep(1000);
            objAdquiraFaturaInternaPage.fieldCNPJCPFFavorecido.SendKeys(Config.AdquiraPage.FaturaInternaForm.CPFCNPJFavorecido);
            Thread.Sleep(1000);
            objAdquiraFaturaInternaPage.fieldCodigoSAPFornecedor.SendKeys(Config.AdquiraPage.FaturaInternaForm.CodigoSAPFornecedor);
            Thread.Sleep(1000);
            objAdquiraFaturaInternaPage.fieldEmpresa.SendKeys(Config.AdquiraPage.FaturaInternaForm.Empresa);
            Thread.Sleep(1000);
            objAdquiraFaturaInternaPage.fieldComarca.SendKeys(Config.AdquiraPage.FaturaInternaForm.Comarca);
            objAdquiraFaturaInternaPage.fieldCodigoOcorrencia.SendKeys(Config.AdquiraPage.FaturaInternaForm.CodigoOcorrencia);

            Thread.Sleep(1000);
            if (objAdquiraFaturaInternaPage.fieldFormaPagamento.Displayed)
                objAdquiraFaturaInternaPage.fieldTipoDocumentoSAP.SendKeys(Config.AdquiraPage.FaturaInternaForm.FormaPagamento);
            objAdquiraFaturaInternaPage.fieldTipoPagamento.SendKeys(Config.AdquiraPage.FaturaInternaForm.TipoPagamento);
            objAdquiraFaturaInternaPage.fieldDescricaoPagamento.SendKeys(Config.AdquiraPage.FaturaInternaForm.DescricaoPagamento);


        }
        public static void FillVivoPagamentoLoginForm(string username, string password)
        {
            VivoPagtoPage objVivoPagto = new VivoPagtoPage();
            objVivoPagto.fieldUserName.SendKeys(username);
            objVivoPagto.fieldPassword.SendKeys(password);
            objVivoPagto.btnLogin.Click();
        }
        public static void FillVivoPagamentoFaturaInternaForm()
        {
            VivoPagtoFaturaInternaPage objVivoPagto = new VivoPagtoFaturaInternaPage();

            if (objVivoPagto.fieldNomeModelo.Displayed)
                objVivoPagto.fieldNomeModelo.Click();

            var lstNomeModelo = objVivoPagto.fieldNomeModelo.FindElements(By.TagName("li"));
            foreach (IWebElement element in lstNomeModelo)
            {
                string elementText = element.Text.Trim();
                if(elementText == Config.VivoPagtoPage.FaturaInternaForm.NomeModelo.Trim())
                {
                    element.Click();
                    break;
                }
            }

            objVivoPagto.formUploadFile.SendKeys(@"C:\Users\andre\Documents\SoldPagamento.pdf");

            AutoItX3 autoIdObj = new AutoItX3();
            
            Thread.Sleep(3000);

            autoIdObj.WinActivate("Enviar arquivo(s)", "");

            string title = autoIdObj.WinGetTitle("[active]");

            autoIdObj.WinActivate("Enviar arquivo(s)");
            autoIdObj.ControlFocus("Enviar arquivo(s)", "", "ComboBox");
            autoIdObj.ControlSetText("Enviar arquivo(s)", "", "ComboBox", "Nome Arquivo");

            objVivoPagto.fieldAreaReclamacao.SendKeys(Config.VivoPagtoPage.FaturaInternaForm.AreaReclamacao);

            Thread.Sleep(5000);
        }
        public static void FillAdquiraForm()
        {

        }

        public static void TearsDownDrive()
        {
            Driver.driver.Quit();
        }
    }
}
