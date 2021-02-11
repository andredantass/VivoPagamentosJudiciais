
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
using VivoPagamentoJudiciais.Services.Selenium;
using VivoPagamentoJudiciais.Services.UIElement;
using VivoPagamentoJudiciais.Services.UIElement.Adquira;
using VivoPagamentoJudiciais.Services.UIElement.VivoPagto;


namespace VivoPagamentoJudiciais.Services.Core
{
    public static class ActionsPage
    {
        public static void InitializeDriver()
        {
            if (Driver.driver == null)
            {
                Driver.driver = new FirefoxDriver();
                Driver.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40);
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
            if (objAdquiraFaturaInternaPage.fieldTomador.Displayed)
                objAdquiraFaturaInternaPage.fieldTomador.SendKeys(Config.AdquiraPage.FaturaInternaForm.Tomador);

            objAdquiraFaturaInternaPage.fieldTomador.SendKeys(Keys.Enter);
            objAdquiraFaturaInternaPage.fieldTomador.SendKeys(Keys.Return);

            Thread.Sleep(2000);
            objAdquiraFaturaInternaPage.fieldSiglaAreaGestora.SendKeys(Config.AdquiraPage.FaturaInternaForm.SiglaAreaGestora);
            Thread.Sleep(2000);
            objAdquiraFaturaInternaPage.fieldNumeroFaturaInterna.SendKeys(Config.AdquiraPage.FaturaInternaForm.NumeroFaturaInterna);
            Thread.Sleep(2000);
            objAdquiraFaturaInternaPage.fieldDataVencimento.SendKeys(Config.AdquiraPage.FaturaInternaForm.DataVencimento);
            Thread.Sleep(2000);
            objAdquiraFaturaInternaPage.fieldNumeroDocumentoSuporte.SendKeys(Config.AdquiraPage.FaturaInternaForm.NumeroDocumentoSuporte);
            Thread.Sleep(2000);
            objAdquiraFaturaInternaPage.fieldSequencial.SendKeys(Config.AdquiraPage.FaturaInternaForm.NumeroSequencial);
            Thread.Sleep(2000);
            if (objAdquiraFaturaInternaPage.fieldTipoDocumentoSAP.Displayed)
                objAdquiraFaturaInternaPage.fieldTipoDocumentoSAP.SendKeys(Config.AdquiraPage.FaturaInternaForm.TipoDocumentoSAP);


            objAdquiraFaturaInternaPage.fieldTipoDocumentoSAP.SendKeys(Keys.Enter);
            objAdquiraFaturaInternaPage.fieldTipoDocumentoSAP.SendKeys(Keys.Return);

            Thread.Sleep(2000);
            objAdquiraFaturaInternaPage.fieldAreaReclamacao.SendKeys(Config.AdquiraPage.FaturaInternaForm.AreaReclamacao);
            objAdquiraFaturaInternaPage.fieldAreaReclamacao.SendKeys(Keys.Enter);
            objAdquiraFaturaInternaPage.fieldAreaReclamacao.SendKeys(Keys.Return);

            Thread.Sleep(2000);
            objAdquiraFaturaInternaPage.fieldTipoNotaFiscalAnaliseTributaria.SendKeys(Config.AdquiraPage.FaturaInternaForm.TipoNotaFiscalAnaliseTributaria);
            objAdquiraFaturaInternaPage.fieldTipoNotaFiscalAnaliseTributaria.SendKeys(Keys.Enter);
            objAdquiraFaturaInternaPage.fieldTipoNotaFiscalAnaliseTributaria.SendKeys(Keys.Return);

            Thread.Sleep(2000);
            objAdquiraFaturaInternaPage.fieldNomeFavorecido.SendKeys(Config.AdquiraPage.FaturaInternaForm.NomeFavorecido);

            Thread.Sleep(2000);
            objAdquiraFaturaInternaPage.fieldCNPJCPFFavorecido.SendKeys(Config.AdquiraPage.FaturaInternaForm.CPFCNPJFavorecido);
            Thread.Sleep(2000);
            objAdquiraFaturaInternaPage.fieldCodigoSAPFornecedor.SendKeys(Config.AdquiraPage.FaturaInternaForm.CodigoSAPFornecedor);
            Thread.Sleep(2000);
            objAdquiraFaturaInternaPage.fieldEmpresa.SendKeys(Config.AdquiraPage.FaturaInternaForm.Empresa);

            Thread.Sleep(2000);
            objAdquiraFaturaInternaPage.fieldComarca.SendKeys(Config.AdquiraPage.FaturaInternaForm.Comarca);
            Thread.Sleep(2000);
            objAdquiraFaturaInternaPage.fieldCodigoOcorrencia.SendKeys(Config.AdquiraPage.FaturaInternaForm.CodigoOcorrencia);


            Thread.Sleep(2000);
            if (objAdquiraFaturaInternaPage.fieldFormaPagamento.Displayed)
                objAdquiraFaturaInternaPage.fieldFormaPagamento.SendKeys(Config.AdquiraPage.FaturaInternaForm.FormaPagamento);
            objAdquiraFaturaInternaPage.fieldFormaPagamento.SendKeys(Keys.Enter);
            objAdquiraFaturaInternaPage.fieldFormaPagamento.SendKeys(Keys.Return);


            Thread.Sleep(2000);
            objAdquiraFaturaInternaPage.fieldTipoPagamento.SendKeys(Config.AdquiraPage.FaturaInternaForm.TipoPagamento);
            objAdquiraFaturaInternaPage.fieldTipoPagamento.SendKeys(Keys.Enter);
            objAdquiraFaturaInternaPage.fieldTipoPagamento.SendKeys(Keys.Return);

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
            WebDriverWait wait = new WebDriverWait(Driver.driver, new TimeSpan(0, 0, 20));

            if (objVivoPagto.fieldNomeModelo.Displayed)
                objVivoPagto.fieldNomeModelo.Click();

            Thread.Sleep(2000);
            var lstNomeModelo = objVivoPagto.fieldNomeModelo.FindElements(By.TagName("li"));
            foreach (IWebElement element in lstNomeModelo)
            {
                string elementText = element.Text.Trim();
                if (elementText == Config.VivoPagtoPage.FaturaInternaForm.NomeModelo.Trim())
                {
                    element.Click();
                    break;
                }
            }
            Thread.Sleep(4000);
            wait.Until(SeleniumHelper.ElementIsVisible(objVivoPagto.fieldAreaReclamacao));
            objVivoPagto.fieldAreaReclamacao.Click();
            objVivoPagto.fieldAreaReclamacao.SendKeys("XXXXXXXXXX");

            Thread.Sleep(4000);
            wait.Until(SeleniumHelper.ElementIsVisible(objVivoPagto.fieldTipoPagamento));
            objVivoPagto.fieldTipoPagamento.Click();
            objVivoPagto.fieldTipoPagamento.SendKeys("XXXXXXXXXX");
            Thread.Sleep(2000);

            objVivoPagto.fieldFornecedor.Click();
            objVivoPagto.fieldFornecedor.SendKeys("XXXXXXXXXX");
            Thread.Sleep(4000);

            objVivoPagto.fieldEmpresa.Click();
            objVivoPagto.fieldEmpresa.SendKeys("XXXXXXXXXX");
            Thread.Sleep(3000);

            objVivoPagto.fieldAreaGestora.Click();
            objVivoPagto.fieldAreaGestora.SendKeys("XXXXXXXXXX");
            Thread.Sleep(3000);

            objVivoPagto.fieldSequencial.Click();
            objVivoPagto.fieldSequencial.SendKeys("XXXXXXXXXX");
            Thread.Sleep(3000);

            SelectElement selectTipoDocumento = new SelectElement(objVivoPagto.fieldTipoDocumento);
            selectTipoDocumento.SelectByIndex(1);
            Thread.Sleep(3000);

            SelectElement selectAreaReclamacao = new SelectElement(objVivoPagto.fieldAreaReclamacao1);
            selectAreaReclamacao.SelectByIndex(1);
            Thread.Sleep(3000);

            objVivoPagto.fieldNomeEmissor.Click();
            objVivoPagto.fieldNomeEmissor.SendKeys("XXXXXXXXXX");
            Thread.Sleep(3000);

            objVivoPagto.fieldTipoDocumentoFavorecido.Click();
            objVivoPagto.fieldTipoDocumentoFavorecido.SendKeys("");
            Thread.Sleep(3000);

            objVivoPagto.fieldCpfCnpjFavorecido.Click();
            IJavaScriptExecutor jse = (IJavaScriptExecutor)Driver.driver;
            jse.ExecuteScript("arguments[0].value='111.111.111-11';", objVivoPagto.fieldCpfCnpjFavorecido);
            Thread.Sleep(3000);

            //objVivoPagto.fieldNomeFavorecido.Click();
            //objVivoPagto.fieldNomeFavorecido.SendKeys("XXXXXXXXXX");
            //Thread.Sleep(3000);

            //objVivoPagto.fieldCodigoSapFornecedor.Click();
            //objVivoPagto.fieldCodigoSapFornecedor.SendKeys("XXXXXXXXX");
            //Thread.Sleep(3000);

            SelectElement selectMeioPagamento = new SelectElement(objVivoPagto.fieldIdMeioPagamento);
            selectMeioPagamento.SelectByIndex(2);
            Thread.Sleep(4000);


            objVivoPagto.fieldMesReferencia.Click();
            IJavaScriptExecutor jsefieldMesReferencia = (IJavaScriptExecutor)Driver.driver;
            jsefieldMesReferencia.ExecuteScript("arguments[0].value='08/2021';", objVivoPagto.fieldMesReferencia);
            Thread.Sleep(3000);

            objVivoPagto.fieldDataVencimento.Click();
            IJavaScriptExecutor jsefieldDataVencimento = (IJavaScriptExecutor)Driver.driver;
            jsefieldDataVencimento.ExecuteScript("arguments[0].value='30/03/2021';", objVivoPagto.fieldDataVencimento);
            objVivoPagto.fieldDataVencimento.SendKeys(Keys.Enter);
            objVivoPagto.fieldDataVencimento.SendKeys(Keys.Return);

            Thread.Sleep(3000);

            objVivoPagto.fieldValor.Click();
            objVivoPagto.fieldValor.SendKeys("1.340,00");
            Thread.Sleep(3000);


            Thread.Sleep(2000);
            foreach (string doc in Config.VivoPagtoPage.FaturaInternaForm.lstFileUpload)
            {
                objVivoPagto.formUploadFile.Click();

                AutoItX3 autoIdObj = new AutoItX3();
                autoIdObj.WinActivate("Enviar arquivo(s)");
                autoIdObj.ControlFocus("Enviar arquivo(s)", "", "Edit1");
                autoIdObj.ControlSetText("Enviar arquivo(s)", "", "Edit1", Config.VivoPagtoPage.FaturaInternaForm.lstFileUpload[0]);

                Thread.Sleep(2000);

                autoIdObj.Send("{ENTER}");


            }
            Thread.Sleep(5000);
            objVivoPagto.fieldAreaReclamacao.SendKeys(Config.VivoPagtoPage.FaturaInternaForm.AreaReclamacao);
        }
        public static void FillAdquiraForm()
        {

        }


    }
}
