using Empresa.Churras.Domain.Model.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Empresa.Churras.Tests.ValueObjects
{
    [TestClass]
    public class PeriodoTest
    {
        [TestMethod]
        public void QuantoDuraEmHoras_Test()
        {
            var periodo = new Periodo
            {
                Inicio = new DateTime(2021, 08, 21, 12, 0, 0),
                Fim = new DateTime(2021, 08, 21, 18, 0, 0)
            };

            var horas = periodo.QuantoDuraEmHoras();

            Assert.AreEqual(6, horas);
        }

        [TestMethod]
        public void FaltaQuantoTempoParaComecar_Dias_Test()
        {
            var dtinicio = DateTime.Now.AddDays(3).AddHours(5);
            var dtFim = dtinicio.AddHours(6);

            var periodo = new Periodo
            {
                Inicio = dtinicio,
                Fim = dtFim
            };

            string quantoFalta = periodo.QuantoFaltaParaComecar();

            Assert.AreEqual("Começa em 3 dias e 4 horas", quantoFalta);
        }

        [TestMethod]
        public void FaltaQuantoTempoParaComecar_Horas_Test()
        {
            var dtinicio = DateTime.Now.AddHours(5);
            var dtFim = dtinicio.AddHours(6);

            var periodo = new Periodo
            {
                Inicio = dtinicio,
                Fim = dtFim
            };

            string quantoFalta = periodo.QuantoFaltaParaComecar();

            Assert.AreEqual("Começa em 4 horas", quantoFalta);
        }

        [TestMethod]
        public void FaltaQuantoTempoParaComecar_JaComecou_Test()
        {
            var dtinicio = DateTime.Now.AddHours(-1);
            var dtFim = dtinicio.AddHours(6);

            var periodo = new Periodo
            {
                Inicio = dtinicio,
                Fim = dtFim
            };

            string quantoFalta = periodo.QuantoFaltaParaComecar();

            Assert.AreEqual("Já começou!", quantoFalta);
        }
    }
}
